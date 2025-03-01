Imports RS_EquipSimulation.Common.Singleton

Namespace MasterSystem.Smelt
    Public Interface ISmeltOpManager
        ' 基本的な操作のコントラクト
        Function GetOptionNames(stage As Integer) As List(Of String)
        Function GetValue(stage As Integer, opName As String, rank As String, level As Integer) As Integer
        Function GetAvailableStages(smeltLevel As Integer) As List(Of Integer)
    End Interface

    Public MustInherit Class SmeltOpManagerBase(Of T)
        Inherits SingletonConstantBase(Of T)
        Implements ISmeltOpManager

        ' 部位ごとのデータを管理
        Private _partData As SmeltDataModel
        Private _currentStage As Integer = 0
        Private _currentOpName As String = Nothing
        Private _currentLineCount As Integer = 0
        Private _currentValues As New List(Of Integer())()

        Protected Overrides Sub SafeInitialize()
            _partData = New SmeltDataModel
            _currentValues = New List(Of Integer())()
        End Sub

        Protected Overrides Sub ProcessLine(parts() As String)
            ' 空行または要素不足の行はスキップ
            If parts.Length < 3 Then Return

            ' 段階行の処理
            If parts(0).Contains("段階") Then
                ProcessCompleteGroup()  ' 前のグループを処理
                _currentStage = Integer.Parse(parts(0).Substring(0, parts(0).IndexOf("段階")))
                _currentLineCount = 0
                _currentValues.Clear()
                Return
            End If

            ' オプション名がある行の処理
            If Not String.IsNullOrWhiteSpace(parts(1)) Then
                ProcessCompleteGroup()  ' 前のグループを処理
                _currentOpName = parts(1)
                _currentLineCount = 0
                _currentValues.Clear()
            End If

            ' 値の処理
            If _currentOpName IsNot Nothing Then
                Dim values = parts.Skip(3).Take(20).Select(Function(v) Integer.Parse(v)).ToArray()
                _currentValues.Add(values)
                _currentLineCount += 1

                ' 3行（下級、中級、上級）揃ったら処理
                If _currentLineCount = 3 Then
                    ProcessCompleteGroup()
                End If
            End If
        End Sub

        Private Sub ProcessCompleteGroup()
            If _currentOpName IsNot Nothing AndAlso _currentValues.Count = 3 Then
                ' 段階グループの取得または作成
                Dim stageGroup = GetOrCreateStageGroup(_partData, _currentStage)

                ' オプションの追加または更新
                stageGroup.AddOrUpdateOption(_currentOpName,
                                       _currentValues(0),  ' 下級
                                       _currentValues(1),  ' 中級
                                       _currentValues(2))  ' 上級
            End If

            ' 状態のリセット
            _currentOpName = Nothing
            _currentLineCount = 0
            _currentValues.Clear()
        End Sub

        Private Function GetOrCreateStageGroup(partData As SmeltDataModel, stage As Integer) As SmeltStageGroup
            Dim stages = partData.GetStages()
            If Not stages.Contains(stage) Then
                Dim newGroup = New SmeltStageGroup(stage)
                partData.AddStage(newGroup)
                Return newGroup
            End If
            Return partData.GetStageGroup(stage)
        End Function

#Region "公開メソッド"
        Public Function GetOptionNames(stage As Integer) As List(Of String) Implements ISmeltOpManager.GetOptionNames
            Return _partData.GetOptionNames(stage)
        End Function

        Public Function GetValue(stage As Integer, opName As String, rank As String, level As Integer) As Integer Implements ISmeltOpManager.GetValue
            Return _partData.GetValue(stage, opName, rank, level)
        End Function

        Public Function GetAvailableStages(smeltLevel As Integer) As List(Of Integer) Implements ISmeltOpManager.GetAvailableStages
            Dim stages As New List(Of Integer)
            If smeltLevel >= 1 Then stages.Add(1)
            If smeltLevel >= 5 Then stages.Add(5)
            If smeltLevel >= 10 Then stages.Add(10)
            If smeltLevel >= 15 Then stages.Add(15)
            If smeltLevel >= 18 Then stages.Add(18)
            Return stages
        End Function
#End Region
    End Class

End Namespace