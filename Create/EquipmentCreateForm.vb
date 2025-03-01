Imports RS_EquipSimulation.MasterSystem.Common
Imports RS_EquipSimulation.MasterSystem.Equipment
Imports RS_EquipSimulation.MasterSystem.Option
Imports RS_EquipSimulation.MasterSystem.Prefix
Imports RS_EquipSimulation.MasterSystem.Smelt
Imports RS_EquipSimulation.Common.Equipment

Namespace CreatorSystem.Equipment

    Public Class EquipmentCreateForm
        ' シングルトンインスタンス
        Private ReadOnly _equipData As BaseItemSingletonModel
        Private ReadOnly _prefixData As PrefixDataSingletonModel
        Private ReadOnly _optionData As OptionSingletonModel
        Private ReadOnly _smeltManager As SmeltOpModelManagerSingletonModel
        Private ReadOnly _jobConfig As JobConfigSingletonModel
        Private ReadOnly _inventory As IEquipInventoryWriter

        ' 一時的な職業定数（後で職業選択システムに置き換え）
        Private Const CURRENT_JOB As String = "剣士"

        ' 選択中のベースアイテム
        Private _selectedBaseItem As BaseItemModel = Nothing

        Public Sub New(inventory As EquipInventoryManager)
            InitializeComponent()

            ' インベントリ情報
            _inventory = inventory

            ' シングルトンの初期化
            _equipData = BaseItemSingletonModel.Instance
            _prefixData = PrefixDataSingletonModel.Instance
            _optionData = OptionSingletonModel.Instance
            _smeltManager = SmeltOpModelManagerSingletonModel.Instance
            _jobConfig = JobConfigSingletonModel.Instance
        End Sub
    End Class

End Namespace