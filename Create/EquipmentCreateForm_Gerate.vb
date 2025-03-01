Namespace CreatorSystem.Equipment

    ''' <summary>
    ''' 装備生成フォーム - 装備生成処理部分
    ''' </summary>
    Partial Public Class EquipmentCreateForm
#Region "装備生成"
        Private Sub EquipGenerateButton_Click(sender As Object, e As EventArgs) Handles EquipGenerateButton.Click
            If ValidateSelections() Then
                GenerateEquipment()
            End If
        End Sub

        ''' <summary>
        ''' 装備生成に必要な選択が揃っているかチェック
        ''' </summary>
        Private Function ValidateSelections() As Boolean
            ' ベースアイテムのチェック
            If _selectedBaseItem Is Nothing Then
                MessageBox.Show("ベースアイテムを選択してください。")
                Return False
            End If

            ' 称号選択のチェック
            ' 現時点では必須としない

            ' 精錬ステップのチェック
            If SmeltStepBox.Value < 1 OrElse SmeltStepBox.Value > 20 Then
                MessageBox.Show("精錬ステップは1-20の範囲で指定してください。")
                Return False
            End If

            Return True
        End Function

        ''' <summary>
        ''' 装備生成処理
        ''' </summary>
        Private Sub GenerateEquipment()
            Try
                ' 称号情報の取得
                Dim prefixOptions = GetSelectedPrefixOptions()

                ' 錬成情報の取得
                Dim nxOptions = GetSelectedNxOptions()

                ' 精錬情報の取得
                Dim smeltOptions = GetSelectedSmeltOptions()

                _inventory.AddInventoryItem(
                    "",
                    _selectedBaseItem.Name,
                    prefixOptions,
                    nxOptions,
                    smeltOptions
                )
            Catch ex As Exception
                MessageBox.Show($"装備生成中にエラーが発生しました。{Environment.NewLine}{ex.Message}")
            End Try
        End Sub

        ''' <summary>
        ''' 選択された称号オプションを取得
        ''' </summary>
        Private Function GetSelectedPrefixOptions() As List(Of OpValue)
            Dim options As New List(Of OpValue)

            For i As Integer = 1 To 3
                Dim prefixBox = DirectCast(Me.Controls.Find($"Prefix{i}SelectBox", True)(0), ComboBox)
                If prefixBox.SelectedItem IsNot Nothing Then
                    Dim prefix = _prefixData.FindByName(prefixBox.SelectedItem.ToString())
                    If prefix IsNot Nothing Then
                        options.Add(New OpValue(prefix.Op, prefix.Value.ToString()))
                    End If
                End If
            Next

            Return options
        End Function

        ''' <summary>
        ''' 選択された錬成オプションを取得
        ''' </summary>
        Private Function GetSelectedNxOptions() As List(Of OpValue)
            Dim options As New List(Of OpValue)

            For i As Integer = 1 To 4
                Dim opBox = DirectCast(Me.Controls.Find($"Nx{i}SelectBox", True)(0), ComboBox)
                Dim valueBox = DirectCast(Me.Controls.Find($"Nx{i}ValueTextBox", True)(0), TextBox)

                If opBox.SelectedItem IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(valueBox.Text) Then
                    options.Add(New OpValue(opBox.SelectedItem.ToString(), valueBox.Text))
                End If
            Next

            Return options
        End Function

        ''' <summary>
        ''' 選択された精錬オプションを取得
        ''' </summary>
        Private Function GetSelectedSmeltOptions() As List(Of OpValue)
            Dim options As New List(Of OpValue)
            Dim partName = PartFilterSelectBox.SelectedItem.ToString()

            For i As Integer = 1 To 5
                Dim opBox = DirectCast(Me.Controls.Find($"Smelt{i}OpSelectBox", True)(0), ComboBox)
                If opBox.SelectedItem IsNot Nothing Then
                    Dim stage = GetStageFromIndex(i)
                    Dim opName = opBox.SelectedItem.ToString()
                    Dim value = _smeltManager.GetValue(partName, stage, opName, _selectedBaseItem.Rank, SmeltStepBox.Value)
                    options.Add(New OpValue(opName, value.ToString()))
                End If
            Next

            Return options
        End Function
#End Region
    End Class

End Namespace