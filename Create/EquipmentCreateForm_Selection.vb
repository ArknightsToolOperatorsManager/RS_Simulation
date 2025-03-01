Namespace CreatorSystem.Equipment

    ''' <summary>
    ''' 装備生成フォーム - 選択処理部分
    ''' </summary>
    Partial Public Class EquipmentCreateForm
#Region "ベースアイテム選択"
        Private Sub PartFilterBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PartFilterSelectBox.SelectedIndexChanged
            UpdateBaseItemList()
        End Sub

        Private Sub BaseItemSelectBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles BaseItemSelectBox.SelectedIndexChanged
            UpdateSelectedBaseItem()
        End Sub

        Private Sub UpdateBaseItemList()
            Dim selectedPart = PartFilterSelectBox.SelectedItem.ToString()

            ' 武器の場合のみ職業でフィルタ
            If selectedPart = "武器" Then
                ' 職業に応じた武器一覧を取得
                Dim weaponList = _equipData.GetItemsByPart(CURRENT_JOB).Select(Function(item) item.Name)
                BaseItemSelectBox.DataSource = weaponList.ToList()
            Else
                BaseItemSelectBox.DataSource = _equipData.GetItemNamesByPart(selectedPart).ToList()
            End If
        End Sub

        Private Sub UpdateSelectedBaseItem()
            If BaseItemSelectBox.SelectedItem Is Nothing Then
                _selectedBaseItem = Nothing
                Return
            End If

            ' 選択されたアイテムの情報を取得
            Dim itemName = BaseItemSelectBox.SelectedItem.ToString()
            _selectedBaseItem = _equipData.GetItemByName(itemName)

            ' UI更新
            UpdateNxDisplay()
            UpdateSmeltControls()
        End Sub
#End Region

#Region "称号選択"
        Private Sub PrefixCategoryBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _
            Prefix1CategorySelectBox.SelectedIndexChanged,
            Prefix2CategorySelectBox.SelectedIndexChanged,
            Prefix3CategorySelectBox.SelectedIndexChanged

            ' 変更されたコントロールのインデックスを取得
            Dim setIndex = GetPrefixSetIndex(DirectCast(sender, ComboBox))
            If setIndex > 0 Then
                UpdatePrefixOptions(setIndex)
            End If
        End Sub

        Private Sub PrefixSelectBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _
            Prefix1SelectBox.SelectedIndexChanged,
            Prefix2SelectBox.SelectedIndexChanged,
            Prefix3SelectBox.SelectedIndexChanged

            ' 変更されたコントロールのインデックスを取得
            Dim setIndex = GetPrefixSetIndex(DirectCast(sender, ComboBox))
            If setIndex > 0 Then
                UpdatePrefixRanks(setIndex)
            End If
        End Sub

        Private Function GetPrefixSetIndex(control As ComboBox) As Integer
            ' コントロール名から称号セットのインデックスを取得
            Dim match = System.Text.RegularExpressions.Regex.Match(control.Name, "Prefix(\d)")
            If match.Success Then
                Return Integer.Parse(match.Groups(1).Value)
            End If
            Return 0
        End Function

        Private Sub UpdatePrefixOptions(setIndex As Integer)
            Dim categoryBox = DirectCast(Me.Controls.Find($"Prefix{setIndex}CategorySelectBox", True)(0), ComboBox)
            Dim prefixBox = DirectCast(Me.Controls.Find($"Prefix{setIndex}SelectBox", True)(0), ComboBox)

            ' 選択された系統の称号一覧を取得
            Dim selectedCategory = categoryBox.SelectedItem.ToString()
            Dim prefixes = _prefixData.GetByCategory(selectedCategory)
            prefixBox.DataSource = prefixes.Select(Function(p) p.Name).ToList()
        End Sub

        Private Sub UpdatePrefixRanks(setIndex As Integer)
            Dim prefixBox = DirectCast(Me.Controls.Find($"Prefix{setIndex}SelectBox", True)(0), ComboBox)
            Dim rankBox = DirectCast(Me.Controls.Find($"Prefix{setIndex}RankSelectBox", True)(0), ComboBox)

            ' 選択された称号のランク一覧を取得
            If prefixBox.SelectedItem IsNot Nothing Then
                Dim selectedPrefix = prefixBox.SelectedItem.ToString()
                Dim prefix = _prefixData.FindByName(selectedPrefix)
                If prefix IsNot Nothing Then
                    rankBox.Text = prefix.Rank
                End If
            End If
        End Sub
#End Region

#Region "錬成選択"
        Private Sub UpdateNxDisplay()
            If _selectedBaseItem Is Nothing Then Return

            ' 錬成値の表示
            For i As Integer = 1 To 4
                Dim opSelectBox = DirectCast(Me.Controls.Find($"Nx{i}SelectBox", True)(0), ComboBox)
                Dim valueTextBox = DirectCast(Me.Controls.Find($"Nx{i}ValueTextBox", True)(0), TextBox)

                ' 現在値の表示
                If _selectedBaseItem.NxOptions.ContainsKey(i) Then
                    Dim currentOp = _selectedBaseItem.NxOptions(i).FirstOrDefault()
                    If currentOp IsNot Nothing Then
                        opSelectBox.SelectedItem = currentOp.OptionName
                        valueTextBox.Text = currentOp.OptionValue
                    End If
                End If
            Next
        End Sub
#End Region

#Region "精錬選択"
        Private Sub UpdateSmeltControls()
            If _selectedBaseItem Is Nothing Then Return

            ' ランク表示を更新
            EquipRankLabel.Text = _selectedBaseItem.Rank

            ' 部位名を取得
            Dim partName = PartFilterSelectBox.SelectedItem.ToString()

            ' 段階ごとのオプション選択肢を更新
            For i As Integer = 1 To 5
                Dim stage = GetStageFromIndex(i)
                Dim options = _smeltManager.GetOptionNames(partName, stage)

                Dim opSelectBox = DirectCast(Me.Controls.Find($"Smelt{i}OpSelectBox", True)(0), ComboBox)
                opSelectBox.DataSource = options
            Next
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
    End Class

End Namespace