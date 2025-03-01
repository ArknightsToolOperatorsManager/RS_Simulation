Namespace CharacterSystem.Summary.Status

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class StatusSummaryForm
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
            GroupBox1 = New GroupBox()
            Label6 = New Label()
            Label4 = New Label()
            Label7 = New Label()
            Label5 = New Label()
            Label2 = New Label()
            Label3 = New Label()
            CharismaTextBox = New TextBox()
            LuckTextBox = New TextBox()
            Label1 = New Label()
            HealthTextBox = New TextBox()
            WisdomTextBox = New TextBox()
            AgilityTextBox = New TextBox()
            KnowledgeTextBox = New TextBox()
            PowerTextBox = New TextBox()
            dgvStatus = New DataGridView()
            GroupBox2 = New GroupBox()
            GroupBox1.SuspendLayout()
            CType(dgvStatus, ComponentModel.ISupportInitialize).BeginInit()
            GroupBox2.SuspendLayout()
            SuspendLayout()
            ' 
            ' GroupBox1
            ' 
            GroupBox1.Controls.Add(Label6)
            GroupBox1.Controls.Add(Label4)
            GroupBox1.Controls.Add(Label7)
            GroupBox1.Controls.Add(Label5)
            GroupBox1.Controls.Add(Label2)
            GroupBox1.Controls.Add(Label3)
            GroupBox1.Controls.Add(CharismaTextBox)
            GroupBox1.Controls.Add(LuckTextBox)
            GroupBox1.Controls.Add(Label1)
            GroupBox1.Controls.Add(HealthTextBox)
            GroupBox1.Controls.Add(WisdomTextBox)
            GroupBox1.Controls.Add(AgilityTextBox)
            GroupBox1.Controls.Add(KnowledgeTextBox)
            GroupBox1.Controls.Add(PowerTextBox)
            GroupBox1.Location = New Point(0, 0)
            GroupBox1.Name = "GroupBox1"
            GroupBox1.Size = New Size(301, 135)
            GroupBox1.TabIndex = 0
            GroupBox1.TabStop = False
            GroupBox1.Text = "最終ステータス"
            ' 
            ' Label6
            ' 
            Label6.AutoSize = True
            Label6.Location = New Point(148, 77)
            Label6.Name = "Label6"
            Label6.Size = New Size(31, 15)
            Label6.TabIndex = 1
            Label6.Text = "威厳"
            ' 
            ' Label4
            ' 
            Label4.AutoSize = True
            Label4.Location = New Point(77, 77)
            Label4.Name = "Label4"
            Label4.Size = New Size(31, 15)
            Label4.TabIndex = 1
            Label4.Text = "知恵"
            ' 
            ' Label7
            ' 
            Label7.AutoSize = True
            Label7.Location = New Point(219, 25)
            Label7.Name = "Label7"
            Label7.Size = New Size(19, 15)
            Label7.TabIndex = 1
            Label7.Text = "運"
            ' 
            ' Label5
            ' 
            Label5.AutoSize = True
            Label5.Location = New Point(148, 25)
            Label5.Name = "Label5"
            Label5.Size = New Size(31, 15)
            Label5.TabIndex = 1
            Label5.Text = "健康"
            ' 
            ' Label2
            ' 
            Label2.AutoSize = True
            Label2.Location = New Point(6, 77)
            Label2.Name = "Label2"
            Label2.Size = New Size(31, 15)
            Label2.TabIndex = 1
            Label2.Text = "知識"
            ' 
            ' Label3
            ' 
            Label3.AutoSize = True
            Label3.Location = New Point(77, 25)
            Label3.Name = "Label3"
            Label3.Size = New Size(31, 15)
            Label3.TabIndex = 1
            Label3.Text = "敏捷"
            ' 
            ' CharismaTextBox
            ' 
            CharismaTextBox.BackColor = SystemColors.Window
            CharismaTextBox.Location = New Point(148, 95)
            CharismaTextBox.Name = "CharismaTextBox"
            CharismaTextBox.ReadOnly = True
            CharismaTextBox.Size = New Size(65, 23)
            CharismaTextBox.TabIndex = 0
            ' 
            ' LuckTextBox
            ' 
            LuckTextBox.BackColor = SystemColors.Window
            LuckTextBox.Location = New Point(219, 43)
            LuckTextBox.Name = "LuckTextBox"
            LuckTextBox.ReadOnly = True
            LuckTextBox.Size = New Size(65, 23)
            LuckTextBox.TabIndex = 0
            ' 
            ' Label1
            ' 
            Label1.AutoSize = True
            Label1.Location = New Point(6, 25)
            Label1.Name = "Label1"
            Label1.Size = New Size(19, 15)
            Label1.TabIndex = 1
            Label1.Text = "力"
            ' 
            ' HealthTextBox
            ' 
            HealthTextBox.BackColor = SystemColors.Window
            HealthTextBox.Location = New Point(148, 43)
            HealthTextBox.Name = "HealthTextBox"
            HealthTextBox.ReadOnly = True
            HealthTextBox.Size = New Size(65, 23)
            HealthTextBox.TabIndex = 0
            ' 
            ' WisdomTextBox
            ' 
            WisdomTextBox.BackColor = SystemColors.Window
            WisdomTextBox.Location = New Point(77, 95)
            WisdomTextBox.Name = "WisdomTextBox"
            WisdomTextBox.ReadOnly = True
            WisdomTextBox.Size = New Size(65, 23)
            WisdomTextBox.TabIndex = 0
            ' 
            ' AgilityTextBox
            ' 
            AgilityTextBox.BackColor = SystemColors.Window
            AgilityTextBox.Location = New Point(77, 43)
            AgilityTextBox.Name = "AgilityTextBox"
            AgilityTextBox.ReadOnly = True
            AgilityTextBox.Size = New Size(65, 23)
            AgilityTextBox.TabIndex = 0
            ' 
            ' KnowledgeTextBox
            ' 
            KnowledgeTextBox.BackColor = SystemColors.Window
            KnowledgeTextBox.Location = New Point(6, 95)
            KnowledgeTextBox.Name = "KnowledgeTextBox"
            KnowledgeTextBox.ReadOnly = True
            KnowledgeTextBox.Size = New Size(65, 23)
            KnowledgeTextBox.TabIndex = 0
            ' 
            ' PowerTextBox
            ' 
            PowerTextBox.BackColor = SystemColors.Window
            PowerTextBox.Location = New Point(6, 43)
            PowerTextBox.Name = "PowerTextBox"
            PowerTextBox.ReadOnly = True
            PowerTextBox.Size = New Size(65, 23)
            PowerTextBox.TabIndex = 0
            ' 
            ' dgvStatus
            ' 
            dgvStatus.AllowUserToAddRows = False
            dgvStatus.AllowUserToDeleteRows = False
            dgvStatus.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            dgvStatus.Dock = DockStyle.Fill
            dgvStatus.Location = New Point(3, 19)
            dgvStatus.Name = "dgvStatus"
            dgvStatus.ReadOnly = True
            dgvStatus.RowHeadersVisible = False
            dgvStatus.Size = New Size(394, 113)
            dgvStatus.TabIndex = 1
            ' 
            ' GroupBox2
            ' 
            GroupBox2.Controls.Add(dgvStatus)
            GroupBox2.Location = New Point(0, 141)
            GroupBox2.Name = "GroupBox2"
            GroupBox2.Size = New Size(400, 135)
            GroupBox2.TabIndex = 2
            GroupBox2.TabStop = False
            GroupBox2.Text = "各グループのステータス"
            ' 
            ' StatusSummaryForm
            ' 
            AutoScaleDimensions = New SizeF(7F, 15F)
            AutoScaleMode = AutoScaleMode.Font
            ClientSize = New Size(400, 276)
            Controls.Add(GroupBox2)
            Controls.Add(GroupBox1)
            FormBorderStyle = FormBorderStyle.None
            Name = "StatusSummaryForm"
            Text = "SummaryStatusForm"
            GroupBox1.ResumeLayout(False)
            GroupBox1.PerformLayout()
            CType(dgvStatus, ComponentModel.ISupportInitialize).EndInit()
            GroupBox2.ResumeLayout(False)
            ResumeLayout(False)
        End Sub

        Friend WithEvents GroupBox1 As GroupBox
        Friend WithEvents Label6 As Label
        Friend WithEvents Label4 As Label
        Friend WithEvents Label7 As Label
        Friend WithEvents Label5 As Label
        Friend WithEvents Label2 As Label
        Friend WithEvents Label3 As Label
        Friend WithEvents CharismaTextBox As TextBox
        Friend WithEvents LuckTextBox As TextBox
        Friend WithEvents Label1 As Label
        Friend WithEvents HealthTextBox As TextBox
        Friend WithEvents WisdomTextBox As TextBox
        Friend WithEvents AgilityTextBox As TextBox
        Friend WithEvents KnowledgeTextBox As TextBox
        Friend WithEvents PowerTextBox As TextBox
        Friend WithEvents dgvStatus As DataGridView
        Friend WithEvents GroupBox2 As GroupBox
    End Class

End Namespace