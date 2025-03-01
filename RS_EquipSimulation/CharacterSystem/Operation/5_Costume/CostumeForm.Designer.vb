<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CostumeForm
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        NumericUpDown1 = New NumericUpDown()
        Label2 = New Label()
        NumericUpDown2 = New NumericUpDown()
        Label3 = New Label()
        CostumeGroupBox = New GroupBox()
        CrestGroupBox = New GroupBox()
        Crest6ComboBox = New ComboBox()
        Crest5ComboBox = New ComboBox()
        Crest4ComboBox = New ComboBox()
        Crest6RankComboBox = New ComboBox()
        CrestColor6ComboBox = New ComboBox()
        Crest3ComboBox = New ComboBox()
        Crest5RankComboBox = New ComboBox()
        Crest4RankComboBox = New ComboBox()
        CrestColor5ComboBox = New ComboBox()
        CrestColor4ComboBox = New ComboBox()
        Crest3RankComboBox = New ComboBox()
        Crest2ComboBox = New ComboBox()
        CrestColor3ComboBox = New ComboBox()
        Crest2RankComboBox = New ComboBox()
        Crest1ComboBox = New ComboBox()
        Crest1RankComboBox = New ComboBox()
        CrestColor2ComboBox = New ComboBox()
        CrestColor1ComboBox = New ComboBox()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).BeginInit()
        CostumeGroupBox.SuspendLayout()
        CrestGroupBox.SuspendLayout()
        SuspendLayout()
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Location = New Point(69, 24)
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New Size(53, 23)
        NumericUpDown1.TabIndex = 0
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 26)
        Label2.Name = "Label2"
        Label2.Size = New Size(51, 15)
        Label2.TabIndex = 1
        Label2.Text = "ステータス"
        ' 
        ' NumericUpDown2
        ' 
        NumericUpDown2.Location = New Point(69, 53)
        NumericUpDown2.Name = "NumericUpDown2"
        NumericUpDown2.Size = New Size(53, 23)
        NumericUpDown2.TabIndex = 0
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(12, 55)
        Label3.Name = "Label3"
        Label3.Size = New Size(43, 15)
        Label3.TabIndex = 1
        Label3.Text = "経験値"
        ' 
        ' CostumeGroupBox
        ' 
        CostumeGroupBox.Controls.Add(NumericUpDown2)
        CostumeGroupBox.Controls.Add(Label3)
        CostumeGroupBox.Controls.Add(NumericUpDown1)
        CostumeGroupBox.Controls.Add(Label2)
        CostumeGroupBox.Location = New Point(1, 1)
        CostumeGroupBox.Name = "CostumeGroupBox"
        CostumeGroupBox.Size = New Size(179, 86)
        CostumeGroupBox.TabIndex = 2
        CostumeGroupBox.TabStop = False
        CostumeGroupBox.Text = "コスチュームステータス"
        ' 
        ' CrestGroupBox
        ' 
        CrestGroupBox.Controls.Add(Crest6ComboBox)
        CrestGroupBox.Controls.Add(Crest5ComboBox)
        CrestGroupBox.Controls.Add(Crest4ComboBox)
        CrestGroupBox.Controls.Add(Crest6RankComboBox)
        CrestGroupBox.Controls.Add(CrestColor6ComboBox)
        CrestGroupBox.Controls.Add(Crest3ComboBox)
        CrestGroupBox.Controls.Add(Crest5RankComboBox)
        CrestGroupBox.Controls.Add(Crest4RankComboBox)
        CrestGroupBox.Controls.Add(CrestColor5ComboBox)
        CrestGroupBox.Controls.Add(CrestColor4ComboBox)
        CrestGroupBox.Controls.Add(Crest3RankComboBox)
        CrestGroupBox.Controls.Add(Crest2ComboBox)
        CrestGroupBox.Controls.Add(CrestColor3ComboBox)
        CrestGroupBox.Controls.Add(Crest2RankComboBox)
        CrestGroupBox.Controls.Add(Crest1ComboBox)
        CrestGroupBox.Controls.Add(Crest1RankComboBox)
        CrestGroupBox.Controls.Add(CrestColor2ComboBox)
        CrestGroupBox.Controls.Add(CrestColor1ComboBox)
        CrestGroupBox.Location = New Point(1, 93)
        CrestGroupBox.Name = "CrestGroupBox"
        CrestGroupBox.Size = New Size(241, 202)
        CrestGroupBox.TabIndex = 3
        CrestGroupBox.TabStop = False
        CrestGroupBox.Text = "クレスト"
        ' 
        ' Crest6ComboBox
        ' 
        Crest6ComboBox.FormattingEnabled = True
        Crest6ComboBox.Location = New Point(55, 172)
        Crest6ComboBox.Name = "Crest6ComboBox"
        Crest6ComboBox.Size = New Size(108, 23)
        Crest6ComboBox.TabIndex = 0
        ' 
        ' Crest5ComboBox
        ' 
        Crest5ComboBox.FormattingEnabled = True
        Crest5ComboBox.Location = New Point(55, 143)
        Crest5ComboBox.Name = "Crest5ComboBox"
        Crest5ComboBox.Size = New Size(108, 23)
        Crest5ComboBox.TabIndex = 0
        ' 
        ' Crest4ComboBox
        ' 
        Crest4ComboBox.FormattingEnabled = True
        Crest4ComboBox.Location = New Point(55, 114)
        Crest4ComboBox.Name = "Crest4ComboBox"
        Crest4ComboBox.Size = New Size(108, 23)
        Crest4ComboBox.TabIndex = 0
        ' 
        ' Crest6RankComboBox
        ' 
        Crest6RankComboBox.FormattingEnabled = True
        Crest6RankComboBox.Location = New Point(169, 172)
        Crest6RankComboBox.Name = "Crest6RankComboBox"
        Crest6RankComboBox.Size = New Size(56, 23)
        Crest6RankComboBox.TabIndex = 0
        ' 
        ' CrestColor6ComboBox
        ' 
        CrestColor6ComboBox.FormattingEnabled = True
        CrestColor6ComboBox.Location = New Point(9, 172)
        CrestColor6ComboBox.Name = "CrestColor6ComboBox"
        CrestColor6ComboBox.Size = New Size(40, 23)
        CrestColor6ComboBox.TabIndex = 0
        ' 
        ' Crest3ComboBox
        ' 
        Crest3ComboBox.FormattingEnabled = True
        Crest3ComboBox.Location = New Point(55, 85)
        Crest3ComboBox.Name = "Crest3ComboBox"
        Crest3ComboBox.Size = New Size(108, 23)
        Crest3ComboBox.TabIndex = 0
        ' 
        ' Crest5RankComboBox
        ' 
        Crest5RankComboBox.FormattingEnabled = True
        Crest5RankComboBox.Location = New Point(169, 143)
        Crest5RankComboBox.Name = "Crest5RankComboBox"
        Crest5RankComboBox.Size = New Size(56, 23)
        Crest5RankComboBox.TabIndex = 0
        ' 
        ' Crest4RankComboBox
        ' 
        Crest4RankComboBox.FormattingEnabled = True
        Crest4RankComboBox.Location = New Point(169, 114)
        Crest4RankComboBox.Name = "Crest4RankComboBox"
        Crest4RankComboBox.Size = New Size(56, 23)
        Crest4RankComboBox.TabIndex = 0
        ' 
        ' CrestColor5ComboBox
        ' 
        CrestColor5ComboBox.FormattingEnabled = True
        CrestColor5ComboBox.Location = New Point(9, 143)
        CrestColor5ComboBox.Name = "CrestColor5ComboBox"
        CrestColor5ComboBox.Size = New Size(40, 23)
        CrestColor5ComboBox.TabIndex = 0
        ' 
        ' CrestColor4ComboBox
        ' 
        CrestColor4ComboBox.FormattingEnabled = True
        CrestColor4ComboBox.Location = New Point(9, 114)
        CrestColor4ComboBox.Name = "CrestColor4ComboBox"
        CrestColor4ComboBox.Size = New Size(40, 23)
        CrestColor4ComboBox.TabIndex = 0
        ' 
        ' Crest3RankComboBox
        ' 
        Crest3RankComboBox.FormattingEnabled = True
        Crest3RankComboBox.Location = New Point(169, 85)
        Crest3RankComboBox.Name = "Crest3RankComboBox"
        Crest3RankComboBox.Size = New Size(56, 23)
        Crest3RankComboBox.TabIndex = 0
        ' 
        ' Crest2ComboBox
        ' 
        Crest2ComboBox.FormattingEnabled = True
        Crest2ComboBox.Location = New Point(55, 56)
        Crest2ComboBox.Name = "Crest2ComboBox"
        Crest2ComboBox.Size = New Size(108, 23)
        Crest2ComboBox.TabIndex = 0
        ' 
        ' CrestColor3ComboBox
        ' 
        CrestColor3ComboBox.FormattingEnabled = True
        CrestColor3ComboBox.Location = New Point(9, 85)
        CrestColor3ComboBox.Name = "CrestColor3ComboBox"
        CrestColor3ComboBox.Size = New Size(40, 23)
        CrestColor3ComboBox.TabIndex = 0
        ' 
        ' Crest2RankComboBox
        ' 
        Crest2RankComboBox.FormattingEnabled = True
        Crest2RankComboBox.Location = New Point(169, 56)
        Crest2RankComboBox.Name = "Crest2RankComboBox"
        Crest2RankComboBox.Size = New Size(56, 23)
        Crest2RankComboBox.TabIndex = 0
        ' 
        ' Crest1ComboBox
        ' 
        Crest1ComboBox.FormattingEnabled = True
        Crest1ComboBox.Location = New Point(55, 27)
        Crest1ComboBox.Name = "Crest1ComboBox"
        Crest1ComboBox.Size = New Size(108, 23)
        Crest1ComboBox.TabIndex = 0
        ' 
        ' Crest1RankComboBox
        ' 
        Crest1RankComboBox.FormattingEnabled = True
        Crest1RankComboBox.Location = New Point(169, 27)
        Crest1RankComboBox.Name = "Crest1RankComboBox"
        Crest1RankComboBox.Size = New Size(56, 23)
        Crest1RankComboBox.TabIndex = 0
        ' 
        ' CrestColor2ComboBox
        ' 
        CrestColor2ComboBox.FormattingEnabled = True
        CrestColor2ComboBox.Location = New Point(9, 56)
        CrestColor2ComboBox.Name = "CrestColor2ComboBox"
        CrestColor2ComboBox.Size = New Size(40, 23)
        CrestColor2ComboBox.TabIndex = 0
        ' 
        ' CrestColor1ComboBox
        ' 
        CrestColor1ComboBox.FormattingEnabled = True
        CrestColor1ComboBox.Location = New Point(9, 27)
        CrestColor1ComboBox.Name = "CrestColor1ComboBox"
        CrestColor1ComboBox.Size = New Size(40, 23)
        CrestColor1ComboBox.TabIndex = 0
        ' 
        ' CostumeForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(243, 295)
        Controls.Add(CrestGroupBox)
        Controls.Add(CostumeGroupBox)
        FormBorderStyle = FormBorderStyle.None
        Name = "CostumeForm"
        Text = "CostumeForm"
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).EndInit()
        CostumeGroupBox.ResumeLayout(False)
        CostumeGroupBox.PerformLayout()
        CrestGroupBox.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents NumericUpDown2 As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents CostumeGroupBox As GroupBox
    Friend WithEvents CrestGroupBox As GroupBox
    Friend WithEvents Crest6ComboBox As ComboBox
    Friend WithEvents Crest5ComboBox As ComboBox
    Friend WithEvents Crest4ComboBox As ComboBox
    Friend WithEvents CrestColor6ComboBox As ComboBox
    Friend WithEvents Crest3ComboBox As ComboBox
    Friend WithEvents CrestColor5ComboBox As ComboBox
    Friend WithEvents CrestColor4ComboBox As ComboBox
    Friend WithEvents Crest2ComboBox As ComboBox
    Friend WithEvents CrestColor3ComboBox As ComboBox
    Friend WithEvents Crest1ComboBox As ComboBox
    Friend WithEvents CrestColor2ComboBox As ComboBox
    Friend WithEvents CrestColor1ComboBox As ComboBox
    Friend WithEvents Crest6RankComboBox As ComboBox
    Friend WithEvents Crest5RankComboBox As ComboBox
    Friend WithEvents Crest4RankComboBox As ComboBox
    Friend WithEvents Crest3RankComboBox As ComboBox
    Friend WithEvents Crest2RankComboBox As ComboBox
    Friend WithEvents Crest1RankComboBox As ComboBox
End Class
