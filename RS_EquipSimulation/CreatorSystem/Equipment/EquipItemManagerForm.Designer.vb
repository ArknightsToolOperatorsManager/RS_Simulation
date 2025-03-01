Namespace CreatorSystem.Equipment

    Partial Class EquipItemManagerForm
        Inherits System.Windows.Forms.Form

        Private components As System.ComponentModel.IContainer = Nothing

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private Sub InitializeComponent()
            CreatePanel = New Panel()
            ComparePanel = New Panel()
            InventoryPanel = New Panel()
            GroupBox1 = New GroupBox()
            GroupBox2 = New GroupBox()
            GroupBox3 = New GroupBox()
            GroupBox1.SuspendLayout()
            GroupBox2.SuspendLayout()
            GroupBox3.SuspendLayout()
            SuspendLayout()
            ' 
            ' CreatePanel
            ' 
            CreatePanel.Location = New Point(6, 22)
            CreatePanel.Name = "CreatePanel"
            CreatePanel.Size = New Size(286, 711)
            CreatePanel.TabIndex = 0
            ' 
            ' ComparePanel
            ' 
            ComparePanel.Location = New Point(7, 22)
            ComparePanel.Name = "ComparePanel"
            ComparePanel.Size = New Size(270, 711)
            ComparePanel.TabIndex = 1
            ' 
            ' InventoryPanel
            ' 
            InventoryPanel.Location = New Point(6, 22)
            InventoryPanel.Name = "InventoryPanel"
            InventoryPanel.Size = New Size(290, 711)
            InventoryPanel.TabIndex = 2
            ' 
            ' GroupBox1
            ' 
            GroupBox1.Controls.Add(CreatePanel)
            GroupBox1.Location = New Point(0, 0)
            GroupBox1.Name = "GroupBox1"
            GroupBox1.Size = New Size(298, 738)
            GroupBox1.TabIndex = 3
            GroupBox1.TabStop = False
            GroupBox1.Text = "Create Equipment"
            ' 
            ' GroupBox2
            ' 
            GroupBox2.Controls.Add(InventoryPanel)
            GroupBox2.Location = New Point(298, 0)
            GroupBox2.Name = "GroupBox2"
            GroupBox2.Size = New Size(303, 738)
            GroupBox2.TabIndex = 4
            GroupBox2.TabStop = False
            GroupBox2.Text = "Inventory"
            ' 
            ' GroupBox3
            ' 
            GroupBox3.Controls.Add(ComparePanel)
            GroupBox3.Location = New Point(600, 0)
            GroupBox3.Name = "GroupBox3"
            GroupBox3.Size = New Size(282, 738)
            GroupBox3.TabIndex = 5
            GroupBox3.TabStop = False
            GroupBox3.Text = "[未実装]Compare 2 items"
            ' 
            ' EquipmentManagerForm
            ' 
            AutoScaleDimensions = New SizeF(7F, 15F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(883, 738)
            Controls.Add(GroupBox3)
            Controls.Add(GroupBox2)
            Controls.Add(GroupBox1)
            FormBorderStyle = FormBorderStyle.FixedSingle
            MaximizeBox = False
            Name = "EquipmentManagerForm"
            StartPosition = FormStartPosition.CenterParent
            Text = "Equipment Manager"
            GroupBox1.ResumeLayout(False)
            GroupBox2.ResumeLayout(False)
            GroupBox3.ResumeLayout(False)
            ResumeLayout(False)
        End Sub

        Friend WithEvents CreatePanel As Panel
        Friend WithEvents ComparePanel As Panel
        Friend WithEvents InventoryPanel As Panel
        Friend WithEvents GroupBox1 As GroupBox
        Friend WithEvents GroupBox2 As GroupBox
        Friend WithEvents GroupBox3 As GroupBox
    End Class

End Namespace