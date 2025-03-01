Imports RS_EquipSimulation.Common.Equipment

Namespace CharacterSystem.Operation.Equipment

    Public Class InventoryPopupForm
#Region "プライベートフィールド"
        Private ReadOnly _viewModel As InventoryPopupViewModel
#End Region

#Region "コンストラクタ"
        Public Sub New(equipManager As EquipmentManagerModel,
                     inventoryReader As IEquipInventoryReader,
                     slot As EquipmentManagerModel.SlotType)
            InitializeComponent()

            ' ViewModelの初期化
            _viewModel = New InventoryPopupViewModel(equipManager, inventoryReader, slot)

            ' タイトル設定
            Me.Text = $"{slot.ToString()}の装備選択"

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
            ' 現在の装備表示
            Dim currentItem = _viewModel.CurrentEquippedItem
            If currentItem IsNot Nothing Then
                CurrentEquipName.Text = $"{currentItem.Name} ({currentItem.BaseItemName})"
            Else
                CurrentEquipName.Text = "なし"
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
                                  (_viewModel.CurrentEquippedItem Is Nothing OrElse
                                   _viewModel.CurrentEquippedItem.ItemId <> _viewModel.SelectedItemId.Value))

            ' 解除ボタンの状態設定
            UnequipButton.Enabled = (_viewModel.CurrentEquippedItem IsNot Nothing)
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
                MessageBox.Show("装備の変更に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                MessageBox.Show("装備の解除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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