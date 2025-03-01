Imports System.ComponentModel

Namespace CharacterSystem.Common

    ''' <summary>
    ''' ステータス表示行の基底ViewModel
    ''' </summary>
    Public MustInherit Class StatusGridRowBase
        Implements INotifyPropertyChanged

        ' PropertyChangedイベント
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        ' 行の名前/識別子
        Private _rowName As String
        Public Property RowName As String
            Get
                Return _rowName
            End Get
            Set(value As String)
                _rowName = value
                OnPropertyChanged(NameOf(RowName))
                OnPropertyChanged(NameOf(DisplayName))
            End Set
        End Property

        ' 表示用名称（オーバーライド可能）
        Public Overridable ReadOnly Property DisplayName As String
            Get
                Return RowName
            End Get
        End Property

        ' 編集可能フラグ（デフォルトはFalse）
        Private _isEditable As Boolean = False
        Public Property IsEditable As Boolean
            Get
                Return _isEditable
            End Get
            Set(value As Boolean)
                _isEditable = value
                OnPropertyChanged(NameOf(IsEditable))
            End Set
        End Property

        ' ステータス値
        Private _power As Integer
        Public Property Power As Integer
            Get
                Return _power
            End Get
            Set(value As Integer)
                _power = value
                OnPropertyChanged(NameOf(Power))
            End Set
        End Property

        Private _agility As Integer
        Public Property Agility As Integer
            Get
                Return _agility
            End Get
            Set(value As Integer)
                _agility = value
                OnPropertyChanged(NameOf(Agility))
            End Set
        End Property

        Private _health As Integer
        Public Property Health As Integer
            Get
                Return _health
            End Get
            Set(value As Integer)
                _health = value
                OnPropertyChanged(NameOf(Health))
            End Set
        End Property

        Private _knowledge As Integer
        Public Property Knowledge As Integer
            Get
                Return _knowledge
            End Get
            Set(value As Integer)
                _knowledge = value
                OnPropertyChanged(NameOf(Knowledge))
            End Set
        End Property

        Private _wisdom As Integer
        Public Property Wisdom As Integer
            Get
                Return _wisdom
            End Get
            Set(value As Integer)
                _wisdom = value
                OnPropertyChanged(NameOf(Wisdom))
            End Set
        End Property

        Private _charisma As Integer
        Public Property Charisma As Integer
            Get
                Return _charisma
            End Get
            Set(value As Integer)
                _charisma = value
                OnPropertyChanged(NameOf(Charisma))
            End Set
        End Property

        Private _luck As Integer
        Public Property Luck As Integer
            Get
                Return _luck
            End Get
            Set(value As Integer)
                _luck = value
                OnPropertyChanged(NameOf(Luck))
            End Set
        End Property

        ' コンストラクタ
        Public Sub New(rowName As String, Optional isEditable As Boolean = False)
            Me.RowName = rowName
            Me.IsEditable = isEditable
            InitializeValues()
        End Sub

        ' 空のコンストラクタ（シリアライズ用）
        Protected Sub New()
            InitializeValues()
        End Sub

        ' 値の初期化
        Protected Overridable Sub InitializeValues()
            Power = 0
            Agility = 0
            Health = 0
            Knowledge = 0
            Wisdom = 0
            Charisma = 0
            Luck = 0
        End Sub

        ' StatusPointからデータを設定するユーティリティメソッド
        Public Sub SetFromStatusPoint(stats As StatusPoint)
            Power = stats.Power
            Agility = stats.Agility
            Health = stats.Health
            Knowledge = stats.Knowledge
            Wisdom = stats.Wisdom
            Charisma = stats.Charisma
            Luck = stats.Luck
        End Sub

        ' StatusPointにデータを書き込むユーティリティメソッド
        Public Sub WriteToStatusPoint(ByRef stats As StatusPoint)
            stats.Power = Power
            stats.Agility = Agility
            stats.Health = Health
            stats.Knowledge = Knowledge
            stats.Wisdom = Wisdom
            stats.Charisma = Charisma
            stats.Luck = Luck
        End Sub

        ' プロパティ変更通知
        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class

End Namespace