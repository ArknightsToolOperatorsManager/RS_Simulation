Namespace CreatorSystem.Equipment

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class EquipmentInventoryForm
        Inherits System.Windows.Forms.Form

        Private components As System.ComponentModel.IContainer = Nothing

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                    RemoveHandler _manager.InventoryChanged, AddressOf OnInventoryChanged
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private Sub InitializeComponent()
            InventoryTreeView = New TreeView()
            ViewSettingGroup = New GroupBox()
            AutoExpandCheck = New CheckBox()
            ShowItemTypeCheck = New CheckBox()
            ViewSettingGroup.SuspendLayout()
            SuspendLayout()
            ' 
            ' InventoryTreeView
            ' 
            InventoryTreeView.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            InventoryTreeView.HideSelection = False
            InventoryTreeView.Location = New Point(0, 50)
            InventoryTreeView.Name = "InventoryTreeView"
            InventoryTreeView.Size = New Size(290, 600)
            InventoryTreeView.TabIndex = 0
            ' 
            ' ViewSettingGroup
            ' 
            ViewSettingGroup.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            ViewSettingGroup.Controls.Add(AutoExpandCheck)
            ViewSettingGroup.Controls.Add(ShowItemTypeCheck)
            ViewSettingGroup.Location = New Point(0, 0)
            ViewSettingGroup.Name = "ViewSettingGroup"
            ViewSettingGroup.Size = New Size(290, 50)
            ViewSettingGroup.TabIndex = 1
            ViewSettingGroup.TabStop = False
            ViewSettingGroup.Text = "表示設定"
            ' 
            ' AutoExpandCheck
            ' 
            AutoExpandCheck.AutoSize = True
            AutoExpandCheck.Location = New Point(10, 20)
            AutoExpandCheck.Name = "AutoExpandCheck"
            AutoExpandCheck.Size = New Size(74, 19)
            AutoExpandCheck.TabIndex = 0
            AutoExpandCheck.Text = "自動展開"
            ' 
            ' ShowItemTypeCheck
            ' 
            ShowItemTypeCheck.AutoSize = True
            ShowItemTypeCheck.Location = New Point(120, 20)
            ShowItemTypeCheck.Name = "ShowItemTypeCheck"
            ShowItemTypeCheck.Size = New Size(86, 19)
            ShowItemTypeCheck.TabIndex = 1
            ShowItemTypeCheck.Text = "部位名表示"
            ' 
            ' EquipmentInventoryForm
            ' 
            AutoScaleDimensions = New SizeF(7F, 15F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(290, 650)
            Controls.Add(InventoryTreeView)
            Controls.Add(ViewSettingGroup)
            FormBorderStyle = FormBorderStyle.None
            Name = "EquipmentInventoryForm"
            ViewSettingGroup.ResumeLayout(False)
            ViewSettingGroup.PerformLayout()
            ResumeLayout(False)
        End Sub

        Friend WithEvents InventoryTreeView As TreeView
        Friend WithEvents ViewSettingGroup As GroupBox
        Friend WithEvents AutoExpandCheck As CheckBox
        Friend WithEvents ShowItemTypeCheck As CheckBox
    End Class

End Namespace