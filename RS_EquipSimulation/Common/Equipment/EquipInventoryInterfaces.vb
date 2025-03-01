Namespace Common.Equipment

    ''' <summary>
    ''' 装備インベントリの読み取り専用インターフェース
    ''' </summary>
    Public Interface IEquipInventoryReader
        ''' <summary>
        ''' インベントリデータの取得
        ''' </summary>
        Function GetInventoryData() As IReadOnlyList(Of EquipInventoryItem)

        ''' <summary>
        ''' 指定IDの装備アイテム取得
        ''' </summary>
        Function GetInventoryItemForId(itemId As Guid) As EquipInventoryItem

        ''' <summary>
        ''' ベースアイテム名から装備アイテムを検索
        ''' </summary>
        Function FindItemsByBaseItemName(baseItemName As String) As IEnumerable(Of EquipInventoryItem)
    End Interface

    ''' <summary>
    ''' 装備インベントリの書き込み用インターフェース (CreatorSystem.Equipment用)
    ''' </summary>
    Friend Interface IEquipInventoryWriter
        ''' <summary>
        ''' インベントリへのアイテム追加
        ''' </summary>
        Function AddInventoryItem(name As String,
                                  baseItemName As String,
                                  prefixOptions As List(Of OpValue),
                                  nxOptions As List(Of OpValue),
                                  smeltOptions As List(Of OpValue)) As EquipInventoryItem

        ''' <summary>
        ''' インベントリからのアイテム削除
        ''' </summary>
        Function RemoveInventoryItem(itemId As Guid) As Boolean
    End Interface

    ''' <summary>
    ''' 装備インベントリの初期化用インターフェース (CharacterData用)
    ''' </summary>
    Friend Interface IEquipInventoryInitializer
        ''' <summary>
        ''' インベントリデータの一括設定
        ''' </summary>
        Sub SetInventoryData(inventory As List(Of EquipInventoryItem))
    End Interface

End Namespace