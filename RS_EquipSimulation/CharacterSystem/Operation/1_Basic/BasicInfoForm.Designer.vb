Namespace CharacterSystem.Operation.Basic

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class BasicInfoForm
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
            pnlCharacterBasic = New Panel()
            txtCharacterName = New TextBox()
            numLevel = New NumericUpDown()
            cmbJob = New ComboBox()
            lblLevel = New Label()
            lblCharaName = New Label()
            lblJob = New Label()
            pnlCharacterBasic.SuspendLayout()
            CType(numLevel, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' pnlCharacterBasic
            ' 
            pnlCharacterBasic.BorderStyle = BorderStyle.FixedSingle
            pnlCharacterBasic.Controls.Add(txtCharacterName)
            pnlCharacterBasic.Controls.Add(numLevel)
            pnlCharacterBasic.Controls.Add(cmbJob)
            pnlCharacterBasic.Controls.Add(lblLevel)
            pnlCharacterBasic.Controls.Add(lblCharaName)
            pnlCharacterBasic.Controls.Add(lblJob)
            pnlCharacterBasic.Dock = DockStyle.Fill
            pnlCharacterBasic.Location = New Point(0, 0)
            pnlCharacterBasic.Name = "pnlCharacterBasic"
            pnlCharacterBasic.Size = New Size(197, 105)
            pnlCharacterBasic.TabIndex = 0
            ' 
            ' txtCharacterName
            ' 
            txtCharacterName.Location = New Point(80, 8)
            txtCharacterName.Name = "txtCharacterName"
            txtCharacterName.Size = New Size(100, 23)
            txtCharacterName.TabIndex = 4
            ' 
            ' numLevel
            ' 
            numLevel.Location = New Point(80, 66)
            numLevel.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
            numLevel.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            numLevel.Name = "numLevel"
            numLevel.Size = New Size(60, 23)
            numLevel.TabIndex = 3
            numLevel.Value = New Decimal(New Integer() {1, 0, 0, 0})
            ' 
            ' cmbJob
            ' 
            cmbJob.FormattingEnabled = True
            cmbJob.Location = New Point(80, 37)
            cmbJob.Name = "cmbJob"
            cmbJob.Size = New Size(100, 23)
            cmbJob.TabIndex = 2
            ' 
            ' lblLevel
            ' 
            lblLevel.AutoSize = True
            lblLevel.Location = New Point(10, 68)
            lblLevel.Name = "lblLevel"
            lblLevel.Size = New Size(36, 15)
            lblLevel.TabIndex = 1
            lblLevel.Text = "レベル"
            ' 
            ' lblCharaName
            ' 
            lblCharaName.AutoSize = True
            lblCharaName.Location = New Point(10, 11)
            lblCharaName.Name = "lblCharaName"
            lblCharaName.Size = New Size(62, 15)
            lblCharaName.TabIndex = 0
            lblCharaName.Text = "キャラクタ名"
            ' 
            ' lblJob
            ' 
            lblJob.AutoSize = True
            lblJob.Location = New Point(10, 40)
            lblJob.Name = "lblJob"
            lblJob.Size = New Size(31, 15)
            lblJob.TabIndex = 0
            lblJob.Text = "職業"
            ' 
            ' BasicInfoForm
            ' 
            AutoScaleDimensions = New SizeF(7F, 15F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(197, 105)
            Controls.Add(pnlCharacterBasic)
            FormBorderStyle = FormBorderStyle.None
            Name = "BasicInfoForm"
            Text = "BasicInfoForm"
            pnlCharacterBasic.ResumeLayout(False)
            pnlCharacterBasic.PerformLayout()
            CType(numLevel, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)

        End Sub

        Friend WithEvents pnlCharacterBasic As Panel
        Friend WithEvents numLevel As NumericUpDown
        Friend WithEvents cmbJob As ComboBox
        Friend WithEvents lblLevel As Label
        Friend WithEvents lblJob As Label
        Friend WithEvents txtCharacterName As TextBox
        Friend WithEvents lblCharaName As Label
    End Class

End Namespace