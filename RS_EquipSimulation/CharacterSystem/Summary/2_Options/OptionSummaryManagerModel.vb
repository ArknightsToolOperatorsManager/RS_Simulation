Imports System.ComponentModel
Imports RS_EquipSimulation.CharacterSystem.Common
Imports RS_EquipSimulation.MasterSystem.Option

Namespace CharacterSystem.Summary.Option

    Public Class OptionSummaryManagerModel
        Implements INotifyPropertyChanged

        ' オプション管理用の参照
        Private ReadOnly _optionManager As OptionManagerModel

        ' フィルタリング済みオプションの集計結果
        Private _summaryOptions As New Dictionary(Of String, Double)
        Public ReadOnly Property SummaryOptions As Dictionary(Of String, Double)
            Get
                Return _summaryOptions
            End Get
        End Property

        ' PropertyChangedイベント
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        ' コンストラクタ
        Public Sub New(optManager As OptionManagerModel)
            _optionManager = optManager
        End Sub

        Public Sub CalculateOptions()
            ' 集計結果をクリア
            _summaryOptions.Clear()

            ' 選択対象外の種類（これらは常に除外）
            Dim excludedTypes As New List(Of String) From {"比率", "ステ上昇", "未実装"}

            ' すべてのオプションを取得し集計
            Dim allOptions = _optionManager.GetAllOptions()

            ' オプション名ごとに集計（除外対象以外すべて）
            For Each op In allOptions
                ' オプション種類を取得
                Dim type = OptionSingletonModel.Instance.GetOptionType(op.OptionName)

                ' 除外対象はスキップ
                If excludedTypes.Contains(type) Then
                    Continue For
                End If

                ' オプション値を数値に変換
                Dim value As Double
                If Double.TryParse(op.OptionValue, value) Then
                    ' 集計結果に加算
                    Select Case op.OptionName
                        Case "全属性強化"
                            RegisterSummary("火強化", value)
                            RegisterSummary("水強化", value)
                            RegisterSummary("風強化", value)
                            RegisterSummary("大地強化", value)
                            RegisterSummary("闇強化", value)
                            RegisterSummary("光強化", value)
                        Case "全属性弱化"
                            RegisterSummary("火弱化", value)
                            RegisterSummary("水弱化", value)
                            RegisterSummary("風弱化", value)
                            RegisterSummary("大地弱化", value)
                            RegisterSummary("闇弱化", value)
                            RegisterSummary("光弱化", value)
                        Case "全属性吸収"
                            RegisterSummary("火吸収", value)
                            RegisterSummary("水吸収", value)
                            RegisterSummary("風吸収", value)
                            RegisterSummary("大地吸収", value)
                            RegisterSummary("闇吸収", value)
                            RegisterSummary("光吸収", value)
                        Case "全属性抵抗"
                            RegisterSummary("火抵抗", value)
                            RegisterSummary("水抵抗", value)
                            RegisterSummary("風抵抗", value)
                            RegisterSummary("大地抵抗", value)
                            RegisterSummary("闇抵抗", value)
                            RegisterSummary("光抵抗", value)
                        Case Else
                            RegisterSummary(op.OptionName, value)
                    End Select
                End If
            Next

            ' 計算結果変化通知
            OnPropertyChanged(NameOf(SummaryOptions))
        End Sub

        Private Sub RegisterSummary(opName As String, val As Double)
            If _summaryOptions.ContainsKey(opName) Then
                _summaryOptions(opName) = val
            Else
                _summaryOptions.Add(opName, val)
            End If
        End Sub

        ' 変更通知メソッド
        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class

End Namespace