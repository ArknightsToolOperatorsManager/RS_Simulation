Namespace CreatorSystem.Equipment

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class EquipmentCompareForm
        Inherits System.Windows.Forms.Form

        Private components As System.ComponentModel.IContainer = Nothing

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private Sub InitializeComponent()
            CompareGridView = New DataGridView()
            CType(CompareGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()

            ' CompareGridView
            CompareGridView.AllowUserToAddRows = False
            CompareGridView.AllowUserToDeleteRows = False
            CompareGridView.AllowUserToResizeRows = False
            CompareGridView.BackgroundColor = SystemColors.Window
            CompareGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            CompareGridView.Dock = DockStyle.Fill
            CompareGridView.Name = "CompareGridView"
            CompareGridView.ReadOnly = True
            CompareGridView.RowHeadersVisible = False
            CompareGridView.RowTemplate.Height = 25

            ' EquipCompareForm
            AutoScaleDimensions = New SizeF(7.0F, 15.0F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(270, 650)
            Controls.Add(CompareGridView)
            FormBorderStyle = FormBorderStyle.None
            Name = "EquipCompareForm"
            CType(CompareGridView, System.ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
        End Sub

        Friend WithEvents CompareGridView As DataGridView
    End Class

End Namespace