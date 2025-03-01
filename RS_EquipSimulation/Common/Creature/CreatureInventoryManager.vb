Imports Newtonsoft.Json

Namespace Common.Creature

    ''' <summary>
    ''' クリーチャーインベントリの管理クラス
    ''' </summary>
    Public Class CreatureInventoryManager
        Implements ICreatureInventoryReader
        Implements ICreatureInventoryWriter
        Implements ICreatureInventoryInitializer

        ' インベントリデータ
        <JsonProperty("Inventory")>
        Private _inventory As New List(Of CreatureInventoryItem)

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        Public Sub New()
        End Sub

#Region "ICreatureInventoryReader の実装"
        ''' <summary>
        ''' インベントリデータの取得
        ''' </summary>
        Public Function GetInventoryData() As IReadOnlyList(Of CreatureInventoryItem) Implements ICreatureInventoryReader.GetInventoryData
            Return _inventory.AsReadOnly()
        End Function

        ''' <summary>
        ''' 指定IDのクリーチャーアイテム取得
        ''' </summary>
        Public Function GetInventoryItemForId(itemId As Guid) As CreatureInventoryItem Implements ICreatureInventoryReader.GetInventoryItemForId
            Return _inventory.FirstOrDefault(Function(item) item.ItemId = itemId)
        End Function
#End Region

#Region "ICreatureInventoryWriter の実装"
        ''' <summary>
        ''' インベントリへのアイテム追加
        ''' </summary>
        Friend Function AddInventoryItem(name As String,
                                       baseRarity As String,
                                       enhancedRarity As String,
                                       mainPassive As PassiveSkill,
                                       sub1Passive As PassiveSkill,
                                       sub2Passive As PassiveSkill,
                                       sub3Passive As PassiveSkill) As CreatureInventoryItem Implements ICreatureInventoryWriter.AddInventoryItem
            Dim item = New CreatureInventoryItem(
                name,
                Guid.NewGuid(),
                baseRarity,
                enhancedRarity,
                mainPassive,
                sub1Passive,
                sub2Passive,
                sub3Passive)

            _inventory.Add(item)
            Return item
        End Function

        ''' <summary>
        ''' インベントリからのアイテム削除
        ''' </summary>
        Friend Function RemoveInventoryItem(itemId As Guid) As Boolean Implements ICreatureInventoryWriter.RemoveInventoryItem
            Dim index = _inventory.FindIndex(Function(i) i.ItemId = itemId)
            If index >= 0 Then
                _inventory.RemoveAt(index)
                Return True
            End If
            Return False
        End Function
#End Region

#Region "ICreatureInventoryInitializer の実装"
        ''' <summary>
        ''' インベントリデータの一括設定
        ''' </summary>
        Friend Sub SetInventoryData(inventory As List(Of CreatureInventoryItem)) Implements ICreatureInventoryInitializer.SetInventoryData
            Me._inventory = inventory
        End Sub
#End Region
    End Class

End Namespace