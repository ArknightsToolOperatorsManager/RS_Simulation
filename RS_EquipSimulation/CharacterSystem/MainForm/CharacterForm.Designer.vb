<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CharacterForm
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
        pnlBasicInfo = New Panel()
        pnlStatus = New Panel()
        pnlEquipment = New Panel()
        pnlCreature = New Panel()
        pnlCostume = New Panel()
        pnlSummaryStatus = New Panel()
        pnlSummaryOption = New Panel()
        GroupBox1 = New GroupBox()
        btnCreature = New Button()
        btnEquipment = New Button()
        GroupBox2 = New GroupBox()
        GroupBox3 = New GroupBox()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlBasicInfo
        ' 
        pnlBasicInfo.BorderStyle = BorderStyle.FixedSingle
        pnlBasicInfo.Location = New Point(6, 22)
        pnlBasicInfo.Name = "pnlBasicInfo"
        pnlBasicInfo.Size = New Size(197, 105)
        pnlBasicInfo.TabIndex = 0
        ' 
        ' pnlStatus
        ' 
        pnlStatus.BorderStyle = BorderStyle.FixedSingle
        pnlStatus.Location = New Point(209, 22)
        pnlStatus.Name = "pnlStatus"
        pnlStatus.Size = New Size(375, 105)
        pnlStatus.TabIndex = 0
        ' 
        ' pnlEquipment
        ' 
        pnlEquipment.BorderStyle = BorderStyle.FixedSingle
        pnlEquipment.Location = New Point(6, 162)
        pnlEquipment.Name = "pnlEquipment"
        pnlEquipment.Size = New Size(242, 218)
        pnlEquipment.TabIndex = 0
        ' 
        ' pnlCreature
        ' 
        pnlCreature.BorderStyle = BorderStyle.FixedSingle
        pnlCreature.Location = New Point(6, 385)
        pnlCreature.Name = "pnlCreature"
        pnlCreature.Size = New Size(152, 129)
        pnlCreature.TabIndex = 0
        ' 
        ' pnlCostume
        ' 
        pnlCostume.BorderStyle = BorderStyle.FixedSingle
        pnlCostume.Location = New Point(304, 162)
        pnlCostume.Name = "pnlCostume"
        pnlCostume.Size = New Size(243, 295)
        pnlCostume.TabIndex = 0
        ' 
        ' pnlSummaryStatus
        ' 
        pnlSummaryStatus.BorderStyle = BorderStyle.FixedSingle
        pnlSummaryStatus.Location = New Point(12, 22)
        pnlSummaryStatus.Name = "pnlSummaryStatus"
        pnlSummaryStatus.Size = New Size(400, 276)
        pnlSummaryStatus.TabIndex = 0
        ' 
        ' pnlSummaryOption
        ' 
        pnlSummaryOption.BorderStyle = BorderStyle.FixedSingle
        pnlSummaryOption.Location = New Point(12, 300)
        pnlSummaryOption.Name = "pnlSummaryOption"
        pnlSummaryOption.Size = New Size(302, 421)
        pnlSummaryOption.TabIndex = 0
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(btnCreature)
        GroupBox1.Controls.Add(btnEquipment)
        GroupBox1.Controls.Add(pnlBasicInfo)
        GroupBox1.Controls.Add(pnlStatus)
        GroupBox1.Controls.Add(pnlCostume)
        GroupBox1.Controls.Add(pnlEquipment)
        GroupBox1.Controls.Add(pnlCreature)
        GroupBox1.Location = New Point(12, 4)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(590, 643)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        GroupBox1.Text = "キャラクタ入力"
        ' 
        ' btnCreature
        ' 
        btnCreature.Location = New Point(111, 132)
        btnCreature.Name = "btnCreature"
        btnCreature.Size = New Size(99, 23)
        btnCreature.TabIndex = 1
        btnCreature.Text = "クリーチャー作成"
        btnCreature.UseVisualStyleBackColor = True
        ' 
        ' btnEquipment
        ' 
        btnEquipment.Location = New Point(6, 133)
        btnEquipment.Name = "btnEquipment"
        btnEquipment.Size = New Size(99, 23)
        btnEquipment.TabIndex = 1
        btnEquipment.Text = "装備作成"
        btnEquipment.UseVisualStyleBackColor = True
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(pnlSummaryStatus)
        GroupBox2.Controls.Add(pnlSummaryOption)
        GroupBox2.Location = New Point(608, 4)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(425, 727)
        GroupBox2.TabIndex = 1
        GroupBox2.TabStop = False
        GroupBox2.Text = "合計"
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Location = New Point(12, 658)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(507, 73)
        GroupBox3.TabIndex = 2
        GroupBox3.TabStop = False
        GroupBox3.Text = "[未実装]ギルド情報入力"
        ' 
        ' CharacterForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1040, 737)
        TopLevel = True
        Controls.Add(GroupBox3)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        Name = "CharacterForm"
        Text = "キャラクタービルダー"
        GroupBox1.ResumeLayout(False)
        GroupBox2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlBasicInfo As Panel
    Friend WithEvents pnlStatus As Panel
    Friend WithEvents pnlEquipment As Panel
    Friend WithEvents pnlCreature As Panel
    Friend WithEvents pnlCostume As Panel
    Friend WithEvents pnlSummaryStatus As Panel
    Friend WithEvents pnlSummaryOption As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents btnCreature As Button
    Friend WithEvents btnEquipment As Button
    Friend WithEvents GroupBox3 As GroupBox
End Class