Imports System.ComponentModel
Imports RS_EquipSimulation.CharacterSystem.Common
Imports RS_EquipSimulation.CharacterSystem.Operation.Basic
Imports RS_EquipSimulation.CharacterSystem.Operation.Status
Imports RS_EquipSimulation.MasterSystem.Common
Imports RS_EquipSimulation.MasterSystem.Option

Namespace CharacterSystem.Summary.Status

    Public Class StatusSummaryManagerModel
        Implements INotifyPropertyChanged
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private _isCalculating As Boolean = False
        Private ReadOnly _optionManager As OptionManagerModel
        Private ReadOnly _statusManager As StatusManagerModel
        Private ReadOnly _basicManager As BasicManagerModel

        ' 基本ステータス（初期値+振り分け+その他）
        Public Property BaseStats As New StatusPoint

        ' 全体の合計結果
        Private _totalStats As New StatusPoint
        Public Property TotalStats As StatusPoint
            Get
                Return _totalStats
            End Get
            Set(value As StatusPoint)
                _totalStats = value
                OnPropertyChanged(NameOf(TotalStats))
            End Set
        End Property

        ' 各ソースごとの計算結果を保持する辞書
        Public Property SourceStatus As New Dictionary(Of String, StatusPoint)()

        Public Sub New(optManager As OptionManagerModel, statusManager As StatusManagerModel, basicInfo As BasicManagerModel)
            _optionManager = optManager
            _statusManager = statusManager
            _basicManager = basicInfo
        End Sub

        ' 計算メソッド
        Public Sub CalculateStatus()
            If _isCalculating Then Return
            Try
                _isCalculating = True
                ' 1. 職業の初期値を取得
                Dim jobData = JobNameManager.GetJobDataByName(_basicManager.JobName)
                If jobData IsNot Nothing Then
                    Dim isReverse = jobData.Reverse

                    ' 2. 基本値の計算（初期値+振り分け+その他）- Reverse適用済み
                    CalculateBaseStats(jobData.JobStatus, jobData.AutoRise, isReverse)

                    ' 3. 各ソースごとの計算（純粋オプションを含む）
                    CalculateSourceStats(isReverse)

                    ' 4. 全体の合計を計算
                    CalculateTotalStats()

                    ' 5. 計算結果変化通知
                    OnPropertyChanged(NameOf(TotalStats))

                End If
            Finally
                _isCalculating = False
            End Try
        End Sub

        ' 基本ステータス値の計算（初期値+振り分け+その他）
        Private Sub CalculateBaseStats(JobStatus As StatusPoint, autoRise As StatusEnum?, isReverse As Boolean)
            ' 初期値の取得
            Dim initialStats = JobStatus
            Dim allocateStatus = _statusManager.GetAllocationStatus

            ' BaseStatsに設定（初期値+振り分け+その他）
            BaseStats.Power = initialStats.Power + allocateStatus.Power
            BaseStats.Agility = initialStats.Agility + allocateStatus.Agility
            BaseStats.Health = initialStats.Health + allocateStatus.Health
            BaseStats.Knowledge = initialStats.Knowledge + allocateStatus.Knowledge
            BaseStats.Wisdom = initialStats.Wisdom + allocateStatus.Wisdom
            BaseStats.Charisma = initialStats.Charisma + allocateStatus.Charisma
            BaseStats.Luck = initialStats.Luck + allocateStatus.Luck

            Dim addLevelStatus = _basicManager.Level - 1
            Select Case (autoRise)
                Case StatusEnum.Power
                    BaseStats.Power += addLevelStatus
                Case StatusEnum.Agility
                    BaseStats.Agility += addLevelStatus
                Case StatusEnum.Health
                    BaseStats.Health += addLevelStatus
                Case StatusEnum.Knowledge
                    BaseStats.Knowledge += addLevelStatus
                Case StatusEnum.Wisdom
                    BaseStats.Wisdom += addLevelStatus
                Case StatusEnum.Charisma
                    BaseStats.Charisma += addLevelStatus
                Case StatusEnum.Luck
                    BaseStats.Luck += addLevelStatus
                Case Else
                    '何もしない
            End Select

            ' Reverse処理が必要な場合は適用
            If isReverse Then
                ApplyReverseToStats(BaseStats)
            End If
        End Sub

        ' 各ソースごとのステータス計算
        Private Sub CalculateSourceStats(isReverse As Boolean)
            ' SourceStatsをクリア
            SourceStatus.Clear()

            ' 登録されている全ソースを取得
            Dim sources = _optionManager.GetRegisteredSources()

            For Each source In sources
                ' ソースごとのオプションを取得
                Dim sourceOptions = _optionManager.GetOptionsFromSource(source)

                ' 新しいステータスポイントを作成
                Dim sourceStats = New StatusPoint()

                ' オプションの分類
                Dim normalOptions As New List(Of OpValue)()
                Dim ratioOptions As New List(Of OpValue)()
                Dim pureOptions As New List(Of OpValue)()

                ' オプションを種類別に分類
                ClassifyOptions(sourceOptions, normalOptions, ratioOptions, pureOptions)

                ' 通常加算の適用
                ApplyNormalOptions(sourceStats, normalOptions, isReverse)

                ' 比率オプションの適用（レベル依存）
                ApplyRatioOptions(sourceStats, ratioOptions, _basicManager.Level, isReverse)

                ' 純粋オプションの適用（BaseStatsに対する比率計算）
                ApplyPureOptions(sourceStats, pureOptions, BaseStats, isReverse)

                ' 結果を保存
                SourceStatus.Add(source, sourceStats)
            Next
        End Sub

        ' 合計ステータスの計算
        Private Sub CalculateTotalStats()
            ' TotalStatsを初期化
            TotalStats = New StatusPoint()
            Dim otherStatus = _statusManager.GetOtherStatus

            ' BaseStats値をコピー
            TotalStats.Power = BaseStats.Power + otherStatus.Power
            TotalStats.Agility = BaseStats.Agility + otherStatus.Agility
            TotalStats.Health = BaseStats.Health + otherStatus.Health
            TotalStats.Knowledge = BaseStats.Knowledge + otherStatus.Knowledge
            TotalStats.Wisdom = BaseStats.Wisdom + otherStatus.Wisdom
            TotalStats.Charisma = BaseStats.Charisma + otherStatus.Charisma
            TotalStats.Luck = BaseStats.Luck + otherStatus.Luck

            ' 各ソースのステータス値を加算
            For Each SourceStats In SourceStatus.Values
                TotalStats.Power += SourceStats.Power
                TotalStats.Agility += SourceStats.Agility
                TotalStats.Health += SourceStats.Health
                TotalStats.Knowledge += SourceStats.Knowledge
                TotalStats.Wisdom += SourceStats.Wisdom
                TotalStats.Charisma += SourceStats.Charisma
                TotalStats.Luck += SourceStats.Luck
            Next
        End Sub

        ' オプションの分類
        Private Sub ClassifyOptions(options As List(Of OpValue), normalOptions As List(Of OpValue),
                               ratioOptions As List(Of OpValue), pureOptions As List(Of OpValue))
            For Each op In options
                Dim optType = OptionSingletonModel.Instance.GetOptionType(op.OptionName)

                If optType = "比率" Then
                    ratioOptions.Add(op)
                ElseIf optType = "ステ上昇" Then
                    If op.OptionName.Contains("純粋") Then
                        pureOptions.Add(op)
                    Else
                        normalOptions.Add(op)
                    End If
                End If
            Next
        End Sub

        ' 通常加算オプションの適用
        Private Sub ApplyNormalOptions(stats As StatusPoint, options As List(Of OpValue), isReverse As Boolean)
            For Each op In options
                Dim value = Convert.ToInt32(op.OptionValue)

                ' 全ステータスに影響するオプションの処理
                If op.OptionName.Contains("全") Then
                    ApplyToAllStats(stats, value)
                    Continue For
                End If

                ' キーワードに基づいてステータスを更新
                If op.OptionName.Contains("力") Then
                    If isReverse Then
                        stats.Knowledge += value
                    Else
                        stats.Power += value
                    End If
                End If

                If op.OptionName.Contains("敏捷") Then
                    If isReverse Then
                        stats.Wisdom += value
                    Else
                        stats.Agility += value
                    End If
                End If

                If op.OptionName.Contains("健康") Then
                    If isReverse Then
                        stats.Charisma += value
                    Else
                        stats.Health += value
                    End If
                End If

                If op.OptionName.Contains("知識") Then
                    If isReverse Then
                        stats.Power += value
                    Else
                        stats.Knowledge += value
                    End If
                End If

                If op.OptionName.Contains("知恵") Then
                    If isReverse Then
                        stats.Agility += value
                    Else
                        stats.Wisdom += value
                    End If
                End If

                If op.OptionName.Contains("威厳") Then
                    If isReverse Then
                        stats.Health += value
                    Else
                        stats.Charisma += value
                    End If
                End If

                If op.OptionName.Contains("運") Then
                    stats.Luck += value
                End If
            Next
        End Sub

        ' 比率オプションの適用
        Private Sub ApplyRatioOptions(stats As StatusPoint, options As List(Of OpValue), level As Integer, isReverse As Boolean)
            For Each op In options
                Dim ratio = Convert.ToDouble(op.OptionValue)
                Dim value = CInt(Math.Floor(level * ratio))

                ' キーワードに基づいてステータスを更新
                If op.OptionName.Contains("力") Then
                    If isReverse Then
                        stats.Knowledge += value
                    Else
                        stats.Power += value
                    End If
                End If

                If op.OptionName.Contains("敏捷") Then
                    If isReverse Then
                        stats.Wisdom += value
                    Else
                        stats.Agility += value
                    End If
                End If

                If op.OptionName.Contains("健康") Then
                    If isReverse Then
                        stats.Charisma += value
                    Else
                        stats.Health += value
                    End If
                End If

                If op.OptionName.Contains("知識") Then
                    If isReverse Then
                        stats.Power += value
                    Else
                        stats.Knowledge += value
                    End If
                End If

                If op.OptionName.Contains("知恵") Then
                    If isReverse Then
                        stats.Agility += value
                    Else
                        stats.Wisdom += value
                    End If
                End If

                If op.OptionName.Contains("威厳") Then
                    If isReverse Then
                        stats.Health += value
                    Else
                        stats.Charisma += value
                    End If
                End If

                If op.OptionName.Contains("運") Then
                    stats.Luck += value
                End If
            Next
        End Sub

        ' 純粋オプションの適用
        Private Sub ApplyPureOptions(stats As StatusPoint, options As List(Of OpValue), baseStats As StatusPoint, isReverse As Boolean)
            For Each op In options
                Dim percentage = Convert.ToDouble(op.OptionValue) / 100.0

                ' 全ステータスに影響する純粋オプションの処理
                If op.OptionName.Contains("全") Then
                    ApplyPureToAllStats(stats, baseStats, percentage)
                    Continue For
                End If

                ' キーワードに基づいてステータスを更新
                If op.OptionName.Contains("力") Then
                    If isReverse Then
                        stats.Knowledge += CInt(Math.Floor(baseStats.Knowledge * percentage))
                    Else
                        stats.Power += CInt(Math.Floor(baseStats.Power * percentage))
                    End If
                End If

                If op.OptionName.Contains("敏捷") Then
                    If isReverse Then
                        stats.Wisdom += CInt(Math.Floor(baseStats.Wisdom * percentage))
                    Else
                        stats.Agility += CInt(Math.Floor(baseStats.Agility * percentage))
                    End If
                End If

                If op.OptionName.Contains("健康") Then
                    If isReverse Then
                        stats.Charisma += CInt(Math.Floor(baseStats.Charisma * percentage))
                    Else
                        stats.Health += CInt(Math.Floor(baseStats.Health * percentage))
                    End If
                End If

                If op.OptionName.Contains("知識") Then
                    If isReverse Then
                        stats.Power += CInt(Math.Floor(baseStats.Power * percentage))
                    Else
                        stats.Knowledge += CInt(Math.Floor(baseStats.Knowledge * percentage))
                    End If
                End If

                If op.OptionName.Contains("知恵") Then
                    If isReverse Then
                        stats.Agility += CInt(Math.Floor(baseStats.Agility * percentage))
                    Else
                        stats.Wisdom += CInt(Math.Floor(baseStats.Wisdom * percentage))
                    End If
                End If

                If op.OptionName.Contains("威厳") Then
                    If isReverse Then
                        stats.Health += CInt(Math.Floor(baseStats.Health * percentage))
                    Else
                        stats.Charisma += CInt(Math.Floor(baseStats.Charisma * percentage))
                    End If
                End If

                If op.OptionName.Contains("運") Then
                    stats.Luck += CInt(Math.Floor(baseStats.Luck * percentage))
                End If
            Next
        End Sub

        ' 全ステータスに値を適用
        Private Sub ApplyToAllStats(stats As StatusPoint, value As Integer)
            stats.Power += value
            stats.Agility += value
            stats.Health += value
            stats.Knowledge += value
            stats.Wisdom += value
            stats.Charisma += value
            stats.Luck += value
        End Sub

        ' 全ステータスに純粋オプションを適用
        Private Sub ApplyPureToAllStats(stats As StatusPoint, baseStats As StatusPoint, percentage As Double)
            stats.Power += CInt(Math.Floor(baseStats.Power * percentage))
            stats.Agility += CInt(Math.Floor(baseStats.Agility * percentage))
            stats.Health += CInt(Math.Floor(baseStats.Health * percentage))
            stats.Knowledge += CInt(Math.Floor(baseStats.Knowledge * percentage))
            stats.Wisdom += CInt(Math.Floor(baseStats.Wisdom * percentage))
            stats.Charisma += CInt(Math.Floor(baseStats.Charisma * percentage))
            stats.Luck += CInt(Math.Floor(baseStats.Luck * percentage))
        End Sub

        ' Reverse処理の適用
        Private Sub ApplyReverseToStats(stats As StatusPoint)
            ' 各ペアを入れ替え
            Dim temp As Integer

            ' 力 ⇔ 知識
            temp = stats.Power
            stats.Power = stats.Knowledge
            stats.Knowledge = temp

            ' 敏捷 ⇔ 知恵
            temp = stats.Agility
            stats.Agility = stats.Wisdom
            stats.Wisdom = temp

            ' 健康 ⇔ 威厳
            temp = stats.Health
            stats.Health = stats.Charisma
            stats.Charisma = temp
        End Sub

        ' 変更通知メソッド
        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class

End Namespace