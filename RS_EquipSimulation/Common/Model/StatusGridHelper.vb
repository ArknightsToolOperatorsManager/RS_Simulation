
Namespace CharacterSystem.Common

    ''' <summary>
    ''' ステータスグリッド表示の共通設定を提供するユーティリティクラス
    ''' </summary>
    Public Class StatusGridHelper

        ''' <summary>
        ''' DataGridViewの基本設定を行う
        ''' </summary>
        Public Shared Sub SetupStatusGrid(grid As DataGridView,
                                        isReadOnly As Boolean,
                                        showRowHeaders As Boolean)
            With grid
                .AutoGenerateColumns = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeRows = False
                .ReadOnly = isReadOnly
                .RowHeadersVisible = showRowHeaders
                .SelectionMode = If(isReadOnly,
                                  DataGridViewSelectionMode.FullRowSelect,
                                  DataGridViewSelectionMode.CellSelect)
                .EditMode = If(isReadOnly,
                             DataGridViewEditMode.EditProgrammatically,
                             DataGridViewEditMode.EditOnEnter)
                .ColumnHeadersDefaultCellStyle = New DataGridViewCellStyle With {
                            .WrapMode = DataGridViewTriState.False,
                            .Alignment = DataGridViewContentAlignment.MiddleCenter
        }
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

                ' 列をクリア
                .Columns.Clear()
            End With
        End Sub

        ''' <summary>
        ''' ステータスグリッドの共通列を追加
        ''' </summary>
        Public Shared Sub AddStatusColumns(grid As DataGridView)
            ' 行名の列
            grid.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "DisplayName",
                .HeaderText = "項目",
                .DataPropertyName = "DisplayName",
                .Width = 40,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                .ReadOnly = True,  ' 項目名は常に読み取り専用
                .DefaultCellStyle = New DataGridViewCellStyle With {
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            })

            ' 各ステータス列を追加
            AddStatusColumn(grid, "Power", "力")
            AddStatusColumn(grid, "Agility", "敏捷")
            AddStatusColumn(grid, "Health", "健康")
            AddStatusColumn(grid, "Knowledge", "知識")
            AddStatusColumn(grid, "Wisdom", "知恵")
            AddStatusColumn(grid, "Charisma", "威厳")
            AddStatusColumn(grid, "Luck", "運")
        End Sub

        ''' <summary>
        ''' 個別のステータス列を追加
        ''' </summary>
        Private Shared Sub AddStatusColumn(grid As DataGridView,
                                         propertyName As String,
                                         headerText As String)
            Dim column = New DataGridViewTextBoxColumn With {
                .Name = propertyName,
                .HeaderText = headerText,
                .DataPropertyName = propertyName,
                .Width = 44,
                .ReadOnly = grid.ReadOnly,  ' グリッド全体の設定に従う
                .DefaultCellStyle = New DataGridViewCellStyle With {
                    .Alignment = DataGridViewContentAlignment.MiddleRight,
                    .Format = "0"  ' 3桁区切りの数値表示
                }
            }

            grid.Columns.Add(column)
        End Sub

        ''' <summary>
        ''' 数値入力検証用のイベントハンドラを設定
        ''' </summary>
        Public Shared Sub SetupNumericValidation(grid As DataGridView)
            ' 既存のハンドラを削除（複数回呼び出し対応）
            RemoveHandler grid.EditingControlShowing, AddressOf EditingControlShowing
            AddHandler grid.EditingControlShowing, AddressOf EditingControlShowing
        End Sub

        ''' <summary>
        ''' 編集コントロール表示時のイベントハンドラ
        ''' </summary>
        Private Shared Sub EditingControlShowing(sender As Object,
                                              e As DataGridViewEditingControlShowingEventArgs)
            ' テキストボックス以外は無視
            If TypeOf e.Control IsNot TextBox Then Return

            Dim grid = DirectCast(sender, DataGridView)
            ' 現在編集中のセルが項目名列（ReadOnly）でない場合
            If grid.CurrentCell.ColumnIndex > 0 Then
                ' テキストボックスのKeyPressイベントハンドラを設定
                Dim textBox As TextBox = DirectCast(e.Control, TextBox)
                RemoveHandler textBox.KeyPress, AddressOf TextBox_KeyPress
                AddHandler textBox.KeyPress, AddressOf TextBox_KeyPress
            End If
        End Sub

        ''' <summary>
        ''' テキストボックスのKeyPressイベントハンドラ
        ''' </summary>
        Private Shared Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
            ' 数字、コントロールキー、バックスペース、マイナス記号以外は無効
            If Not (Char.IsDigit(e.KeyChar) OrElse
                   Char.IsControl(e.KeyChar) OrElse e.KeyChar = "-"c) Then
                e.Handled = True
            End If

            ' マイナス記号は先頭のみ許可
            If e.KeyChar = "-"c AndAlso DirectCast(sender, TextBox).SelectionStart > 0 Then
                e.Handled = True
            End If
        End Sub
    End Class

End Namespace