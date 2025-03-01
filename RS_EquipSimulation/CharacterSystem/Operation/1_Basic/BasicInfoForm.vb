Imports System.ComponentModel
Imports System.Reflection

Namespace CharacterSystem.Operation.Basic

    Public Class BasicInfoForm

        Private ReadOnly _manager As BasicManagerModel

        Public Sub New(manager As BasicManagerModel)
            InitializeComponent()
            _manager = manager

            ' コントロールの初期設定
            SetupControls()
            ' データバインディングの設定
            SetupBinding()
        End Sub

        Private Sub SetupControls()
            ' 職業コンボボックスの設定
            cmbJob.DataSource = BasicManagerModel.JobList
            cmbJob.DropDownStyle = ComboBoxStyle.DropDownList
        End Sub

        Private Sub SetupBinding()
            ' キャラクター名のバインディング
            txtCharacterName.DataBindings.Add(
            "Text",
            _manager,
            NameOf(_manager.CharacterName),
            False,
            DataSourceUpdateMode.OnPropertyChanged)

            ' 職業のバインディング
            cmbJob.DataBindings.Add(
            "Text",
            _manager,
            NameOf(_manager.JobName),
            False,
            DataSourceUpdateMode.OnValidation)

            ' レベルのバインディング
            numLevel.DataBindings.Add(
            "Value",
            _manager,
            NameOf(_manager.Level),
            False,
            DataSourceUpdateMode.OnValidation)
        End Sub

        Private Sub cmbJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJob.SelectedIndexChanged
            _manager.JobName = cmbJob.SelectedItem.ToString()
        End Sub

    End Class

End Namespace