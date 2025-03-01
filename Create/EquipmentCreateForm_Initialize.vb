Namespace CreatorSystem.Equipment

    ''' <summary>
    ''' 装備生成フォーム - 初期化処理部分
    ''' </summary>
    Partial Public Class EquipmentCreateForm
#Region "フォーム初期化"
        Private Sub EquipmentCreateForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            InitializePartSelection()
            InitializePrefixControls()
            InitializeOptionTypes()
            InitializeOptionNames()
            InitializeSmeltControls()
        End Sub

        ''' <summary>
        ''' 部位選択の初期化
        ''' </summary>
        Private Sub InitializePartSelection()
            ' 性別による背/耳の選択
            Dim jobData = _jobConfig.GetJobData(CURRENT_JOB)
            Dim backOrEar = If(jobData?.Sex = "Male", "背", "耳")

            ' 部位選択の選択肢を設定
            Dim partList As New List(Of String) From {
            "武器",
            "サブ武器",
            "首",
            "頭",
            backOrEar,
            "腰",
            "手",
            "胴",
            "足"
        }
            PartFilterSelectBox.DataSource = partList
        End Sub

        ''' <summary>
        ''' 称号選択の初期化
        ''' </summary>
        Private Sub InitializePrefixControls()
            ' 系統の選択肢を取得
            Dim categories = _prefixData.PrefixList.
            Select(Function(p) p.Category).
            Distinct().
            OrderBy(Function(c) c).
            ToList()

            ' 3セット分の初期化
            For i As Integer = 1 To 3
                Dim categoryBox = DirectCast(Me.Controls.Find($"Prefix{i}CategorySelectBox", True)(0), ComboBox)
                categoryBox.DataSource = New List(Of String)(categories)
            Next
        End Sub

        ''' <summary>
        ''' オプション種類の初期化
        ''' </summary>
        Private Sub InitializeOptionTypes()
            Dim optionTypes = _optionData.GetAllOptionTypes()

            ' CheckedListBoxをクリアしてから新しい項目を追加
            OptionTypeListBox.Items.Clear()
            OptionTypeListBox.Items.AddRange(optionTypes.ToArray())

            ' すべての種類を初期状態でチェック
            For i As Integer = 0 To OptionTypeListBox.Items.Count - 1
                OptionTypeListBox.SetItemChecked(i, True)
            Next
        End Sub

        ''' <summary>
        ''' オプション名の初期化
        ''' </summary>
        Private Sub InitializeOptionNames()
            ' オプションリストを取得
            Dim allOptions = _optionData.GetAllOptions()
            OptionNameComboBox.Items.Clear()
            OptionNameComboBox.Items.AddRange(allOptions.ToArray())
        End Sub

        ''' <summary>
        ''' 精錬関連コントロールの初期化
        ''' </summary>
        Private Sub InitializeSmeltControls()
            ' 精錬ステップの初期値設定
            SmeltStepBox.Value = 1
        End Sub
#End Region

#Region "初期化ボタン処理"
        Private Sub SelectedInitButton_Click(sender As Object, e As EventArgs) Handles SelectedInitButton.Click
            ClearAllSelections()
        End Sub

        ''' <summary>
        ''' すべての選択をクリア
        ''' </summary>
        Private Sub ClearAllSelections()
            ClearBaseItemSelection()
            ClearPrefixSelections()
            ClearNxSelections()
            ClearSmeltSelections()
        End Sub

        ''' <summary>
        ''' ベースアイテム選択のクリア
        ''' </summary>
        Private Sub ClearBaseItemSelection()
            BaseItemSelectBox.SelectedIndex = -1
            _selectedBaseItem = Nothing
        End Sub

        ''' <summary>
        ''' 称号選択のクリア
        ''' </summary>
        Private Sub ClearPrefixSelections()
            For i As Integer = 1 To 3
                Dim prefixBox = DirectCast(Me.Controls.Find($"Prefix{i}SelectBox", True)(0), ComboBox)
                Dim rankBox = DirectCast(Me.Controls.Find($"Prefix{i}RankSelectBox", True)(0), ComboBox)
                prefixBox.SelectedIndex = -1
                rankBox.Text = ""
            Next
        End Sub

        ''' <summary>
        ''' 錬成選択のクリア
        ''' </summary>
        Private Sub ClearNxSelections()
            For i As Integer = 1 To 4
                Dim opBox = DirectCast(Me.Controls.Find($"Nx{i}SelectBox", True)(0), ComboBox)
                Dim valueBox = DirectCast(Me.Controls.Find($"Nx{i}ValueTextBox", True)(0), TextBox)
                opBox.SelectedIndex = -1
                valueBox.Text = ""
            Next
        End Sub

        ''' <summary>
        ''' 精錬選択のクリア
        ''' </summary>
        Private Sub ClearSmeltSelections()
            SmeltStepBox.Value = 1
            For i As Integer = 1 To 5
                Dim opBox = DirectCast(Me.Controls.Find($"Smelt{i}OpSelectBox", True)(0), ComboBox)
                opBox.SelectedIndex = -1
            Next
        End Sub
#End Region
    End Class

End Namespace