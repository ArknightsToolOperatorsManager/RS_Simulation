Imports Newtonsoft.Json
Imports RS_EquipSimulation.Common.Equipment
Imports RS_EquipSimulation.Common.Creature
Imports RS_EquipSimulation.CharacterSystem.Operation.Basic
Imports RS_EquipSimulation.CharacterSystem.Operation.Status
Imports RS_EquipSimulation.CharacterSystem.Operation.Equipment
Imports RS_EquipSimulation.CharacterSystem.Operation.Creature
Imports RS_EquipSimulation.CharacterSystem.Operation.Costume
Imports RS_EquipSimulation.CharacterSystem.Summary.Status
Imports RS_EquipSimulation.CharacterSystem.Summary.Option
Imports RS_EquipSimulation.CharacterSystem.Common

Namespace CharacterSystem.Core

    ''' <summary>
    ''' キャラクターデータを管理するクラス
    ''' </summary>
    Public Class CharacterData
        Implements IDisposable

#Region "プロパティ"
        ' オプション管理
        <JsonIgnore>
        Public ReadOnly _optionManager As New OptionManagerModel

        ' インベントリ管理
        <JsonIgnore>
        Public _equipmentInventory As EquipInventoryManager
        <JsonIgnore>
        Public _creatureInventory As CreatureInventoryManager

        ' オペレーションマネージャー
        Public Property BasicManager As BasicManagerModel
        Public Property StatusManager As StatusManagerModel
        Public Property EquipmentManager As EquipmentManagerModel
        Public Property CreatureManager As CreatureManagerModel
        Public Property CostumeManager As CostumeManagerModel

        ' サマリーマネージャー
        <JsonIgnore>
        Public _statusSummaryManager As StatusSummaryManagerModel
        <JsonIgnore>
        Public _optionSummaryManager As OptionSummaryManagerModel

        ' 更新通知のためのイベント
        Public Event DataUpdated As EventHandler

        ' 更新タイマー
        <JsonIgnore>
        Private ReadOnly _updateTimer As New Timer()

        ' 更新履歴フラグ
        <JsonIgnore>
        Public _updateFlg As Boolean
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' 新規キャラクター作成用コンストラクタ
        ''' </summary>
        Public Sub New()
            ' インベントリの設定
            _equipmentInventory = New EquipInventoryManager
            _creatureInventory = New CreatureInventoryManager

            BasicManager = New BasicManagerModel()
            StatusManager = New StatusManagerModel()
            EquipmentManager = New EquipmentManagerModel()
            CreatureManager = New CreatureManagerModel()
            CostumeManager = New CostumeManagerModel()
        End Sub

        ''' <summary>
        ''' カスタムインベントリを使用するコンストラクタ
        ''' </summary>
        Public Sub New(equipmentInventory As EquipInventoryManager, creatureInventory As CreatureInventoryManager)
            ' インベントリの設定
            _equipmentInventory = equipmentInventory
            _creatureInventory = creatureInventory

            ' マネージャーの初期化
            InitializeNewManagers()

            ' タイマーの開始
            TimerStart()
        End Sub

        ''' <summary>
        ''' 新規マネージャーの初期化
        ''' </summary>
        Private Sub InitializeNewManagers()
            BasicManager = New BasicManagerModel(_optionManager)
            StatusManager = New StatusManagerModel(_optionManager)
            EquipmentManager = New EquipmentManagerModel(_optionManager, _equipmentInventory, BasicManager)
            CreatureManager = New CreatureManagerModel(_optionManager, _creatureInventory)
            CostumeManager = New CostumeManagerModel(_optionManager)

            _statusSummaryManager = New StatusSummaryManagerModel(_optionManager, StatusManager, BasicManager)
            _optionSummaryManager = New OptionSummaryManagerModel(_optionManager)
        End Sub

        ''' <summary>
        ''' タイマーの初期化と開始
        ''' </summary>
        Private Sub TimerStart()
            ' 更新タイマーの設定
            _updateTimer.Interval = 1000  ' 1秒ごとに更新
            AddHandler _updateTimer.Tick, AddressOf UpdateTimer_Tick
            _updateTimer.Start()
        End Sub
#End Region

#Region "マネージャー初期化"
        ''' <summary>
        ''' シリアライズされたデータからマネージャーを初期化
        ''' </summary>
        Public Sub InitializeFromSerializedData(serializedData As CharacterData,
                                              equipInventory As EquipInventoryManager,
                                              creatureInventory As CreatureInventoryManager)
            ' インベントリの設定
            _equipmentInventory = equipInventory
            _creatureInventory = creatureInventory

            ' 各マネージャーの初期化
            If serializedData.BasicManager IsNot Nothing Then
                BasicManager = serializedData.BasicManager
                BasicManager.SetOptionManager(_optionManager)
            End If

            If serializedData.StatusManager IsNot Nothing Then
                StatusManager = serializedData.StatusManager
                StatusManager.SetOptionManager(_optionManager)
            End If

            If serializedData.EquipmentManager IsNot Nothing Then
                EquipmentManager = serializedData.EquipmentManager
                EquipmentManager.SetOptionManager(_optionManager)
                EquipmentManager.SetInventoryManager(equipInventory)
                EquipmentManager.SetBasicManager(BasicManager)
            End If

            If serializedData.CreatureManager IsNot Nothing Then
                CreatureManager = serializedData.CreatureManager
                CreatureManager.SetOptionManager(_optionManager)
                CreatureManager.SetInventoryManager(creatureInventory)
            End If

            If serializedData.CostumeManager IsNot Nothing Then
                CostumeManager = serializedData.CostumeManager
                CostumeManager.SetOptionManager(_optionManager)
            End If

            ' サマリーマネージャーの再初期化
            _statusSummaryManager = New StatusSummaryManagerModel(_optionManager, StatusManager, BasicManager)
            _optionSummaryManager = New OptionSummaryManagerModel(_optionManager)

            ' タイマーの開始
            TimerStart()
        End Sub
#End Region

#Region "更新処理"
        ''' <summary>
        ''' タイマーによる更新処理
        ''' </summary>
        Private Sub UpdateTimer_Tick(sender As Object, e As EventArgs)
            ' データ計算と更新
            UpdateCalculations()

            ' 更新通知
            RaiseEvent DataUpdated(Me, EventArgs.Empty)
        End Sub

        ''' <summary>
        ''' 計算更新処理
        ''' </summary>
        Private Sub UpdateCalculations()
            If BasicManager.HasChanged OrElse StatusManager.HasChanged OrElse
               EquipmentManager.HasChanged OrElse CreatureManager.HasChanged OrElse
               CostumeManager.HasChanged Then

                _statusSummaryManager.CalculateStatus()
                _optionSummaryManager.CalculateOptions()

                ' 更新履歴フラグをON
                _updateFlg = True

                ' 変更フラグをリセット
                BasicManager.ResetChangeFlag()
                StatusManager.ResetChangeFlag()
                EquipmentManager.ResetChangeFlag()
                CreatureManager.ResetChangeFlag()
                CostumeManager.ResetChangeFlag()
            End If
        End Sub

        ''' <summary>
        ''' 手動更新用メソッド（必要に応じて呼び出す）
        ''' </summary>
        Public Sub ForceUpdate()
            UpdateCalculations()
            RaiseEvent DataUpdated(Me, EventArgs.Empty)
        End Sub
#End Region

#Region "アクセサメソッド"
        ''' <summary>
        ''' ステータスサマリーの取得
        ''' </summary>
        Public Function GetStatusSummary() As StatusSummaryManagerModel
            Return _statusSummaryManager
        End Function

        ''' <summary>
        ''' オプションサマリーの取得
        ''' </summary>
        Public Function GetOptionSummary() As OptionSummaryManagerModel
            Return _optionSummaryManager
        End Function

        ''' <summary>
        ''' 装備インベントリマネージャーの取得
        ''' </summary>
        Public Function GetEquipInventory() As EquipInventoryManager
            Return _equipmentInventory
        End Function

        ''' <summary>
        ''' クリーチャーインベントリマネージャーの取得
        ''' </summary>
        Public Function GetCreatureInventory() As CreatureInventoryManager
            Return _creatureInventory
        End Function

        ''' <summary>
        ''' オプションマネージャーの取得
        ''' </summary>
        Public Function GetOptionManager() As OptionManagerModel
            Return _optionManager
        End Function
#End Region

#Region "IDisposable"
        ''' <summary>
        ''' リソースの解放
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' タイマーの停止と解放
            _updateTimer.Stop()
            RemoveHandler _updateTimer.Tick, AddressOf UpdateTimer_Tick
            _updateTimer.Dispose()

            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

End Namespace