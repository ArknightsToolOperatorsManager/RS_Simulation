Imports RS_EquipSimulation.MasterSystem.Equipment

Namespace CreatorSystem.Equipment

    ''' <summary>
    ''' 装備生成フォーム - 検索処理部分
    ''' </summary>
    Partial Public Class EquipmentCreateForm
#Region "オプション検索"
        ''' <summary>
        ''' オプション種類のチェック状態変更時の処理
        ''' </summary>
        Private Sub OptionTypeListBox_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles OptionTypeListBox.ItemCheck
            ' チェック状態の変更を反映させるために、BeginInvokeを使用
            BeginInvoke(Sub()
                            UpdateOptionNameFilter()
                        End Sub)
        End Sub

        ''' <summary>
        ''' 選択されたオプション種類に基づいてオプション名をフィルタリング
        ''' </summary>
        Private Sub UpdateOptionNameFilter()
            ' 現在選択されているオプション名を保持
            Dim currentSelection = OptionNameComboBox.SelectedItem

            ' チェックされているオプション種類を取得
            Dim selectedTypes = GetSelectedOptionTypes()

            ' フィルタリングされたオプションリストを取得
            Dim filteredOptions = _optionData.GetOptionsByTypes(selectedTypes)

            ' コンボボックスを更新
            OptionNameComboBox.Items.Clear()
            OptionNameComboBox.Items.AddRange(filteredOptions.ToArray())

            ' 可能であれば前の選択を復元
            If currentSelection IsNot Nothing AndAlso OptionNameComboBox.Items.Contains(currentSelection) Then
                OptionNameComboBox.SelectedItem = currentSelection
            ElseIf OptionNameComboBox.Items.Count > 0 Then
                OptionNameComboBox.SelectedIndex = 0
            End If
        End Sub

        ''' <summary>
        ''' チェックされているオプション種類を取得
        ''' </summary>
        Private Function GetSelectedOptionTypes() As List(Of String)
            Dim selectedTypes As New List(Of String)

            For i As Integer = 0 To OptionTypeListBox.Items.Count - 1
                If OptionTypeListBox.GetItemChecked(i) Then
                    selectedTypes.Add(OptionTypeListBox.Items(i).ToString())
                End If
            Next

            Return selectedTypes
        End Function

        ''' <summary>
        ''' 部位とオプションによる装備検索
        ''' </summary>
        Private Sub SearchEquipment()
            If OptionNameComboBox.SelectedItem Is Nothing Then
                MessageBox.Show("検索するオプションを選択してください。")
                Return
            End If

            ' 検索条件の取得
            Dim optionName = OptionNameComboBox.SelectedItem.ToString()
            Dim partName = If(SearchPartComboBox.SelectedItem?.ToString(), "")

            ' 検索実行
            Dim searchResults = _equipData.FindItemsByOption(optionName, partName)

            ' 検索結果の表示
            DisplaySearchResults(searchResults)
        End Sub

        ''' <summary>
        ''' 検索結果をリストビューに表示
        ''' </summary>
        Private Sub DisplaySearchResults(results As IEnumerable(Of BaseItemModel))
            SearchResultListView.Items.Clear()

            For Each equip In results
                Dim item = New ListViewItem(equip.Name)
                item.SubItems.Add(equip.PartName)
                item.SubItems.Add(equip.Rank)

                ' オプションの詳細を取得
                Dim details = _equipData.GetOptionDetails(equip, OptionNameComboBox.SelectedItem.ToString())
                item.SubItems.Add(String.Join(", ", details))

                SearchResultListView.Items.Add(item)
            Next
        End Sub
#End Region
    End Class

End Namespace