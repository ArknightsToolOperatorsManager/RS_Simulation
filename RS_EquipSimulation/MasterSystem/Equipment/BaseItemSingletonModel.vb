Imports System.IO
Imports RS_EquipSimulation.Common.Singleton
Imports RS_EquipSimulation.MasterSystem.Common
Imports RS_EquipSimulation.MasterSystem.Equipment
Imports RS_EquipSimulation.MasterSystem.Update

Public Class BaseItemSingletonModel
    Inherits SingletonJsonBase(Of BaseItemSingletonModel)

    Private _baseItemDataList As List(Of BaseItemModel)
    Private Shared _updating As Boolean = False

    Protected Overrides ReadOnly Property FilePath As String
        Get
            ' 現在使用中のファイルパスを取得
            Return GetCurrentFilePath()
        End Get
    End Property

    ' 現在使用中のファイルパスを取得
    Private Function GetCurrentFilePath() As String
        Dim directory = Path.GetDirectoryName(InputFileConfig.BaseItemFilePath)
        Dim fileName = Path.GetFileNameWithoutExtension(InputFileConfig.BaseItemFilePath)
        Dim currentPath = Path.Combine(directory, $"{fileName}_current.json")

        ' current.jsonが存在しない場合は元のパスを返す
        If Not File.Exists(currentPath) Then
            Return InputFileConfig.BaseItemFilePath
        End If

        Return currentPath
    End Function

    Protected Overrides Function GetDataType() As Type
        Return GetType(List(Of BaseItemModel))
    End Function

    Protected Overrides Sub SetLoadedData(data As Object)
        _baseItemDataList = DirectCast(data, List(Of BaseItemModel))

        ' データ再読み込みイベントを発行
        RaiseEvent DataReloaded(Me, EventArgs.Empty)
    End Sub

    ' 初期化処理のオーバーライド
    Protected Overrides Sub SafeInitialize()
        MyBase.SafeInitialize()

        ' マスターデータ更新マネージャーのイベント登録
        AddHandler MasterDataUpdateManager.Instance.ItemsFileUpdated, AddressOf OnItemsFileUpdated
    End Sub

    ' items.jsonファイル更新イベントハンドラ
    Private Sub OnItemsFileUpdated(sender As Object, e As EventArgs)
        ' データを再読み込み
        ReloadData()
    End Sub

    ' データ再読み込みメソッド
    Public Function ReloadData() As Boolean
        If _updating Then Return False

        Try
            _updating = True
            LoadFromFile()
            Return True
        Catch ex As Exception
            Debug.WriteLine($"ベースアイテムデータの再読み込みに失敗: {ex.Message}")
            Return False
        Finally
            _updating = False
        End Try
    End Function

    ' データ再読み込みイベント
    Public Shared Event DataReloaded As EventHandler

    ' 基本的な検索メソッド
    Public Function GetItemByName(name As String) As BaseItemModel
        Return _baseItemDataList.FirstOrDefault(Function(item) item.Name = name)
    End Function

    Public Function GetItemsByPart(partName As String) As IEnumerable(Of BaseItemModel)
        Return _baseItemDataList.Where(Function(item) item.PartName.Contains(partName))
    End Function

    Public Function GetItemNamesByPart(partName As String) As IEnumerable(Of String)
        Return GetItemsByPart(partName).Select(Function(item) item.Name)
    End Function

    Public Function FilterItemsByJob(partName As String, jobName As String) As List(Of String)
        ' 職業名に基づいてアイテムをフィルタリング
        ' 例: 武器の場合は職業名に基づいて絞り込み
        If partName = "" Then
            Return Nothing
        ElseIf partName = "武器" Then
            Return GetItemsByPart(jobName).Select(Function(item) item.Name).ToList()
        Else
            Return GetItemNamesByPart(partName).ToList()
        End If
    End Function

    ' フィルタリングメソッド
    Public Function FindItemsByOption(optionName As String, Optional partName As String = "") As IEnumerable(Of BaseItemModel)
        Dim query = _baseItemDataList.AsEnumerable()

        If Not String.IsNullOrEmpty(partName) Then
            query = query.Where(Function(item) item.PartName = partName)
        End If

        Return query.Where(Function(item) HasOption(item, optionName))
    End Function

    ' すべての部位名を取得するメソッド
    Public Function GetAllPartNames() As IEnumerable(Of String)
        Return _baseItemDataList.Select(Function(item) item.PartName).Distinct()
    End Function

    Private Function HasOption(item As BaseItemModel, optionName As String) As Boolean
        ' 通常オプションをチェック
        If item.Options.Any(Function(opt) opt.OptionName = optionName) Then
            Return True
        End If

        ' NXオプションをチェック
        Return item.NxOptions.Any(Function(nxOpt) nxOpt.Value.Any(Function(opt) opt.OptionName = optionName))
    End Function

    ' オプション詳細を取得
    Public Function GetOptionDetails(item As BaseItemModel, optionName As String) As List(Of String)
        Dim details As New List(Of String)

        ' 通常オプションの確認
        Dim normalOptions = item.Options.Where(Function(opt) opt.OptionName = optionName)
        For Each opt In normalOptions
            details.Add($"通常オプション: {opt.OptionValue}")
        Next

        ' NXオプションの確認
        For Each nxPair In item.NxOptions
            Dim nxOptions = nxPair.Value.Where(Function(opt) opt.OptionName = optionName)
            For Each opt In nxOptions
                details.Add($"NXレベル{nxPair.Key}: {opt.OptionValue}")
            Next
        Next

        Return details
    End Function
End Class