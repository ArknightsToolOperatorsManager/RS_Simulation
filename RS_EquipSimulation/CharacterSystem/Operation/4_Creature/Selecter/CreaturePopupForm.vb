Imports RS_EquipSimulation.Common.Creature

Namespace CharacterSystem.Operation.Creature

    Public Class CreaturePopupForm

#Region "プライベートフィールド"
        Private ReadOnly _viewModel As CreaturePopupViewModel
#End Region

#Region "コンストラクタ"
        Public Sub New(creatureManager As CreatureManagerModel,
                     inventoryReader As ICreatureInventoryReader,
                     slot As CreatureManagerModel.SlotType)
            InitializeComponent()

            ' ViewModelの初期化
            _viewModel = New CreaturePopupViewModel(creatureManager, inventoryReader, slot)

            ' タイトル設定
            Me.Text = $"{slot.ToString()}のクリーチャー選択"

            ' 初期表示の設定
            InitializeDisplay()
            SetButtonState()
        End Sub
#End Region

#Region "初期化処理"
        ''' <summary>
        ''' 表示の初期化
        ''' </summary>
        Private Sub InitializeDisplay()
            ' 現在のクリーチャー表示
            Dim currentItem = _viewModel.CurrentEquippedCreature
            If currentItem IsNot Nothing Then
                CurrentCreatureName.Text = $"{currentItem.Name} ({currentItem.BaseRarity}→{currentItem.EnhancedRarity})"
            Else
                CurrentCreatureName.Text = "なし"
            End If

            ' ツリーの構築
            InventoryTreeView.Nodes.AddRange(_viewModel.CreateInventoryNodes())
            InventoryTreeView.ExpandAll()

            ' 現在装備中のアイテムが表示されるようにスクロール
            If currentItem IsNot Nothing Then
                Dim currentNode = InventoryTreeView.Nodes.Find(currentItem.ItemId.ToString(), True).FirstOrDefault()
                If currentNode IsNot Nothing Then
                    InventoryTreeView.SelectedNode = currentNode
                    currentNode.EnsureVisible()
                End If
            End If
        End Sub

        ''' <summary>
        ''' ボタン状態の設定
        ''' </summary>
        Private Sub SetButtonState()
            ' 装備ボタンの状態設定
            EquipButton.Enabled = (_viewModel.SelectedItemId.HasValue AndAlso
                                  (_viewModel.CurrentEquippedCreature Is Nothing OrElse
                                   _viewModel.CurrentEquippedCreature.ItemId <> _viewModel.SelectedItemId.Value))

            ' 解除ボタンの状態設定
            UnequipButton.Enabled = (_viewModel.CurrentEquippedCreature IsNot Nothing)
        End Sub
#End Region

#Region "イベントハンドラ"
        ''' <summary>
        ''' ツリー選択変更時の処理
        ''' </summary>
        Private Sub InventoryTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles InventoryTreeView.AfterSelect
            ' ルートノード以外は選択させない
            If e.Node.Parent IsNot Nothing Then
                InventoryTreeView.SelectedNode = e.Node.Parent
                Return
            End If

            ' 選択アイテムの更新
            _viewModel.SelectItem(e.Node.Name)
            SetButtonState()
        End Sub

        ''' <summary>
        ''' 装備ボタンクリック時の処理
        ''' </summary>
        Private Sub EquipButton_Click(sender As Object, e As EventArgs) Handles EquipButton.Click
            If _viewModel.ExecuteEquip() Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("クリーチャーの装備に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Sub

        ''' <summary>
        ''' 解除ボタンクリック時の処理
        ''' </summary>
        Private Sub UnequipButton_Click(sender As Object, e As EventArgs) Handles UnequipButton.Click
            If _viewModel.ExecuteUnequip() Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("クリーチャーの解除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Sub

        ''' <summary>
        ''' 閉じるボタンクリック時の処理
        ''' </summary>
        Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub
#End Region

    End Class

End Namespace