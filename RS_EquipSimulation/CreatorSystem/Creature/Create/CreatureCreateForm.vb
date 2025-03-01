Imports RS_EquipSimulation.Common.Creature
Imports RS_EquipSimulation.MasterSystem.Creature

Namespace CreatorSystem.Creature

    Partial Public Class CreatureCreateForm
        ' シングルトンデータ
        Private ReadOnly _creaturePassiveEffectData As CreaturePassiveEffectSingletonModel

        ' インベントリ
        Private ReadOnly _inventory As ICreatureInventoryWriter

        ' レアリティリスト
        Private ReadOnly _rarityList As String() = {"N", "R", "HR", "SR", "LR", "GR"}

        ' 初期化
        Public Sub New(inventory As CreatureInventoryManager)
            InitializeComponent()

            _inventory = inventory
            _creaturePassiveEffectData = CreaturePassiveEffectSingletonModel.Instance

            ' コントロール初期化
            InitializeControls()
        End Sub
    End Class

End Namespace