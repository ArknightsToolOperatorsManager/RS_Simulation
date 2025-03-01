Namespace CreatorSystem.Creature

    ''' <summary>
    ''' クリーチャー生成フォーム - 初期化処理部分
    ''' </summary>
    Partial Public Class CreatureCreateForm
#Region "フォーム初期化"
        Private Sub CreatureCreateForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            InitializeControls()
        End Sub

        ''' <summary>
        ''' コントロールの初期化
        ''' </summary>
        Private Sub InitializeControls()
            Me.TopLevel = True
            InitializeRarityControls()
            InitializePassiveNameControls()
        End Sub

        ''' <summary>
        ''' レアリティ選択コントロールの初期化
        ''' </summary>
        Private Sub InitializeRarityControls()
            ' ベースレアリティの選択肢を設定
            BaseRarityComboBox.Items.Clear()
            BaseRarityComboBox.Items.AddRange(_rarityList)

            ' 強化後レアリティの選択肢を設定
            EnhancedRarityComboBox.Items.Clear()
            EnhancedRarityComboBox.Items.AddRange(_rarityList)

            ' 初期選択を設定
            If BaseRarityComboBox.Items.Count > 0 Then
                BaseRarityComboBox.SelectedIndex = 0
            End If
        End Sub

        ''' <summary>
        ''' パッシブ名選択コントロールの初期化
        ''' </summary>
        Private Sub InitializePassiveNameControls()
            ' パッシブ効果の名前を取得
            Dim passiveNames = GetPassiveNames()

            ' サブパッシブコンボボックスを設定
            SubPassive1NameComboBox.Items.Clear()
            SubPassive1NameComboBox.Items.AddRange(passiveNames.ToArray())

            ' サブパッシブ1以外は空白の選択肢を追加
            Dim passiveNamesWithEmpty = New List(Of String)(passiveNames)
            passiveNamesWithEmpty.Insert(0, "")  ' 先頭に空白項目を追加

            ' メインパッシブコンボボックスを設定
            MainPassiveNameComboBox.Items.Clear()
            MainPassiveNameComboBox.Items.AddRange(passiveNamesWithEmpty.ToArray())

            ' サブパッシブ2,3コンボボックスを設定
            SubPassive2NameComboBox.Items.Clear()
            SubPassive2NameComboBox.Items.AddRange(passiveNamesWithEmpty.ToArray())

            SubPassive3NameComboBox.Items.Clear()
            SubPassive3NameComboBox.Items.AddRange(passiveNamesWithEmpty.ToArray())

            ' 初期選択を設定
            If MainPassiveNameComboBox.Items.Count > 0 Then
                MainPassiveNameComboBox.SelectedIndex = 0
            End If

            If SubPassive1NameComboBox.Items.Count > 0 Then
                SubPassive1NameComboBox.SelectedIndex = 0
            End If

            If SubPassive2NameComboBox.Items.Count > 0 Then
                SubPassive2NameComboBox.SelectedIndex = 0
            End If

            If SubPassive3NameComboBox.Items.Count > 0 Then
                SubPassive3NameComboBox.SelectedIndex = 0
            End If
        End Sub

        ''' <summary>
        ''' パッシブ名のリストを取得
        ''' </summary>
        Private Function GetPassiveNames() As List(Of String)
            ' CreaturePassiveEffectSingletonModelから一意のパッシブ名を取得
            Dim passiveEffects = _creaturePassiveEffectData.GetAllPassiveEffects()
            Return passiveEffects.Select(Function(pe) pe.Name).Distinct().OrderBy(Function(n) n).ToList()
        End Function
#End Region

#Region "初期化ボタン処理"
        Private Sub SelectedInitButton_Click(sender As Object, e As EventArgs) Handles SelectedInitButton.Click
            ClearAllSelections()
        End Sub

        ''' <summary>
        ''' すべての選択をクリア
        ''' </summary>
        Private Sub ClearAllSelections()
            ' 基本情報のクリア
            NameTextBox.Text = ""

            ' レアリティ選択のクリア
            If BaseRarityComboBox.Items.Count > 0 Then
                BaseRarityComboBox.SelectedIndex = 0
            End If

            ' パッシブ選択のクリア
            If MainPassiveNameComboBox.Items.Count > 0 Then
                MainPassiveNameComboBox.SelectedIndex = 0
            End If

            If SubPassive1NameComboBox.Items.Count > 0 Then
                SubPassive1NameComboBox.SelectedIndex = 0
            End If

            If SubPassive2NameComboBox.Items.Count > 0 Then
                SubPassive2NameComboBox.SelectedIndex = 0
            End If

            If SubPassive3NameComboBox.Items.Count > 0 Then
                SubPassive3NameComboBox.SelectedIndex = 0
            End If

            ' 数値入力のクリア
            MainPassiveAwakeningNumeric.Value = 0
            SubPassive1AwakeningNumeric.Value = 0
            SubPassive2AwakeningNumeric.Value = 0
            SubPassive3AwakeningNumeric.Value = 0
        End Sub
#End Region
    End Class

End Namespace