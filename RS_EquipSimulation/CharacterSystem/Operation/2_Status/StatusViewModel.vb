Imports System.ComponentModel

Namespace CharacterSystem.Operation.Status

    Public Class StatusViewModel
        Implements INotifyPropertyChanged

        ' モデルの参照
        Private ReadOnly _model As StatusManagerModel

        ' DataGridView用のデータソース
        Private ReadOnly _gridData As New BindingList(Of StatusGridRowViewModel)
        Public ReadOnly Property GridData As BindingList(Of StatusGridRowViewModel)
            Get
                Return _gridData
            End Get
        End Property

        ' コンストラクタ
        Public Sub New(model As StatusManagerModel)
            _model = model
            InitializeGridData()
        End Sub

        ' グリッドデータの初期化
        Private Sub InitializeGridData()
            ' グリッドデータの設定
            _gridData.Clear()
            For Each stat In _model.GetAllStatus()
                ' 編集可能なStatusGridRowViewModelを作成
                Dim row = New StatusGridRowViewModel(stat.Key, True, stat.Value)
                _gridData.Add(row)
            Next
        End Sub

        ' 値の更新
        Public Function UpdateValue(name As String, propertyName As String, value As Integer) As Boolean
            ' モデルの検証を通す
            If Not _model.ValidateValue(name, propertyName, value) Then
                Return False
            End If

            ' 対応する行を見つける
            Dim row = _gridData.FirstOrDefault(Function(r) r.RowName = name)
            If row Is Nothing Then Return False

            ' 行を更新
            Dim ret = row.ApplyChanges(propertyName, value)
            _model.AfterChangedStatus()
            OnPropertyChanged("GridData")
            Return ret
        End Function

        Public Function GetTotalAllocation() As Integer
            Return _model.GetAllocationStatusTotal()
        End Function

        ' PropertyChanged実装
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class

End Namespace