Imports System.Windows.Forms
Imports RS_EquipSimulation.Common.Equipment

Namespace CharacterSystem.Operation.Equipment

    ''' <summary>
    ''' 装備画面のViewModel
    ''' </summary>
    Public Class EquipmentViewModel
        Implements IDisposable

#Region "プロパティ"
        ' モデルへの参照
        Private ReadOnly _manager As EquipmentManagerModel
        Private ReadOnly _inventoryReader As IEquipInventoryReader

        ' スロットとボタンの関連付け
        Private ReadOnly _slotButtons As New Dictionary(Of EquipmentManagerModel.SlotType, Button)

        ' ツールチップ
        Private ReadOnly _toolTip As New ToolTip() With {
            .InitialDelay = 200,    ' 表示開始までの待ち時間を200ms
            .AutoPopDelay = 3000,   ' 表示時間を3秒
            .ReshowDelay = 50,      ' 次の表示までの待ち時間を50ms
            .ShowAlways = True      ' ウィンドウ非アクティブでも表示
        }
#End Region

#Region "コンストラクタ/Dispose"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="manager">装備管理モデル</param>
        ' EquipmentViewModel コンストラクタ修正
        Public Sub New(manager As EquipmentManagerModel, inventoryReader As IEquipInventoryReader)
            _manager = manager
            _inventoryReader = inventoryReader
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

#Region "ボタン管理"
        ''' <summary>
        ''' ボタンの登録
        ''' </summary>
        ''' <param name="slot">装備スロット</param>
        ''' <param name="button">対応するボタン</param>
        Public Sub RegisterButton(slot As EquipmentManagerModel.SlotType, button As Button)
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
        End Sub

        ''' <summary>
        ''' ボタンの状態を更新
        ''' </summary>
        ''' <param name="slot">対象スロット</param>
        Private Sub UpdateButtonState(slot As EquipmentManagerModel.SlotType)
            Dim button = _slotButtons(slot)
            Dim item = _manager.GetEquippedItem(slot)

            If item IsNot Nothing Then
                button.BackColor = SystemColors.ActiveCaption  ' 装備中の色
            Else
                button.BackColor = SystemColors.Control  ' デフォルトの色
            End If
        End Sub
#End Region

#Region "イベントハンドラ"
        ''' <summary>
        ''' ボタンクリック時の処理
        ''' </summary>
        Private Sub OnButtonClick(sender As Object, e As EventArgs)
            ' クリックされたボタンに対応するスロットを特定
            Dim slotEntry = _slotButtons.FirstOrDefault(Function(kvp) kvp.Value Is sender)
            If slotEntry.Equals(New KeyValuePair(Of EquipmentManagerModel.SlotType, Button)()) Then Return

            ' インベントリポップアップを表示
            Using popup As New InventoryPopupForm(_manager, _inventoryReader, slotEntry.Key)
                If popup.ShowDialog() = DialogResult.OK Then
                    ' 選択結果を反映
                    UpdateButtonState(slotEntry.Key)
                End If
            End Using
        End Sub

        ''' <summary>
        ''' マウスホバー時の処理
        ''' </summary>
        Private Sub OnButtonHover(sender As Object, e As EventArgs)
            ' ホバーされたボタンに対応するスロットを特定
            Dim slot = _slotButtons.FirstOrDefault(Function(kvp) kvp.Value Is sender).Key
            If slot = Nothing Then Return

            ' ツールチップのテキストを設定
            Dim button = DirectCast(sender, Button)
            Dim item = _manager.GetEquippedItem(slot)

            If item IsNot Nothing Then
                _toolTip.SetToolTip(button, $"{item.Name}{vbCrLf}({item.BaseItemName})")
            Else
                _toolTip.SetToolTip(button, "装備なし")
            End If
        End Sub
#End Region

    End Class

End Namespace