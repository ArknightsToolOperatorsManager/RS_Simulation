Imports RS_EquipSimulation.Common.Equipment

Namespace CreatorSystem.Equipment

    Public Class EquipmentInventoryForm
        Private _manager As EquipInventoryManager

        ' インベントリマネージャーの設定
        Public Sub SetInventoryManager(manager As EquipInventoryManager)
            _manager = manager

            AddHandler _manager.InventoryChanged, AddressOf OnInventoryChanged
            RefreshInventoryTree()
        End Sub

        ' ツリーの更新
        Public Sub RefreshInventoryTree()
            InventoryTreeView.Nodes.Clear()
            If _manager Is Nothing Then Return

            For Each item In _manager.GetInventoryData()
                Dim itemName = $"{item.Name}:{item.BaseItemName}"
                Dim itemText = If(ShowItemTypeCheck.Checked, $"[{GetItemTypeName(item)}] {itemName}", itemName)
                Dim itemNode = InventoryTreeView.Nodes.Add(item.ItemId.ToString(), itemText)

                ' 称号オプション
                If item.PrefixOptions.Count > 0 Then
                    Dim prefixNode = itemNode.Nodes.Add("称号オプション")
                    For Each op In item.PrefixOptions
                        prefixNode.Nodes.Add($"{op.OptionName}: {op.OptionValue}")
                    Next
                End If

                ' 錬成オプション
                If item.NxOptions.Count > 0 Then
                    Dim nxNode = itemNode.Nodes.Add("錬成オプション")
                    For Each op In item.NxOptions
                        nxNode.Nodes.Add($"{op.OptionName}: {op.OptionValue}")
                    Next
                End If

                ' 精錬オプション
                If item.SmeltOptions.Count > 0 Then
                    Dim smeltNode = itemNode.Nodes.Add("精錬オプション")
                    For Each op In item.SmeltOptions
                        smeltNode.Nodes.Add($"{op.OptionName}: {op.OptionValue}")
                    Next
                End If

                ' 自動展開設定の反映
                If AutoExpandCheck.Checked Then
                    itemNode.ExpandAll()
                End If
            Next
        End Sub

        ' アイテムの部位名を取得（実際の実装では、適切なマッピングを使用）
        Private Function GetItemTypeName(item As EquipInventoryItem) As String
            ' TODO: 実際のアイテム部位名の取得処理を実装
            Return "" ' 仮の実装
        End Function

        Private Sub OnInventoryChanged(sender As Object, e As EventArgs)
            RefreshInventoryTree()
        End Sub

        ' 表示設定変更時の更新
        Private Sub ViewSetting_CheckedChanged(sender As Object, e As EventArgs) Handles AutoExpandCheck.CheckedChanged, ShowItemTypeCheck.CheckedChanged
            RefreshInventoryTree()
        End Sub

        ' 選択変更時のイベント
        Private Sub InventoryTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles InventoryTreeView.AfterSelect
            ' 比較表示用のイベント発火など
            ' TODO: 選択アイテムの処理を実装
        End Sub
    End Class

End Namespace