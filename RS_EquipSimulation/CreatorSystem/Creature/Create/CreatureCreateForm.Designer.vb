Namespace CreatorSystem.Creature

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class CreatureCreateForm
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
            BasicGroup = New GroupBox()
            NameTextBox = New TextBox()
            NameLabel = New Label()
            RarityGroup = New GroupBox()
            EnhancedRarityComboBox = New ComboBox()
            EnhancedRarityLabel = New Label()
            Label3 = New Label()
            BaseRarityComboBox = New ComboBox()
            BaseRarityLabel = New Label()
            MainPassiveGroup = New GroupBox()
            MainPassiveAwakeningNumeric = New NumericUpDown()
            MainPassiveLevelNumeric = New NumericUpDown()
            Label1 = New Label()
            MainPassiveLevelLabel = New Label()
            MainPassiveNameComboBox = New ComboBox()
            MainPassiveNameLabel = New Label()
            SubPassiveGroup = New GroupBox()
            SubPassive1AwakeningNumeric = New NumericUpDown()
            SubPassive1LevelNumeric = New NumericUpDown()
            Label2 = New Label()
            SubPassiveLevelLabel = New Label()
            SubPassive1NameComboBox = New ComboBox()
            SubPassiveNameLabel = New Label()
            SelectedInitButton = New Button()
            CreatureGenerateButton = New Button()
            GroupBox1 = New GroupBox()
            SubPassive2AwakeningNumeric = New NumericUpDown()
            SubPassive2LevelNumeric = New NumericUpDown()
            Label4 = New Label()
            Label5 = New Label()
            SubPassive2NameComboBox = New ComboBox()
            Label6 = New Label()
            GroupBox2 = New GroupBox()
            SubPassive3AwakeningNumeric = New NumericUpDown()
            SubPassive3LevelNumeric = New NumericUpDown()
            Label7 = New Label()
            Label8 = New Label()
            SubPassive3NameComboBox = New ComboBox()
            Label9 = New Label()
            BasicGroup.SuspendLayout()
            RarityGroup.SuspendLayout()
            MainPassiveGroup.SuspendLayout()
            CType(MainPassiveAwakeningNumeric, ComponentModel.ISupportInitialize).BeginInit()
            CType(MainPassiveLevelNumeric, ComponentModel.ISupportInitialize).BeginInit()
            SubPassiveGroup.SuspendLayout()
            CType(SubPassive1AwakeningNumeric, ComponentModel.ISupportInitialize).BeginInit()
            CType(SubPassive1LevelNumeric, ComponentModel.ISupportInitialize).BeginInit()
            GroupBox1.SuspendLayout()
            CType(SubPassive2AwakeningNumeric, ComponentModel.ISupportInitialize).BeginInit()
            CType(SubPassive2LevelNumeric, ComponentModel.ISupportInitialize).BeginInit()
            GroupBox2.SuspendLayout()
            CType(SubPassive3AwakeningNumeric, ComponentModel.ISupportInitialize).BeginInit()
            CType(SubPassive3LevelNumeric, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' BasicGroup
            ' 
            BasicGroup.Controls.Add(NameTextBox)
            BasicGroup.Controls.Add(NameLabel)
            BasicGroup.Location = New Point(12, 12)
            BasicGroup.Name = "BasicGroup"
            BasicGroup.Size = New Size(220, 60)
            BasicGroup.TabIndex = 0
            BasicGroup.TabStop = False
            BasicGroup.Text = "基本情報"
            ' 
            ' NameTextBox
            ' 
            NameTextBox.Location = New Point(43, 22)
            NameTextBox.Name = "NameTextBox"
            NameTextBox.Size = New Size(171, 23)
            NameTextBox.TabIndex = 1
            ' 
            ' NameLabel
            ' 
            NameLabel.AutoSize = True
            NameLabel.Location = New Point(6, 25)
            NameLabel.Name = "NameLabel"
            NameLabel.Size = New Size(31, 15)
            NameLabel.TabIndex = 0
            NameLabel.Text = "名前"
            ' 
            ' RarityGroup
            ' 
            RarityGroup.Controls.Add(EnhancedRarityComboBox)
            RarityGroup.Controls.Add(EnhancedRarityLabel)
            RarityGroup.Controls.Add(Label3)
            RarityGroup.Controls.Add(BaseRarityComboBox)
            RarityGroup.Controls.Add(BaseRarityLabel)
            RarityGroup.Location = New Point(12, 78)
            RarityGroup.Name = "RarityGroup"
            RarityGroup.Size = New Size(220, 90)
            RarityGroup.TabIndex = 1
            RarityGroup.TabStop = False
            RarityGroup.Text = "レアリティ"
            ' 
            ' EnhancedRarityComboBox
            ' 
            EnhancedRarityComboBox.FormattingEnabled = True
            EnhancedRarityComboBox.Location = New Point(124, 51)
            EnhancedRarityComboBox.Name = "EnhancedRarityComboBox"
            EnhancedRarityComboBox.Size = New Size(69, 23)
            EnhancedRarityComboBox.TabIndex = 1
            ' 
            ' EnhancedRarityLabel
            ' 
            EnhancedRarityLabel.AutoSize = True
            EnhancedRarityLabel.Location = New Point(124, 25)
            EnhancedRarityLabel.Name = "EnhancedRarityLabel"
            EnhancedRarityLabel.Size = New Size(85, 15)
            EnhancedRarityLabel.TabIndex = 0
            EnhancedRarityLabel.Text = "強化後レアリティ"
            ' 
            ' Label3
            ' 
            Label3.AutoSize = True
            Label3.Location = New Point(90, 54)
            Label3.Name = "Label3"
            Label3.Size = New Size(19, 15)
            Label3.TabIndex = 0
            Label3.Text = "→"
            ' 
            ' BaseRarityComboBox
            ' 
            BaseRarityComboBox.FormattingEnabled = True
            BaseRarityComboBox.Location = New Point(6, 51)
            BaseRarityComboBox.Name = "BaseRarityComboBox"
            BaseRarityComboBox.Size = New Size(69, 23)
            BaseRarityComboBox.TabIndex = 1
            ' 
            ' BaseRarityLabel
            ' 
            BaseRarityLabel.AutoSize = True
            BaseRarityLabel.Location = New Point(6, 25)
            BaseRarityLabel.Name = "BaseRarityLabel"
            BaseRarityLabel.Size = New Size(76, 15)
            BaseRarityLabel.TabIndex = 0
            BaseRarityLabel.Text = "ベースレアリティ"
            ' 
            ' MainPassiveGroup
            ' 
            MainPassiveGroup.Controls.Add(MainPassiveAwakeningNumeric)
            MainPassiveGroup.Controls.Add(MainPassiveLevelNumeric)
            MainPassiveGroup.Controls.Add(Label1)
            MainPassiveGroup.Controls.Add(MainPassiveLevelLabel)
            MainPassiveGroup.Controls.Add(MainPassiveNameComboBox)
            MainPassiveGroup.Controls.Add(MainPassiveNameLabel)
            MainPassiveGroup.Location = New Point(12, 174)
            MainPassiveGroup.Name = "MainPassiveGroup"
            MainPassiveGroup.Size = New Size(220, 86)
            MainPassiveGroup.TabIndex = 2
            MainPassiveGroup.TabStop = False
            MainPassiveGroup.Text = "メインパッシブ"
            ' 
            ' MainPassiveAwakeningNumeric
            ' 
            MainPassiveAwakeningNumeric.Location = New Point(154, 51)
            MainPassiveAwakeningNumeric.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
            MainPassiveAwakeningNumeric.Name = "MainPassiveAwakeningNumeric"
            MainPassiveAwakeningNumeric.Size = New Size(60, 23)
            MainPassiveAwakeningNumeric.TabIndex = 2
            ' 
            ' MainPassiveLevelNumeric
            ' 
            MainPassiveLevelNumeric.Location = New Point(67, 51)
            MainPassiveLevelNumeric.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
            MainPassiveLevelNumeric.Name = "MainPassiveLevelNumeric"
            MainPassiveLevelNumeric.ReadOnly = True
            MainPassiveLevelNumeric.Size = New Size(60, 23)
            MainPassiveLevelNumeric.TabIndex = 2
            ' 
            ' Label1
            ' 
            Label1.AutoSize = True
            Label1.Location = New Point(133, 53)
            Label1.Name = "Label1"
            Label1.Size = New Size(15, 15)
            Label1.TabIndex = 0
            Label1.Text = "+"
            ' 
            ' MainPassiveLevelLabel
            ' 
            MainPassiveLevelLabel.AutoSize = True
            MainPassiveLevelLabel.Location = New Point(6, 53)
            MainPassiveLevelLabel.Name = "MainPassiveLevelLabel"
            MainPassiveLevelLabel.Size = New Size(36, 15)
            MainPassiveLevelLabel.TabIndex = 0
            MainPassiveLevelLabel.Text = "レベル"
            ' 
            ' MainPassiveNameComboBox
            ' 
            MainPassiveNameComboBox.FormattingEnabled = True
            MainPassiveNameComboBox.Location = New Point(67, 22)
            MainPassiveNameComboBox.Name = "MainPassiveNameComboBox"
            MainPassiveNameComboBox.Size = New Size(147, 23)
            MainPassiveNameComboBox.TabIndex = 1
            ' 
            ' MainPassiveNameLabel
            ' 
            MainPassiveNameLabel.AutoSize = True
            MainPassiveNameLabel.Location = New Point(6, 25)
            MainPassiveNameLabel.Name = "MainPassiveNameLabel"
            MainPassiveNameLabel.Size = New Size(56, 15)
            MainPassiveNameLabel.TabIndex = 0
            MainPassiveNameLabel.Text = "パッシブ名"
            ' 
            ' SubPassiveGroup
            ' 
            SubPassiveGroup.Controls.Add(SubPassive1AwakeningNumeric)
            SubPassiveGroup.Controls.Add(SubPassive1LevelNumeric)
            SubPassiveGroup.Controls.Add(Label2)
            SubPassiveGroup.Controls.Add(SubPassiveLevelLabel)
            SubPassiveGroup.Controls.Add(SubPassive1NameComboBox)
            SubPassiveGroup.Controls.Add(SubPassiveNameLabel)
            SubPassiveGroup.Location = New Point(12, 266)
            SubPassiveGroup.Name = "SubPassiveGroup"
            SubPassiveGroup.Size = New Size(220, 86)
            SubPassiveGroup.TabIndex = 2
            SubPassiveGroup.TabStop = False
            SubPassiveGroup.Text = "サブパッシブ1"
            ' 
            ' SubPassive1AwakeningNumeric
            ' 
            SubPassive1AwakeningNumeric.Location = New Point(154, 51)
            SubPassive1AwakeningNumeric.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
            SubPassive1AwakeningNumeric.Name = "SubPassive1AwakeningNumeric"
            SubPassive1AwakeningNumeric.Size = New Size(60, 23)
            SubPassive1AwakeningNumeric.TabIndex = 2
            ' 
            ' SubPassive1LevelNumeric
            ' 
            SubPassive1LevelNumeric.Location = New Point(67, 51)
            SubPassive1LevelNumeric.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
            SubPassive1LevelNumeric.Name = "SubPassive1LevelNumeric"
            SubPassive1LevelNumeric.ReadOnly = True
            SubPassive1LevelNumeric.Size = New Size(60, 23)
            SubPassive1LevelNumeric.TabIndex = 2
            ' 
            ' Label2
            ' 
            Label2.AutoSize = True
            Label2.Location = New Point(133, 53)
            Label2.Name = "Label2"
            Label2.Size = New Size(15, 15)
            Label2.TabIndex = 0
            Label2.Text = "+"
            ' 
            ' SubPassiveLevelLabel
            ' 
            SubPassiveLevelLabel.AutoSize = True
            SubPassiveLevelLabel.Location = New Point(6, 53)
            SubPassiveLevelLabel.Name = "SubPassiveLevelLabel"
            SubPassiveLevelLabel.Size = New Size(36, 15)
            SubPassiveLevelLabel.TabIndex = 0
            SubPassiveLevelLabel.Text = "レベル"
            ' 
            ' SubPassive1NameComboBox
            ' 
            SubPassive1NameComboBox.FormattingEnabled = True
            SubPassive1NameComboBox.Location = New Point(67, 22)
            SubPassive1NameComboBox.Name = "SubPassive1NameComboBox"
            SubPassive1NameComboBox.Size = New Size(147, 23)
            SubPassive1NameComboBox.TabIndex = 1
            ' 
            ' SubPassiveNameLabel
            ' 
            SubPassiveNameLabel.AutoSize = True
            SubPassiveNameLabel.Location = New Point(6, 25)
            SubPassiveNameLabel.Name = "SubPassiveNameLabel"
            SubPassiveNameLabel.Size = New Size(56, 15)
            SubPassiveNameLabel.TabIndex = 0
            SubPassiveNameLabel.Text = "パッシブ名"
            ' 
            ' SelectedInitButton
            ' 
            SelectedInitButton.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            SelectedInitButton.Location = New Point(12, 547)
            SelectedInitButton.Name = "SelectedInitButton"
            SelectedInitButton.Size = New Size(75, 23)
            SelectedInitButton.TabIndex = 3
            SelectedInitButton.Text = "初期化"
            SelectedInitButton.UseVisualStyleBackColor = True
            ' 
            ' CreatureGenerateButton
            ' 
            CreatureGenerateButton.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            CreatureGenerateButton.Font = New Font("Yu Gothic UI", 10F, FontStyle.Bold)
            CreatureGenerateButton.Location = New Point(157, 544)
            CreatureGenerateButton.Name = "CreatureGenerateButton"
            CreatureGenerateButton.Size = New Size(75, 27)
            CreatureGenerateButton.TabIndex = 3
            CreatureGenerateButton.Text = "生成！"
            CreatureGenerateButton.UseVisualStyleBackColor = True
            ' 
            ' GroupBox1
            ' 
            GroupBox1.Controls.Add(SubPassive2AwakeningNumeric)
            GroupBox1.Controls.Add(SubPassive2LevelNumeric)
            GroupBox1.Controls.Add(Label4)
            GroupBox1.Controls.Add(Label5)
            GroupBox1.Controls.Add(SubPassive2NameComboBox)
            GroupBox1.Controls.Add(Label6)
            GroupBox1.Location = New Point(12, 358)
            GroupBox1.Name = "GroupBox1"
            GroupBox1.Size = New Size(220, 86)
            GroupBox1.TabIndex = 2
            GroupBox1.TabStop = False
            GroupBox1.Text = "サブパッシブ2"
            ' 
            ' SubPassive2AwakeningNumeric
            ' 
            SubPassive2AwakeningNumeric.Location = New Point(154, 51)
            SubPassive2AwakeningNumeric.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
            SubPassive2AwakeningNumeric.Name = "SubPassive2AwakeningNumeric"
            SubPassive2AwakeningNumeric.Size = New Size(60, 23)
            SubPassive2AwakeningNumeric.TabIndex = 2
            ' 
            ' SubPassive2LevelNumeric
            ' 
            SubPassive2LevelNumeric.Location = New Point(67, 51)
            SubPassive2LevelNumeric.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
            SubPassive2LevelNumeric.Name = "SubPassive2LevelNumeric"
            SubPassive2LevelNumeric.ReadOnly = True
            SubPassive2LevelNumeric.Size = New Size(60, 23)
            SubPassive2LevelNumeric.TabIndex = 2
            ' 
            ' Label4
            ' 
            Label4.AutoSize = True
            Label4.Location = New Point(133, 53)
            Label4.Name = "Label4"
            Label4.Size = New Size(15, 15)
            Label4.TabIndex = 0
            Label4.Text = "+"
            ' 
            ' Label5
            ' 
            Label5.AutoSize = True
            Label5.Location = New Point(6, 53)
            Label5.Name = "Label5"
            Label5.Size = New Size(36, 15)
            Label5.TabIndex = 0
            Label5.Text = "レベル"
            ' 
            ' SubPassive2NameComboBox
            ' 
            SubPassive2NameComboBox.FormattingEnabled = True
            SubPassive2NameComboBox.Location = New Point(67, 22)
            SubPassive2NameComboBox.Name = "SubPassive2NameComboBox"
            SubPassive2NameComboBox.Size = New Size(147, 23)
            SubPassive2NameComboBox.TabIndex = 1
            ' 
            ' Label6
            ' 
            Label6.AutoSize = True
            Label6.Location = New Point(6, 25)
            Label6.Name = "Label6"
            Label6.Size = New Size(56, 15)
            Label6.TabIndex = 0
            Label6.Text = "パッシブ名"
            ' 
            ' GroupBox2
            ' 
            GroupBox2.Controls.Add(SubPassive3AwakeningNumeric)
            GroupBox2.Controls.Add(SubPassive3LevelNumeric)
            GroupBox2.Controls.Add(Label7)
            GroupBox2.Controls.Add(Label8)
            GroupBox2.Controls.Add(SubPassive3NameComboBox)
            GroupBox2.Controls.Add(Label9)
            GroupBox2.Location = New Point(12, 450)
            GroupBox2.Name = "GroupBox2"
            GroupBox2.Size = New Size(220, 86)
            GroupBox2.TabIndex = 2
            GroupBox2.TabStop = False
            GroupBox2.Text = "サブパッシブ3"
            ' 
            ' SubPassive3AwakeningNumeric
            ' 
            SubPassive3AwakeningNumeric.Location = New Point(154, 51)
            SubPassive3AwakeningNumeric.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
            SubPassive3AwakeningNumeric.Name = "SubPassive3AwakeningNumeric"
            SubPassive3AwakeningNumeric.Size = New Size(60, 23)
            SubPassive3AwakeningNumeric.TabIndex = 2
            ' 
            ' SubPassive3LevelNumeric
            ' 
            SubPassive3LevelNumeric.Location = New Point(67, 51)
            SubPassive3LevelNumeric.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
            SubPassive3LevelNumeric.Name = "SubPassive3LevelNumeric"
            SubPassive3LevelNumeric.ReadOnly = True
            SubPassive3LevelNumeric.Size = New Size(60, 23)
            SubPassive3LevelNumeric.TabIndex = 2
            ' 
            ' Label7
            ' 
            Label7.AutoSize = True
            Label7.Location = New Point(133, 53)
            Label7.Name = "Label7"
            Label7.Size = New Size(15, 15)
            Label7.TabIndex = 0
            Label7.Text = "+"
            ' 
            ' Label8
            ' 
            Label8.AutoSize = True
            Label8.Location = New Point(6, 53)
            Label8.Name = "Label8"
            Label8.Size = New Size(36, 15)
            Label8.TabIndex = 0
            Label8.Text = "レベル"
            ' 
            ' SubPassive3NameComboBox
            ' 
            SubPassive3NameComboBox.FormattingEnabled = True
            SubPassive3NameComboBox.Location = New Point(67, 22)
            SubPassive3NameComboBox.Name = "SubPassive3NameComboBox"
            SubPassive3NameComboBox.Size = New Size(147, 23)
            SubPassive3NameComboBox.TabIndex = 1
            ' 
            ' Label9
            ' 
            Label9.AutoSize = True
            Label9.Location = New Point(6, 25)
            Label9.Name = "Label9"
            Label9.Size = New Size(56, 15)
            Label9.TabIndex = 0
            Label9.Text = "パッシブ名"
            ' 
            ' CreatureCreateForm
            ' 
            AutoScaleDimensions = New SizeF(7F, 15F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(244, 582)
            Controls.Add(CreatureGenerateButton)
            Controls.Add(SelectedInitButton)
            Controls.Add(GroupBox2)
            Controls.Add(GroupBox1)
            Controls.Add(SubPassiveGroup)
            Controls.Add(MainPassiveGroup)
            Controls.Add(RarityGroup)
            Controls.Add(BasicGroup)
            Name = "CreatureCreateForm"
            StartPosition = FormStartPosition.CenterParent
            Text = "クリーチャー生成"
            BasicGroup.ResumeLayout(False)
            BasicGroup.PerformLayout()
            RarityGroup.ResumeLayout(False)
            RarityGroup.PerformLayout()
            MainPassiveGroup.ResumeLayout(False)
            MainPassiveGroup.PerformLayout()
            CType(MainPassiveAwakeningNumeric, ComponentModel.ISupportInitialize).EndInit()
            CType(MainPassiveLevelNumeric, ComponentModel.ISupportInitialize).EndInit()
            SubPassiveGroup.ResumeLayout(False)
            SubPassiveGroup.PerformLayout()
            CType(SubPassive1AwakeningNumeric, ComponentModel.ISupportInitialize).EndInit()
            CType(SubPassive1LevelNumeric, ComponentModel.ISupportInitialize).EndInit()
            GroupBox1.ResumeLayout(False)
            GroupBox1.PerformLayout()
            CType(SubPassive2AwakeningNumeric, ComponentModel.ISupportInitialize).EndInit()
            CType(SubPassive2LevelNumeric, ComponentModel.ISupportInitialize).EndInit()
            GroupBox2.ResumeLayout(False)
            GroupBox2.PerformLayout()
            CType(SubPassive3AwakeningNumeric, ComponentModel.ISupportInitialize).EndInit()
            CType(SubPassive3LevelNumeric, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
        End Sub

        Friend WithEvents BasicGroup As GroupBox
        Friend WithEvents NameTextBox As TextBox
        Friend WithEvents NameLabel As Label
        Friend WithEvents RarityGroup As GroupBox
        Friend WithEvents EnhancedRarityComboBox As ComboBox
        Friend WithEvents EnhancedRarityLabel As Label
        Friend WithEvents BaseRarityComboBox As ComboBox
        Friend WithEvents BaseRarityLabel As Label
        Friend WithEvents MainPassiveGroup As GroupBox
        Friend WithEvents MainPassiveAwakeningNumeric As NumericUpDown
        Friend WithEvents MainPassiveLevelNumeric As NumericUpDown
        Friend WithEvents MainPassiveLevelLabel As Label
        Friend WithEvents MainPassiveNameComboBox As ComboBox
        Friend WithEvents MainPassiveNameLabel As Label
        Friend WithEvents SubPassiveGroup As GroupBox
        Friend WithEvents SubPassive1AwakeningNumeric As NumericUpDown
        Friend WithEvents SubPassive1LevelNumeric As NumericUpDown
        Friend WithEvents SubPassiveLevelLabel As Label
        Friend WithEvents SubPassive1NameComboBox As ComboBox
        Friend WithEvents SubPassiveNameLabel As Label
        Friend WithEvents SelectedInitButton As Button
        Friend WithEvents CreatureGenerateButton As Button
        Friend WithEvents Label1 As Label
        Friend WithEvents Label2 As Label
        Friend WithEvents Label3 As Label
        Friend WithEvents GroupBox1 As GroupBox
        Friend WithEvents SubPassive2AwakeningNumeric As NumericUpDown
        Friend WithEvents SubPassive2LevelNumeric As NumericUpDown
        Friend WithEvents Label4 As Label
        Friend WithEvents Label5 As Label
        Friend WithEvents SubPassive2NameComboBox As ComboBox
        Friend WithEvents Label6 As Label
        Friend WithEvents GroupBox2 As GroupBox
        Friend WithEvents SubPassive3AwakeningNumeric As NumericUpDown
        Friend WithEvents SubPassive3LevelNumeric As NumericUpDown
        Friend WithEvents Label7 As Label
        Friend WithEvents Label8 As Label
        Friend WithEvents SubPassive3NameComboBox As ComboBox
        Friend WithEvents Label9 As Label
    End Class

End Namespace