Imports RS_EquipSimulation.CharacterSystem.Operation.Basic
Imports RS_EquipSimulation.CharacterSystem.Operation.Status
Imports RS_EquipSimulation.CharacterSystem.Operation.Equipment
Imports RS_EquipSimulation.CharacterSystem.Operation.Creature
Imports RS_EquipSimulation.CharacterSystem.Summary.Status
Imports RS_EquipSimulation.CharacterSystem.Summary.Option
Imports RS_EquipSimulation.CharacterSystem.Operation.Costume
Imports System.ComponentModel
Imports RS_EquipSimulation.CreatorSystem.Equipment
Imports RS_EquipSimulation.CreatorSystem.Creature
Imports RS_EquipSimulation.MasterSystem.Update

Public Class CharacterForm
    Private WithEvents _viewModel As New CharacterViewModel()

    ' 各フォームを保持するフィールド
    Private _basicInfoForm As BasicInfoForm
    Private _statusForm As StatusForm
    Private _equipmentForm As EquipmentForm
    Private _creatureForm As CreatureForm
    Private _costumeForm As CostumeForm
    Private _summaryStatusForm As StatusSummaryForm
    Private _summaryOptionForm As OptionSummaryForm

    Private _equipmentCreateForm As EquipItemManagerForm
    Private _creatureCreateForm As CreatureCreateForm

    ' キャラクター選択用コントロール
    Private WithEvents cmbCharacters As ComboBox
    Private WithEvents btnNewCharacter As Button
    Private WithEvents btnSaveCharacter As Button

    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        _viewModel = New CharacterViewModel()
    End Sub

    Private Sub CharacterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' マスターデータ更新マネージャーの初期化
        InitializeMasterDataUpdater()

        ' キャラクター選択UIの初期化
        InitializeCharacterSelectorUI()

        ' キャラクターが存在しない場合は新規作成
        If _viewModel.CharacterList.Count = 0 Then
            _viewModel.CreateNewCharacter()
        Else
            ' 既存のキャラクターを選択
            UpdateCharacterComboBox()
            _viewModel.SelectedCharacterIndex = 0
        End If
    End Sub

    Private Sub InitializeCharacterSelectorUI()
        ' キャラクター選択コンボボックスを作成
        cmbCharacters = New ComboBox With {
            .Location = New Point(12, 8),
            .Size = New Size(200, 23),
            .DropDownStyle = ComboBoxStyle.DropDownList
        }

        ' 新規キャラクターボタンを作成
        btnNewCharacter = New Button With {
            .Location = New Point(222, 8),
            .Size = New Size(100, 23),
            .Text = "新規作成"
        }

        ' 保存ボタンを作成
        btnSaveCharacter = New Button With {
            .Location = New Point(332, 8),
            .Size = New Size(100, 23),
            .Text = "保存"
        }

        ' コントロールをフォームに追加
        Me.Controls.Add(cmbCharacters)
        Me.Controls.Add(btnNewCharacter)
        Me.Controls.Add(btnSaveCharacter)

        ' 既存のグループボックスを下に移動
        Dim yOffset As Integer = 40
        GroupBox1.Location = New Point(GroupBox1.Left, GroupBox1.Top + yOffset)
        GroupBox2.Location = New Point(GroupBox2.Left, GroupBox2.Top + yOffset)

        ' フォームサイズを調整
        Me.Height += yOffset
    End Sub

    Private Sub UpdateCharacterComboBox()
        ' DataBindingでの実装も可能ですが、簡単のためここでは手動で実装
        cmbCharacters.Items.Clear()

        For Each characterName In _viewModel.CharacterList
            cmbCharacters.Items.Add(characterName)
        Next

        If _viewModel.SelectedCharacterIndex >= 0 AndAlso
           _viewModel.SelectedCharacterIndex < cmbCharacters.Items.Count Then
            cmbCharacters.SelectedIndex = _viewModel.SelectedCharacterIndex
        End If
    End Sub

    Private Sub InitializeChildForms()
        ' 各パネルのコントロールをクリア
        pnlBasicInfo.Controls.Clear()
        pnlStatus.Controls.Clear()
        pnlEquipment.Controls.Clear()
        pnlCreature.Controls.Clear()
        pnlCostume.Controls.Clear()
        pnlSummaryStatus.Controls.Clear()
        pnlSummaryOption.Controls.Clear()

        ' 前のフォームのリソースを解放
        DisposeChildForms()

        ' 現在のキャラクターを取得
        Dim currentCharacter = _viewModel.CurrentCharacter
        If currentCharacter Is Nothing Then
            Return
        End If

        ' 基本情報フォーム
        _basicInfoForm = New BasicInfoForm(currentCharacter.BasicManager)
        ConfigureChildForm(_basicInfoForm, pnlBasicInfo)

        ' ステータスフォーム
        _statusForm = New StatusForm(currentCharacter.StatusManager, currentCharacter.BasicManager)
        ConfigureChildForm(_statusForm, pnlStatus)

        ' 装備フォーム
        _equipmentForm = New EquipmentForm(currentCharacter.EquipmentManager, currentCharacter._equipmentInventory)
        ConfigureChildForm(_equipmentForm, pnlEquipment)

        ' クリーチャーフォーム
        _creatureForm = New CreatureForm(currentCharacter.CreatureManager, currentCharacter._creatureInventory)
        ConfigureChildForm(_creatureForm, pnlCreature)

        ' コスチュームフォーム
        _costumeForm = New CostumeForm()
        _costumeForm.SetViewModel(New CostumeViewModel(currentCharacter.CostumeManager))
        ConfigureChildForm(_costumeForm, pnlCostume)

        ' ステータスサマリーフォーム
        _summaryStatusForm = New StatusSummaryForm(
            currentCharacter._optionManager,
            currentCharacter.BasicManager,
            currentCharacter.StatusManager,
            currentCharacter._statusSummaryManager)
        ConfigureChildForm(_summaryStatusForm, pnlSummaryStatus)

        ' オプションサマリーフォーム
        _summaryOptionForm = New OptionSummaryForm(currentCharacter._optionSummaryManager)
        ConfigureChildForm(_summaryOptionForm, pnlSummaryOption)
    End Sub

    Private Sub DisposeChildForms()
        SafeDispose(_basicInfoForm)
        SafeDispose(_statusForm)
        SafeDispose(_equipmentForm)
        SafeDispose(_creatureForm)
        SafeDispose(_costumeForm)
        SafeDispose(_summaryStatusForm)
        SafeDispose(_summaryOptionForm)
    End Sub

    Private Sub SafeDispose(ByRef form As Form)
        If form IsNot Nothing Then
            form.Dispose()
            form = Nothing
        End If
    End Sub

    Private Sub ConfigureChildForm(form As Form, parentPanel As Panel, Optional topLevel As Boolean = False)
        ' フォームをトップレベルから子コントロールに変更
        form.TopLevel = topLevel
        form.FormBorderStyle = FormBorderStyle.None

        ' パネルにフォームを埋め込む
        parentPanel.Controls.Add(form)
        form.Dock = DockStyle.Fill  ' パネルサイズに合わせる

        ' フォームを表示
        form.Show()
    End Sub

    ' ViewModelのイベントハンドラ
    Private Sub ViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles _viewModel.PropertyChanged
        Select Case e.PropertyName
            Case NameOf(_viewModel.CharacterList)
                UpdateCharacterComboBox()

            Case NameOf(_viewModel.CurrentCharacter)
                InitializeChildForms()

            Case NameOf(_viewModel.SelectedCharacterIndex)
                If cmbCharacters.SelectedIndex <> _viewModel.SelectedCharacterIndex Then
                    cmbCharacters.SelectedIndex = _viewModel.SelectedCharacterIndex
                End If
        End Select
    End Sub

    Private Sub ViewModel_CharacterDataUpdated(sender As Object, e As EventArgs) Handles _viewModel.CharacterDataUpdated
        ' キャラクターデータが更新されたときに必要な処理
        ' 例: ステータスサマリーの更新など
    End Sub

    ' ComboBoxの選択変更イベントハンドラ
    Private Sub cmbCharacters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCharacters.SelectedIndexChanged
        _viewModel.SelectedCharacterIndex = cmbCharacters.SelectedIndex
    End Sub

    ' 新規キャラクターボタンのクリックイベントハンドラ
    Private Sub btnNewCharacter_Click(sender As Object, e As EventArgs) Handles btnNewCharacter.Click
        ' 現在のキャラクターを保存
        _viewModel.SaveCurrentCharacter()

        ' 新規キャラクターを作成
        If Not _viewModel.CreateNewCharacter() Then
            MessageBox.Show("新規キャラクターの作成に失敗しました。",
                           "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' 保存ボタンのクリックイベントハンドラ
    Private Sub btnSaveCharacter_Click(sender As Object, e As EventArgs) Handles btnSaveCharacter.Click
        ' 現在のキャラクターを保存
        If _viewModel.SaveCurrentCharacter() Then
            MessageBox.Show("キャラクターを保存しました。",
                           "保存完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("キャラクターの保存に失敗しました。",
                           "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnEquipment_Click(sender As Object, e As EventArgs) Handles btnEquipment.Click
        ShowEquipmentCreation()
    End Sub

    Private Sub ShowEquipmentCreation()
        If _equipmentCreateForm Is Nothing OrElse _equipmentCreateForm.IsDisposed Then
            _equipmentCreateForm = New EquipItemManagerForm(_viewModel.CurrentCharacter._equipmentInventory, _viewModel.CurrentCharacter.BasicManager)
            _equipmentCreateForm.Owner = Me
        End If

        _equipmentCreateForm.Show()
    End Sub

    Private Sub btnCreature_Click(sender As Object, e As EventArgs) Handles btnCreature.Click
        ShowCreatureCreation()
    End Sub

    Private Sub ShowCreatureCreation()
        If _creatureCreateForm Is Nothing OrElse _creatureCreateForm.IsDisposed Then
            _creatureCreateForm = New CreatureCreateForm(_viewModel.CurrentCharacter._creatureInventory)
            _creatureCreateForm.Owner = Me
        End If

        _creatureCreateForm.Show()
    End Sub

    Private Sub CharacterForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' 終了前に保存確認
        Dim result = MessageBox.Show("キャラクターデータを保存しますか？",
                                    "保存確認",
                                    MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Question)

        If result = DialogResult.Cancel Then
            ' キャンセル
            e.Cancel = True
            Return
        ElseIf result = DialogResult.Yes Then
            ' 保存実行
            _viewModel.SaveCurrentCharacter()
        End If

        ' リソースの解放
        DisposeChildForms()
        _viewModel.Dispose()
    End Sub

    ''' <summary>
    ''' マスターデータ更新マネージャーの初期化
    ''' </summary>
    Private Shared Sub InitializeMasterDataUpdater()
        Try
            ' 更新マネージャーの初期化
            MasterDataUpdateManager.Instance.InitializeOnStartup()
        Catch ex As Exception
            Debug.WriteLine($"マスターデータ更新マネージャーの初期化に失敗: {ex.Message}")
            ' エラーがあっても続行（ローカルファイルを使用）
        End Try
    End Sub
End Class