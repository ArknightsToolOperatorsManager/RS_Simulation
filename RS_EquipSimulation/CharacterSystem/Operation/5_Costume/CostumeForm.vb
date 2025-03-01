' CostumeForm.vb
Imports RS_EquipSimulation.CharacterSystem.Operation.Costume

Public Class CostumeForm
    ' ビューモデル
    Private _viewModel As CostumeViewModel

    ' コントロール配列
    Private _crestColorComboBoxes As ComboBox()
    Private _crestNameComboBoxes As ComboBox()
    Private _crestRankComboBoxes As ComboBox()

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        ' このコールは、デザイナーで必要です。
        InitializeComponent()

        ' コントロール配列を初期化
        _crestColorComboBoxes = {
            CrestColor1ComboBox, CrestColor2ComboBox, CrestColor3ComboBox,
            CrestColor4ComboBox, CrestColor5ComboBox, CrestColor6ComboBox
        }

        _crestNameComboBoxes = {
            Crest1ComboBox, Crest2ComboBox, Crest3ComboBox,
            Crest4ComboBox, Crest5ComboBox, Crest6ComboBox
        }

        _crestRankComboBoxes = {
            Crest1RankComboBox, Crest2RankComboBox, Crest3RankComboBox,
            Crest4RankComboBox, Crest5RankComboBox, Crest6RankComboBox
        }
    End Sub

    ''' <summary>
    ''' ビューモデルを設定
    ''' </summary>
    ''' <param name="viewModel">ビューモデル</param>
    Public Sub SetViewModel(viewModel As CostumeViewModel)
        _viewModel = viewModel

        ' データバインディング
        NumericUpDown1.DataBindings.Add("Value", _viewModel, "CostumeStatus", True, DataSourceUpdateMode.OnPropertyChanged)
        NumericUpDown2.DataBindings.Add("Value", _viewModel, "CostumeExp", True, DataSourceUpdateMode.OnPropertyChanged)

        ' 色コンボボックスを初期化
        For Each colorComboBox In _crestColorComboBoxes
            colorComboBox.DataSource = New BindingSource(_viewModel.ColorList, Nothing)
            colorComboBox.SelectedIndex = -1
        Next

        ' 現在のクレスト設定を反映
        LoadCrestSettings()

        ' イベントハンドラを設定
        SetupEventHandlers()
    End Sub

    ''' <summary>
    ''' クレスト設定を読み込む
    ''' </summary>
    Private Sub LoadCrestSettings()
        For i As Integer = 0 To 5
            Dim crest = _viewModel.GetCrest(i)
            If crest IsNot Nothing AndAlso crest.IsSet() Then
                _crestColorComboBoxes(i).SelectedItem = crest.Color

                ' クレスト名コンボボックスを更新
                UpdateCrestNameComboBox(i)
                _crestNameComboBoxes(i).SelectedItem = crest.Name

                ' クレストランクコンボボックスを更新
                UpdateCrestRankComboBox(i)
                _crestRankComboBoxes(i).SelectedItem = crest.Rank
            End If
        Next
    End Sub

    ''' <summary>
    ''' イベントハンドラを設定
    ''' </summary>
    Private Sub SetupEventHandlers()
        ' 色コンボボックスのイベント
        For i As Integer = 0 To 5
            Dim index = i
            AddHandler _crestColorComboBoxes(i).SelectedIndexChanged, Sub(sender, e) ColorComboBox_SelectedIndexChanged(index)
            AddHandler _crestNameComboBoxes(i).SelectedIndexChanged, Sub(sender, e) NameComboBox_SelectedIndexChanged(index)
            AddHandler _crestRankComboBoxes(i).SelectedIndexChanged, Sub(sender, e) RankComboBox_SelectedIndexChanged(index)
        Next
    End Sub

    ''' <summary>
    ''' 色コンボボックスの選択変更時
    ''' </summary>
    ''' <param name="slotIndex">スロットインデックス</param>
    Private Sub ColorComboBox_SelectedIndexChanged(slotIndex As Integer)
        UpdateCrestNameComboBox(slotIndex)
        UpdateCrest(slotIndex)
    End Sub

    ''' <summary>
    ''' クレスト名コンボボックスの選択変更時
    ''' </summary>
    ''' <param name="slotIndex">スロットインデックス</param>
    Private Sub NameComboBox_SelectedIndexChanged(slotIndex As Integer)
        UpdateCrestRankComboBox(slotIndex)
        UpdateCrest(slotIndex)
    End Sub

    ''' <summary>
    ''' クレストランクコンボボックスの選択変更時
    ''' </summary>
    ''' <param name="slotIndex">スロットインデックス</param>
    Private Sub RankComboBox_SelectedIndexChanged(slotIndex As Integer)
        UpdateCrest(slotIndex)
    End Sub

    ''' <summary>
    ''' クレスト名コンボボックスを更新
    ''' </summary>
    ''' <param name="slotIndex">スロットインデックス</param>
    Private Sub UpdateCrestNameComboBox(slotIndex As Integer)
        Dim colorComboBox = _crestColorComboBoxes(slotIndex)
        Dim nameComboBox = _crestNameComboBoxes(slotIndex)

        If colorComboBox.SelectedIndex >= 0 Then
            Dim color = colorComboBox.SelectedItem.ToString()
            Dim names = _viewModel.GetCrestNamesByColor(color)

            nameComboBox.DataSource = names
            nameComboBox.SelectedIndex = If(names.Count > 0, 0, -1)
        Else
            nameComboBox.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' クレストランクコンボボックスを更新
    ''' </summary>
    ''' <param name="slotIndex">スロットインデックス</param>
    Private Sub UpdateCrestRankComboBox(slotIndex As Integer)
        Dim colorComboBox = _crestColorComboBoxes(slotIndex)
        Dim nameComboBox = _crestNameComboBoxes(slotIndex)
        Dim rankComboBox = _crestRankComboBoxes(slotIndex)

        If colorComboBox.SelectedIndex >= 0 AndAlso nameComboBox.SelectedIndex >= 0 Then
            Dim color = colorComboBox.SelectedItem.ToString()
            Dim name = nameComboBox.SelectedItem.ToString()
            Dim ranks = _viewModel.GetAvailableRanks(color, name)

            rankComboBox.DataSource = ranks
            rankComboBox.SelectedIndex = If(ranks.Count > 0, 0, -1)
        Else
            rankComboBox.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' クレスト設定を更新
    ''' </summary>
    ''' <param name="slotIndex">スロットインデックス</param>
    Private Sub UpdateCrest(slotIndex As Integer)
        Dim colorComboBox = _crestColorComboBoxes(slotIndex)
        Dim nameComboBox = _crestNameComboBoxes(slotIndex)
        Dim rankComboBox = _crestRankComboBoxes(slotIndex)

        If colorComboBox.SelectedIndex >= 0 AndAlso
           nameComboBox.SelectedIndex >= 0 AndAlso
           rankComboBox.SelectedIndex >= 0 Then

            Dim color = colorComboBox.SelectedItem.ToString()
            Dim name = nameComboBox.SelectedItem.ToString()
            Dim rank = rankComboBox.SelectedItem.ToString()

            _viewModel.SetCrest(slotIndex, color, name, rank)
        Else
            _viewModel.ClearCrest(slotIndex)
        End If
    End Sub
End Class