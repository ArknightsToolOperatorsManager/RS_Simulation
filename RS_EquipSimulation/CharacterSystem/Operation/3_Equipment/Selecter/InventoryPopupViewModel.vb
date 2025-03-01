Imports System.Windows.Forms
Imports RS_EquipSimulation.Common.Equipment
Imports RS_EquipSimulation.MasterSystem.Equipment

Namespace CharacterSystem.Operation.Equipment

    ''' <summary>
    ''' 装備選択ポップアップのViewModel
    ''' </summary>
    Public Class InventoryPopupViewModel
        Implements IDisposable

#Region "プロパティ"
        ' マネージャー参照
        Private ReadOnly _equipManager As EquipmentManagerModel
        Private ReadOnly _inventoryReader As IEquipInventoryReader
        Private ReadOnly _slot As EquipmentManagerModel.SlotType

        ' マスターデータ参照
        Private ReadOnly _baseItemMaster As BaseItemSingletonModel = BaseItemSingletonModel.Instance

        ' 選択状態
        Private _selectedItemId As Guid?
        Public ReadOnly Property SelectedItemId As Guid?
            Get
                Return _selectedItemId
            End Get
        End Property

        ' 現在の装備品
        Public ReadOnly Property CurrentEquippedItem As EquipInventoryItem
            Get
                Return _equipManager.GetEquippedItem(_slot)
            End Get
        End Property
#End Region

#Region "コンストラクタ/Dispose"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        Public Sub New(equipManager As EquipmentManagerModel,
                     inventoryReader As IEquipInventoryReader,
                     slot As EquipmentManagerModel.SlotType)
            _equipManager = equipManager
            _inventoryReader = inventoryReader
            _slot = slot
        End Sub

        ''' <summary>
        ''' リソース解放
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' 現状は特に解放するリソースなし
        End Sub
#End Region

#Region "TreeViewデータ作成"
        ''' <summary>
        ''' 装備可能アイテムのTreeNodeコレクションを作成
        ''' </summary>
        Public Function CreateInventoryNodes() As TreeNode()
            Dim nodeList As New List(Of TreeNode)

            ' インベントリからスロットに合致するアイテムを抽出
            For Each item In GetEquippableItems()
                Dim node = CreateItemNode(item)
                If node IsNot Nothing Then
                    nodeList.Add(node)
                End If
            Next

            Return nodeList.ToArray()
        End Function

        ''' <summary>
        ''' アイテムのTreeNodeを作成
        ''' </summary>
        Private Function CreateItemNode(item As EquipInventoryItem) As TreeNode
            ' ノード作成
            Dim node = New TreeNode($"{item.Name}:{item.BaseItemName}") With {
                .Name = item.ItemId.ToString(),
                .Tag = item
            }

            ' 現在装備中のアイテムは強調表示
            If CurrentEquippedItem IsNot Nothing AndAlso
               CurrentEquippedItem.ItemId = item.ItemId Then
                node.BackColor = SystemColors.ActiveCaption
                node.ForeColor = SystemColors.ActiveCaptionText
            End If

            ' 称号オプション
            If item.PrefixOptions.Count > 0 Then
                Dim prefixNode = node.Nodes.Add("称号オプション")
                For Each op In item.PrefixOptions
                    prefixNode.Nodes.Add($"{op.OptionName}: {op.OptionValue}")
                Next
            End If

            ' 錬成オプション
            If item.NxOptions.Count > 0 Then
                Dim nxNode = node.Nodes.Add("錬成オプション")
                For Each op In item.NxOptions
                    nxNode.Nodes.Add($"{op.OptionName}: {op.OptionValue}")
                Next
            End If

            ' 精錬オプション
            If item.SmeltOptions.Count > 0 Then
                Dim smeltNode = node.Nodes.Add("精錬オプション")
                For Each op In item.SmeltOptions
                    smeltNode.Nodes.Add($"{op.OptionName}: {op.OptionValue}")
                Next
            End If

            Return node
        End Function

        ''' <summary>
        ''' 装備可能なアイテムをインベントリから抽出
        ''' </summary>
        Private Function GetEquippableItems() As IEnumerable(Of EquipInventoryItem)
            Return _inventoryReader.GetInventoryData().
                   Where(Function(item) IsEquippableToSlot(item))
        End Function

        ''' <summary>
        ''' アイテムがスロットに装備可能か判定
        ''' </summary>
        Private Function IsEquippableToSlot(item As EquipInventoryItem) As Boolean
            ' ベースアイテムの情報を取得
            Dim baseItem = _baseItemMaster.GetItemByName(item.BaseItemName)
            If baseItem Is Nothing Then Return False

            ' スロットと部位の整合性チェック
            Return IsValidSlotForItem(_slot, baseItem)
        End Function

        ''' <summary>
        ''' スロットと部位の整合性チェック
        ''' </summary>
        Private Function IsValidSlotForItem(slot As EquipmentManagerModel.SlotType, baseItem As BaseItemModel) As Boolean
            Select Case slot
                Case EquipmentManagerModel.SlotType.Ring1 To EquipmentManagerModel.SlotType.Ring10
                    Return baseItem.PartName.Contains("指輪")
                Case EquipmentManagerModel.SlotType.Weapon
                    Return baseItem.PartName.Contains("武器")
                Case EquipmentManagerModel.SlotType.SubWeapon
                    Return baseItem.PartName.Contains("補助")
                Case EquipmentManagerModel.SlotType.Neckless
                    Return baseItem.PartName.Contains("首"c)
                Case EquipmentManagerModel.SlotType.Helmet
                    Return baseItem.PartName.Contains("頭"c)
                Case EquipmentManagerModel.SlotType.Accessary
                    Return baseItem.PartName.Contains("耳"c) OrElse baseItem.PartName.Contains("背"c)
                Case EquipmentManagerModel.SlotType.Belt
                    Return baseItem.PartName.Contains("腰"c)
                Case EquipmentManagerModel.SlotType.Glove
                    Return baseItem.PartName.Contains("手"c)
                Case EquipmentManagerModel.SlotType.Armer
                    Return baseItem.PartName.Contains("胴"c)
                Case EquipmentManagerModel.SlotType.Boots
                    Return baseItem.PartName.Contains("足"c)
                Case Else
                    Return False
            End Select
        End Function
#End Region

#Region "操作メソッド"
        ''' <summary>
        ''' アイテムの選択
        ''' </summary>
        Public Sub SelectItem(itemId As String)
            If Guid.TryParse(itemId, New Guid) Then
                _selectedItemId = New Guid(itemId)
            End If
        End Sub

        ''' <summary>
        ''' 装備の実行
        ''' </summary>
        Public Function ExecuteEquip() As Boolean
            If Not _selectedItemId.HasValue Then Return False
            Return _equipManager.EquipItem(_slot, _selectedItemId.Value)
        End Function

        ''' <summary>
        ''' 装備の解除
        ''' </summary>
        Public Function ExecuteUnequip() As Boolean
            Return _equipManager.UnequipItem(_slot)
        End Function
#End Region
    End Class

End Namespace