Imports RS_EquipSimulation.MasterSystem.Equipment

Public Class EquipmentCreateViewModel
    Private ReadOnly _model As EquipmentCreateModel

    ' UIの状態を管理するプロパティ
    Public Property ItemName As String
    Public Property SelectedBaseItem As BaseItemModel
    Public Property SelectedPart As String

    ' 選択されたオプションの状態
    Public Property SelectedPrefixOptions As List(Of OpValue)
    Public Property SelectedNxOptions As List(Of OpValue)
    Public Property SelectedSmeltOptions As List(Of OpValue)

    Public Sub New(model As EquipmentCreateModel)
        _model = model
        InitializeCollections()
    End Sub

    Private Sub InitializeCollections()
        SelectedPrefixOptions = New List(Of OpValue)
        SelectedNxOptions = New List(Of OpValue)
        SelectedSmeltOptions = New List(Of OpValue)
    End Sub

    Public Sub SetBaseItem(itemName As String)
        Dim item = BaseItemSingletonModel.Instance.GetItemByName(itemName)

        If item IsNot Nothing Then
            SelectedBaseItem = item
        End If
    End Sub

    ' UI状態のバリデーション
    Public Function ValidateCreation() As Boolean
        If SelectedBaseItem Is Nothing Then Return False

        ' 追加のバリデーションロジック
        ' 例: 称号、NX、精錬オプションの数や組み合わせのチェック

        Return True
    End Function

    ' 装備生成の実行
    Public Function GenerateEquipment(prefix, nx, smelt) As Boolean
        If Not ValidateCreation() Then Return False

        Return _model.AddInventoryItem(
            ItemName,
            SelectedBaseItem.Name,
            prefix,
            nx,
            smelt
        )
    End Function

    ' UI状態のリセット
    Public Sub ResetState()
        SelectedBaseItem = Nothing
        SelectedPart = Nothing
        InitializeCollections()
    End Sub
End Class