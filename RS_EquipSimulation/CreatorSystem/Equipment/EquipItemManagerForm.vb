Imports RS_EquipSimulation.Common.Equipment
Imports RS_EquipSimulation.CharacterSystem.Operation.Basic

Namespace CreatorSystem.Equipment

    Public Class EquipItemManagerForm
        Private WithEvents _createForm As EquipmentCreateForm
        Private WithEvents _inventoryForm As EquipmentInventoryForm
        Private WithEvents _compareForm As EquipmentCompareForm
        Private WithEvents ToggleButton As PictureBox

        Private _isPanelExpanded As Boolean = False
        Private _collapsedWidth As Integer
        Private _expandedWidth As Integer

        Public Sub New(inventory As EquipInventoryManager, basicInfo As BasicManagerModel)
            InitializeComponent()
            InitializeForms(inventory, basicInfo)
            InitializeTogglerButton()
            SetupPanelState(False) ' 初期状態は閉じている
        End Sub

        Private Sub InitializeForms(inventory As EquipInventoryManager, basicInfo As BasicManagerModel)
            Me.TopLevel = True
            ' 各フォームの初期化
            _createForm = New EquipmentCreateForm(inventory, basicInfo)
            _inventoryForm = New EquipmentInventoryForm()
            _inventoryForm.SetInventoryManager(inventory)
            _compareForm = New EquipmentCompareForm()

            ' フォームをパネルに埋め込み
            _createForm.FormBorderStyle = FormBorderStyle.None
            _createForm.TopLevel = False
            _inventoryForm.TopLevel = False
            _compareForm.TopLevel = False

            ' フォームのサイズをパネルに合わせる
            _createForm.Dock = DockStyle.Fill
            _inventoryForm.Dock = DockStyle.Fill
            _compareForm.Dock = DockStyle.Fill

            ' フォームの追加
            CreatePanel.Controls.Add(_createForm)
            InventoryPanel.Controls.Add(_inventoryForm)
            ComparePanel.Controls.Add(_compareForm)

            ' フォームの表示
            _createForm.Show()
            _inventoryForm.Show()
            _compareForm.Show()

            ' 幅の保存
            _collapsedWidth = GroupBox1.Width + 10 ' 余白を含む
            _expandedWidth = GroupBox3.Location.X + GroupBox3.Width + 10 ' 元のフォーム幅
        End Sub

        Private Sub InitializeTogglerButton()
            ' トグルボタンの追加
            ToggleButton = New PictureBox()
            ToggleButton.Size = New Size(16, 16)
            ToggleButton.Location = New Point(GroupBox1.Width - 16, GroupBox1.Height \ 2)
            ToggleButton.Image = CreateTriangleImage(True) ' 右向き黒三角
            ToggleButton.Cursor = Cursors.Hand
            ToggleButton.BackColor = Color.Transparent
            Me.Controls.Add(ToggleButton)
            ToggleButton.BringToFront()
        End Sub

        Private Sub SetupPanelState(expanded As Boolean)
            _isPanelExpanded = expanded

            If expanded Then
                ' パネルを展開
                Me.ClientSize = New Size(_expandedWidth, Me.ClientSize.Height)
                GroupBox2.Visible = True
                GroupBox3.Visible = True
                ToggleButton.Image = CreateTriangleImage(False) ' 左向き黒三角
                ToggleButton.Location = New Point(GroupBox2.Location.X - 16, ToggleButton.Location.Y)
            Else
                ' パネルを折りたたむ
                GroupBox2.Visible = False
                GroupBox3.Visible = False
                Me.ClientSize = New Size(_collapsedWidth, Me.ClientSize.Height)
                ToggleButton.Image = CreateTriangleImage(True) ' 右向き黒三角
                ToggleButton.Location = New Point(GroupBox1.Width - 16, ToggleButton.Location.Y)
            End If
        End Sub

        Private Sub ToggleButton_Click(sender As Object, e As EventArgs) Handles ToggleButton.Click
            ' パネル状態を反転
            SetupPanelState(Not _isPanelExpanded)
        End Sub

        Private Function CreateTriangleImage(isRight As Boolean) As Bitmap
            ' 三角形の画像を作成（右向きまたは左向き）
            Dim bmp As New Bitmap(16, 16)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.Transparent)

                Dim points As Point()
                If isRight Then
                    ' 右向き三角形
                    points = {
                        New Point(4, 4),
                        New Point(12, 8),
                        New Point(4, 12)
                    }
                Else
                    ' 左向き三角形
                    points = {
                        New Point(12, 4),
                        New Point(4, 8),
                        New Point(12, 12)
                    }
                End If

                Using brush As New SolidBrush(Color.Black)
                    g.FillPolygon(brush, points)
                End Using
            End Using

            Return bmp
        End Function

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)

            ' 初期状態は閉じている状態を再確認
            SetupPanelState(False)

            ' インベントリのロードなど、必要な初期化処理
        End Sub

        Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
            MyBase.OnFormClosing(e)

            ' インベントリの保存など、終了時の処理
        End Sub
    End Class

End Namespace