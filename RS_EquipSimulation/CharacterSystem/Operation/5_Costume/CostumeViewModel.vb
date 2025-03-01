' CostumeViewModel.vb
Imports System.ComponentModel
Imports RS_EquipSimulation.MasterSystem.Crest

Namespace CharacterSystem.Operation.Costume

    ''' <summary>
    ''' コスチュームビューモデル
    ''' </summary>
    Public Class CostumeViewModel
        Implements INotifyPropertyChanged

#Region "イベント"
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub NotifyPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
#End Region

#Region "プロパティ"
        ' マネージャーモデル
        Private ReadOnly _managerModel As CostumeManagerModel

        ' クレストシングルトン
        Private ReadOnly _crestSingleton As CrestSingletonModel

        ' コスチュームステータス
        Public Property CostumeStatus As Integer
            Get
                Return _managerModel.CostumeStatus
            End Get
            Set(value As Integer)
                If _managerModel.CostumeStatus <> value Then
                    _managerModel.CostumeStatus = value
                    NotifyPropertyChanged(NameOf(CostumeStatus))
                End If
            End Set
        End Property

        ' コスチューム経験値
        Public Property CostumeExp As Integer
            Get
                Return _managerModel.CostumeExp
            End Get
            Set(value As Integer)
                If _managerModel.CostumeExp <> value Then
                    _managerModel.CostumeExp = value
                    NotifyPropertyChanged(NameOf(CostumeExp))
                End If
            End Set
        End Property

        ' 色の一覧
        Public ReadOnly Property ColorList As BindingList(Of String)
            Get
                Return New BindingList(Of String)(_crestSingleton.GetColorList())
            End Get
        End Property
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="managerModel">マネージャーモデル</param>
        Public Sub New(managerModel As CostumeManagerModel)
            _managerModel = managerModel
            _crestSingleton = CrestSingletonModel.Instance
        End Sub
#End Region

#Region "クレスト操作"
        ''' <summary>
        ''' 指定された色のクレスト名一覧を取得
        ''' </summary>
        ''' <param name="color">クレストの色</param>
        Public Function GetCrestNamesByColor(color As String) As List(Of String)
            Return _crestSingleton.GetCrestNamesByColor(color)
        End Function

        ''' <summary>
        ''' 指定された色・クレスト名に対して利用可能なランク一覧を取得
        ''' </summary>
        ''' <param name="color">クレストの色</param>
        ''' <param name="crestName">クレスト名</param>
        Public Function GetAvailableRanks(color As String, crestName As String) As List(Of String)
            Return _crestSingleton.GetAvailableRanks(color, crestName)
        End Function

        ''' <summary>
        ''' クレストを設定
        ''' </summary>
        ''' <param name="slotIndex">スロットインデックス (0-5)</param>
        ''' <param name="color">クレストの色</param>
        ''' <param name="name">クレスト名</param>
        ''' <param name="rank">クレストのランク</param>
        Public Sub SetCrest(slotIndex As Integer, color As String, name As String, rank As String)
            _managerModel.SetCrest(slotIndex, color, name, rank)
            NotifyPropertyChanged($"Crest{slotIndex + 1}")
        End Sub

        ''' <summary>
        ''' クレストを解除
        ''' </summary>
        ''' <param name="slotIndex">スロットインデックス (0-5)</param>
        Public Sub ClearCrest(slotIndex As Integer)
            _managerModel.ClearCrest(slotIndex)
            NotifyPropertyChanged($"Crest{slotIndex + 1}")
        End Sub

        ''' <summary>
        ''' クレストを取得
        ''' </summary>
        ''' <param name="slotIndex">スロットインデックス (0-5)</param>
        Public Function GetCrest(slotIndex As Integer) As CrestData
            Return _managerModel.GetCrest(slotIndex)
        End Function
#End Region
    End Class

End Namespace