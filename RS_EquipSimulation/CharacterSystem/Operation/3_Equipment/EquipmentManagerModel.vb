Imports Newtonsoft.Json
Imports RS_EquipSimulation.CharacterSystem.Common
Imports RS_EquipSimulation.CharacterSystem.Operation.Basic
Imports RS_EquipSimulation.Common.Equipment
Imports RS_EquipSimulation.MasterSystem.Equipment
Imports RS_EquipSimulation.MasterSystem.Common

Namespace CharacterSystem.Operation.Equipment

    ''' <summary>
    ''' 装備品の管理を行うマネージャークラス
    ''' </summary>
    Public Class EquipmentManagerModel
        Inherits OperationManagerBase

#Region "プロパティ"
        ' 装備スロット定義
        Public Enum SlotType
            Weapon     ' 武器
            SubWeapon  ' サブ武器
            Neckless   ' 首
            Helmet     ' 頭
            Accessary  ' 耳/背
            Belt       ' 腰
            Glove      ' 手
            Armer      ' 胴
            Boots      ' 足
            Ring1      ' 指輪1
            Ring2      ' 指輪2
            Ring3      ' 指輪3
            Ring4      ' 指輪4
            Ring5      ' 指輪5
            Ring6      ' 指輪6
            Ring7      ' 指輪7
            Ring8      ' 指輪8
            Ring9      ' 指輪9
            Ring10     ' 指輪10
        End Enum

        ' スロットと装備品の紐付け
        <JsonProperty("EquipSlots")>
        Private ReadOnly _slots As New Dictionary(Of SlotType, Guid?)

        ' インベントリ管理（シリアライズ対象外）
        <JsonIgnore>
        Private _inventoryManager As EquipInventoryManager
        <JsonIgnore>
        Private _basicInfoManager As BasicManagerModel

        ' マスターデータ参照（シリアライズ対象外）
        <JsonIgnore>
        Private ReadOnly Property BaseItemMaster As BaseItemSingletonModel
            Get
                Return BaseItemSingletonModel.Instance
            End Get
        End Property
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' 新規作成用コンストラクタ
        ''' </summary>
        Public Sub New(optManager As OptionManagerModel, inventoryManager As EquipInventoryManager, basicInfo As BasicManagerModel)
            MyBase.New(optManager)
            _basicInfoManager = basicInfo
            _inventoryManager = inventoryManager
            InitializeSlots()
        End Sub

        ''' <summary>
        ''' デシリアライズ用の空のコンストラクタ
        ''' </summary>
        Public Sub New()
            MyBase.New(Nothing)
            InitializeSlots()
        End Sub
#End Region

#Region "初期化処理"
        ''' <summary>
        ''' スロットの初期化
        ''' </summary>
        Private Sub InitializeSlots()
            For Each slotType As SlotType In [Enum].GetValues(GetType(SlotType))
                _slots(slotType) = Nothing
            Next
        End Sub

        ''' <summary>
        ''' デシリアライズ後の初期化処理
        ''' </summary>
        Protected Overrides Sub OnInitializeAfterDeserialize()
            MyBase.OnInitializeAfterDeserialize()
            ' スロットがロードされていない場合は初期化
            If _slots Is Nothing OrElse _slots.Count = 0 Then
                InitializeSlots()
            End If
        End Sub

        ''' <summary>
        ''' スロットの整合性チェック
        ''' </summary>
        Public Sub ValidateSlots()
            If _inventoryManager Is Nothing Then Return

            For Each slot In _slots.Keys.ToList()
                ' 存在しないアイテムIDのクリア
                If _slots(slot).HasValue AndAlso
                   _inventoryManager.GetInventoryItemForId(_slots(slot).Value) Is Nothing Then
                    _slots(slot) = Nothing
                End If
            Next

            ' オプション更新
            NotifyChanged()
        End Sub

        ''' <summary>
        ''' インベントリマネージャーの設定
        ''' </summary>
        ''' <param name="inventory">装備インベントリマネージャー</param>
        Public Sub SetInventoryManager(inventory As EquipInventoryManager)
            _inventoryManager = inventory
            ValidateSlots()
        End Sub

        ''' <summary>
        ''' 基本情報マネージャーの設定
        ''' </summary>
        ''' <param name="basicManager">基本情報マネージャー</param>
        Public Sub SetBasicManager(basicManager As BasicManagerModel)
            _basicInfoManager = basicManager
            ValidateSlots()
        End Sub
#End Region

#Region "装備操作"
        ''' <summary>
        ''' 装備の装着
        ''' </summary>
        ''' <param name="slot">装着先スロット</param>
        ''' <param name="itemId">装備アイテムID</param>
        ''' <returns>成功したかどうか</returns>
        Public Function EquipItem(slot As SlotType, itemId As Guid) As Boolean
            If _inventoryManager Is Nothing OrElse _basicInfoManager Is Nothing Then Return False

            If Not CanEquip(slot, itemId) Then Return False

            _slots(slot) = itemId
            NotifyChanged()
            Return True
        End Function

        ''' <summary>
        ''' 装備の取り外し
        ''' </summary>
        ''' <param name="slot">取り外し対象スロット</param>
        ''' <returns>成功したかどうか</returns>
        Public Function UnequipItem(slot As SlotType) As Boolean
            If Not _slots.ContainsKey(slot) Then Return False

            _slots(slot) = Nothing
            NotifyChanged()
            Return True
        End Function

        ''' <summary>
        ''' 装備品の取得
        ''' </summary>
        ''' <param name="slot">取得対象スロット</param>
        ''' <returns>装備中のアイテム情報</returns>
        Public Function GetEquippedItem(slot As SlotType) As EquipInventoryItem
            If _inventoryManager Is Nothing Then Return Nothing
            If Not _slots.ContainsKey(slot) OrElse Not _slots(slot).HasValue Then Return Nothing

            Return _inventoryManager.GetInventoryItemForId(_slots(slot).Value)
        End Function

        ''' <summary>
        ''' 装備可能判定
        ''' </summary>
        Private Function CanEquip(slot As SlotType, itemId As Guid) As Boolean
            If _inventoryManager Is Nothing OrElse _basicInfoManager Is Nothing Then Return False

            ' インベントリアイテムの取得
            Dim item = _inventoryManager.GetInventoryItemForId(itemId)
            If item Is Nothing Then Return False

            ' ベースアイテムの取得
            Dim baseItem = BaseItemMaster.GetItemByName(item.BaseItemName)
            If baseItem Is Nothing Then Return False

            ' 部位チェック
            Return IsValidSlotForItem(slot, baseItem)
        End Function

        ''' <summary>
        ''' スロットと部位の整合性チェック
        ''' </summary>
        Public Function IsValidSlotForItem(slot As SlotType, baseItem As BaseItemModel) As Boolean
            If _basicInfoManager Is Nothing Then Return False

            Select Case slot
                Case SlotType.Ring1 To SlotType.Ring10
                    Return baseItem.PartName.Contains("指輪")
                Case SlotType.Weapon
                    Return baseItem.PartName.Contains(_basicInfoManager.JobName)
                Case SlotType.SubWeapon
                    Return baseItem.PartName.Contains("補助")
                Case SlotType.Neckless
                    Return baseItem.PartName.Contains("首"c)
                Case SlotType.Helmet
                    Return baseItem.PartName.Contains("頭"c)
                Case SlotType.Accessary
                    Dim part = If(JobNameManager.GetJobDataByName(_basicInfoManager.JobName).Sex = "Male", "背"c, "耳"c)
                    Return baseItem.PartName.Contains(part)
                Case SlotType.Belt
                    Return baseItem.PartName.Contains("腰"c)
                Case SlotType.Glove
                    Return baseItem.PartName.Contains("手"c)
                Case SlotType.Armer
                    Return baseItem.PartName.Contains("胴"c)
                Case SlotType.Boots
                    Return baseItem.PartName.Contains("足"c)
                Case Else
                    Return False
            End Select
        End Function
#End Region

#Region "オプション管理"
        ''' <summary>
        ''' オプション値の更新
        ''' </summary>
        Protected Overrides Sub UpdateOptionValues()
            If _inventoryManager Is Nothing Then Return

            Dim opList As New List(Of OpValue)

            ' 各スロットの装備品からオプションを集約
            For Each kvp In _slots
                If kvp.Value.HasValue Then
                    Dim item = GetEquippedItem(kvp.Key)
                    If item IsNot Nothing Then
                        ' ベースアイテムのオプション
                        Dim baseItem = BaseItemMaster.GetItemByName(item.BaseItemName)
                        If baseItem IsNot Nothing AndAlso baseItem.Options IsNot Nothing Then
                            opList.AddRange(baseItem.Options)
                        End If

                        ' 称号オプション
                        opList.AddRange(item.PrefixOptions)
                        ' NXオプション
                        opList.AddRange(item.NxOptions)
                        ' 精錬オプション
                        opList.AddRange(item.SmeltOptions)
                    End If
                End If
            Next

            ' オプションを登録
            RegisterMergedOptions("Equipment", opList)
        End Sub
#End Region

    End Class

End Namespace