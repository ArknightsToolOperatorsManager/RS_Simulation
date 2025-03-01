Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Namespace Common.Equipment

    Public Class EquipInventoryItem
        Public ReadOnly Property Name As String
        Public ReadOnly Property ItemId As Guid
        Public ReadOnly Property BaseItemName As String
        Public ReadOnly Property PrefixOptions As List(Of OpValue)
        Public ReadOnly Property NxOptions As List(Of OpValue)
        Public ReadOnly Property SmeltOptions As List(Of OpValue)

        Public Sub New(name As String,
                      ItemId As Guid,
                      BaseItemName As String,
                      PrefixOptions As List(Of OpValue),
                      nxOptions As List(Of OpValue),
                      smeltOptions As List(Of OpValue))
            Me.Name = name
            Me.ItemId = ItemId
            Me.BaseItemName = BaseItemName
            Me.PrefixOptions = PrefixOptions
            Me.NxOptions = nxOptions
            Me.SmeltOptions = smeltOptions
        End Sub
    End Class

End Namespace
