Imports RS_EquipSimulation.Common.Equipment

Public Class EquipmentCreateModel
    Private ReadOnly _inventory As EquipInventoryManager

    Public Sub New(inventory As EquipInventoryManager)
        _inventory = inventory
    End Sub

    Public Function AddInventoryItem(
        name As String,
        baseItemName As String,
        prefixOptions As List(Of OpValue),
        nxOptions As List(Of OpValue),
        smeltOptions As List(Of OpValue)) As Boolean

        Try
            ' 装備生成に関する追加のバリデーションやビジネスルールがあれば、ここに実装

            _inventory.AddInventoryItem(name, baseItemName, prefixOptions, nxOptions, smeltOptions)
            Return True
        Catch ex As Exception
            ' エラーログや詳細なエラーハンドリングを追加可能
            Return False
        End Try
    End Function
End Class