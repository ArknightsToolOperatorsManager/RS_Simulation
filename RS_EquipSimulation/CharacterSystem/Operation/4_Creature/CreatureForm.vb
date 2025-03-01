Imports RS_EquipSimulation.Common.Creature

Namespace CharacterSystem.Operation.Creature

    Public Class CreatureForm
        ' デザイナーで生成されたコンポーネントは既に定義されています

        Private ReadOnly _viewModel As CreatureViewModel

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        Public Sub New(creatureManager As CreatureManagerModel, inventoryReader As ICreatureInventoryReader)
            ' これはコンポーネント設計からの呼び出しで必要です
            InitializeComponent()

            ' パッシブリストの設定
            ListView1.View = View.Details
            ListView1.FullRowSelect = True
            ListView1.GridLines = True
            ListView1.Columns.Add("パッシブ名", 100)
            ListView1.Columns.Add("レベル", 45)

            ' ViewModelの初期化
            _viewModel = New CreatureViewModel(creatureManager, inventoryReader, ListView1)

            ' ボタンの登録
            _viewModel.RegisterButton(CreatureManagerModel.SlotType.Main, MainCreatureButton)
            _viewModel.RegisterButton(CreatureManagerModel.SlotType.Sub1, SubCreature1Buton)
            _viewModel.RegisterButton(CreatureManagerModel.SlotType.Sub2, SubCreature2Button)
            _viewModel.RegisterButton(CreatureManagerModel.SlotType.Sub3, SubCreature3Button)

            ' 初期表示の更新
            _viewModel.UpdateAllButtons()
        End Sub

        ''' <summary>
        ''' フォームが閉じられる前にリソースを解放
        ''' </summary>
        Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
            _viewModel.Dispose()
            MyBase.OnFormClosing(e)
        End Sub
    End Class

End Namespace