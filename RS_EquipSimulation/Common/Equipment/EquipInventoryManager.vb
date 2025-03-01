Imports Newtonsoft.Json

Namespace Common.Equipment

    ''' <summary>
    ''' 装備インベントリの管理クラス
    ''' </summary>
    Public Class EquipInventoryManager
        Implements IEquipInventoryReader
        Implements IEquipInventoryWriter
        Implements IEquipInventoryInitializer

        ' インベントリデータ
        <JsonProperty("Inventory")>
        Private _inventory As New List(Of EquipInventoryItem)

        ' 変更通知イベント
        Public Event InventoryChanged As EventHandler

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        Public Sub New()
        End Sub

#Region "IEquipInventoryReader の実装"
        ''' <summary>
        ''' インベントリデータの取得
        ''' </summary>
        Public Function GetInventoryData() As IReadOnlyList(Of EquipInventoryItem) Implements IEquipInventoryReader.GetInventoryData
            Return _inventory.AsReadOnly()
        End Function

        ''' <summary>
        ''' 指定IDの装備アイテム取得
        ''' </summary>
        Public Function GetInventoryItemForId(itemId As Guid) As EquipInventoryItem Implements IEquipInventoryReader.GetInventoryItemForId
            Return _inventory.FirstOrDefault(Function(item) item.ItemId = itemId)
        End Function

        ''' <summary>
        ''' ベースアイテム名から装備アイテムを検索
        ''' </summary>
        Public Function FindItemsByBaseItemName(baseItemName As String) As IEnumerable(Of EquipInventoryItem) Implements IEquipInventoryReader.FindItemsByBaseItemName
            Return _inventory.Where(Function(item) item.BaseItemName = baseItemName)
        End Function
#End Region

#Region "IEquipInventoryWriter の実装"
        ''' <summary>
        ''' インベントリへのアイテム追加
        ''' </summary>
        Friend Function AddInventoryItem(name As String,
                                         baseItemName As String,
                                         prefixOptions As List(Of OpValue),
                                         nxOptions As List(Of OpValue),
                                         smeltOptions As List(Of OpValue)) As EquipInventoryItem Implements IEquipInventoryWriter.AddInventoryItem
            Dim item = New EquipInventoryItem(name, Guid.NewGuid(), baseItemName, prefixOptions, nxOptions, smeltOptions)

            _inventory.Add(item)
            ' 変更通知を発行
            RaiseEvent InventoryChanged(Me, EventArgs.Empty)

            Return item
        End Function

        ''' <summary>
        ''' インベントリからのアイテム削除
        ''' </summary>
        Friend Function RemoveInventoryItem(itemId As Guid) As Boolean Implements IEquipInventoryWriter.RemoveInventoryItem
            Dim index = _inventory.FindIndex(Function(i) i.ItemId = itemId)
            If index >= 0 Then
                _inventory.RemoveAt(index)
                Return True
            End If
            Return False
        End Function
#End Region

#Region "IEquipInventoryInitializer の実装"
        ''' <summary>
        ''' インベントリデータの一括設定
        ''' </summary>
        Friend Sub SetInventoryData(inventory As List(Of EquipInventoryItem)) Implements IEquipInventoryInitializer.SetInventoryData
            Me._inventory = inventory
        End Sub
#End Region
    End Class

End Namespace