Namespace CharacterSystem.Operation.Status

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class StatusForm
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
            dgvStatus = New DataGridView()
            Label1 = New Label()
            TotalAllocationLabel = New Label()
            Label2 = New Label()
            InfoAllocationLabel = New Label()
            TableLayoutPanel1 = New TableLayoutPanel()
            CType(dgvStatus, ComponentModel.ISupportInitialize).BeginInit()
            TableLayoutPanel1.SuspendLayout()
            SuspendLayout()
            ' 
            ' dgvStatus
            ' 
            dgvStatus.AllowUserToAddRows = False
            dgvStatus.AllowUserToDeleteRows = False
            dgvStatus.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            TableLayoutPanel1.SetColumnSpan(dgvStatus, 4)
            dgvStatus.Dock = DockStyle.Fill
            dgvStatus.Location = New Point(3, 35)
            dgvStatus.Name = "dgvStatus"
            dgvStatus.RowHeadersVisible = False
            dgvStatus.Size = New Size(369, 67)
            dgvStatus.TabIndex = 0
            ' 
            ' Label1
            ' 
            Label1.Anchor = AnchorStyles.None
            Label1.AutoSize = True
            Label1.Location = New Point(4, 8)
            Label1.Name = "Label1"
            Label1.Size = New Size(43, 15)
            Label1.TabIndex = 1
            Label1.Text = "合計："
            ' 
            ' TotalAllocationLabel
            ' 
            TotalAllocationLabel.Anchor = AnchorStyles.Right
            TotalAllocationLabel.AutoSize = True
            TotalAllocationLabel.Location = New Point(54, 8)
            TotalAllocationLabel.Name = "TotalAllocationLabel"
            TotalAllocationLabel.Size = New Size(37, 15)
            TotalAllocationLabel.TabIndex = 1
            TotalAllocationLabel.Text = "12345"
            TotalAllocationLabel.TextAlign = ContentAlignment.MiddleRight
            ' 
            ' Label2
            ' 
            Label2.Anchor = AnchorStyles.None
            Label2.AutoSize = True
            Label2.Location = New Point(98, 8)
            Label2.Name = "Label2"
            Label2.Size = New Size(55, 15)
            Label2.TabIndex = 1
            Label2.Text = "参考値："
            ' 
            ' InfoAllocationLabel
            ' 
            InfoAllocationLabel.Anchor = AnchorStyles.Left
            InfoAllocationLabel.AutoSize = True
            InfoAllocationLabel.Location = New Point(161, 8)
            InfoAllocationLabel.Name = "InfoAllocationLabel"
            InfoAllocationLabel.Size = New Size(187, 15)
            InfoAllocationLabel.TabIndex = 1
            InfoAllocationLabel.Text = "レベル:99999　MQ1:50　MQ2:1800"
            ' 
            ' TableLayoutPanel1
            ' 
            TableLayoutPanel1.ColumnCount = 4
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 51F))
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 43F))
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 64F))
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 104F))
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
            TableLayoutPanel1.Controls.Add(TotalAllocationLabel, 1, 0)
            TableLayoutPanel1.Controls.Add(dgvStatus, 0, 1)
            TableLayoutPanel1.Controls.Add(Label1, 0, 0)
            TableLayoutPanel1.Controls.Add(InfoAllocationLabel, 3, 0)
            TableLayoutPanel1.Controls.Add(Label2, 2, 0)
            TableLayoutPanel1.Dock = DockStyle.Fill
            TableLayoutPanel1.Location = New Point(0, 0)
            TableLayoutPanel1.Margin = New Padding(0)
            TableLayoutPanel1.Name = "TableLayoutPanel1"
            TableLayoutPanel1.RowCount = 2
            TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 32F))
            TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 8F))
            TableLayoutPanel1.Size = New Size(375, 105)
            TableLayoutPanel1.TabIndex = 2
            ' 
            ' StatusForm
            ' 
            AutoScaleDimensions = New SizeF(7F, 15F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(375, 105)
            Controls.Add(TableLayoutPanel1)
            FormBorderStyle = FormBorderStyle.None
            Name = "StatusForm"
            Text = "StatusForm"
            CType(dgvStatus, ComponentModel.ISupportInitialize).EndInit()
            TableLayoutPanel1.ResumeLayout(False)
            TableLayoutPanel1.PerformLayout()
            ResumeLayout(False)
        End Sub

        Friend WithEvents dgvStatus As DataGridView
        Friend WithEvents Label1 As Label
        Friend WithEvents TotalAllocationLabel As Label
        Friend WithEvents Label2 As Label
        Friend WithEvents InfoAllocationLabel As Label
        Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    End Class

End Namespace