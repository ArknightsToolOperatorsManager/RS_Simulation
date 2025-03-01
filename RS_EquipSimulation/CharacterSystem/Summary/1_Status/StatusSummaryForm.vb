Imports RS_EquipSimulation.CharacterSystem.Common
Imports RS_EquipSimulation.CharacterSystem.Operation.Basic
Imports RS_EquipSimulation.CharacterSystem.Operation.Status

Namespace CharacterSystem.Summary.Status

    Public Class StatusSummaryForm
        Private ReadOnly _viewModel As StatusSummaryViewModel

        ' コンストラクタ
        Public Sub New(optManager As OptionManagerModel,
                      basicManager As BasicManagerModel,
                      statusManager As StatusManagerModel,
                      summaryManager As StatusSummaryManagerModel)

            ' デザイナで作成された初期化
            InitializeComponent()

            ' ViewModelの初期化
            _viewModel = New StatusSummaryViewModel(summaryManager)

            ' テキストボックスのバインディング
            BindTextBoxes()

            ' 共通ヘルパーを使用してDataGridViewの設定
            StatusGridHelper.SetupStatusGrid(dgvStatus, True, False)  ' 読み取り専用
            StatusGridHelper.AddStatusColumns(dgvStatus)

            ' その他の設定
            dgvStatus.DataSource = _viewModel.GridData
        End Sub

        ' テキストボックスのバインディング
        Private Sub BindTextBoxes()
            ' 各テキストボックスにバインド
            PowerTextBox.DataBindings.Add("Text", _viewModel, "Power", True, DataSourceUpdateMode.OnPropertyChanged)
            AgilityTextBox.DataBindings.Add("Text", _viewModel, "Agility", True, DataSourceUpdateMode.OnPropertyChanged)
            HealthTextBox.DataBindings.Add("Text", _viewModel, "Health", True, DataSourceUpdateMode.OnPropertyChanged)
            KnowledgeTextBox.DataBindings.Add("Text", _viewModel, "Knowledge", True, DataSourceUpdateMode.OnPropertyChanged)
            WisdomTextBox.DataBindings.Add("Text", _viewModel, "Wisdom", True, DataSourceUpdateMode.OnPropertyChanged)
            CharismaTextBox.DataBindings.Add("Text", _viewModel, "Charisma", True, DataSourceUpdateMode.OnPropertyChanged)
            LuckTextBox.DataBindings.Add("Text", _viewModel, "Luck", True, DataSourceUpdateMode.OnPropertyChanged)
        End Sub
    End Class

End Namespace