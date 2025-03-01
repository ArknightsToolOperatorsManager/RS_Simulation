Namespace Common.Creature

    ''' <summary>
    ''' クリーチャーインベントリの読み取り専用インターフェース
    ''' </summary>
    Public Interface ICreatureInventoryReader
        ''' <summary>
        ''' インベントリデータの取得
        ''' </summary>
        Function GetInventoryData() As IReadOnlyList(Of CreatureInventoryItem)

        ''' <summary>
        ''' 指定IDのクリーチャーアイテム取得
        ''' </summary>
        Function GetInventoryItemForId(itemId As Guid) As CreatureInventoryItem
    End Interface

    ''' <summary>
    ''' クリーチャーインベントリの書き込み用インターフェース (CreatorSystem.Creature用)
    ''' </summary>
    Friend Interface ICreatureInventoryWriter
        ''' <summary>
        ''' インベントリへのアイテム追加
        ''' </summary>
        Function AddInventoryItem(name As String,
                                 baseRarity As String,
                                 enhancedRarity As String,
                                 mainPassive As PassiveSkill,
                                 sub1Passive As PassiveSkill,
                                 sub2Passive As PassiveSkill,
                                 sub3Passive As PassiveSkill) As CreatureInventoryItem

        ''' <summary>
        ''' インベントリからのアイテム削除
        ''' </summary>
        Function RemoveInventoryItem(itemId As Guid) As Boolean
    End Interface

    ''' <summary>
    ''' クリーチャーインベントリの初期化用インターフェース (CharacterData用)
    ''' </summary>
    Friend Interface ICreatureInventoryInitializer
        ''' <summary>
        ''' インベントリデータの一括設定
        ''' </summary>
        Sub SetInventoryData(inventory As List(Of CreatureInventoryItem))
    End Interface

End Namespace