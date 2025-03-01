Imports RS_EquipSimulation.CharacterSystem.Common
Imports RS_EquipSimulation.MasterSystem.Common

Namespace CharacterSystem.Operation.Basic

    Public Class BasicManagerModel
        Inherits OperationManagerBase

#Region "プロパティ"
        Private _basicData As BasicData

        Public Property CharacterName As String
            Get
                Return _basicData.CharacterName
            End Get
            Set(value As String)
                If _basicData.CharacterName <> value Then
                    _basicData.CharacterName = value
                    NotifyChanged()
                End If
            End Set
        End Property

        Public Property JobName As String
            Get
                Return _basicData.JobName
            End Get
            Set(value As String)
                If _basicData.JobName <> value Then
                    _basicData.JobName = value
                    NotifyChanged()
                End If
            End Set
        End Property

        Public Property Level As Integer
            Get
                Return _basicData.Level
            End Get
            Set(value As Integer)
                If _basicData.Level <> value Then
                    _basicData.Level = value
                    NotifyChanged()
                End If
            End Set
        End Property

        ' 職業リストはJobNameSingletonModelから取得
        Public Shared ReadOnly Property JobList As IEnumerable(Of String)
            Get
                Return JobNameManager.GetAllJobNames
            End Get
        End Property
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="optManager">オプション管理クラス</param>
        Public Sub New(optManager As OptionManagerModel)
            MyBase.New(optManager)
            _basicData = New BasicData With {
                    .CharacterName = "",
                    .JobName = "剣士",
                    .Level = 2000
                }
        End Sub

        ''' <summary>
        ''' デシリアライズ用コンストラクタ
        ''' </summary>
        Public Sub New()
            MyBase.New()
            _basicData = New BasicData With {
                    .CharacterName = "",
                    .JobName = "剣士",
                    .Level = 2000
                }
        End Sub
#End Region

#Region "シリアライズ対応"
        ''' <summary>
        ''' デシリアライズ後の初期化
        ''' </summary>
        Protected Overrides Sub OnInitializeAfterDeserialize()
            MyBase.OnInitializeAfterDeserialize()
            If _basicData Is Nothing Then
                _basicData = New BasicData()
            End If
            ' オプション更新通知
            NotifyChanged()
        End Sub
#End Region
    End Class

    ''' <summary>
    ''' 基本情報を保持するデータクラス
    ''' </summary>
    Public Class BasicData
        Public Property CharacterName As String
        Public Property JobName As String
        Public Property Level As Integer
    End Class

End Namespace