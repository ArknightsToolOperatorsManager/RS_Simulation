Imports RS_EquipSimulation.Common.Equipment

Namespace CharacterSystem.Operation.Equipment

    Public Class EquipmentForm
        Private ReadOnly _viewModel As EquipmentViewModel
        Public Sub New(manager As EquipmentManagerModel, inventoryReader As IEquipInventoryReader)
            InitializeComponent()
            _viewModel = New EquipmentViewModel(manager, inventoryReader)

            ' 各ボタンの登録
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Weapon, WeaponButton)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.SubWeapon, SubWeaponButton)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Neckless, NecklessButton)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Helmet, HelmetButton)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Accessary, AccessaryButton)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Belt, BeltButton)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Glove, GloveButton)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Armer, ArmerButton)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Boots, BootsButton)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Ring1, Ring1Button)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Ring2, Ring2Button)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Ring3, Ring3Button)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Ring4, Ring4Button)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Ring5, Ring5Button)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Ring6, Ring6Button)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Ring7, Ring7Button)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Ring8, Ring8Button)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Ring9, Ring9Button)
            _viewModel.RegisterButton(EquipmentManagerModel.SlotType.Ring10, Ring10Button)
        End Sub
    End Class

End Namespace