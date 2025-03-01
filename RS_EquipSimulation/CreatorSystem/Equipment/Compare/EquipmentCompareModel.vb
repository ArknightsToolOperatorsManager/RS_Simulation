Imports RS_EquipSimulation.Common.Equipment

Namespace CreatorSystem.Equipment

    Public Class EquipCompareManager
        Private ReadOnly _gridView As New DataGridView

        Public Sub New()
            InitializeColumns()
        End Sub

        Private Sub InitializeColumns()
            With _gridView
                .Columns.Clear()
                .AutoGenerateColumns = False

                ' オプション名列
                .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "OpName",
                .HeaderText = "性能",
                .Width = 120,
                .DefaultCellStyle = New DataGridViewCellStyle With {
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            })

                ' 装備1の値
                .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "Item1Value",
                .HeaderText = "装備1",
                .Width = 50,
                .DefaultCellStyle = New DataGridViewCellStyle With {
                    .Alignment = DataGridViewContentAlignment.MiddleRight
                }
            })

                ' 装備2の値
                .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "Item2Value",
                .HeaderText = "装備2",
                .Width = 50,
                .DefaultCellStyle = New DataGridViewCellStyle With {
                    .Alignment = DataGridViewContentAlignment.MiddleRight
                }
            })

                ' 差分値
                .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "DiffValue",
                .HeaderText = "比較",
                .Width = 50,
                .DefaultCellStyle = New DataGridViewCellStyle With {
                    .Alignment = DataGridViewContentAlignment.MiddleRight
                }
            })
            End With
        End Sub

        Public Sub DisplaySingleItem(item As EquipInventoryItem)
            _gridView.Rows.Clear()
            SetColumnHeaders("性能", "値", "", "")

            ' 2,3列を非表示
            _gridView.Columns("Item2Value").Visible = False
            _gridView.Columns("DiffValue").Visible = False

            DisplayOptionValues(item)
        End Sub

        Public Sub CompareItems(item1 As EquipInventoryItem, item2 As EquipInventoryItem)
            _gridView.Rows.Clear()
            SetColumnHeaders("性能", "装備1", "装備2", "差分")

            ' すべての列を表示
            For Each col As DataGridViewColumn In _gridView.Columns
                col.Visible = True
            Next

            CompareOptionValues(item1, item2)
        End Sub

        Public Sub ClearDisplay()
            _gridView.Rows.Clear()
        End Sub

        Private Sub DisplayOptionValues(item As EquipInventoryItem)
            Dim optionSums = CalcOptionSums(item)

            For Each kvp In optionSums.OrderBy(Function(x) x.Key)
                Dim rowIndex = _gridView.Rows.Add(kvp.Key, kvp.Value, "", "")
                ApplyValueStyle(_gridView.Rows(rowIndex).Cells("Item1Value"), kvp.Value)
            Next
        End Sub

        Private Sub CompareOptionValues(item1 As EquipInventoryItem, item2 As EquipInventoryItem)
            Dim sums1 = CalcOptionSums(item1)
            Dim sums2 = CalcOptionSums(item2)
            Dim allOptions = sums1.Keys.Union(sums2.Keys).OrderBy(Function(x) x)

            For Each opName In allOptions
                Dim value1 = If(sums1.ContainsKey(opName), sums1(opName), 0)
                Dim value2 = If(sums2.ContainsKey(opName), sums2(opName), 0)
                Dim diff = value1 - value2

                Dim rowIndex = _gridView.Rows.Add(opName, value1, value2, GetDiffText(diff))

                With _gridView.Rows(rowIndex)
                    ApplyValueStyle(.Cells("Item1Value"), value1)
                    ApplyValueStyle(.Cells("Item2Value"), value2)
                    ApplyDiffStyle(.Cells("DiffValue"), diff)
                End With
            Next
        End Sub

        Private Function CalcOptionSums(item As EquipInventoryItem) As Dictionary(Of String, Integer)
            Dim sums As New Dictionary(Of String, Integer)

            For Each ops In {item.PrefixOptions, item.NxOptions, item.SmeltOptions}
                For Each op In ops
                    If Not sums.ContainsKey(op.OptionName) Then
                        sums(op.OptionName) = 0
                    End If

                    ' 値が比率形式かどうかをチェック
                    If op.OptionValue.Contains("/"c) Then
                        sums(op.OptionName) += CalcRatioValue(op.OptionValue)
                    Else
                        sums(op.OptionName) += CInt(op.OptionValue)
                    End If
                Next
            Next

            Return sums
        End Function

        '仮の固定値 キャラクタマネージャ等から取得
        Private Const FIXED_LEVEL As Integer = 2000

        Private Function CalcRatioValue(ratioText As String) As Integer
            ' "1/2" のような文字列から計算
            Dim parts = ratioText.Split("/"c)
            If parts.Length = 2 Then
                Dim numerator As Integer
                Dim denominator As Integer
                If Integer.TryParse(parts(0), numerator) AndAlso
           Integer.TryParse(parts(1), denominator) AndAlso
           denominator <> 0 Then
                    Return CInt(FIXED_LEVEL * numerator / denominator)
                End If
            End If
            Return 0  ' パース失敗時は0を返す
        End Function

        Private Sub SetColumnHeaders(opName As String, value1 As String, value2 As String, diff As String)
            _gridView.Columns("OpName").HeaderText = opName
            _gridView.Columns("Item1Value").HeaderText = value1
            _gridView.Columns("Item2Value").HeaderText = value2
            _gridView.Columns("DiffValue").HeaderText = diff
        End Sub

        Private Function GetDiffText(diff As Integer) As String
            If diff = 0 Then Return "0"
            Return If(diff > 0, "+" & diff.ToString(), diff.ToString())
        End Function

        Private Sub ApplyValueStyle(cell As DataGridViewCell, value As Integer)
            If value = 0 Then
                cell.Style.ForeColor = SystemColors.GrayText
            End If
        End Sub

        Private Sub ApplyDiffStyle(cell As DataGridViewCell, diff As Integer)
            If diff > 0 Then
                cell.Style.ForeColor = Color.Blue
            ElseIf diff < 0 Then
                cell.Style.ForeColor = Color.Red
            Else
                cell.Style.ForeColor = SystemColors.GrayText
            End If
        End Sub
    End Class

End Namespace