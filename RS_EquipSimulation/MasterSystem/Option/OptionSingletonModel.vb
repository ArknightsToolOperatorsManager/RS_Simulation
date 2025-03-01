Imports RS_EquipSimulation.Common.Singleton
Imports RS_EquipSimulation.MasterSystem.Option
Imports RS_EquipSimulation.MasterSystem.Common

Public Class OptionSingletonModel
    Inherits SingletonCSVBase(Of OptionSingletonModel)

    Private _optionList As List(Of OptionModel)

    Protected Overrides ReadOnly Property FilePath As String
        Get
            Return InputFileConfig.OptionListFilePath
        End Get
    End Property

    Protected Overrides Sub SafeInitialize()
        _optionList = New List(Of OptionModel)
    End Sub

    Protected Overrides Sub ProcessLine(parts() As String)
        ' 通常オプションの処理
        Dim normalOp = parts(0)
        Dim petOp = parts(1)
        Dim opType = parts(2)

        ' 空でない通常オプションを追加
        If Not String.IsNullOrWhiteSpace(normalOp) Then
            _optionList.Add(New OptionModel(normalOp, opType))
        End If

        ' 空でないペットオプションを追加
        If Not String.IsNullOrWhiteSpace(petOp) Then
            _optionList.Add(New OptionModel(petOp, opType))
        End If
    End Sub

    ' カテゴリ一覧を取得
    Public Function GetCategories() As List(Of String)
        Return _optionList.
            Select(Function(o) o.OptionType).
            Distinct().
            OrderBy(Function(c) c).
            ToList()
    End Function

    Public Function FilterOptionsByTypes(selectedTypes As List(Of String)) As IEnumerable(Of String)
        Return GetOptionsByTypes(selectedTypes)
    End Function

    ' 特定のカテゴリに属するオプション一覧を取得
    Public Function GetOptionsByType(opType As String) As List(Of OptionModel)
        Return _optionList.
            Where(Function(o) o.OptionType = opType).
            OrderBy(Function(o) o.OptionName).
            ToList()
    End Function

    ' すべてのオプション種類を取得
    Public Function GetAllOptionTypes() As IEnumerable(Of String)
        Return _optionList.Select(Function(o) o.OptionType).OrderBy(Function(t) t).Distinct()
    End Function

    ' すべてのオプション名を取得
    Public Function GetAllOptions() As IEnumerable(Of String)
        Return _optionList.Select(Function(o) o.OptionName).OrderBy(Function(n) n).Distinct()
    End Function


    ' オプション名で検索
    Public Function GetOptionByName(opName As String) As OptionModel
        Return _optionList.FirstOrDefault(Function(o) o.OptionName = opName)
    End Function

    ' オプション名の一覧を取得
    Public Function GetOptionNames() As List(Of String)
        Return _optionList.Select(Function(o) o.OptionName).Distinct().ToList()
    End Function

    ' オプション種類を取得
    Public Function GetOptionType(optionName As String) As String
        Dim opt = _optionList.FirstOrDefault(Function(o) o.OptionName = optionName)
        Return If(opt IsNot Nothing, opt.OptionType, String.Empty)
    End Function


    ' 指定された種類に属するオプションを取得
    Public Function GetOptionsByTypes(types As IEnumerable(Of String)) As IEnumerable(Of String)
        Return _optionList.
                Where(Function(o) types.Contains(o.OptionType)).
                Select(Function(o) o.OptionName).
                OrderBy(Function(n) n).
                Distinct()
    End Function

    ' オプションの基本的な値チェック
    Public Function IsValidOption(opName As String) As Boolean
        Return _optionList.Any(Function(o) o.OptionName = opName)
    End Function
End Class