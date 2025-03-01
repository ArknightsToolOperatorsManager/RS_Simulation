Namespace CreatorSystem.Equipment

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Public Class EquipmentCreateForm
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
            PartFilterSelectBox = New ComboBox()
            Label1 = New Label()
            BaseItemGroup = New GroupBox()
            BaseItemSelectBox = New ComboBox()
            Label2 = New Label()
            NxGroup = New GroupBox()
            Nx4ValueTextBox = New TextBox()
            Nx3ValueTextBox = New TextBox()
            Nx2ValueTextBox = New TextBox()
            Nx1ValueTextBox = New TextBox()
            Nx4SelectBox = New ComboBox()
            Label8 = New Label()
            Nx3SelectBox = New ComboBox()
            Label7 = New Label()
            Nx2SelectBox = New ComboBox()
            Label6 = New Label()
            Nx1SelectBox = New ComboBox()
            Label5 = New Label()
            Label3 = New Label()
            Label4 = New Label()
            SmeltGroup = New GroupBox()
            SmeltStepBox = New NumericUpDown()
            Smelt5OpSelectBox = New ComboBox()
            Smelt3OpSelectBox = New ComboBox()
            Label14 = New Label()
            Label10 = New Label()
            Smelt4OpSelectBox = New ComboBox()
            Label13 = New Label()
            Smelt2OpSelectBox = New ComboBox()
            Label11 = New Label()
            Smelt1OpSelectBox = New ComboBox()
            EquipRankLabel = New Label()
            Label18 = New Label()
            Label9 = New Label()
            Label12 = New Label()
            SelectedInitButton = New Button()
            EquipGenerateButton = New Button()
            MainTabControl = New TabControl()
            GenerateTab = New TabPage()
            PrefixGroup = New GroupBox()
            Prefix3SelectBox = New ComboBox()
            Prefix3RankSelectBox = New ComboBox()
            Prefix3CategorySelectBox = New ComboBox()
            Prefix2RankSelectBox = New ComboBox()
            Prefix2SelectBox = New ComboBox()
            Prefix2CategorySelectBox = New ComboBox()
            Prefix1RankSelectBox = New ComboBox()
            Prefix1SelectBox = New ComboBox()
            Prefix1CategorySelectBox = New ComboBox()
            Label15 = New Label()
            Label16 = New Label()
            Label17 = New Label()
            SearchTab = New TabPage()
            SearchGroup = New GroupBox()
            SearchPartLabel = New Label()
            SearchPartComboBox = New ComboBox()
            OptionTypeLabel = New Label()
            OptionNameLabel = New Label()
            OptionTypeListBox = New CheckedListBox()
            OptionNameComboBox = New ComboBox()
            SearchResultListView = New ListView()
            BaseItemGroup.SuspendLayout()
            NxGroup.SuspendLayout()
            SmeltGroup.SuspendLayout()
            CType(SmeltStepBox, ComponentModel.ISupportInitialize).BeginInit()
            MainTabControl.SuspendLayout()
            GenerateTab.SuspendLayout()
            PrefixGroup.SuspendLayout()
            SearchTab.SuspendLayout()
            SearchGroup.SuspendLayout()
            SuspendLayout()
            ' 
            ' PartFilterSelectBox
            ' 
            PartFilterSelectBox.FormattingEnabled = True
            PartFilterSelectBox.Location = New Point(6, 37)
            PartFilterSelectBox.Name = "PartFilterSelectBox"
            PartFilterSelectBox.Size = New Size(65, 23)
            PartFilterSelectBox.TabIndex = 0
            ' 
            ' Label1
            ' 
            Label1.AutoSize = True
            Label1.Location = New Point(6, 19)
            Label1.Name = "Label1"
            Label1.Size = New Size(55, 15)
            Label1.TabIndex = 1
            Label1.Text = "部位選択"
            ' 
            ' BaseItemGroup
            ' 
            BaseItemGroup.Controls.Add(BaseItemSelectBox)
            BaseItemGroup.Controls.Add(PartFilterSelectBox)
            BaseItemGroup.Controls.Add(Label2)
            BaseItemGroup.Controls.Add(Label1)
            BaseItemGroup.Location = New Point(9, 6)
            BaseItemGroup.Name = "BaseItemGroup"
            BaseItemGroup.Size = New Size(217, 67)
            BaseItemGroup.TabIndex = 2
            BaseItemGroup.TabStop = False
            BaseItemGroup.Text = "ベース選択"
            ' 
            ' BaseItemSelectBox
            ' 
            BaseItemSelectBox.FormattingEnabled = True
            BaseItemSelectBox.Location = New Point(77, 37)
            BaseItemSelectBox.Name = "BaseItemSelectBox"
            BaseItemSelectBox.Size = New Size(134, 23)
            BaseItemSelectBox.TabIndex = 0
            ' 
            ' Label2
            ' 
            Label2.AutoSize = True
            Label2.Location = New Point(77, 19)
            Label2.Name = "Label2"
            Label2.Size = New Size(67, 15)
            Label2.TabIndex = 1
            Label2.Text = "アイテム選択"
            ' 
            ' NxGroup
            ' 
            NxGroup.Controls.Add(Nx4ValueTextBox)
            NxGroup.Controls.Add(Nx3ValueTextBox)
            NxGroup.Controls.Add(Nx2ValueTextBox)
            NxGroup.Controls.Add(Nx1ValueTextBox)
            NxGroup.Controls.Add(Nx4SelectBox)
            NxGroup.Controls.Add(Label8)
            NxGroup.Controls.Add(Nx3SelectBox)
            NxGroup.Controls.Add(Label7)
            NxGroup.Controls.Add(Nx2SelectBox)
            NxGroup.Controls.Add(Label6)
            NxGroup.Controls.Add(Nx1SelectBox)
            NxGroup.Controls.Add(Label5)
            NxGroup.Controls.Add(Label3)
            NxGroup.Controls.Add(Label4)
            NxGroup.Location = New Point(9, 216)
            NxGroup.Name = "NxGroup"
            NxGroup.Size = New Size(217, 157)
            NxGroup.TabIndex = 2
            NxGroup.TabStop = False
            NxGroup.Text = "錬成"
            ' 
            ' Nx4ValueTextBox
            ' 
            Nx4ValueTextBox.Location = New Point(174, 124)
            Nx4ValueTextBox.Name = "Nx4ValueTextBox"
            Nx4ValueTextBox.Size = New Size(37, 23)
            Nx4ValueTextBox.TabIndex = 3
            ' 
            ' Nx3ValueTextBox
            ' 
            Nx3ValueTextBox.Location = New Point(174, 95)
            Nx3ValueTextBox.Name = "Nx3ValueTextBox"
            Nx3ValueTextBox.Size = New Size(37, 23)
            Nx3ValueTextBox.TabIndex = 3
            ' 
            ' Nx2ValueTextBox
            ' 
            Nx2ValueTextBox.Location = New Point(174, 66)
            Nx2ValueTextBox.Name = "Nx2ValueTextBox"
            Nx2ValueTextBox.Size = New Size(37, 23)
            Nx2ValueTextBox.TabIndex = 3
            ' 
            ' Nx1ValueTextBox
            ' 
            Nx1ValueTextBox.Location = New Point(174, 37)
            Nx1ValueTextBox.Name = "Nx1ValueTextBox"
            Nx1ValueTextBox.Size = New Size(37, 23)
            Nx1ValueTextBox.TabIndex = 3
            ' 
            ' Nx4SelectBox
            ' 
            Nx4SelectBox.FormattingEnabled = True
            Nx4SelectBox.Location = New Point(40, 124)
            Nx4SelectBox.Name = "Nx4SelectBox"
            Nx4SelectBox.Size = New Size(128, 23)
            Nx4SelectBox.TabIndex = 0
            ' 
            ' Label8
            ' 
            Label8.AutoSize = True
            Label8.Location = New Point(6, 127)
            Label8.Name = "Label8"
            Label8.Size = New Size(28, 15)
            Label8.TabIndex = 1
            Label8.Text = "Nx4"
            ' 
            ' Nx3SelectBox
            ' 
            Nx3SelectBox.FormattingEnabled = True
            Nx3SelectBox.Location = New Point(40, 95)
            Nx3SelectBox.Name = "Nx3SelectBox"
            Nx3SelectBox.Size = New Size(128, 23)
            Nx3SelectBox.TabIndex = 0
            ' 
            ' Label7
            ' 
            Label7.AutoSize = True
            Label7.Location = New Point(6, 98)
            Label7.Name = "Label7"
            Label7.Size = New Size(28, 15)
            Label7.TabIndex = 1
            Label7.Text = "Nx3"
            ' 
            ' Nx2SelectBox
            ' 
            Nx2SelectBox.FormattingEnabled = True
            Nx2SelectBox.Location = New Point(40, 66)
            Nx2SelectBox.Name = "Nx2SelectBox"
            Nx2SelectBox.Size = New Size(128, 23)
            Nx2SelectBox.TabIndex = 0
            ' 
            ' Label6
            ' 
            Label6.AutoSize = True
            Label6.Location = New Point(6, 69)
            Label6.Name = "Label6"
            Label6.Size = New Size(28, 15)
            Label6.TabIndex = 1
            Label6.Text = "Nx2"
            ' 
            ' Nx1SelectBox
            ' 
            Nx1SelectBox.FormattingEnabled = True
            Nx1SelectBox.Location = New Point(40, 37)
            Nx1SelectBox.Name = "Nx1SelectBox"
            Nx1SelectBox.Size = New Size(128, 23)
            Nx1SelectBox.TabIndex = 0
            ' 
            ' Label5
            ' 
            Label5.AutoSize = True
            Label5.Location = New Point(6, 40)
            Label5.Name = "Label5"
            Label5.Size = New Size(28, 15)
            Label5.TabIndex = 1
            Label5.Text = "Nx1"
            ' 
            ' Label3
            ' 
            Label3.AutoSize = True
            Label3.Location = New Point(174, 19)
            Label3.Name = "Label3"
            Label3.Size = New Size(31, 15)
            Label3.TabIndex = 1
            Label3.Text = "数値"
            ' 
            ' Label4
            ' 
            Label4.AutoSize = True
            Label4.Location = New Point(40, 19)
            Label4.Name = "Label4"
            Label4.Size = New Size(55, 15)
            Label4.TabIndex = 1
            Label4.Text = "部位選択"
            ' 
            ' SmeltGroup
            ' 
            SmeltGroup.Controls.Add(SmeltStepBox)
            SmeltGroup.Controls.Add(Smelt5OpSelectBox)
            SmeltGroup.Controls.Add(Smelt3OpSelectBox)
            SmeltGroup.Controls.Add(Label14)
            SmeltGroup.Controls.Add(Label10)
            SmeltGroup.Controls.Add(Smelt4OpSelectBox)
            SmeltGroup.Controls.Add(Label13)
            SmeltGroup.Controls.Add(Smelt2OpSelectBox)
            SmeltGroup.Controls.Add(Label11)
            SmeltGroup.Controls.Add(Smelt1OpSelectBox)
            SmeltGroup.Controls.Add(EquipRankLabel)
            SmeltGroup.Controls.Add(Label18)
            SmeltGroup.Controls.Add(Label9)
            SmeltGroup.Controls.Add(Label12)
            SmeltGroup.Location = New Point(9, 379)
            SmeltGroup.Name = "SmeltGroup"
            SmeltGroup.Size = New Size(217, 202)
            SmeltGroup.TabIndex = 2
            SmeltGroup.TabStop = False
            SmeltGroup.Text = "精錬"
            ' 
            ' SmeltStepBox
            ' 
            SmeltStepBox.Location = New Point(67, 23)
            SmeltStepBox.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
            SmeltStepBox.Name = "SmeltStepBox"
            SmeltStepBox.Size = New Size(42, 23)
            SmeltStepBox.TabIndex = 4
            ' 
            ' Smelt5OpSelectBox
            ' 
            Smelt5OpSelectBox.FormattingEnabled = True
            Smelt5OpSelectBox.Location = New Point(67, 166)
            Smelt5OpSelectBox.Name = "Smelt5OpSelectBox"
            Smelt5OpSelectBox.Size = New Size(144, 23)
            Smelt5OpSelectBox.TabIndex = 0
            ' 
            ' Smelt3OpSelectBox
            ' 
            Smelt3OpSelectBox.FormattingEnabled = True
            Smelt3OpSelectBox.Location = New Point(67, 108)
            Smelt3OpSelectBox.Name = "Smelt3OpSelectBox"
            Smelt3OpSelectBox.Size = New Size(144, 23)
            Smelt3OpSelectBox.TabIndex = 0
            ' 
            ' Label14
            ' 
            Label14.AutoSize = True
            Label14.Location = New Point(6, 169)
            Label14.Name = "Label14"
            Label14.Size = New Size(55, 15)
            Label14.TabIndex = 1
            Label14.Text = "18段階～"
            ' 
            ' Label10
            ' 
            Label10.AutoSize = True
            Label10.Location = New Point(6, 111)
            Label10.Name = "Label10"
            Label10.Size = New Size(55, 15)
            Label10.TabIndex = 1
            Label10.Text = "10段階～"
            ' 
            ' Smelt4OpSelectBox
            ' 
            Smelt4OpSelectBox.FormattingEnabled = True
            Smelt4OpSelectBox.Location = New Point(67, 137)
            Smelt4OpSelectBox.Name = "Smelt4OpSelectBox"
            Smelt4OpSelectBox.Size = New Size(144, 23)
            Smelt4OpSelectBox.TabIndex = 0
            ' 
            ' Label13
            ' 
            Label13.AutoSize = True
            Label13.Location = New Point(6, 140)
            Label13.Name = "Label13"
            Label13.Size = New Size(55, 15)
            Label13.TabIndex = 1
            Label13.Text = "15段階～"
            ' 
            ' Smelt2OpSelectBox
            ' 
            Smelt2OpSelectBox.FormattingEnabled = True
            Smelt2OpSelectBox.Location = New Point(67, 79)
            Smelt2OpSelectBox.Name = "Smelt2OpSelectBox"
            Smelt2OpSelectBox.Size = New Size(144, 23)
            Smelt2OpSelectBox.TabIndex = 0
            ' 
            ' Label11
            ' 
            Label11.AutoSize = True
            Label11.Location = New Point(6, 82)
            Label11.Name = "Label11"
            Label11.Size = New Size(49, 15)
            Label11.TabIndex = 1
            Label11.Text = "5段階～"
            ' 
            ' Smelt1OpSelectBox
            ' 
            Smelt1OpSelectBox.FormattingEnabled = True
            Smelt1OpSelectBox.Location = New Point(67, 50)
            Smelt1OpSelectBox.Name = "Smelt1OpSelectBox"
            Smelt1OpSelectBox.Size = New Size(144, 23)
            Smelt1OpSelectBox.TabIndex = 0
            ' 
            ' EquipRankLabel
            ' 
            EquipRankLabel.AutoSize = True
            EquipRankLabel.Location = New Point(177, 25)
            EquipRankLabel.Name = "EquipRankLabel"
            EquipRankLabel.Size = New Size(34, 15)
            EquipRankLabel.TabIndex = 1
            EquipRankLabel.Text = "UMU"
            ' 
            ' Label18
            ' 
            Label18.AutoSize = True
            Label18.Location = New Point(6, 25)
            Label18.Name = "Label18"
            Label18.Size = New Size(42, 15)
            Label18.TabIndex = 1
            Label18.Text = "ステップ"
            ' 
            ' Label9
            ' 
            Label9.AutoSize = True
            Label9.Location = New Point(138, 25)
            Label9.Name = "Label9"
            Label9.Size = New Size(45, 15)
            Label9.TabIndex = 1
            Label9.Text = "ランク："
            ' 
            ' Label12
            ' 
            Label12.AutoSize = True
            Label12.Location = New Point(6, 53)
            Label12.Name = "Label12"
            Label12.Size = New Size(49, 15)
            Label12.TabIndex = 1
            Label12.Text = "1段階～"
            ' 
            ' SelectedInitButton
            ' 
            SelectedInitButton.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            SelectedInitButton.Location = New Point(9, 590)
            SelectedInitButton.Name = "SelectedInitButton"
            SelectedInitButton.Size = New Size(75, 23)
            SelectedInitButton.TabIndex = 3
            SelectedInitButton.Text = "初期化"
            SelectedInitButton.UseVisualStyleBackColor = True
            ' 
            ' EquipGenerateButton
            ' 
            EquipGenerateButton.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            EquipGenerateButton.Font = New Font("Yu Gothic UI", 10.0F, FontStyle.Bold)
            EquipGenerateButton.Location = New Point(151, 587)
            EquipGenerateButton.Name = "EquipGenerateButton"
            EquipGenerateButton.Size = New Size(75, 27)
            EquipGenerateButton.TabIndex = 3
            EquipGenerateButton.Text = "生成！"
            EquipGenerateButton.UseVisualStyleBackColor = True
            ' 
            ' MainTabControl
            ' 
            MainTabControl.Controls.Add(GenerateTab)
            MainTabControl.Controls.Add(SearchTab)
            MainTabControl.Dock = DockStyle.Fill
            MainTabControl.Location = New Point(0, 0)
            MainTabControl.Name = "MainTabControl"
            MainTabControl.SelectedIndex = 0
            MainTabControl.Size = New Size(242, 650)
            MainTabControl.TabIndex = 0
            ' 
            ' GenerateTab
            ' 
            GenerateTab.Controls.Add(EquipGenerateButton)
            GenerateTab.Controls.Add(PrefixGroup)
            GenerateTab.Controls.Add(BaseItemGroup)
            GenerateTab.Controls.Add(SelectedInitButton)
            GenerateTab.Controls.Add(NxGroup)
            GenerateTab.Controls.Add(SmeltGroup)
            GenerateTab.Location = New Point(4, 24)
            GenerateTab.Name = "GenerateTab"
            GenerateTab.Padding = New Padding(3)
            GenerateTab.Size = New Size(234, 622)
            GenerateTab.TabIndex = 0
            GenerateTab.Text = "装備生成"
            GenerateTab.UseVisualStyleBackColor = True
            ' 
            ' PrefixGroup
            ' 
            PrefixGroup.Controls.Add(Prefix3SelectBox)
            PrefixGroup.Controls.Add(Prefix3RankSelectBox)
            PrefixGroup.Controls.Add(Prefix3CategorySelectBox)
            PrefixGroup.Controls.Add(Prefix2RankSelectBox)
            PrefixGroup.Controls.Add(Prefix2SelectBox)
            PrefixGroup.Controls.Add(Prefix2CategorySelectBox)
            PrefixGroup.Controls.Add(Prefix1RankSelectBox)
            PrefixGroup.Controls.Add(Prefix1SelectBox)
            PrefixGroup.Controls.Add(Prefix1CategorySelectBox)
            PrefixGroup.Controls.Add(Label15)
            PrefixGroup.Controls.Add(Label16)
            PrefixGroup.Controls.Add(Label17)
            PrefixGroup.Location = New Point(9, 79)
            PrefixGroup.Name = "PrefixGroup"
            PrefixGroup.Size = New Size(217, 128)
            PrefixGroup.TabIndex = 2
            PrefixGroup.TabStop = False
            PrefixGroup.Text = "称号"
            ' 
            ' Prefix3SelectBox
            ' 
            Prefix3SelectBox.FormattingEnabled = True
            Prefix3SelectBox.Location = New Point(61, 95)
            Prefix3SelectBox.Name = "Prefix3SelectBox"
            Prefix3SelectBox.Size = New Size(107, 23)
            Prefix3SelectBox.TabIndex = 0
            ' 
            ' Prefix3RankSelectBox
            ' 
            Prefix3RankSelectBox.FormattingEnabled = True
            Prefix3RankSelectBox.Location = New Point(174, 95)
            Prefix3RankSelectBox.Name = "Prefix3RankSelectBox"
            Prefix3RankSelectBox.Size = New Size(37, 23)
            Prefix3RankSelectBox.TabIndex = 0
            ' 
            ' Prefix3CategorySelectBox
            ' 
            Prefix3CategorySelectBox.FormattingEnabled = True
            Prefix3CategorySelectBox.Location = New Point(6, 95)
            Prefix3CategorySelectBox.Name = "Prefix3CategorySelectBox"
            Prefix3CategorySelectBox.Size = New Size(49, 23)
            Prefix3CategorySelectBox.TabIndex = 0
            ' 
            ' Prefix2RankSelectBox
            ' 
            Prefix2RankSelectBox.FormattingEnabled = True
            Prefix2RankSelectBox.Location = New Point(174, 66)
            Prefix2RankSelectBox.Name = "Prefix2RankSelectBox"
            Prefix2RankSelectBox.Size = New Size(37, 23)
            Prefix2RankSelectBox.TabIndex = 0
            ' 
            ' Prefix2SelectBox
            ' 
            Prefix2SelectBox.FormattingEnabled = True
            Prefix2SelectBox.Location = New Point(61, 66)
            Prefix2SelectBox.Name = "Prefix2SelectBox"
            Prefix2SelectBox.Size = New Size(107, 23)
            Prefix2SelectBox.TabIndex = 0
            ' 
            ' Prefix2CategorySelectBox
            ' 
            Prefix2CategorySelectBox.FormattingEnabled = True
            Prefix2CategorySelectBox.Location = New Point(6, 66)
            Prefix2CategorySelectBox.Name = "Prefix2CategorySelectBox"
            Prefix2CategorySelectBox.Size = New Size(49, 23)
            Prefix2CategorySelectBox.TabIndex = 0
            ' 
            ' Prefix1RankSelectBox
            ' 
            Prefix1RankSelectBox.FormattingEnabled = True
            Prefix1RankSelectBox.Location = New Point(174, 37)
            Prefix1RankSelectBox.Name = "Prefix1RankSelectBox"
            Prefix1RankSelectBox.Size = New Size(37, 23)
            Prefix1RankSelectBox.TabIndex = 0
            ' 
            ' Prefix1SelectBox
            ' 
            Prefix1SelectBox.FormattingEnabled = True
            Prefix1SelectBox.Location = New Point(61, 37)
            Prefix1SelectBox.Name = "Prefix1SelectBox"
            Prefix1SelectBox.Size = New Size(107, 23)
            Prefix1SelectBox.TabIndex = 0
            ' 
            ' Prefix1CategorySelectBox
            ' 
            Prefix1CategorySelectBox.FormattingEnabled = True
            Prefix1CategorySelectBox.Location = New Point(6, 37)
            Prefix1CategorySelectBox.Name = "Prefix1CategorySelectBox"
            Prefix1CategorySelectBox.Size = New Size(49, 23)
            Prefix1CategorySelectBox.TabIndex = 0
            ' 
            ' Label15
            ' 
            Label15.AutoSize = True
            Label15.Location = New Point(61, 19)
            Label15.Name = "Label15"
            Label15.Size = New Size(55, 15)
            Label15.TabIndex = 1
            Label15.Text = "称号選択"
            ' 
            ' Label16
            ' 
            Label16.AutoSize = True
            Label16.Location = New Point(6, 19)
            Label16.Name = "Label16"
            Label16.Size = New Size(31, 15)
            Label16.TabIndex = 1
            Label16.Text = "系統"
            ' 
            ' Label17
            ' 
            Label17.AutoSize = True
            Label17.Location = New Point(174, 19)
            Label17.Name = "Label17"
            Label17.Size = New Size(33, 15)
            Label17.TabIndex = 1
            Label17.Text = "ランク"
            ' 
            ' SearchTab
            ' 
            SearchTab.Controls.Add(SearchGroup)
            SearchTab.Controls.Add(SearchResultListView)
            SearchTab.Location = New Point(4, 24)
            SearchTab.Name = "SearchTab"
            SearchTab.Padding = New Padding(3)
            SearchTab.Size = New Size(234, 622)
            SearchTab.TabIndex = 1
            SearchTab.Text = "装備検索"
            SearchTab.UseVisualStyleBackColor = True
            ' 
            ' SearchGroup
            ' 
            SearchGroup.Controls.Add(SearchPartLabel)
            SearchGroup.Controls.Add(SearchPartComboBox)
            SearchGroup.Controls.Add(OptionTypeLabel)
            SearchGroup.Controls.Add(OptionNameLabel)
            SearchGroup.Controls.Add(OptionTypeListBox)
            SearchGroup.Controls.Add(OptionNameComboBox)
            SearchGroup.Location = New Point(6, 6)
            SearchGroup.Name = "SearchGroup"
            SearchGroup.Size = New Size(222, 157)
            SearchGroup.TabIndex = 0
            SearchGroup.TabStop = False
            SearchGroup.Text = "検索条件"
            ' 
            ' SearchPartLabel
            ' 
            SearchPartLabel.AutoSize = True
            SearchPartLabel.Location = New Point(6, 20)
            SearchPartLabel.Name = "SearchPartLabel"
            SearchPartLabel.Size = New Size(79, 15)
            SearchPartLabel.TabIndex = 0
            SearchPartLabel.Text = "部位（任意）"
            ' 
            ' SearchPartComboBox
            ' 
            SearchPartComboBox.FormattingEnabled = True
            SearchPartComboBox.Location = New Point(6, 38)
            SearchPartComboBox.Name = "SearchPartComboBox"
            SearchPartComboBox.Size = New Size(210, 23)
            SearchPartComboBox.TabIndex = 1
            ' 
            ' OptionTypeLabel
            ' 
            OptionTypeLabel.AutoSize = True
            OptionTypeLabel.Location = New Point(6, 64)
            OptionTypeLabel.Name = "OptionTypeLabel"
            OptionTypeLabel.Size = New Size(74, 15)
            OptionTypeLabel.TabIndex = 4
            OptionTypeLabel.Text = "オプション種類"
            ' 
            ' OptionNameLabel
            ' 
            OptionNameLabel.AutoSize = True
            OptionNameLabel.Location = New Point(6, 108)
            OptionNameLabel.Name = "OptionNameLabel"
            OptionNameLabel.Size = New Size(62, 15)
            OptionNameLabel.TabIndex = 4
            OptionNameLabel.Text = "オプション名"
            ' 
            ' OptionTypeListBox
            ' 
            OptionTypeListBox.FormattingEnabled = True
            OptionTypeListBox.Location = New Point(6, 82)
            OptionTypeListBox.Name = "OptionTypeListBox"
            OptionTypeListBox.Size = New Size(210, 22)
            OptionTypeListBox.TabIndex = 5
            ' 
            ' OptionNameComboBox
            ' 
            OptionNameComboBox.FormattingEnabled = True
            OptionNameComboBox.Location = New Point(6, 126)
            OptionNameComboBox.Name = "OptionNameComboBox"
            OptionNameComboBox.Size = New Size(210, 23)
            OptionNameComboBox.TabIndex = 5
            ' 
            ' SearchResultListView
            ' 
            SearchResultListView.Location = New Point(6, 169)
            SearchResultListView.Name = "SearchResultListView"
            SearchResultListView.Size = New Size(222, 447)
            SearchResultListView.TabIndex = 6
            SearchResultListView.UseCompatibleStateImageBehavior = False
            SearchResultListView.View = View.Details
            ' 
            ' EquipmentCreateForm
            ' 
            AutoScaleDimensions = New SizeF(7.0F, 15.0F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(242, 650)
            Controls.Add(MainTabControl)
            Name = "EquipmentCreateForm"
            Text = "EquipForm"
            BaseItemGroup.ResumeLayout(False)
            BaseItemGroup.PerformLayout()
            NxGroup.ResumeLayout(False)
            NxGroup.PerformLayout()
            SmeltGroup.ResumeLayout(False)
            SmeltGroup.PerformLayout()
            CType(SmeltStepBox, ComponentModel.ISupportInitialize).EndInit()
            MainTabControl.ResumeLayout(False)
            GenerateTab.ResumeLayout(False)
            PrefixGroup.ResumeLayout(False)
            PrefixGroup.PerformLayout()
            SearchTab.ResumeLayout(False)
            SearchGroup.ResumeLayout(False)
            SearchGroup.PerformLayout()
            ResumeLayout(False)
        End Sub

        Friend WithEvents PartFilterSelectBox As ComboBox
        Friend WithEvents Label1 As Label
        Friend WithEvents BaseItemGroup As GroupBox
        Friend WithEvents BaseItemSelectBox As ComboBox
        Friend WithEvents Label2 As Label
        Friend WithEvents NxGroup As GroupBox
        Friend WithEvents Nx1SelectBox As ComboBox
        Friend WithEvents Label3 As Label
        Friend WithEvents Label4 As Label
        Friend WithEvents Nx1ValueTextBox As TextBox
        Friend WithEvents Label5 As Label
        Friend WithEvents Nx4ValueTextBox As TextBox
        Friend WithEvents Nx3ValueTextBox As TextBox
        Friend WithEvents Nx2ValueTextBox As TextBox
        Friend WithEvents Nx4SelectBox As ComboBox
        Friend WithEvents Label8 As Label
        Friend WithEvents Nx3SelectBox As ComboBox
        Friend WithEvents Label7 As Label
        Friend WithEvents Nx2SelectBox As ComboBox
        Friend WithEvents Label6 As Label
        Friend WithEvents SmeltGroup As GroupBox
        Friend WithEvents Smelt3OpSelectBox As ComboBox
        Friend WithEvents Label10 As Label
        Friend WithEvents Smelt2OpSelectBox As ComboBox
        Friend WithEvents Label11 As Label
        Friend WithEvents Smelt1OpSelectBox As ComboBox
        Friend WithEvents Label12 As Label
        Friend WithEvents Label9 As Label
        Friend WithEvents SelectedInitButton As Button
        Friend WithEvents EquipGenerateButton As Button
        Friend WithEvents Smelt5OpSelectBox As ComboBox
        Friend WithEvents Label14 As Label
        Friend WithEvents Smelt4OpSelectBox As ComboBox
        Friend WithEvents Label13 As Label

        Friend WithEvents MainTabControl As TabControl
        Friend WithEvents GenerateTab As TabPage
        Friend WithEvents SearchTab As TabPage

        ' 検索タブのコントロール
        Friend WithEvents SearchGroup As GroupBox
        Friend WithEvents SearchPartComboBox As ComboBox
        Friend WithEvents SearchPartLabel As Label
        Friend WithEvents OptionNameComboBox As ComboBox
        Friend WithEvents OptionNameLabel As Label
        Friend WithEvents SearchResultListView As ListView
        Friend WithEvents OptionTypeLabel As Label
        Friend WithEvents OptionTypeListBox As CheckedListBox
        Friend WithEvents PrefixGroup As GroupBox
        Friend WithEvents Prefix3SelectBox As ComboBox
        Friend WithEvents Prefix3CategorySelectBox As ComboBox
        Friend WithEvents Prefix2SelectBox As ComboBox
        Friend WithEvents Prefix2CategorySelectBox As ComboBox
        Friend WithEvents Prefix1SelectBox As ComboBox
        Friend WithEvents Prefix1CategorySelectBox As ComboBox
        Friend WithEvents Label15 As Label
        Friend WithEvents Label16 As Label
        Friend WithEvents Label17 As Label
        Friend WithEvents Prefix3RankSelectBox As ComboBox
        Friend WithEvents Prefix2RankSelectBox As ComboBox
        Friend WithEvents Prefix1RankSelectBox As ComboBox
        Friend WithEvents EquipRankLabel As Label
        Friend WithEvents Label18 As Label
        Friend WithEvents SmeltStepBox As NumericUpDown
    End Class

End Namespace