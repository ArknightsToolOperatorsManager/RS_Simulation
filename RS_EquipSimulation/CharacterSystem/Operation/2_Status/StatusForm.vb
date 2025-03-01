Imports System.ComponentModel
Imports RS_EquipSimulation.CharacterSystem.Common
Imports RS_EquipSimulation.CharacterSystem.Operation.Basic
Imports RS_EquipSimulation.MasterSystem.Common

Namespace CharacterSystem.Operation.Status

    Public Class StatusForm
        Private ReadOnly _viewModel As StatusViewModel
        Private ReadOnly _basicInfo As BasicManagerModel

        Public Sub New(model As StatusManagerModel, basicInfo As BasicManagerModel)
            InitializeComponent()
            _viewModel = New StatusViewModel(model)
            _basicInfo = basicInfo

            ' 共通ヘルパーを使用してDataGridViewの設定
            StatusGridHelper.SetupStatusGrid(dgvStatus, False, False)
            StatusGridHelper.AddStatusColumns(dgvStatus)
            StatusGridHelper.SetupNumericValidation(dgvStatus)

            ' その他の設定
            dgvStatus.DataSource = _viewModel.GridData

            ' イベントハンドラの設定
            AddHandler dgvStatus.CellValidating, AddressOf dgvStatus_CellValidating
            AddHandler dgvStatus.CellEndEdit, AddressOf dgvStatus_CellEndEdit
            AddHandler dgvStatus.DataError, AddressOf dgvStatus_DataError

            TotalAllocationLabel.Text = _viewModel.GetTotalAllocation().ToString()
            AddHandler _viewModel.PropertyChanged, AddressOf ViewModel_PropertyChanged
            Update_InfoAllocation()
        End Sub

        Private Sub dgvStatus_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs)
            ' 項目名列は検証不要
            If e.ColumnIndex = 0 Then Return

            ' 数値チェック
            Dim value As Integer
            If Not Integer.TryParse(e.FormattedValue.ToString(), value) Then
                e.Cancel = True
                ' 元の値に戻す
                dgvStatus.CancelEdit()
                Return
            End If

            ' どのステータスプロパティを更新するか特定
            Dim row = DirectCast(dgvStatus.Rows(e.RowIndex).DataBoundItem, StatusGridRowViewModel)
            Dim statusName = row.RowName

            ' 列インデックスからプロパティ名を取得
            Dim propertyNames As String() = {"DisplayName", "Power", "Agility", "Health", "Knowledge", "Wisdom", "Charisma", "Luck"}
            If e.ColumnIndex >= propertyNames.Length Then
                e.Cancel = True
                dgvStatus.CancelEdit()
                Return
            End If
            Dim propertyName = propertyNames(e.ColumnIndex)

            ' 値の更新を試みる
            If Not _viewModel.UpdateValue(statusName, propertyName, value) Then
                ' 検証失敗時は編集をキャンセル
                e.Cancel = True
                dgvStatus.CancelEdit()
            End If
        End Sub

        Private Sub Update_InfoAllocation()
            Dim jobData = GetJobDataByName(_basicInfo.JobName)
            Dim jobAllocate As Integer

            If jobData.AutoRise Is Nothing Then
                jobAllocate = (_basicInfo.Level - 1) * 5
            Else
                jobAllocate = (_basicInfo.Level - 1) * 4
            End If

            InfoAllocationLabel.Text = $"合計:{jobAllocate + 50 + 50 * 16 + 250 * 4}{vbCrLf}レベル:{jobAllocate},MQ1:50,MQ2:{50 * 16 + 250 * 4}"
        End Sub

        Private Sub dgvStatus_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
            ' 編集完了時に再表示して確実に更新
            dgvStatus.Refresh()
        End Sub

        Private Sub dgvStatus_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)
            ' エラーの抑制（値の復元は別途行われる）
            e.Cancel = True
        End Sub

        Private Sub ViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            If e.PropertyName = "GridData" Then
                ' ステータスが変更されたら合計値を更新
                TotalAllocationLabel.Text = _viewModel.GetTotalAllocation().ToString()
                Update_InfoAllocation()
            End If
        End Sub
    End Class

End Namespace