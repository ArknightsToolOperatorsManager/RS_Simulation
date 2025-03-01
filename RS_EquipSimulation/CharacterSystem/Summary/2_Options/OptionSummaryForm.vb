Imports System.ComponentModel
Imports System.Windows.Forms
Imports RS_EquipSimulation.CharacterSystem.Common
Imports RS_EquipSimulation.MasterSystem.Option

Namespace CharacterSystem.Summary.Option

    Public Class OptionSummaryForm
        Private ReadOnly _viewModel As OptionSummaryViewModel

        ' コンストラクタ
        Public Sub New(summaryManager As OptionSummaryManagerModel)
            ' デザイナで作成された初期化
            InitializeComponent()

            ' ViewModelの初期化
            _viewModel = New OptionSummaryViewModel(summaryManager)

            ' フィルタチェックボックスの初期化
            InitializeFilterCheckboxes()

            ' リストビューの設定
            SetupListView()

            ' データの初期表示
            UpdateOptionList()

            ' チェックボックスのイベントハンドラ設定
            SetupEventHandlers()
        End Sub

        ' フィルタチェックボックスの初期化
        Private Sub InitializeFilterCheckboxes()
            ' FilterPanelをクリア
            FilterPanel.Controls.Clear()

            ' 選択対象外の種類
            Dim excludedTypes As New List(Of String) From {"比率", "ステ上昇", "未実装", "物理系/魔法系"}

            ' OptionSingletonModelから全種類を取得
            Dim allTypes = OptionSingletonModel.Instance.GetAllOptionTypes()

            ' 除外対象以外の種類を選択肢として追加
            For Each type As String In allTypes
                If Not excludedTypes.Contains(type) Then
                    Dim chk As New CheckBox With {
                        .Text = type,
                        .AutoSize = True,
                        .Checked = True,
                        .Tag = type
                    }

                    ' チェック状態変更時のイベントハンドラを登録
                    AddHandler chk.CheckedChanged, AddressOf FilterCheckBox_CheckedChanged
                    AddHandler _viewModel.PropertyChanged, AddressOf ViewModel_PropertyChanged

                    ' パネルに追加
                    FilterPanel.Controls.Add(chk)
                End If
            Next
        End Sub

        ' リストビューの設定
        Private Sub SetupListView()
            With OptionListView
                .View = View.Details
                .FullRowSelect = True
                .GridLines = True
                .HideSelection = False

                ' 列のクリア
                .Columns.Clear()

                ' 列の追加
                .Columns.Add("OptionName", "オプション名", 180)
                .Columns.Add("Value", "合計値", 95)
            End With
        End Sub

        ' チェックボックスのイベントハンドラ設定
        Private Sub SetupEventHandlers()
            For Each control As Control In FilterPanel.Controls
                If TypeOf control Is CheckBox Then
                    AddHandler DirectCast(control, CheckBox).CheckedChanged, AddressOf FilterCheckBox_CheckedChanged
                End If
            Next
        End Sub

        ' CharacterDataから呼び出される更新メソッド
        Public Sub UpdateFromCharacterData()
            ' リストを更新
            UpdateOptionList()
        End Sub

        Private Sub ViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            If e.PropertyName = "FilteredOptions" Then
                UpdateOptionList()
            End If
        End Sub

        ' フィルターチェックボックス変更イベント
        Private Sub FilterCheckBox_CheckedChanged(sender As Object, e As EventArgs)
            Dim cb = DirectCast(sender, CheckBox)
            Dim type = cb.Tag.ToString()

            ' 選択状態をビューモデルに反映
            _viewModel.UpdateTypeSelection(type, cb.Checked)

            ' リストを更新
            UpdateOptionList()
        End Sub

        ' オプションリストの更新
        Private Sub UpdateOptionList()
            OptionListView.Items.Clear()

            ' フィルタリングされたオプションを取得して表示
            Dim filteredOptions = _viewModel.GetFilteredOptions()
            For Each op In filteredOptions
                Dim item = New ListViewItem(op.Item1)
                item.SubItems.Add(op.Item2.ToString())
                OptionListView.Items.Add(item)
            Next
        End Sub
    End Class

End Namespace