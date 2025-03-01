Imports RS_EquipSimulation.CharacterSystem.Common
Imports RS_EquipSimulation.CharacterSystem.Core
Imports RS_EquipSimulation.MasterSystem.Crest

Namespace CharacterSystem.Operation.Costume

    ''' <summary>
    ''' コスチュームを管理するモデル
    ''' </summary>
    Public Class CostumeManagerModel
        Inherits OperationManagerBase

#Region "プロパティ"
        ' コスチュームデータ
        Private _costumeData As CostumeData

        ' コスチュームステータス
        Public Property CostumeStatus As Integer
            Get
                Return _costumeData.CostumeStatus
            End Get
            Set(value As Integer)
                If _costumeData.CostumeStatus <> value Then
                    _costumeData.CostumeStatus = value
                    NotifyChanged()
                End If
            End Set
        End Property

        ' コスチューム経験値
        Public Property CostumeExp As Integer
            Get
                Return _costumeData.CostumeExp
            End Get
            Set(value As Integer)
                If _costumeData.CostumeExp <> value Then
                    _costumeData.CostumeExp = value
                    NotifyChanged()
                End If
            End Set
        End Property

        ' クレストスロット数
        Const CrestSlotCount As Integer = 6
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="optManager">オプション管理クラス</param>
        Public Sub New(optManager As OptionManagerModel)
            MyBase.New(optManager)
            _costumeData = New CostumeData()
            InitializeCostume()
        End Sub

        ''' <summary>
        ''' デシリアライズ用コンストラクタ
        ''' </summary>
        Public Sub New()
            MyBase.New(Nothing)
            _costumeData = New CostumeData()
            InitializeCostume()
        End Sub

        ''' <summary>
        ''' コスチュームの初期化
        ''' </summary>
        Private Sub InitializeCostume()
            ' クレストスロットの初期化
            If _costumeData.Crests Is Nothing OrElse _costumeData.Crests.Count <> CrestSlotCount Then
                _costumeData.Crests = New List(Of CrestData)(CrestSlotCount - 1)
                For i As Integer = 0 To CrestSlotCount - 1
                    _costumeData.Crests(i) = New CrestData()
                Next
            End If
        End Sub
#End Region

#Region "シリアライズ関連"
        ''' <summary>
        ''' デシリアライズ後の初期化
        ''' </summary>
        Protected Overrides Sub OnInitializeAfterDeserialize()
            MyBase.OnInitializeAfterDeserialize()

            ' データの検証
            If _costumeData Is Nothing Then
                _costumeData = New CostumeData()
            End If

            ' クレストスロットの検証
            InitializeCostume()

            ' オプション更新
            UpdateOptions()
        End Sub
#End Region

#Region "クレスト操作"
        ''' <summary>
        ''' クレストの設定
        ''' </summary>
        ''' <param name="slotIndex">スロットインデックス (0-5)</param>
        ''' <param name="color">クレストの色</param>
        ''' <param name="name">クレスト名</param>
        ''' <param name="rank">クレストのランク</param>
        Public Function SetCrest(slotIndex As Integer, color As String, name As String, rank As String) As Boolean
            If slotIndex < 0 OrElse slotIndex >= CrestSlotCount Then Return False

            ' クレストのパラメータを設定
            _costumeData.Crests(slotIndex).Color = color
            _costumeData.Crests(slotIndex).Name = name
            _costumeData.Crests(slotIndex).Rank = rank

            NotifyChanged()
            Return True
        End Function

        ''' <summary>
        ''' クレストの解除
        ''' </summary>
        ''' <param name="slotIndex">スロットインデックス (0-5)</param>
        Public Function ClearCrest(slotIndex As Integer) As Boolean
            If slotIndex < 0 OrElse slotIndex >= CrestSlotCount Then Return False

            ' クレストの設定をクリア
            _costumeData.Crests(slotIndex) = New CrestData()

            NotifyChanged()
            Return True
        End Function

        ''' <summary>
        ''' クレストの取得
        ''' </summary>
        ''' <param name="slotIndex">スロットインデックス (0-5)</param>
        Public Function GetCrest(slotIndex As Integer) As CrestData
            If slotIndex < 0 OrElse slotIndex >= CrestSlotCount Then Return Nothing

            Return _costumeData.Crests(slotIndex)
        End Function
#End Region

#Region "オプション管理"
        ''' <summary>
        ''' オプションの更新
        ''' </summary>
        Private Sub UpdateOptions()
            ' コスチュームとクレストからのオプションを計算
            Dim options As New List(Of OpValue)

            ' コスチュームステータス値によるオプション
            If CostumeStatus > 0 Then
                options.Add(New OpValue("全ステータス上昇", CostumeStatus.ToString()))
            End If

            ' コスチューム経験値によるオプション（必要に応じて）
            If CostumeExp > 0 Then
                options.Add(New OpValue("経験値", CostumeExp.ToString()))
            End If

            ' クレストからのオプション
            Dim crestSingleton = CrestSingletonModel.Instance

            For Each crest In _costumeData.Crests
                If crest.IsSet() Then
                    ' CrestSingletonModelから値を取得
                    Dim value = crestSingleton.GetValue(crest.Color, crest.Name, crest.Rank)
                    If value > 0 Then
                        options.Add(New OpValue(crest.Name, value.ToString()))
                    End If
                End If
            Next

            ' オプションの登録
            RegisterMergedOptions("Costume", options)
        End Sub

        ''' <summary>
        ''' オプション値の更新
        ''' </summary>
        Protected Overrides Sub UpdateOptionValues()
            UpdateOptions()
        End Sub
#End Region
    End Class

End Namespace