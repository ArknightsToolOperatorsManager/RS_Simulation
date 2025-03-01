Imports Newtonsoft.Json
Imports RS_EquipSimulation.CharacterSystem.Common
Imports RS_EquipSimulation.Common.Creature
Imports RS_EquipSimulation.MasterSystem.Creature

Namespace CharacterSystem.Operation.Creature

    ''' <summary>
    ''' クリーチャーの管理を行うマネージャークラス
    ''' </summary>
    Public Class CreatureManagerModel
        Inherits OperationManagerBase

#Region "プロパティ"
        ' スロット定義
        Public Enum SlotType
            Main    ' メイン
            Sub1    ' サブ1
            Sub2    ' サブ2
            Sub3    ' サブ3
        End Enum

        ' スロットとクリーチャーの紐付け
        <JsonProperty("CreatureSlots")>
        Private ReadOnly _slots As New Dictionary(Of SlotType, Guid?)

        ' インベントリ管理（シリアライズ対象外）
        <JsonIgnore>
        Private _inventoryManager As CreatureInventoryManager

        ' マスターデータ参照（シリアライズ対象外）
        <JsonIgnore>
        Private ReadOnly Property PassiveEffectMaster As CreaturePassiveEffectSingletonModel
            Get
                Return CreaturePassiveEffectSingletonModel.Instance
            End Get
        End Property
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' 新規作成用コンストラクタ
        ''' </summary>
        Public Sub New(optManager As OptionManagerModel, inventoryManager As CreatureInventoryManager)
            MyBase.New(optManager)
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
        ''' <param name="inventory">クリーチャーインベントリマネージャー</param>
        Public Sub SetInventoryManager(inventory As CreatureInventoryManager)
            _inventoryManager = inventory
            ValidateSlots()
        End Sub
#End Region

#Region "クリーチャー操作"
        ''' <summary>
        ''' クリーチャーの装着
        ''' </summary>
        ''' <param name="slot">装着先スロット</param>
        ''' <param name="itemId">クリーチャーアイテムID</param>
        ''' <returns>成功したかどうか</returns>
        Public Function EquipCreature(slot As SlotType, itemId As Guid) As Boolean
            If _inventoryManager Is Nothing Then Return False

            ' クリーチャーの存在確認
            Dim creature = _inventoryManager.GetInventoryItemForId(itemId)
            If creature Is Nothing Then Return False

            _slots(slot) = itemId
            NotifyChanged()
            Return True
        End Function

        ''' <summary>
        ''' クリーチャーの取り外し
        ''' </summary>
        ''' <param name="slot">取り外し対象スロット</param>
        ''' <returns>成功したかどうか</returns>
        Public Function UnequipCreature(slot As SlotType) As Boolean
            If Not _slots.ContainsKey(slot) Then Return False

            _slots(slot) = Nothing
            NotifyChanged()
            Return True
        End Function

        ''' <summary>
        ''' クリーチャーの取得
        ''' </summary>
        ''' <param name="slot">取得対象スロット</param>
        ''' <returns>装備中のクリーチャー情報</returns>
        Public Function GetEquippedCreature(slot As SlotType) As CreatureInventoryItem
            If _inventoryManager Is Nothing Then Return Nothing
            If Not _slots.ContainsKey(slot) OrElse Not _slots(slot).HasValue Then Return Nothing

            Return _inventoryManager.GetInventoryItemForId(_slots(slot).Value)
        End Function
#End Region

#Region "パッシブ効果取得"
        ''' <summary>
        ''' 装備中のすべてのクリーチャーからパッシブ効果の合計を取得
        ''' </summary>
        ''' <returns>パッシブ効果名と合計レベルのディクショナリ</returns>
        Public Function GetAllPassiveEffects() As Dictionary(Of String, Integer)
            Dim passiveEffects As New Dictionary(Of String, Integer)

            ' メインクリーチャーのメインパッシブ効果
            Dim mainCreature = GetEquippedCreature(SlotType.Main)
            If mainCreature IsNot Nothing AndAlso mainCreature.MainPassive IsNot Nothing Then
                AddPassiveEffect(passiveEffects, mainCreature.MainPassive)
            End If

            ' すべてのクリーチャーのサブパッシブ効果
            For Each slot In _slots.Keys
                Dim creature = GetEquippedCreature(slot)
                If creature IsNot Nothing Then
                    For Each passive In creature.SubPassive
                        AddPassiveEffect(passiveEffects, passive)
                    Next
                End If
            Next

            Return passiveEffects
        End Function

        ''' <summary>
        ''' パッシブ効果をディクショナリに追加（既存の値があれば合算）
        ''' </summary>
        Private Sub AddPassiveEffect(passiveEffects As Dictionary(Of String, Integer), passive As PassiveSkill)
            If passive Is Nothing OrElse String.IsNullOrEmpty(passive.Name) Then Return

            Dim totalLevel = passive.GetTotalValue()
            If totalLevel <= 0 Then Return

            If passiveEffects.ContainsKey(passive.Name) Then
                passiveEffects(passive.Name) += totalLevel
            Else
                passiveEffects.Add(passive.Name, totalLevel)
            End If
        End Sub
#End Region

#Region "オプション管理"
        ''' <summary>
        ''' オプション値の更新
        ''' </summary>
        Protected Overrides Sub UpdateOptionValues()
            Dim opList As New List(Of OpValue)

            ' すべてのパッシブ効果の合計を取得
            Dim allPassives = GetAllPassiveEffects()

            ' 各パッシブ効果からオプションを生成
            For Each passive In allPassives
                Dim passiveName = passive.Key
                Dim totalLevel = passive.Value

                ' パッシブエフェクトからオプションを生成して追加
                opList.AddRange(GetPassiveOptions(passiveName, totalLevel))
            Next

            ' オプションを登録
            RegisterMergedOptions("Creature", opList)
        End Sub

        ''' <summary>
        ''' パッシブ名と合計レベルからオプション値のリストを生成
        ''' </summary>
        Private Function GetPassiveOptions(passiveName As String, totalLevel As Integer) As List(Of OpValue)
            Dim options As New List(Of OpValue)

            ' パッシブが無効な場合は空リストを返す
            If String.IsNullOrEmpty(passiveName) OrElse totalLevel <= 0 Then
                Return options
            End If

            ' パッシブエフェクトのマスターデータを取得
            Dim passiveEffect = PassiveEffectMaster.GetPassiveEffect(passiveName)
            If passiveEffect Is Nothing Then Return options

            ' レベルが50を超える場合は50に制限
            If totalLevel > 50 Then totalLevel = 50

            ' パッシブ効果からオプションを生成
            For Each effect In passiveEffect.Effects
                Dim effectName = effect.Key
                Dim effectValues = effect.Value

                ' レベルに対応する値を取得（インデックスは0始まりなのでレベル-1）
                Dim value = effectValues(totalLevel - 1)
                If value > 0 Then
                    options.Add(New OpValue(effectName, value.ToString()))
                End If
            Next

            Return options
        End Function
#End Region
    End Class
End Namespace