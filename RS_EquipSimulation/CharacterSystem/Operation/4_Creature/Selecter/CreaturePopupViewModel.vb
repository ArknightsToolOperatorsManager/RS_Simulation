Imports System.Windows.Forms
Imports RS_EquipSimulation.Common.Creature

Namespace CharacterSystem.Operation.Creature

    ''' <summary>
    ''' クリーチャー選択ポップアップのViewModel
    ''' </summary>
    Public Class CreaturePopupViewModel
        Implements IDisposable

#Region "プロパティ"
        ' マネージャー参照
        Private ReadOnly _creatureManager As CreatureManagerModel
        Private ReadOnly _inventoryReader As ICreatureInventoryReader
        Private ReadOnly _slot As CreatureManagerModel.SlotType

        ' 選択状態
        Private _selectedItemId As Guid?
        Public ReadOnly Property SelectedItemId As Guid?
            Get
                Return _selectedItemId
            End Get
        End Property

        ' 現在の装備品
        Public ReadOnly Property CurrentEquippedCreature As CreatureInventoryItem
            Get
                Return _creatureManager.GetEquippedCreature(_slot)
            End Get
        End Property
#End Region

#Region "コンストラクタ/Dispose"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        Public Sub New(creatureManager As CreatureManagerModel,
                     inventoryReader As ICreatureInventoryReader,
                     slot As CreatureManagerModel.SlotType)
            _creatureManager = creatureManager
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
        ''' インベントリのTreeNodeコレクションを作成
        ''' </summary>
        Public Function CreateInventoryNodes() As TreeNode()
            Dim nodeList As New List(Of TreeNode)

            ' インベントリからすべてのクリーチャーを取得
            For Each item In _inventoryReader.GetInventoryData()
                Dim node = CreateCreatureNode(item)
                If node IsNot Nothing Then
                    nodeList.Add(node)
                End If
            Next

            Return nodeList.ToArray()
        End Function

        ''' <summary>
        ''' クリーチャーのTreeNodeを作成
        ''' </summary>
        Private Function CreateCreatureNode(creature As CreatureInventoryItem) As TreeNode
            ' ノード作成
            Dim node = New TreeNode($"{creature.Name} ({creature.BaseRarity}→{creature.EnhancedRarity})") With {
                .Name = creature.ItemId.ToString(),
                .Tag = creature
            }

            ' 現在装備中のクリーチャーは強調表示
            If CurrentEquippedCreature IsNot Nothing AndAlso
               CurrentEquippedCreature.ItemId = creature.ItemId Then
                node.BackColor = SystemColors.ActiveCaption
                node.ForeColor = SystemColors.ActiveCaptionText
            End If

            ' メインパッシブ
            If creature.MainPassive IsNot Nothing Then
                Dim mainNode = node.Nodes.Add($"メインパッシブ: {creature.MainPassive.Name}")
                mainNode.Nodes.Add($"レベル: {creature.MainPassive.GetDisplayValue()}")
            End If

            ' サブパッシブ
            For Each passive In creature.SubPassive
                Dim subNode = node.Nodes.Add($"サブパッシブ: {passive.Name}")
                subNode.Nodes.Add($"レベル: {passive.GetDisplayValue()}")
            Next

            Return node
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
            Return _creatureManager.EquipCreature(_slot, _selectedItemId.Value)
        End Function

        ''' <summary>
        ''' 装備の解除
        ''' </summary>
        Public Function ExecuteUnequip() As Boolean
            Return _creatureManager.UnequipCreature(_slot)
        End Function
#End Region

    End Class

End Namespace