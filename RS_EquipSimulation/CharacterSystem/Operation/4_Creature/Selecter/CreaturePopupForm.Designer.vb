Namespace CharacterSystem.Operation.Creature

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class CreaturePopupForm
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
            CurrentCreatureLabel = New Label()
            CurrentCreatureName = New Label()
            InventoryTreeView = New TreeView()
            ButtonPanel = New Panel()
            EquipButton = New Button()
            UnequipButton = New Button()
            CloseButton = New Button()
            ButtonPanel.SuspendLayout()
            SuspendLayout()
            ' 
            ' CurrentCreatureLabel
            ' 
            CurrentCreatureLabel.AutoSize = True
            CurrentCreatureLabel.Location = New Point(10, 10)
            CurrentCreatureLabel.Name = "CurrentCreatureLabel"
            CurrentCreatureLabel.Size = New Size(120, 15)
            CurrentCreatureLabel.TabIndex = 0
            CurrentCreatureLabel.Text = "現在のクリーチャー："
            ' 
            ' CurrentCreatureName
            ' 
            CurrentCreatureName.AutoSize = True
            CurrentCreatureName.Location = New Point(150, 10)
            CurrentCreatureName.Name = "CurrentCreatureName"
            CurrentCreatureName.Size = New Size(0, 15)
            CurrentCreatureName.TabIndex = 1
            ' 
            ' InventoryTreeView
            ' 
            InventoryTreeView.HideSelection = False
            InventoryTreeView.Location = New Point(10, 40)
            InventoryTreeView.Name = "InventoryTreeView"
            InventoryTreeView.Size = New Size(380, 410)
            InventoryTreeView.TabIndex = 2
            ' 
            ' ButtonPanel
            ' 
            ButtonPanel.Controls.Add(EquipButton)
            ButtonPanel.Controls.Add(UnequipButton)
            ButtonPanel.Controls.Add(CloseButton)
            ButtonPanel.Dock = DockStyle.Bottom
            ButtonPanel.Location = New Point(0, 460)
            ButtonPanel.Name = "ButtonPanel"
            ButtonPanel.Size = New Size(400, 40)
            ButtonPanel.TabIndex = 3
            ' 
            ' EquipButton
            ' 
            EquipButton.Location = New Point(315, 8)
            EquipButton.Name = "EquipButton"
            EquipButton.Size = New Size(75, 23)
            EquipButton.TabIndex = 0
            EquipButton.Text = "装備"
            EquipButton.UseVisualStyleBackColor = True
            ' 
            ' UnequipButton
            ' 
            UnequipButton.Location = New Point(160, 8)
            UnequipButton.Name = "UnequipButton"
            UnequipButton.Size = New Size(75, 23)
            UnequipButton.TabIndex = 1
            UnequipButton.Text = "解除"
            UnequipButton.UseVisualStyleBackColor = True
            ' 
            ' CloseButton
            ' 
            CloseButton.Location = New Point(10, 8)
            CloseButton.Name = "CloseButton"
            CloseButton.Size = New Size(75, 23)
            CloseButton.TabIndex = 2
            CloseButton.Text = "キャンセル"
            CloseButton.UseVisualStyleBackColor = True
            ' 
            ' CreaturePopupForm
            ' 
            AutoScaleDimensions = New SizeF(7.0F, 15.0F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(400, 500)
            Controls.Add(CurrentCreatureLabel)
            Controls.Add(CurrentCreatureName)
            Controls.Add(InventoryTreeView)
            Controls.Add(ButtonPanel)
            FormBorderStyle = FormBorderStyle.FixedDialog
            MaximizeBox = False
            MinimizeBox = False
            Name = "CreaturePopupForm"
            StartPosition = FormStartPosition.CenterParent
            Text = "クリーチャー選択"
            ButtonPanel.ResumeLayout(False)
            ResumeLayout(False)
            PerformLayout()
        End Sub

        Friend WithEvents CurrentCreatureLabel As Label
        Friend WithEvents CurrentCreatureName As Label
        Friend WithEvents InventoryTreeView As TreeView
        Friend WithEvents ButtonPanel As Panel
        Friend WithEvents EquipButton As Button
        Friend WithEvents UnequipButton As Button
        Friend WithEvents CloseButton As Button
    End Class

End Namespace