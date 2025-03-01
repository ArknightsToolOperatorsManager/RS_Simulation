Imports RS_EquipSimulation.Common.Equipment

Namespace CreatorSystem.Equipment

    Public Class EquipmentCompareForm
        Private _compareManager As New EquipCompareManager()

        Public Sub DisplaySingleItem(item As EquipInventoryItem)
            _compareManager.DisplaySingleItem(item)
        End Sub

        Public Sub CompareItems(item1 As EquipInventoryItem, item2 As EquipInventoryItem)
            _compareManager.CompareItems(item1, item2)
        End Sub

        Public Sub ClearDisplay()
            _compareManager.ClearDisplay()
        End Sub
    End Class

End Namespace