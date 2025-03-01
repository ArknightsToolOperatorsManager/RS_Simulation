Namespace CreatorSystem.Creature

    ''' <summary>
    ''' クリーチャー生成フォーム - 選択処理部分
    ''' </summary>
    Partial Public Class CreatureCreateForm
#Region "レアリティ選択処理"
        Private Sub BaseRarityComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles BaseRarityComboBox.SelectedIndexChanged
            UpdateEnhancedRarityOptions()
            UpdatePassiveSelectionOptions()
            UpdatePassiveLevels()
            UpdateAwakeningLimits()
        End Sub

        Private Sub EnhancedRarityComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles EnhancedRarityComboBox.SelectedIndexChanged
            UpdatePassiveLevels()
        End Sub

        ''' <summary>
        ''' ベースレアリティに基づいて強化後レアリティの選択肢を更新
        ''' </summary>
        Private Sub UpdateEnhancedRarityOptions()
            If BaseRarityComboBox.SelectedItem Is Nothing Then Return

            Dim baseRarity = BaseRarityComboBox.SelectedItem.ToString()

            ' LR以上のベースレアリティは強化後レアリティを変更できない（同じ値に固定）
            If baseRarity = "LR" OrElse baseRarity = "GR" Then
                EnhancedRarityComboBox.SelectedItem = baseRarity
                EnhancedRarityComboBox.Enabled = False
                Return
            End If

            ' SR以下のベースレアリティはLRまで強化可能
            EnhancedRarityComboBox.Enabled = True

            ' 選択可能な強化後レアリティを決定
            Dim baseIndex = Array.IndexOf(_rarityList, baseRarity)
            If baseIndex < 0 Then Return

            ' 現在の選択を保持
            Dim currentSelection = EnhancedRarityComboBox.SelectedItem

            ' 有効なアイテムだけを残す
            EnhancedRarityComboBox.Items.Clear()

            ' ベースレアリティ以上、LR以下の選択肢を追加
            For i As Integer = baseIndex To Array.IndexOf(_rarityList, "LR")
                EnhancedRarityComboBox.Items.Add(_rarityList(i))
            Next

            ' 可能であれば以前の選択を復元、不可能ならベースレアリティを選択
            If currentSelection IsNot Nothing AndAlso EnhancedRarityComboBox.Items.Contains(currentSelection) Then
                EnhancedRarityComboBox.SelectedItem = currentSelection
            Else
                EnhancedRarityComboBox.SelectedItem = baseRarity
            End If
        End Sub

        ''' <summary>
        ''' ベースレアリティに基づいてパッシブ選択肢をフィルタリング
        ''' </summary>
        Private Sub UpdatePassiveSelectionOptions()
            If BaseRarityComboBox.SelectedItem Is Nothing Then Return

            Dim baseRarity = BaseRarityComboBox.SelectedItem.ToString()

            ' 現在の選択を保持
            Dim mainSelection = MainPassiveNameComboBox.SelectedItem
            Dim sub1Selection = SubPassive1NameComboBox.SelectedItem
            Dim sub2Selection = SubPassive2NameComboBox.SelectedItem
            Dim sub3Selection = SubPassive3NameComboBox.SelectedItem

            ' パッシブリストを取得（レアリティに基づいてフィルタリング）
            Dim filteredPassives = GetFilteredPassiveNames(baseRarity)

            ' サブパッシブコンボボックスを更新
            SubPassive1NameComboBox.Items.Clear()
            SubPassive1NameComboBox.Items.AddRange(filteredPassives.ToArray())

            ' サブパッシブ1以外は空白の選択肢を追加
            Dim filteredPassivesWithEmpty = New List(Of String)(filteredPassives)
            filteredPassivesWithEmpty.Insert(0, "")  ' 先頭に空白項目を追加
            ' メインパッシブコンボボックスを更新
            MainPassiveNameComboBox.Items.Clear()
            MainPassiveNameComboBox.Items.AddRange(filteredPassivesWithEmpty.ToArray())

            ' サブパッシブコンボボックスを更新
            SubPassive2NameComboBox.Items.Clear()
            SubPassive2NameComboBox.Items.AddRange(filteredPassivesWithEmpty.ToArray())
            SubPassive3NameComboBox.Items.Clear()
            SubPassive3NameComboBox.Items.AddRange(filteredPassivesWithEmpty.ToArray())

            ' 可能であれば以前の選択を復元
            If mainSelection IsNot Nothing AndAlso MainPassiveNameComboBox.Items.Contains(mainSelection) Then
                MainPassiveNameComboBox.SelectedItem = mainSelection
            ElseIf MainPassiveNameComboBox.Items.Count > 0 Then
                MainPassiveNameComboBox.SelectedIndex = 0
            End If

            If sub1Selection IsNot Nothing AndAlso SubPassive1NameComboBox.Items.Contains(sub1Selection) Then
                SubPassive1NameComboBox.SelectedItem = sub1Selection
            ElseIf SubPassive1NameComboBox.Items.Count > 0 Then
                SubPassive1NameComboBox.SelectedIndex = 0
            End If

            If sub2Selection IsNot Nothing AndAlso SubPassive2NameComboBox.Items.Contains(sub2Selection) Then
                SubPassive2NameComboBox.SelectedItem = sub2Selection
            ElseIf SubPassive2NameComboBox.Items.Count > 0 Then
                SubPassive2NameComboBox.SelectedIndex = 0
            End If

            If sub3Selection IsNot Nothing AndAlso SubPassive3NameComboBox.Items.Contains(sub3Selection) Then
                SubPassive3NameComboBox.SelectedItem = sub3Selection
            ElseIf SubPassive3NameComboBox.Items.Count > 0 Then
                SubPassive3NameComboBox.SelectedIndex = 0
            End If
        End Sub
#End Region

#Region "パッシブ選択処理"
        Private Sub MainPassiveNameComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MainPassiveNameComboBox.SelectedIndexChanged
            UpdatePassiveLevels()
            UpdateAwakeningLimits()
        End Sub

        Private Sub SubPassiveNameComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SubPassive1NameComboBox.SelectedIndexChanged, SubPassive2NameComboBox.SelectedIndexChanged, SubPassive3NameComboBox.SelectedIndexChanged
            UpdatePassiveLevels()
            UpdateAwakeningLimits()
        End Sub

        ''' <summary>
        ''' レアリティに基づいてフィルタリングされたパッシブ名のリストを取得
        ''' </summary>
        Private Function GetFilteredPassiveNames(rarity As String) As List(Of String)
            ' すべてのパッシブ効果を取得
            Dim passiveEffects = _creaturePassiveEffectData.GetAllPassiveEffects()

            ' LR、GRの場合は上級パッシブのみフィルタリング
            If rarity = "LR" OrElse rarity = "GR" Then
                Return passiveEffects.
                Where(Function(pe) pe.Rank = "上級").
                Select(Function(pe) pe.Name).
                Distinct().
                OrderBy(Function(n) n).
                ToList()
            End If

            ' それ以外の場合はすべてのパッシブを返す
            Return passiveEffects.
                Select(Function(pe) pe.Name).
                Distinct().
                OrderBy(Function(n) n).
                ToList()
        End Function

        ''' <summary>
        ''' レアリティとパッシブ名に基づいてパッシブレベルを更新
        ''' </summary>
        Private Sub UpdatePassiveLevels()
            ' 必要な選択がされているか確認
            If BaseRarityComboBox.SelectedItem Is Nothing OrElse
               EnhancedRarityComboBox.SelectedItem Is Nothing OrElse
               MainPassiveNameComboBox.SelectedItem Is Nothing OrElse
               SubPassive1NameComboBox.SelectedItem Is Nothing OrElse
               SubPassive2NameComboBox.SelectedItem Is Nothing OrElse
               SubPassive3NameComboBox.SelectedItem Is Nothing Then
                Return
            End If

            Dim baseRarity = BaseRarityComboBox.SelectedItem.ToString()
            Dim enhancedRarity = EnhancedRarityComboBox.SelectedItem.ToString()

            ' メインパッシブのレベル設定
            Dim mainPassiveName = MainPassiveNameComboBox.SelectedItem.ToString()
            Dim mainPassiveEffect = _creaturePassiveEffectData.GetPassiveEffect(mainPassiveName)

            If mainPassiveEffect IsNot Nothing Then
                Dim mainPassiveRank = mainPassiveEffect.Rank
                MainPassiveLevelNumeric.Value = GetPassiveLevel(mainPassiveRank, baseRarity, True)
            End If

            ' サブパッシブ1のレベル設定
            Dim subPassiveName = SubPassive1NameComboBox.SelectedItem.ToString()
            Dim subPassiveEffect = _creaturePassiveEffectData.GetPassiveEffect(subPassiveName)
            If subPassiveEffect IsNot Nothing Then
                Dim subPassiveRank = subPassiveEffect.Rank
                SubPassive1LevelNumeric.Value = GetPassiveLevel(subPassiveRank, enhancedRarity, False)
            End If

            ' サブパッシブ2のレベル設定
            subPassiveName = SubPassive2NameComboBox.SelectedItem.ToString()
            subPassiveEffect = _creaturePassiveEffectData.GetPassiveEffect(subPassiveName)
            If subPassiveEffect IsNot Nothing Then
                Dim subPassiveRank = subPassiveEffect.Rank
                SubPassive2LevelNumeric.Value = GetPassiveLevel(subPassiveRank, enhancedRarity, False)
            End If

            ' サブパッシブ3のレベル設定
            subPassiveName = SubPassive3NameComboBox.SelectedItem.ToString()
            subPassiveEffect = _creaturePassiveEffectData.GetPassiveEffect(subPassiveName)
            If subPassiveEffect IsNot Nothing Then
                Dim subPassiveRank = subPassiveEffect.Rank
                SubPassive3LevelNumeric.Value = GetPassiveLevel(subPassiveRank, enhancedRarity, False)
            End If
        End Sub

        ''' <summary>
        ''' レアリティとパッシブランクに基づいて覚醒値の上限を更新
        ''' </summary>
        Private Sub UpdateAwakeningLimits()
            If BaseRarityComboBox.SelectedItem Is Nothing Then Return

            Dim baseRarity = BaseRarityComboBox.SelectedItem.ToString()

            ' メインパッシブの覚醒値上限を設定
            Dim mainMax = GetAwakeningLimit(baseRarity, True)
            MainPassiveAwakeningNumeric.Maximum = mainMax
            MainPassiveAwakeningNumeric.Value = mainMax

            ' サブパッシブの覚醒値上限を設定
            Dim subMax = GetAwakeningLimit(baseRarity, False)
            SubPassive1AwakeningNumeric.Maximum = subMax
            SubPassive1AwakeningNumeric.Value = subMax

            SubPassive2AwakeningNumeric.Maximum = subMax
            SubPassive2AwakeningNumeric.Value = subMax

            SubPassive3AwakeningNumeric.Maximum = subMax
            SubPassive3AwakeningNumeric.Value = subMax
        End Sub

        ''' <summary>
        ''' パッシブのレベルを取得
        ''' </summary>
        Private Function GetPassiveLevel(passiveRank As String, rarity As String, isMain As Boolean) As Integer
            ' パッシブレベルテーブルに基づいて値を返す
            Select Case passiveRank
                Case "一般"
                    ' 一般パッシブのレベル表
                    Select Case rarity
                        Case "N"
                            Return If(isMain, 8, 4)
                        Case "R"
                            Return If(isMain, 16, 8)
                        Case "HR"
                            Return If(isMain, 24, 12)
                        Case "SR"
                            Return If(isMain, 32, 16)
                        Case "LR"
                            Return If(isMain, 32, 20)
                        Case "GR"
                            Return If(isMain, 32, 20)
                        Case Else
                            Return 0
                    End Select
                Case "上級"
                    ' 上級パッシブのレベル表
                    Select Case rarity
                        Case "HR"
                            Return If(isMain, 5, 3)
                        Case "SR"
                            Return If(isMain, 10, 5)
                        Case "LR"
                            Return If(isMain, 20, 10)
                        Case "GR"
                            Return If(isMain, 30, 15)
                        Case Else
                            Return 0
                    End Select
                Case Else
                    Return 0
            End Select
        End Function

        ''' <summary>
        ''' パッシブの覚醒値上限を取得
        ''' </summary>
        Private Function GetAwakeningLimit(rarity As String, isMain As Boolean) As Integer
            ' 覚醒値上限テーブルに基づいて値を返す
            Select Case rarity
                Case "N"
                    Return If(isMain, 4, 2)
                Case "R"
                    Return If(isMain, 8, 4)
                Case "HR"
                    Return If(isMain, 12, 6)
                Case "SR"
                    Return If(isMain, 16, 8)
                Case "LR", "GR"
                    Return If(isMain, 20, 10)
                Case Else
                    Return 0
            End Select
        End Function
#End Region
    End Class

End Namespace