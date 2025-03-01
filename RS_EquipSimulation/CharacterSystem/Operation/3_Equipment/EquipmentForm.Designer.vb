Namespace CharacterSystem.Operation.Equipment

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class EquipmentForm
        Inherits Form

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
            TableLayoutPanel1 = New TableLayoutPanel()
            BootsButton = New Button()
            ArmerButton = New Button()
            GloveButton = New Button()
            BeltButton = New Button()
            AccessaryButton = New Button()
            HelmetButton = New Button()
            NecklessButton = New Button()
            SubWeaponButton = New Button()
            WeaponButton = New Button()
            TableLayoutPanel2 = New TableLayoutPanel()
            TableLayoutPanel3 = New TableLayoutPanel()
            TableLayoutPanel4 = New TableLayoutPanel()
            Ring6Button = New Button()
            Ring10Button = New Button()
            Ring9Button = New Button()
            Ring8Button = New Button()
            Ring7Button = New Button()
            Ring5Button = New Button()
            Ring4Button = New Button()
            Ring3Button = New Button()
            Ring2Button = New Button()
            Ring1Button = New Button()
            GroupBox1 = New GroupBox()
            TableLayoutPanel1.SuspendLayout()
            TableLayoutPanel4.SuspendLayout()
            GroupBox1.SuspendLayout()
            SuspendLayout()
            ' 
            ' TableLayoutPanel1
            ' 
            TableLayoutPanel1.ColumnCount = 5
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel1.Controls.Add(BootsButton, 4, 1)
            TableLayoutPanel1.Controls.Add(ArmerButton, 3, 1)
            TableLayoutPanel1.Controls.Add(GloveButton, 2, 1)
            TableLayoutPanel1.Controls.Add(BeltButton, 1, 1)
            TableLayoutPanel1.Controls.Add(AccessaryButton, 4, 0)
            TableLayoutPanel1.Controls.Add(HelmetButton, 3, 0)
            TableLayoutPanel1.Controls.Add(NecklessButton, 2, 0)
            TableLayoutPanel1.Controls.Add(SubWeaponButton, 1, 0)
            TableLayoutPanel1.Controls.Add(WeaponButton, 0, 0)
            TableLayoutPanel1.Location = New Point(6, 22)
            TableLayoutPanel1.Name = "TableLayoutPanel1"
            TableLayoutPanel1.RowCount = 2
            TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
            TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
            TableLayoutPanel1.Size = New Size(230, 92)
            TableLayoutPanel1.TabIndex = 0
            ' 
            ' BootsButton
            ' 
            BootsButton.Dock = DockStyle.Fill
            BootsButton.Location = New Point(187, 49)
            BootsButton.Name = "BootsButton"
            BootsButton.Size = New Size(40, 40)
            BootsButton.TabIndex = 9
            BootsButton.Text = "足"
            BootsButton.UseVisualStyleBackColor = True
            ' 
            ' ArmerButton
            ' 
            ArmerButton.Dock = DockStyle.Fill
            ArmerButton.Location = New Point(141, 49)
            ArmerButton.Name = "ArmerButton"
            ArmerButton.Size = New Size(40, 40)
            ArmerButton.TabIndex = 8
            ArmerButton.Text = "胴"
            ArmerButton.UseVisualStyleBackColor = True
            ' 
            ' GloveButton
            ' 
            GloveButton.Dock = DockStyle.Fill
            GloveButton.Location = New Point(95, 49)
            GloveButton.Name = "GloveButton"
            GloveButton.Size = New Size(40, 40)
            GloveButton.TabIndex = 7
            GloveButton.Text = "手"
            GloveButton.UseVisualStyleBackColor = True
            ' 
            ' BeltButton
            ' 
            BeltButton.Dock = DockStyle.Fill
            BeltButton.Location = New Point(49, 49)
            BeltButton.Name = "BeltButton"
            BeltButton.Size = New Size(40, 40)
            BeltButton.TabIndex = 6
            BeltButton.Text = "腰"
            BeltButton.UseVisualStyleBackColor = True
            ' 
            ' AccessaryButton
            ' 
            AccessaryButton.Dock = DockStyle.Fill
            AccessaryButton.Location = New Point(187, 3)
            AccessaryButton.Name = "AccessaryButton"
            AccessaryButton.Size = New Size(40, 40)
            AccessaryButton.TabIndex = 4
            AccessaryButton.Text = "耳背"
            AccessaryButton.UseVisualStyleBackColor = True
            ' 
            ' HelmetButton
            ' 
            HelmetButton.Dock = DockStyle.Fill
            HelmetButton.Location = New Point(141, 3)
            HelmetButton.Name = "HelmetButton"
            HelmetButton.Size = New Size(40, 40)
            HelmetButton.TabIndex = 3
            HelmetButton.Text = "頭"
            HelmetButton.UseVisualStyleBackColor = True
            ' 
            ' NecklessButton
            ' 
            NecklessButton.Dock = DockStyle.Fill
            NecklessButton.Location = New Point(95, 3)
            NecklessButton.Name = "NecklessButton"
            NecklessButton.Size = New Size(40, 40)
            NecklessButton.TabIndex = 2
            NecklessButton.Text = "首"
            NecklessButton.UseVisualStyleBackColor = True
            ' 
            ' SubWeaponButton
            ' 
            SubWeaponButton.Dock = DockStyle.Fill
            SubWeaponButton.Location = New Point(49, 3)
            SubWeaponButton.Name = "SubWeaponButton"
            SubWeaponButton.Size = New Size(40, 40)
            SubWeaponButton.TabIndex = 1
            SubWeaponButton.Text = "補助"
            SubWeaponButton.UseVisualStyleBackColor = True
            ' 
            ' WeaponButton
            ' 
            WeaponButton.Dock = DockStyle.Fill
            WeaponButton.Location = New Point(3, 3)
            WeaponButton.Name = "WeaponButton"
            WeaponButton.Size = New Size(40, 40)
            WeaponButton.TabIndex = 0
            WeaponButton.Text = "武器"
            WeaponButton.UseVisualStyleBackColor = True
            ' 
            ' TableLayoutPanel2
            ' 
            TableLayoutPanel2.ColumnCount = 5
            TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
            TableLayoutPanel2.Location = New Point(0, 0)
            TableLayoutPanel2.Name = "TableLayoutPanel2"
            TableLayoutPanel2.RowCount = 2
            TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
            TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
            TableLayoutPanel2.Size = New Size(200, 100)
            TableLayoutPanel2.TabIndex = 0
            ' 
            ' TableLayoutPanel3
            ' 
            TableLayoutPanel3.ColumnCount = 5
            TableLayoutPanel3.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel3.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel3.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel3.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel3.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
            TableLayoutPanel3.Location = New Point(0, 0)
            TableLayoutPanel3.Name = "TableLayoutPanel3"
            TableLayoutPanel3.RowCount = 2
            TableLayoutPanel3.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
            TableLayoutPanel3.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
            TableLayoutPanel3.Size = New Size(200, 100)
            TableLayoutPanel3.TabIndex = 0
            ' 
            ' TableLayoutPanel4
            ' 
            TableLayoutPanel4.ColumnCount = 5
            TableLayoutPanel4.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel4.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel4.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel4.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel4.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
            TableLayoutPanel4.Controls.Add(Ring6Button, 0, 1)
            TableLayoutPanel4.Controls.Add(Ring10Button, 4, 1)
            TableLayoutPanel4.Controls.Add(Ring9Button, 3, 1)
            TableLayoutPanel4.Controls.Add(Ring8Button, 2, 1)
            TableLayoutPanel4.Controls.Add(Ring7Button, 1, 1)
            TableLayoutPanel4.Controls.Add(Ring5Button, 4, 0)
            TableLayoutPanel4.Controls.Add(Ring4Button, 3, 0)
            TableLayoutPanel4.Controls.Add(Ring3Button, 2, 0)
            TableLayoutPanel4.Controls.Add(Ring2Button, 1, 0)
            TableLayoutPanel4.Controls.Add(Ring1Button, 0, 0)
            TableLayoutPanel4.Location = New Point(6, 120)
            TableLayoutPanel4.Name = "TableLayoutPanel4"
            TableLayoutPanel4.RowCount = 2
            TableLayoutPanel4.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
            TableLayoutPanel4.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
            TableLayoutPanel4.Size = New Size(230, 92)
            TableLayoutPanel4.TabIndex = 0
            ' 
            ' Ring6Button
            ' 
            Ring6Button.Dock = DockStyle.Fill
            Ring6Button.Location = New Point(3, 49)
            Ring6Button.Name = "Ring6Button"
            Ring6Button.Size = New Size(40, 40)
            Ring6Button.TabIndex = 10
            Ring6Button.Text = "指6"
            Ring6Button.UseVisualStyleBackColor = True
            ' 
            ' Ring10Button
            ' 
            Ring10Button.Dock = DockStyle.Fill
            Ring10Button.Location = New Point(187, 49)
            Ring10Button.Name = "Ring10Button"
            Ring10Button.Size = New Size(40, 40)
            Ring10Button.TabIndex = 9
            Ring10Button.Text = "指10"
            Ring10Button.UseVisualStyleBackColor = True
            ' 
            ' Ring9Button
            ' 
            Ring9Button.Dock = DockStyle.Fill
            Ring9Button.Location = New Point(141, 49)
            Ring9Button.Name = "Ring9Button"
            Ring9Button.Size = New Size(40, 40)
            Ring9Button.TabIndex = 8
            Ring9Button.Text = "指9"
            Ring9Button.UseVisualStyleBackColor = True
            ' 
            ' Ring8Button
            ' 
            Ring8Button.Dock = DockStyle.Fill
            Ring8Button.Location = New Point(95, 49)
            Ring8Button.Name = "Ring8Button"
            Ring8Button.Size = New Size(40, 40)
            Ring8Button.TabIndex = 7
            Ring8Button.Text = "指8"
            Ring8Button.UseVisualStyleBackColor = True
            ' 
            ' Ring7Button
            ' 
            Ring7Button.Dock = DockStyle.Fill
            Ring7Button.Location = New Point(49, 49)
            Ring7Button.Name = "Ring7Button"
            Ring7Button.Size = New Size(40, 40)
            Ring7Button.TabIndex = 6
            Ring7Button.Text = "指7"
            Ring7Button.UseVisualStyleBackColor = True
            ' 
            ' Ring5Button
            ' 
            Ring5Button.Dock = DockStyle.Fill
            Ring5Button.Location = New Point(187, 3)
            Ring5Button.Name = "Ring5Button"
            Ring5Button.Size = New Size(40, 40)
            Ring5Button.TabIndex = 4
            Ring5Button.Text = "指5"
            Ring5Button.UseVisualStyleBackColor = True
            ' 
            ' Ring4Button
            ' 
            Ring4Button.Dock = DockStyle.Fill
            Ring4Button.Location = New Point(141, 3)
            Ring4Button.Name = "Ring4Button"
            Ring4Button.Size = New Size(40, 40)
            Ring4Button.TabIndex = 3
            Ring4Button.Text = "指4"
            Ring4Button.UseVisualStyleBackColor = True
            ' 
            ' Ring3Button
            ' 
            Ring3Button.Dock = DockStyle.Fill
            Ring3Button.Location = New Point(95, 3)
            Ring3Button.Name = "Ring3Button"
            Ring3Button.Size = New Size(40, 40)
            Ring3Button.TabIndex = 2
            Ring3Button.Text = "指3"
            Ring3Button.UseVisualStyleBackColor = True
            ' 
            ' Ring2Button
            ' 
            Ring2Button.Dock = DockStyle.Fill
            Ring2Button.Location = New Point(49, 3)
            Ring2Button.Name = "Ring2Button"
            Ring2Button.Size = New Size(40, 40)
            Ring2Button.TabIndex = 1
            Ring2Button.Text = "指2"
            Ring2Button.UseVisualStyleBackColor = True
            ' 
            ' Ring1Button
            ' 
            Ring1Button.Dock = DockStyle.Fill
            Ring1Button.Location = New Point(3, 3)
            Ring1Button.Name = "Ring1Button"
            Ring1Button.Size = New Size(40, 40)
            Ring1Button.TabIndex = 0
            Ring1Button.Text = "指1"
            Ring1Button.UseVisualStyleBackColor = True
            ' 
            ' GroupBox1
            ' 
            GroupBox1.Controls.Add(TableLayoutPanel4)
            GroupBox1.Controls.Add(TableLayoutPanel1)
            GroupBox1.Dock = DockStyle.Fill
            GroupBox1.Location = New Point(0, 0)
            GroupBox1.Name = "GroupBox1"
            GroupBox1.Size = New Size(242, 218)
            GroupBox1.TabIndex = 1
            GroupBox1.TabStop = False
            GroupBox1.Text = "装備"
            ' 
            ' EquipmentForm
            ' 
            AutoScaleDimensions = New SizeF(7F, 15F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(242, 218)
            Controls.Add(GroupBox1)
            FormBorderStyle = FormBorderStyle.None
            Name = "EquipmentForm"
            Text = "EquipmentForm"
            TableLayoutPanel1.ResumeLayout(False)
            TableLayoutPanel4.ResumeLayout(False)
            GroupBox1.ResumeLayout(False)
            ResumeLayout(False)
        End Sub

        Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
        Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
        Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
        Friend WithEvents BootsButton As Button
        Friend WithEvents ArmerButton As Button
        Friend WithEvents GloveButton As Button
        Friend WithEvents BeltButton As Button
        Friend WithEvents AccessaryButton As Button
        Friend WithEvents HelmetButton As Button
        Friend WithEvents NecklessButton As Button
        Friend WithEvents SubWeaponButton As Button
        Friend WithEvents WeaponButton As Button
        Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
        Friend WithEvents Ring10Button As Button
        Friend WithEvents Ring9Button As Button
        Friend WithEvents Ring8Button As Button
        Friend WithEvents Ring7Button As Button
        Friend WithEvents Ring5Button As Button
        Friend WithEvents Ring4Button As Button
        Friend WithEvents Ring3Button As Button
        Friend WithEvents Ring2Button As Button
        Friend WithEvents Ring1Button As Button
        Friend WithEvents Ring6Button As Button
        Friend WithEvents GroupBox1 As GroupBox
    End Class

End Namespace