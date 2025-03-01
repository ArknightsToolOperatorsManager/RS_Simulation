Imports RS_EquipSimulation.MasterSystem.Common
Imports RS_EquipSimulation.MasterSystem.Equipment
Imports RS_EquipSimulation.MasterSystem.Option
Imports RS_EquipSimulation.MasterSystem.Prefix
Imports RS_EquipSimulation.MasterSystem.Smelt
Imports RS_EquipSimulation.Common.Equipment
Imports RS_EquipSimulation.CharacterSystem.Operation.Basic
Imports System.Windows.Forms.Design.AxImporter

Namespace CreatorSystem.Equipment

    Partial Public Class EquipmentCreateForm
        Private ReadOnly _viewModel As EquipmentCreateViewModel
        Private ReadOnly _basicManager As BasicManagerModel

        Public Sub New(inventory As EquipInventoryManager, basicManager As BasicManagerModel)
            InitializeComponent()
            _basicManager = basicManager

            Dim model = New EquipmentCreateModel(inventory)

            _viewModel = New EquipmentCreateViewModel(model)

            InitializeControls()
        End Sub

        Private Sub InitializeControls()
            InitializePartSelection()
            InitializePrefixControls()
            InitializeOptionTypes()
            InitializeOptionNames()
            InitializeNxControls()
            InitializeSmeltControls()
        End Sub

#Region "フォーム初期化"
        Private Sub EquipmentCreateForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ' すでにInitializeControlsメソッドで初期化されているため、特に追加の処理は不要
        End Sub

        Private Sub InitializePartSelection()
            Dim sex = JobNameManager.GetJobDataByName(_basicManager.JobName).Sex
            Dim parts As New List(Of String) From {
                "",
                "武器",
                "補助",
                "首",
                "頭",
                If(sex = "Male", "背", "耳"),
                "腰",
                "手",
                "胴",
                "足",
                "指輪"
            }
            PartFilterSelectBox.DataSource = parts
            PartFilterSelectBox.SelectedIndex = 0
        End Sub

        Private Sub InitializePrefixControls()
            Dim categories = PrefixSingletonModel.Instance.GetCategories()

            For i As Integer = 1 To 3
                Dim categoryBox = DirectCast(Me.Controls.Find($"Prefix{i}CategorySelectBox", True)(0), ComboBox)
                categoryBox.DataSource = New List(Of String)(categories)
            Next
        End Sub

        Private Sub InitializeOptionTypes()
            Dim optionTypes = OptionSingletonModel.Instance.GetAllOptionTypes()

            OptionTypeListBox.Items.Clear()
            OptionTypeListBox.Items.AddRange(optionTypes.ToArray())

            For i As Integer = 0 To OptionTypeListBox.Items.Count - 1
                OptionTypeListBox.SetItemChecked(i, True)
            Next
        End Sub

        Private Sub InitializeOptionNames()
            Dim allOptions = OptionSingletonModel.Instance.GetAllOptions()
            OptionNameComboBox.Items.Clear()
            OptionNameComboBox.Items.AddRange(allOptions.ToArray())
        End Sub

        Private Sub InitializeNxControls()
            Dim optionsList = OptionSingletonModel.Instance.GetAllOptions().ToList()
            Dim allOptions = New List(Of String) From {String.Empty}
            allOptions.AddRange(optionsList)

            For i As Integer = 1 To 4
                Dim opBox = DirectCast(Me.Controls.Find($"Nx{i}SelectBox", True)(0), ComboBox)
                Dim valueBox = DirectCast(Me.Controls.Find($"Nx{i}ValueTextBox", True)(0), TextBox)
                opBox.Items.AddRange(allOptions.ToArray())
                valueBox.Text = ""
            Next
        End Sub

        Private Sub InitializeSmeltControls()
            SmeltStepBox.Value = 10
        End Sub
#End Region

#Region "コントロール初期化処理"
        Private Sub SelectedInitButton_Click(sender As Object, e As EventArgs) Handles SelectedInitButton.Click
            _viewModel.ResetState()
            ClearAllSelections()
        End Sub

        Private Sub ClearAllSelections()
            ClearBaseItemSelection()
            ClearPrefixSelections()
            ClearNxSelections()
            ClearSmeltSelections()
        End Sub

        Private Sub ClearBaseItemSelection()
            BaseItemSelectBox.SelectedIndex = -1
            _viewModel.SelectedBaseItem = Nothing
        End Sub

        Private Sub ClearPrefixSelections()
            For i As Integer = 1 To 3
                Dim prefixBox = DirectCast(Me.Controls.Find($"Prefix{i}SelectBox", True)(0), ComboBox)
                Dim rankBox = DirectCast(Me.Controls.Find($"Prefix{i}RankSelectBox", True)(0), ComboBox)
                prefixBox.SelectedIndex = -1
                rankBox.Text = ""
            Next
        End Sub

        Private Sub ClearNxSelections()
            For i As Integer = 1 To 4
                Dim opBox = DirectCast(Me.Controls.Find($"Nx{i}SelectBox", True)(0), ComboBox)
                Dim valueBox = DirectCast(Me.Controls.Find($"Nx{i}ValueTextBox", True)(0), TextBox)
                opBox.SelectedIndex = -1
                valueBox.Text = ""
            Next
        End Sub

        Private Sub ClearSmeltSelections()
            SmeltStepBox.Value = 1
            For i As Integer = 1 To 5
                Dim opBox = DirectCast(Me.Controls.Find($"Smelt{i}OpSelectBox", True)(0), ComboBox)
                opBox.SelectedIndex = -1
            Next
        End Sub
#End Region

#Region "ベースアイテム選択"
        Private Sub PartFilterBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PartFilterSelectBox.SelectedIndexChanged
            Dim currentJob = _basicManager.JobName
            Dim selectedPart = PartFilterSelectBox.SelectedItem.ToString()
            BaseItemSelectBox.DataSource = BaseItemSingletonModel.Instance.FilterItemsByJob(selectedPart, currentJob)
        End Sub

        Private Sub BaseItemSelectBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles BaseItemSelectBox.SelectedValueChanged
            If BaseItemSelectBox.SelectedItem IsNot Nothing Then
                _viewModel.SetBaseItem(BaseItemSelectBox.SelectedItem)
                UpdateNxDisplay()
                UpdateSmeltControls()
            Else
            End If
        End Sub

        Private Sub UpdateNxDisplay()
            If _viewModel.SelectedBaseItem Is Nothing Then Return

            For i As Integer = 1 To 4
                Dim opSelectBox = DirectCast(Me.Controls.Find($"Nx{i}SelectBox", True)(0), ComboBox)
                Dim valueTextBox = DirectCast(Me.Controls.Find($"Nx{i}ValueTextBox", True)(0), TextBox)

                If _viewModel.SelectedBaseItem.NxOptions IsNot Nothing AndAlso _viewModel.SelectedBaseItem.NxOptions.ContainsKey(i) Then
                    Dim currentOp = _viewModel.SelectedBaseItem.NxOptions(i).FirstOrDefault()
                    If currentOp IsNot Nothing Then
                        opSelectBox.SelectedItem = currentOp.OptionName
                        valueTextBox.Text = currentOp.OptionValue
                    End If
                Else
                    opSelectBox.SelectedItem = ""
                    valueTextBox.Text = ""
                End If
            Next
        End Sub

        Private Sub UpdateSmeltControls()
            If _viewModel.SelectedBaseItem Is Nothing Then Return

            EquipRankLabel.Text = _viewModel.SelectedBaseItem.Rank

            Dim partName = PartFilterSelectBox.SelectedItem.ToString()

            Dim ins = SmeltOpModelManagerSingletonModel.Instance
            For i As Integer = 1 To 5
                Dim stage = GetStageFromIndex(i)
                Dim options = ins.GetOptionNames(partName, stage)

                Dim opSelectBox = DirectCast(Me.Controls.Find($"Smelt{i}OpSelectBox", True)(0), ComboBox)
                If options IsNot Nothing Then
                    opSelectBox.DataSource = options
                Else
                    opSelectBox.DataSource = Nothing
                End If
            Next

            ' 錬成が1つもない＝錬成できない装備であるため、ステップ0、選択不可とする
            Dim flg As Boolean
            If {"補助", "指輪"}.Contains(_viewModel.SelectedBaseItem.PartName) Then
                flg = False
            Else
                flg = True
            End If
            Smelt1OpSelectBox.Enabled = flg
            Smelt2OpSelectBox.Enabled = flg
            Smelt3OpSelectBox.Enabled = flg
            Smelt4OpSelectBox.Enabled = flg
            Smelt5OpSelectBox.Enabled = flg
            SmeltStepBox.Enabled = flg
        End Sub

        Private Function GetStageFromIndex(index As Integer) As Integer
            Select Case index
                Case 1
                    Return 1
                Case 2
                    Return 5
                Case 3
                    Return 10
                Case 4
                    Return 15
                Case 5
                    Return 18
                Case Else
                    Return 1
            End Select
        End Function
#End Region

#Region "称号選択"
        Private Sub PrefixCategoryBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _
            Prefix1CategorySelectBox.SelectedIndexChanged,
            Prefix2CategorySelectBox.SelectedIndexChanged,
            Prefix3CategorySelectBox.SelectedIndexChanged

            Dim setIndex = GetPrefixSetIndex(DirectCast(sender, ComboBox))
            If setIndex > 0 Then
                UpdatePrefixOptions(setIndex)
            End If
        End Sub

        Private Sub PrefixSelectBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _
            Prefix1SelectBox.SelectedIndexChanged,
            Prefix2SelectBox.SelectedIndexChanged,
            Prefix3SelectBox.SelectedIndexChanged

            Dim setIndex = GetPrefixSetIndex(DirectCast(sender, ComboBox))
            If setIndex > 0 Then
                UpdatePrefixRanks(setIndex)
            End If
        End Sub

        Private Function GetPrefixSetIndex(control As ComboBox) As Integer
            Dim match = System.Text.RegularExpressions.Regex.Match(control.Name, "Prefix(\d)")
            If match.Success Then
                Return Integer.Parse(match.Groups(1).Value)
            End If
            Return 0
        End Function

        Private Sub UpdatePrefixOptions(setIndex As Integer)
            Dim categoryBox = DirectCast(Me.Controls.Find($"Prefix{setIndex}CategorySelectBox", True)(0), ComboBox)
            Dim prefixBox = DirectCast(Me.Controls.Find($"Prefix{setIndex}SelectBox", True)(0), ComboBox)

            Dim selectedCategory = categoryBox.SelectedItem.ToString()
            Dim prefixes = PrefixSingletonModel.Instance.GetByCategory(selectedCategory)
            prefixBox.DataSource = prefixes.Distinct().ToList
        End Sub

        Private Sub UpdatePrefixRanks(setIndex As Integer)
            Dim prefixBox = DirectCast(Me.Controls.Find($"Prefix{setIndex}SelectBox", True)(0), ComboBox)
            Dim rankBox = DirectCast(Me.Controls.Find($"Prefix{setIndex}RankSelectBox", True)(0), ComboBox)

            If prefixBox.SelectedItem IsNot Nothing Then
                Dim selectedPrefix = prefixBox.SelectedItem.ToString()
                rankBox.DataSource = PrefixSingletonModel.Instance.FindPrefixesByOption(selectedPrefix)
            End If
        End Sub
#End Region

#Region "装備生成"
        Private Sub EquipGenerateButton_Click(sender As Object, e As EventArgs) Handles EquipGenerateButton.Click
            If ValidateSelections() Then
                GenerateEquipment()
            End If
        End Sub

        Private Function ValidateSelections() As Boolean
            If _viewModel.SelectedBaseItem Is Nothing Then
                MessageBox.Show("ベースアイテムを選択してください。")
                Return False
            End If

            If SmeltStepBox.Value < 1 OrElse SmeltStepBox.Value > 20 Then
                MessageBox.Show("精錬ステップは1-20の範囲で指定してください。")
                Return False
            End If

            Return True
        End Function

        Private Sub GenerateEquipment()
            Try
                Dim prefixOptions = GetSelectedPrefixOptions()
                Dim nxOptions = GetSelectedNxOptions()
                Dim smeltOptions = GetSelectedSmeltOptions()

                If _viewModel.GenerateEquipment(prefixOptions, nxOptions, smeltOptions) Then
                    MessageBox.Show("装備を生成しました。")
                Else
                    MessageBox.Show("装備の生成に失敗しました。")
                End If
            Catch ex As Exception
                MessageBox.Show($"装備生成中にエラーが発生しました。{Environment.NewLine}{ex.Message}")
            End Try
        End Sub

        Private Function GetSelectedPrefixOptions() As List(Of OpValue)
            Dim options As New List(Of OpValue)

            For i As Integer = 1 To 3
                Dim prefixBox = DirectCast(Me.Controls.Find($"Prefix{i}SelectBox", True)(0), ComboBox)
                If prefixBox.SelectedItem IsNot Nothing Then
                    Dim prefixName = prefixBox.SelectedItem.ToString()
                    Dim prefix = PrefixSingletonModel.Instance.FindByName(prefixName).Value
                    options.Add(New OpValue(prefixName, prefix))
                End If
            Next

            Return options
        End Function

        Private Function GetSelectedNxOptions() As List(Of OpValue)
            Dim options As New List(Of OpValue)

            For i As Integer = 1 To 4
                Dim opBox = DirectCast(Me.Controls.Find($"Nx{i}SelectBox", True)(0), ComboBox)
                Dim valueBox = DirectCast(Me.Controls.Find($"Nx{i}ValueTextBox", True)(0), TextBox)

                If opBox.SelectedItem IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(valueBox.Text) Then
                    options.Add(New OpValue(opBox.SelectedItem.ToString(), valueBox.Text))
                End If
            Next

            Return options
        End Function

        Private Function GetSelectedSmeltOptions() As List(Of OpValue)
            Dim options As New List(Of OpValue)
            Dim partName = PartFilterSelectBox.SelectedItem.ToString()

            For i As Integer = 1 To 5
                Dim opBox = DirectCast(Me.Controls.Find($"Smelt{i}OpSelectBox", True)(0), ComboBox)
                If opBox.SelectedItem IsNot Nothing Then
                    Dim stage = GetStageFromIndex(i)
                    Dim opName = opBox.SelectedItem.ToString()
                    Dim value = SmeltOpModelManagerSingletonModel.Instance.CalculateSmeltValue(
                        partName,
                        stage,
                        opName,
                        _viewModel.SelectedBaseItem.Rank,
                        SmeltStepBox.Value.ToString()
                    )
                    options.Add(New OpValue(opName, value.ToString()))
                End If
            Next

            Return options
        End Function
#End Region

#Region "オプション検索"
        Private Sub OptionTypeListBox_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles OptionTypeListBox.ItemCheck
            UpdateOptionNameFilter()
        End Sub

        Private Sub UpdateOptionNameFilter()
            Dim currentSelection = OptionNameComboBox.SelectedItem
            Dim selectedTypes = GetSelectedOptionTypes()
            Dim filteredOptions = OptionSingletonModel.Instance.GetOptionsByTypes(selectedTypes)

            OptionNameComboBox.Items.Clear()
            OptionNameComboBox.Items.AddRange(filteredOptions.ToArray())

            If currentSelection IsNot Nothing AndAlso OptionNameComboBox.Items.Contains(currentSelection) Then
                OptionNameComboBox.SelectedItem = currentSelection
            ElseIf OptionNameComboBox.Items.Count > 0 Then
                OptionNameComboBox.SelectedIndex = 0
            End If
        End Sub

        Private Function GetSelectedOptionTypes() As List(Of String)
            Dim selectedTypes As New List(Of String)

            For i As Integer = 0 To OptionTypeListBox.Items.Count - 1
                If OptionTypeListBox.GetItemChecked(i) Then
                    selectedTypes.Add(OptionTypeListBox.Items(i).ToString())
                End If
            Next

            Return selectedTypes
        End Function

        Private Sub SearchEquipmentBtn_Click(sender As Object, e As EventArgs) Handles SearchResultListView.Click
            If OptionNameComboBox.SelectedItem Is Nothing Then
                MessageBox.Show("検索するオプションを選択してください。")
                Return
            End If

            Dim optionName = OptionNameComboBox.SelectedItem.ToString()
            Dim partName = If(SearchPartComboBox.SelectedItem?.ToString(), "")

            Dim searchResults = BaseItemSingletonModel.Instance.FindItemsByOption(optionName, partName)
            DisplaySearchResults(searchResults)
        End Sub

        Private Sub DisplaySearchResults(results As IEnumerable(Of BaseItemModel))
            SearchResultListView.Items.Clear()

            For Each equip In results
                Dim item = New ListViewItem(equip.Name)
                item.SubItems.Add(equip.PartName)
                item.SubItems.Add(equip.Rank)

                Dim details = BaseItemSingletonModel.Instance.GetOptionDetails(equip, OptionNameComboBox.SelectedItem.ToString())
                item.SubItems.Add(String.Join(", ", details))

                SearchResultListView.Items.Add(item)
            Next
        End Sub

        Private Sub ItemNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles ItemNameTextBox.TextChanged
            _viewModel.ItemName = ItemNameTextBox.Text
        End Sub
#End Region
    End Class
End Namespace