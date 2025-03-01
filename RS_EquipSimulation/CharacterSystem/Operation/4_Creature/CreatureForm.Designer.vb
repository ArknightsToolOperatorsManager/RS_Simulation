Namespace CharacterSystem.Operation.Creature

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class CreatureForm
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
            CreatureGroupBox = New GroupBox()
            TableLayoutPanel1 = New TableLayoutPanel()
            MainCreatureButton = New Button()
            SubCreature2Button = New Button()
            SubCreature3Button = New Button()
            SubCreature1Buton = New Button()
            GroupBox1 = New GroupBox()
            ListView1 = New ListView()
            CreatureGroupBox.SuspendLayout()
            TableLayoutPanel1.SuspendLayout()
            GroupBox1.SuspendLayout()
            SuspendLayout()
            ' 
            ' CreatureGroupBox
            ' 
            CreatureGroupBox.Controls.Add(TableLayoutPanel1)
            CreatureGroupBox.Location = New Point(0, 0)
            CreatureGroupBox.Name = "CreatureGroupBox"
            CreatureGroupBox.Size = New Size(152, 121)
            CreatureGroupBox.TabIndex = 0
            CreatureGroupBox.TabStop = False
            CreatureGroupBox.Text = "クリーチャー"
            ' 
            ' TableLayoutPanel1
            ' 
            TableLayoutPanel1.ColumnCount = 3
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
            TableLayoutPanel1.Controls.Add(SubCreature1Buton, 0, 1)
            TableLayoutPanel1.Controls.Add(SubCreature3Button, 0, 1)
            TableLayoutPanel1.Controls.Add(SubCreature2Button, 0, 1)
            TableLayoutPanel1.Controls.Add(MainCreatureButton, 0, 0)
            TableLayoutPanel1.Location = New Point(6, 22)
            TableLayoutPanel1.Name = "TableLayoutPanel1"
            TableLayoutPanel1.RowCount = 2
            TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))
            TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))
            TableLayoutPanel1.Size = New Size(138, 92)
            TableLayoutPanel1.TabIndex = 0
            ' 
            ' MainCreatureButton
            ' 
            TableLayoutPanel1.SetColumnSpan(MainCreatureButton, 2)
            MainCreatureButton.Dock = DockStyle.Fill
            MainCreatureButton.Location = New Point(3, 3)
            MainCreatureButton.Name = "MainCreatureButton"
            MainCreatureButton.Size = New Size(86, 40)
            MainCreatureButton.TabIndex = 1
            MainCreatureButton.Text = "メイン"
            MainCreatureButton.UseVisualStyleBackColor = True
            ' 
            ' SubCreature1Buton
            ' 
            SubCreature1Buton.Dock = DockStyle.Fill
            SubCreature1Buton.Location = New Point(3, 49)
            SubCreature1Buton.Name = "SubCreature1Buton"
            SubCreature1Buton.Size = New Size(40, 40)
            SubCreature1Buton.TabIndex = 2
            SubCreature1Buton.Text = "サブ1"
            SubCreature1Buton.UseVisualStyleBackColor = True
            ' 
            ' SubCreature2Button
            ' 
            SubCreature2Button.Dock = DockStyle.Fill
            SubCreature2Button.Location = New Point(49, 49)
            SubCreature2Button.Name = "SubCreature2Button"
            SubCreature2Button.Size = New Size(40, 40)
            SubCreature2Button.TabIndex = 3
            SubCreature2Button.Text = "サブ2"
            SubCreature2Button.UseVisualStyleBackColor = True
            ' 
            ' SubCreature3Button
            ' 
            SubCreature3Button.Dock = DockStyle.Fill
            SubCreature3Button.Location = New Point(95, 49)
            SubCreature3Button.Name = "SubCreature3Button"
            SubCreature3Button.Size = New Size(40, 40)
            SubCreature3Button.TabIndex = 4
            SubCreature3Button.Text = "サブ3"
            SubCreature3Button.UseVisualStyleBackColor = True
            ' 
            ' GroupBox1
            ' 
            GroupBox1.Controls.Add(ListView1)
            GroupBox1.Location = New Point(0, 127)
            GroupBox1.Name = "GroupBox1"
            GroupBox1.Size = New Size(152, 129)
            GroupBox1.TabIndex = 1
            GroupBox1.TabStop = False
            GroupBox1.Text = "クリーチャー効果"
            ' 
            ' ListView1
            ' 
            ListView1.Dock = DockStyle.Fill
            ListView1.Location = New Point(3, 19)
            ListView1.Name = "ListView1"
            ListView1.Size = New Size(146, 107)
            ListView1.TabIndex = 0
            ListView1.UseCompatibleStateImageBehavior = False
            ' 
            ' CreatureForm
            ' 
            AutoScaleDimensions = New SizeF(7.0F, 15.0F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(153, 256)
            Controls.Add(GroupBox1)
            Controls.Add(CreatureGroupBox)
            FormBorderStyle = FormBorderStyle.None
            Name = "CreatureForm"
            Text = "CreatureFormvb"
            CreatureGroupBox.ResumeLayout(False)
            TableLayoutPanel1.ResumeLayout(False)
            GroupBox1.ResumeLayout(False)
            ResumeLayout(False)
        End Sub

        Friend WithEvents CreatureGroupBox As GroupBox
        Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
        Friend WithEvents MainCreatureButton As Button
        Friend WithEvents SubCreature1Buton As Button
        Friend WithEvents SubCreature3Button As Button
        Friend WithEvents SubCreature2Button As Button
        Friend WithEvents GroupBox1 As GroupBox
        Friend WithEvents ListView1 As ListView
    End Class

End Namespace