Imports RS_EquipSimulation.Common.Creature

Namespace CreatorSystem.Creature

    ''' <summary>
    ''' クリーチャー生成フォーム - 生成処理部分
    ''' </summary>
    Partial Public Class CreatureCreateForm
#Region "クリーチャー生成"
        Private Sub CreatureGenerateButton_Click(sender As Object, e As EventArgs) Handles CreatureGenerateButton.Click
            If ValidateSelections() Then
                GenerateCreature()
            End If
        End Sub

        ''' <summary>
        ''' クリーチャー生成に必要な選択が揃っているかチェック
        ''' </summary>
        Private Function ValidateSelections() As Boolean
            ' 名前のチェック

            ' レアリティのチェック
            If BaseRarityComboBox.SelectedItem Is Nothing OrElse EnhancedRarityComboBox.SelectedItem Is Nothing Then
                MessageBox.Show("レアリティを選択してください。")
                Return False
            End If

            If SubPassive1NameComboBox.SelectedItem Is Nothing Then
                MessageBox.Show("サブパッシブを選択してください。")
                Return False
            End If

            Return True
        End Function

        ''' <summary>
        ''' クリーチャー生成処理
        ''' </summary>
        Private Sub GenerateCreature()
            Try
                ' 基本情報の取得
                Dim baseRarity = BaseRarityComboBox.SelectedItem.ToString()
                Dim enhancedRarity = EnhancedRarityComboBox.SelectedItem.ToString()

                ' パッシブリストの作成（有効なものだけ）
                Dim activePassives As New List(Of PassiveSkill)

                ' メインパッシブの情報取得（オプション）
                Dim mainPassive As PassiveSkill = Nothing
                If MainPassiveNameComboBox.SelectedItem IsNot Nothing AndAlso Not String.IsNullOrEmpty(MainPassiveNameComboBox.SelectedItem.ToString()) Then
                    Dim mainPassiveName = MainPassiveNameComboBox.SelectedItem.ToString()
                    Dim mainPassiveLevel = CInt(MainPassiveLevelNumeric.Value)
                    Dim mainPassiveAwakening = CInt(MainPassiveAwakeningNumeric.Value)
                    Dim mainPassiveRank = GetPassiveRank(mainPassiveName)
                    mainPassive = New PassiveSkill(mainPassiveName, mainPassiveRank, mainPassiveLevel, mainPassiveAwakening)
                    activePassives.Add(mainPassive)
                End If

                ' サブパッシブ1の情報取得（必須）
                Dim sub1PassiveName = SubPassive1NameComboBox.SelectedItem.ToString()
                Dim sub1PassiveLevel = CInt(SubPassive1LevelNumeric.Value)
                Dim sub1PassiveAwakening = CInt(SubPassive1AwakeningNumeric.Value)
                Dim sub1PassiveRank = GetPassiveRank(sub1PassiveName)
                Dim sub1Passive = New PassiveSkill(sub1PassiveName, sub1PassiveRank, sub1PassiveLevel, sub1PassiveAwakening)
                activePassives.Add(sub1Passive)

                ' サブパッシブ2の情報取得（オプション）
                Dim sub2Passive As PassiveSkill = Nothing
                If SubPassive2NameComboBox.SelectedItem IsNot Nothing AndAlso Not String.IsNullOrEmpty(SubPassive2NameComboBox.SelectedItem.ToString()) Then
                    Dim sub2PassiveName = SubPassive2NameComboBox.SelectedItem.ToString()
                    Dim sub2PassiveLevel = CInt(SubPassive2LevelNumeric.Value)
                    Dim sub2PassiveAwakening = CInt(SubPassive2AwakeningNumeric.Value)
                    Dim sub2PassiveRank = GetPassiveRank(sub2PassiveName)
                    sub2Passive = New PassiveSkill(sub2PassiveName, sub2PassiveRank, sub2PassiveLevel, sub2PassiveAwakening)
                    activePassives.Add(sub2Passive)
                End If

                ' サブパッシブ3の情報取得（オプション）
                Dim sub3Passive As PassiveSkill = Nothing
                If SubPassive3NameComboBox.SelectedItem IsNot Nothing AndAlso Not String.IsNullOrEmpty(SubPassive3NameComboBox.SelectedItem.ToString()) Then
                    Dim sub3PassiveName = SubPassive3NameComboBox.SelectedItem.ToString()
                    Dim sub3PassiveLevel = CInt(SubPassive3LevelNumeric.Value)
                    Dim sub3PassiveAwakening = CInt(SubPassive3AwakeningNumeric.Value)
                    Dim sub3PassiveRank = GetPassiveRank(sub3PassiveName)
                    sub3Passive = New PassiveSkill(sub3PassiveName, sub3PassiveRank, sub3PassiveLevel, sub3PassiveAwakening)
                    activePassives.Add(sub3Passive)
                End If

                ' 名前の設定 - 空白の場合はパッシブ名から自動生成
                Dim name As String = NameTextBox.Text
                If String.IsNullOrWhiteSpace(name) Then
                    ' activePassivesからパッシブ名を抽出してカンマ区切りで結合
                    name = String.Join(",", activePassives.Select(Function(p) p.Name))
                End If

                ' クリーチャーをインベントリに追加
                _inventory.AddInventoryItem(name, baseRarity, enhancedRarity,
                                    mainPassive,
                                    sub1Passive,
                                    sub2Passive,
                                    sub3Passive)

                MessageBox.Show("クリーチャーを生成しました。")
                ClearAllSelections()

            Catch ex As Exception
                MessageBox.Show($"クリーチャー生成中にエラーが発生しました。{Environment.NewLine}{ex.Message}")
            End Try
        End Sub

        ''' <summary>
        ''' パッシブ名からランク（一般/上級）を取得
        ''' </summary>
        Private Function GetPassiveRank(passiveName As String) As String
            Dim passiveEffect = _creaturePassiveEffectData.GetPassiveEffect(passiveName)
            Return If(passiveEffect IsNot Nothing, passiveEffect.Rank, "一般")
        End Function
#End Region
    End Class

End Namespace