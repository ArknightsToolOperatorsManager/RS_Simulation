Imports System.ComponentModel

Namespace CharacterSystem.Summary.Status

    Public Class StatusSummaryViewModel
        Implements INotifyPropertyChanged
        Implements IDisposable

        ' モデル参照
        Private ReadOnly _summaryManager As StatusSummaryManagerModel

        ' グリッドデータソース
        Private ReadOnly _gridData As New BindingList(Of StatusSummaryGridRowViewModel)
        Public ReadOnly Property GridData As BindingList(Of StatusSummaryGridRowViewModel)
            Get
                Return _gridData
            End Get
        End Property

        ' ステータス合計値
        Private _power As Integer
        Private _agility As Integer
        Private _health As Integer
        Private _knowledge As Integer
        Private _wisdom As Integer
        Private _charisma As Integer
        Private _luck As Integer

        ' 各ステータス値のプロパティ
        Public Property Power As Integer
            Get
                Return _power
            End Get
            Set(value As Integer)
                If _power <> value Then
                    _power = value
                    OnPropertyChanged(NameOf(Power))
                End If
            End Set
        End Property

        Public Property Agility As Integer
            Get
                Return _agility
            End Get
            Set(value As Integer)
                If _agility <> value Then
                    _agility = value
                    OnPropertyChanged(NameOf(Agility))
                End If
            End Set
        End Property

        Public Property Health As Integer
            Get
                Return _health
            End Get
            Set(value As Integer)
                If _health <> value Then
                    _health = value
                    OnPropertyChanged(NameOf(Health))
                End If
            End Set
        End Property

        Public Property Knowledge As Integer
            Get
                Return _knowledge
            End Get
            Set(value As Integer)
                If _knowledge <> value Then
                    _knowledge = value
                    OnPropertyChanged(NameOf(Knowledge))
                End If
            End Set
        End Property

        Public Property Wisdom As Integer
            Get
                Return _wisdom
            End Get
            Set(value As Integer)
                If _wisdom <> value Then
                    _wisdom = value
                    OnPropertyChanged(NameOf(Wisdom))
                End If
            End Set
        End Property

        Public Property Charisma As Integer
            Get
                Return _charisma
            End Get
            Set(value As Integer)
                If _charisma <> value Then
                    _charisma = value
                    OnPropertyChanged(NameOf(Charisma))
                End If
            End Set
        End Property

        Public Property Luck As Integer
            Get
                Return _luck
            End Get
            Set(value As Integer)
                If _luck <> value Then
                    _luck = value
                    OnPropertyChanged(NameOf(Luck))
                End If
            End Set
        End Property

        ' コンストラクタ
        Public Sub New(summaryManager As StatusSummaryManagerModel)
            _summaryManager = summaryManager

            ' SummaryManagerのプロパティ変更イベントを購読
            AddHandler _summaryManager.PropertyChanged, AddressOf SummaryManager_PropertyChanged

            ' 初回更新
            UpdateDisplay()
        End Sub

        ' 状態の更新
        Public Sub UpdateDisplay()
            ' ステータス計算の実行
            _summaryManager.CalculateStatus()

            ' グリッドデータの更新
            UpdateGridData()

            ' 合計値の更新
            Power = _summaryManager.TotalStats.Power
            Agility = _summaryManager.TotalStats.Agility
            Health = _summaryManager.TotalStats.Health
            Knowledge = _summaryManager.TotalStats.Knowledge
            Wisdom = _summaryManager.TotalStats.Wisdom
            Charisma = _summaryManager.TotalStats.Charisma
            Luck = _summaryManager.TotalStats.Luck
        End Sub

        ' グリッドデータの更新
        Private Sub UpdateGridData()
            _gridData.Clear()

            ' 各ソースのデータを追加
            For Each source In _summaryManager.SourceStatus
                Dim sourceVM = New StatusSummaryGridRowViewModel(source.Key, False)  ' 読み取り専用
                sourceVM.SetFromStatusPoint(source.Value)
                _gridData.Add(sourceVM)
            Next
        End Sub

        ' SummaryManagerのプロパティ変更イベントハンドラ
        Private Sub SummaryManager_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            UpdateDisplay()
        End Sub

        ' PropertyChangedイベント
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        ' IDisposable実装
        Public Sub Dispose() Implements IDisposable.Dispose
            RemoveHandler _summaryManager.PropertyChanged, AddressOf SummaryManager_PropertyChanged
        End Sub
    End Class

End Namespace