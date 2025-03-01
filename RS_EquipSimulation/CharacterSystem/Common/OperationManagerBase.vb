Imports Newtonsoft.Json

Namespace CharacterSystem.Common

    ''' <summary>
    ''' Operation系マネージャーの基底クラス
    ''' </summary>
    ''' <remarks>
    ''' Operation層のManagerモデルは必ず継承すること
    ''' </remarks>
    Public MustInherit Class OperationManagerBase
        Inherits OptionProviderBase
        Implements IChangeTrackable

#Region "変更フラグ"
        Private _hasChanged As Boolean

        ''' <summary>
        ''' データの変更有無
        ''' </summary>
        <JsonIgnore>
        Public Property HasChanged As Boolean Implements IChangeTrackable.HasChanged
            Get
                Return _hasChanged
            End Get
            Protected Set(value As Boolean)
                _hasChanged = value
            End Set
        End Property

        ''' <summary>
        ''' データ変更時の通知メソッド
        ''' </summary>
        Protected Overrides Sub NotifyChanged()
            MyBase.NotifyChanged()
            HasChanged = True
        End Sub

        ''' <summary>
        ''' 変更フラグのリセット
        ''' </summary>
        Public Sub ResetChangeFlag()
            HasChanged = False
        End Sub
#End Region

#Region "コンストラクタ"
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="optManager">オプション管理クラス</param>
        Protected Sub New(optManager As OptionManagerModel)
            MyBase.New(optManager)
            ' 変更フラグの初期化
            ResetChangeFlag()
        End Sub
#End Region

#Region "シリアライズ用"
        ''' <summary>
        ''' デシリアライズ時に必要な空のコンストラクタ
        ''' </summary>
        Protected Sub New()
            MyBase.New(Nothing)
            ' 変更フラグの初期化
            ResetChangeFlag()
        End Sub

        ''' <summary>
        ''' シリアライズ後の初期化処理
        ''' </summary>
        ''' <param name="optManager">オプション管理クラス</param>
        Public Sub InitializeAfterDeserialize()
            ' 派生クラス固有の初期化
            OnInitializeAfterDeserialize()
            ' オプション値の更新
            UpdateOptionValues()
        End Sub

        ''' <summary>
        ''' デシリアライズ後の派生クラス固有の初期化処理
        ''' </summary>
        Protected Overridable Sub OnInitializeAfterDeserialize()
            ' 派生クラスで必要に応じてオーバーライド
        End Sub
#End Region
    End Class

    ''' <summary>
    ''' 変更管理インターフェース
    ''' </summary>
    Public Interface IChangeTrackable
        ''' <summary>
        ''' データの変更有無
        ''' </summary>
        Property HasChanged As Boolean
    End Interface

End Namespace