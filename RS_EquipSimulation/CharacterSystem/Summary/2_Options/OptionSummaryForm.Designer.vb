
Namespace CharacterSystem.Summary.Option

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class OptionSummaryForm
        Inherits System.Windows.Forms.Form

        'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Windows フォーム デザイナーで必要です。
        Private components As System.ComponentModel.IContainer

        'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
        'Windows フォーム デザイナーを使用して変更できます。  
        'コード エディターを使って変更しないでください。
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            MainPanel = New Panel()
            FilterPanel = New FlowLayoutPanel()
            OptionListView = New ListView()
            OptionNameColumn = New ColumnHeader()
            OptionValueColumn = New ColumnHeader()
            MainPanel.SuspendLayout()
            SuspendLayout()
            ' 
            ' MainPanel
            ' 
            MainPanel.Controls.Add(FilterPanel)
            MainPanel.Controls.Add(OptionListView)
            MainPanel.Dock = DockStyle.Fill
            MainPanel.Location = New Point(0, 0)
            MainPanel.Name = "MainPanel"
            MainPanel.Size = New Size(302, 421)
            MainPanel.TabIndex = 0
            ' 
            ' FilterPanel
            ' 
            FilterPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            FilterPanel.Location = New Point(5, 5)
            FilterPanel.Name = "FilterPanel"
            FilterPanel.Size = New Size(292, 75)
            FilterPanel.TabIndex = 0
            ' 
            ' OptionListView
            ' 
            OptionListView.Columns.AddRange(New ColumnHeader() {OptionNameColumn, OptionValueColumn})
            OptionListView.Dock = DockStyle.Bottom
            OptionListView.FullRowSelect = True
            OptionListView.GridLines = True
            OptionListView.Location = New Point(0, 86)
            OptionListView.Name = "OptionListView"
            OptionListView.Size = New Size(302, 335)
            OptionListView.TabIndex = 1
            OptionListView.UseCompatibleStateImageBehavior = False
            OptionListView.View = View.Details
            ' 
            ' OptionNameColumn
            ' 
            OptionNameColumn.Text = "オプション名"
            OptionNameColumn.Width = 180
            ' 
            ' OptionValueColumn
            ' 
            OptionValueColumn.Text = "合計値"
            OptionValueColumn.TextAlign = HorizontalAlignment.Right
            OptionValueColumn.Width = 95
            ' 
            ' OptionsSummaryForm
            ' 
            AutoScaleDimensions = New SizeF(7.0F, 15.0F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(302, 421)
            Controls.Add(MainPanel)
            FormBorderStyle = FormBorderStyle.None
            Name = "OptionsSummaryForm"
            Text = "オプションサマリ"
            MainPanel.ResumeLayout(False)
            ResumeLayout(False)
        End Sub

        Friend WithEvents MainPanel As Panel
        Friend WithEvents FilterPanel As FlowLayoutPanel
        Friend WithEvents OptionListView As ListView
        Friend WithEvents OptionNameColumn As ColumnHeader
        Friend WithEvents OptionValueColumn As ColumnHeader

    End Class

End Namespace