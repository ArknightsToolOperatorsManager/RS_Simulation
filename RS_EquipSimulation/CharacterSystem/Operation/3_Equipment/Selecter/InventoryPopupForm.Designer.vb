Namespace CharacterSystem.Operation.Equipment

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class InventoryPopupForm
        Inherits System.Windows.Forms.Form

        'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Windows フォーム デザイナーで必要です。
        Private components As System.ComponentModel.IContainer

        'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
        'Windows フォーム デザイナーを使用して変更できます。  
        'コード エディターを使って変更しないでください。
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            CurrentEquipLabel = New Label()
            CurrentEquipName = New Label()
            InventoryTreeView = New TreeView()
            ButtonPanel = New Panel()
            EquipButton = New Button()
            UnequipButton = New Button()
            CloseButton = New Button()
            ButtonPanel.SuspendLayout()
            SuspendLayout()
            ' 
            ' CurrentEquipLabel
            ' 
            CurrentEquipLabel.AutoSize = True
            CurrentEquipLabel.Location = New Point(10, 10)
            CurrentEquipLabel.Name = "CurrentEquipLabel"
            CurrentEquipLabel.Size = New Size(77, 15)
            CurrentEquipLabel.TabIndex = 0
            CurrentEquipLabel.Text = "現在の装備："
            ' 
            ' CurrentEquipName
            ' 
            CurrentEquipName.AutoSize = True
            CurrentEquipName.Location = New Point(90, 10)
            CurrentEquipName.Name = "CurrentEquipName"
            CurrentEquipName.Size = New Size(0, 15)
            CurrentEquipName.TabIndex = 1
            ' 
            ' InventoryTreeView
            ' 
            InventoryTreeView.HideSelection = False
            InventoryTreeView.Location = New Point(10, 40)
            InventoryTreeView.Name = "InventoryTreeView"
            InventoryTreeView.Size = New Size(362, 375)
            InventoryTreeView.TabIndex = 2
            ' 
            ' ButtonPanel
            ' 
            ButtonPanel.Controls.Add(EquipButton)
            ButtonPanel.Controls.Add(UnequipButton)
            ButtonPanel.Controls.Add(CloseButton)
            ButtonPanel.Dock = DockStyle.Bottom
            ButtonPanel.Location = New Point(0, 421)
            ButtonPanel.Name = "ButtonPanel"
            ButtonPanel.Padding = New Padding(5)
            ButtonPanel.Size = New Size(384, 40)
            ButtonPanel.TabIndex = 3
            ' 
            ' EquipButton
            ' 
            EquipButton.Location = New Point(297, 8)
            EquipButton.Name = "EquipButton"
            EquipButton.Size = New Size(75, 23)
            EquipButton.TabIndex = 0
            EquipButton.Text = "装備"
            ' 
            ' UnequipButton
            ' 
            UnequipButton.Location = New Point(144, 8)
            UnequipButton.Name = "UnequipButton"
            UnequipButton.Size = New Size(75, 23)
            UnequipButton.TabIndex = 1
            UnequipButton.Text = "解除"
            ' 
            ' CloseButton
            ' 
            CloseButton.Location = New Point(10, 8)
            CloseButton.Name = "CloseButton"
            CloseButton.Size = New Size(75, 23)
            CloseButton.TabIndex = 2
            CloseButton.Text = "キャンセル"
            ' 
            ' InventoryPopupForm
            ' 
            AutoScaleDimensions = New SizeF(7.0F, 15.0F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(384, 461)
            Controls.Add(CurrentEquipLabel)
            Controls.Add(CurrentEquipName)
            Controls.Add(InventoryTreeView)
            Controls.Add(ButtonPanel)
            FormBorderStyle = FormBorderStyle.FixedDialog
            MaximizeBox = False
            MinimizeBox = False
            Name = "InventoryPopupForm"
            StartPosition = FormStartPosition.CenterParent
            Text = "装備選択"
            ButtonPanel.ResumeLayout(False)
            ResumeLayout(False)
            PerformLayout()
        End Sub

        Friend WithEvents CurrentEquipLabel As Label
        Friend WithEvents CurrentEquipName As Label
        Friend WithEvents InventoryTreeView As TreeView
        Friend WithEvents ButtonPanel As Panel
        Friend WithEvents EquipButton As Button
        Friend WithEvents UnequipButton As Button
        Friend WithEvents CloseButton As Button
    End Class

End Namespace
