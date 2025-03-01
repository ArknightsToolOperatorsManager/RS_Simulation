Imports System.ComponentModel
Imports RS_EquipSimulation.CharacterSystem.Common
Imports RS_EquipSimulation.MasterSystem.Option

Namespace CharacterSystem.Summary.Option

    Public Class OptionSummaryViewModel
        Implements INotifyPropertyChanged
        Implements IDisposable

        ' モデル参照
        Private ReadOnly _summaryManager As OptionSummaryManagerModel

        ' 種類フィルタ
        Private ReadOnly _selectedTypes As New List(Of String)
        Public ReadOnly Property SelectedTypes As List(Of String)
            Get
                Return _selectedTypes
            End Get
        End Property

        ' PropertyChangedイベント
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        ' コンストラクタ
        Public Sub New(summaryManager As OptionSummaryManagerModel)
            _summaryManager = summaryManager

            ' 初期状態では全ての種類（除外対象以外）を選択
            InitializeSelectedTypes()

            ' SummaryManagerのプロパティ変更イベントを購読
            AddHandler _summaryManager.PropertyChanged, AddressOf SummaryManager_PropertyChanged
        End Sub

        ' 種類選択状態の初期化
        Private Sub InitializeSelectedTypes()
            ' 選択対象外の種類
            Dim excludedTypes As New List(Of String) From {"比率", "ステ上昇", "未実装"}

            ' 利用可能な全種類を取得
            Dim allTypes = OptionSingletonModel.Instance.GetAllOptionTypes()

            ' 除外対象以外の種類を選択状態に
            _selectedTypes.Clear()
            For Each type As String In allTypes
                If Not excludedTypes.Contains(type) Then
                    _selectedTypes.Add(type)
                End If
            Next
        End Sub

        ' 種類選択状態の更新
        Public Sub UpdateTypeSelection(type As String, isSelected As Boolean)
            If isSelected Then
                If Not _selectedTypes.Contains(type) Then
                    _selectedTypes.Add(type)
                End If
            Else
                _selectedTypes.Remove(type)
            End If

            ' 変更通知
            OnPropertyChanged(NameOf(SelectedTypes))
        End Sub

        ' フィルタリングされたオプションを取得 - ViewModelの責務
        Public Function GetFilteredOptions() As List(Of (String, Double))
            Dim filteredResults As New List(Of (String, Double))

            ' _summaryManagerのデータをフィルタリング
            For Each kvp In _summaryManager.SummaryOptions
                Dim optionName = kvp.Key
                Dim type = OptionSingletonModel.Instance.GetOptionType(optionName)

                ' 選択された種類に含まれている場合
                If _selectedTypes.Contains(type) Then
                    filteredResults.Add((optionName, kvp.Value))
                    Continue For
                End If

                ' 「物理系/魔法系」の場合の特別処理
                ' 「物理系」または「魔法系」が選択されていれば「物理系/魔法系」も表示
                If type = "物理系/魔法系" AndAlso (_selectedTypes.Contains("物理系") OrElse _selectedTypes.Contains("魔法系")) Then
                    filteredResults.Add((optionName, kvp.Value))
                End If
            Next

            ' 結果をソートして返す
            Return filteredResults.OrderBy(Function(item) item.Item1).ToList()
        End Function

        ' SummaryManagerのプロパティ変更イベントハンドラ
        Private Sub SummaryManager_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            ' 変更通知
            OnPropertyChanged("FilteredOptions")
        End Sub

        ' 変更通知メソッド
        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        ' リソース解放
        Public Sub Dispose() Implements IDisposable.Dispose
            ' イベントハンドラの解除
            RemoveHandler _summaryManager.PropertyChanged, AddressOf SummaryManager_PropertyChanged
        End Sub
    End Class

End Namespace