Imports System.Windows.Forms
Imports RS_EquipSimulation.CharacterSystem.Operation.Equipment
Imports RS_EquipSimulation.Common.Creature

Namespace CharacterSystem.Operation.Creature

    ''' <summary>
    ''' クリーチャー画面のViewModel
    ''' </summary>
    Public Class CreatureViewModel
        Implements IDisposable

#Region "プロパティ"
        ' モデルへの参照
        Private ReadOnly _manager As CreatureManagerModel
        Private ReadOnly _inventoryReader As ICreatureInventoryReader

        ' スロットとボタンの関連付け
        Private ReadOnly _slotButtons As New Dictionary(Of CreatureManagerModel.SlotType, Button)

        ' パッシブ効果リストビュー
        Private ReadOnly _passiveListView As ListView

        ' ツールチップ
        Private ReadOnly _toolTip As New ToolTip() With {
            .InitialDelay = 200,    ' 表示開始までの待ち時間を200ms
            .AutoPopDelay = 5000,   ' 表示時間を5秒
            .ReshowDelay = 50,      ' 次の表示までの待ち時間を50ms
            .ShowAlways = True      ' ウィンドウ非アクティブでも表示
        }
#End Region

#Region "コンストラクタ/Dispose"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="manager">クリーチャー管理モデル</param>
        ''' <param name="inventoryReader">インベントリ読み取り</param>
        ''' <param name="passiveListView">パッシブ効果表示用リストビュー</param>
        Public Sub New(manager As CreatureManagerModel, inventoryReader As ICreatureInventoryReader, passiveListView As ListView)
            _manager = manager
            _inventoryReader = inventoryReader
            _passiveListView = passiveListView

            ' リストビューの初期設定
            InitializeListView()
        End Sub

        ''' <summary>
        ''' リソースの解放
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            _toolTip.Dispose()

            ' イベントハンドラの解除
            For Each button In _slotButtons.Values
                RemoveHandler button.Click, AddressOf OnButtonClick
                RemoveHandler button.MouseHover, AddressOf OnButtonHover
            Next

            _slotButtons.Clear()
        End Sub
#End Region

#Region "初期化"
        ''' <summary>
        ''' リストビューの初期設定
        ''' </summary>
        Private Sub InitializeListView()
            ' リストビューの設定
            _passiveListView.View = View.Details
            _passiveListView.FullRowSelect = True
            _passiveListView.GridLines = True
            _passiveListView.HideSelection = False

            ' 列の設定
            _passiveListView.Columns.Clear()
            _passiveListView.Columns.Add("パッシブ名", 100)
            _passiveListView.Columns.Add("レベル", 45)

            ' データの更新
            UpdatePassiveListView()
        End Sub
#End Region

#Region "ボタン管理"
        ''' <summary>
        ''' ボタンの登録
        ''' </summary>
        ''' <param name="slot">クリーチャースロット</param>
        ''' <param name="button">対応するボタン</param>
        Public Sub RegisterButton(slot As CreatureManagerModel.SlotType, button As Button)
            ' 既存の登録があれば解除
            If _slotButtons.ContainsKey(slot) Then
                Dim oldButton = _slotButtons(slot)
                RemoveHandler oldButton.Click, AddressOf OnButtonClick
                RemoveHandler oldButton.MouseHover, AddressOf OnButtonHover
            End If

            ' 新しいボタンを登録
            _slotButtons(slot) = button

            ' イベントハンドラを設定
            AddHandler button.Click, AddressOf OnButtonClick
            AddHandler button.MouseHover, AddressOf OnButtonHover

            ' 初期状態を反映
            UpdateButtonState(slot)
        End Sub

        ''' <summary>
        ''' 全ボタンの状態を更新
        ''' </summary>
        Public Sub UpdateAllButtons()
            For Each slot In _slotButtons.Keys
                UpdateButtonState(slot)
            Next

            ' パッシブリストの更新
            UpdatePassiveListView()
        End Sub

        ''' <summary>
        ''' ボタンの状態を更新
        ''' </summary>
        ''' <param name="slot">対象スロット</param>
        Private Sub UpdateButtonState(slot As CreatureManagerModel.SlotType)
            Dim button = _slotButtons(slot)
            Dim creature = _manager.GetEquippedCreature(slot)

            ' ボタンの表示を更新
            If creature IsNot Nothing Then
                button.BackColor = SystemColors.ActiveCaption  ' 装備中の色
            Else
                button.BackColor = SystemColors.Control  ' デフォルトの色
            End If
        End Sub

        ''' <summary>
        ''' デフォルトのスロット名を取得
        ''' </summary>
        Private Function GetDefaultSlotName(slot As CreatureManagerModel.SlotType) As String
            Select Case slot
                Case CreatureManagerModel.SlotType.Main
                    Return "メイン"
                Case CreatureManagerModel.SlotType.Sub1
                    Return "サブ1"
                Case CreatureManagerModel.SlotType.Sub2
                    Return "サブ2"
                Case CreatureManagerModel.SlotType.Sub3
                    Return "サブ3"
                Case Else
                    Return "?"
            End Select
        End Function

        ''' <summary>
        ''' クリーチャー名の短縮版を取得
        ''' </summary>
        Private Function GetShortName(name As String) As String
            ' 名前が長い場合は短縮
            If name.Length > 5 Then
                Return name.Substring(0, 4) & "…"
            Else
                Return name
            End If
        End Function
#End Region

#Region "パッシブリスト管理"
        ''' <summary>
        ''' パッシブ効果リストビューの更新
        ''' </summary>
        Public Sub UpdatePassiveListView()
            _passiveListView.Items.Clear()

            ' パッシブ効果の合計を取得
            Dim allPassives = _manager.GetAllPassiveEffects()

            ' リストビューに追加
            For Each passive In allPassives
                Dim item = New ListViewItem(passive.Key)
                item.SubItems.Add(passive.Value.ToString())
                _passiveListView.Items.Add(item)
            Next

            ' 列幅の自動調整
            For Each col As ColumnHeader In _passiveListView.Columns
                col.Width = -2  ' -2は内容に合わせた自動調整
            Next
        End Sub
#End Region

#Region "イベントハンドラ"
        ''' <summary>
        ''' ボタンクリック時の処理
        ''' </summary>
        Private Sub OnButtonClick(sender As Object, e As EventArgs)
            ' クリックされたボタンに対応するスロットを特定
            Dim slotEntry = _slotButtons.FirstOrDefault(Function(kvp) kvp.Value Is sender)
            If slotEntry.Equals(New KeyValuePair(Of CreatureManagerModel.SlotType, Button)()) Then Return

            ' クリーチャーポップアップを表示
            Using popup As New CreaturePopupForm(_manager, _inventoryReader, slotEntry.Key)
                If popup.ShowDialog() = DialogResult.OK Then
                    ' 選択結果を反映
                    UpdateButtonState(slotEntry.Key)
                    ' パッシブリストの更新
                    UpdatePassiveListView()
                End If
            End Using
        End Sub

        ''' <summary>
        ''' マウスホバー時の処理
        ''' </summary>
        Private Sub OnButtonHover(sender As Object, e As EventArgs)
            ' ホバーされたボタンに対応するスロットを特定
            Dim slotEntry = _slotButtons.FirstOrDefault(Function(kvp) kvp.Value Is sender)
            If slotEntry.Equals(New KeyValuePair(Of CreatureManagerModel.SlotType, Button)()) Then Return

            ' ツールチップのテキストを設定
            Dim button = DirectCast(sender, Button)
            Dim creature = _manager.GetEquippedCreature(slotEntry.Key)

            If creature IsNot Nothing Then
                Dim tooltipText = CreateCreatureTooltip(creature)
                _toolTip.SetToolTip(button, tooltipText)
            Else
                _toolTip.SetToolTip(button, "クリーチャーなし")
            End If
        End Sub

        ''' <summary>
        ''' クリーチャー情報のツールチップテキストを作成
        ''' </summary>
        Private Function CreateCreatureTooltip(creature As CreatureInventoryItem) As String
            Dim sb As New System.Text.StringBuilder()

            ' 基本情報
            sb.AppendLine(creature.Name)
            sb.AppendLine($"レアリティ: {creature.BaseRarity} → {creature.EnhancedRarity}")
            sb.AppendLine()

            ' メインパッシブ
            If creature.MainPassive IsNot Nothing Then
                sb.AppendLine($"メイン: {creature.MainPassive.Name}")
                sb.AppendLine($"  Lv.{creature.MainPassive.PassiveLevel}+{creature.MainPassive.AwakeningValue}")
            End If

            ' サブパッシブ
            For Each passive In creature.SubPassive
                sb.AppendLine($"サブ: {passive.Name}")
                sb.AppendLine($"  Lv.{passive.PassiveLevel}+{passive.AwakeningValue}")
            Next

            Return sb.ToString()
        End Function
#End Region

    End Class

End Namespace