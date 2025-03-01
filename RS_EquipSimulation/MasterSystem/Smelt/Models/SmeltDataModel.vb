Namespace MasterSystem.Smelt

    ' 段階ごとのオプション定義
    Public Class SmeltOpDefinition
        ' オプション名
        Public Property Name As String

        ' ランクごとの値配列（1-20の値）
        Private ReadOnly _rankValues As New Dictionary(Of String, Integer()) From {
            {"下級", Nothing},
            {"中級", Nothing},
            {"上級", Nothing}
        }

        Public Sub New(name As String)
            Me.Name = name
        End Sub

        Public Sub SetRankValues(rank As String, values As Integer())
            If _rankValues.ContainsKey(rank) Then
                _rankValues(rank) = values
            End If
        End Sub

        Public Function GetValue(rank As String, level As Integer) As Integer
            If Not _rankValues.ContainsKey(rank) OrElse
               _rankValues(rank) Is Nothing OrElse
               level < 1 OrElse
               level > _rankValues(rank).Length Then
                Return 0
            End If
            Return _rankValues(rank)(level - 1)
        End Function

        Public Sub SetAllRankValues(lower As Integer(), middle As Integer(), upper As Integer())
            SetRankValues("下級", lower)
            SetRankValues("中級", middle)
            SetRankValues("上級", upper)
        End Sub
    End Class

    ' 段階（1,5,10等）のオプション群を管理
    Public Class SmeltStageGroup
        ' 段階値（1,5,10,15,18）
        Public Property Stage As Integer

        ' この段階で選択可能なオプション群
        Private ReadOnly _options As New Dictionary(Of String, SmeltOpDefinition)

        Public Sub New(stage As Integer)
            Me.Stage = stage
        End Sub

        Public Sub AddOrUpdateOption(name As String, lower As Integer(), middle As Integer(), upper As Integer())
            If Not _options.ContainsKey(name) Then
                _options(name) = New SmeltOpDefinition(name)
            End If
            _options(name).SetAllRankValues(lower, middle, upper)
        End Sub

        Public Function GetOptionNames() As List(Of String)
            Return _options.Keys.ToList()
        End Function

        Public Function GetOption(name As String) As SmeltOpDefinition
            Return If(_options.ContainsKey(name), _options(name), Nothing)
        End Function
    End Class

    ' 部位ごとの精錬オプションを管理
    Public Class SmeltDataModel
        Private ReadOnly _stageGroups As New Dictionary(Of Integer, SmeltStageGroup)

        ' 段階を追加
        Public Sub AddStage(stage As SmeltStageGroup)
            _stageGroups(stage.Stage) = stage
        End Sub

        ' 段階のリストを取得
        Public Function GetStages() As List(Of Integer)
            Return _stageGroups.Keys.OrderBy(Function(k) k).ToList()
        End Function

        ' 指定段階のオプション名リストを取得
        Public Function GetOptionNames(stage As Integer) As List(Of String)
            Dim value As SmeltStageGroup = Nothing
            If Not _stageGroups.TryGetValue(stage, value) Then Return New List(Of String)
            Return value.GetOptionNames()
        End Function

        ' オプション値を取得
        Public Function GetValue(stage As Integer, opName As String, rank As String, level As Integer) As Integer
            If Not _stageGroups.ContainsKey(stage) Then Return 0

            Dim op = _stageGroups(stage).GetOption(opName)
            If op Is Nothing Then Return 0

            Return op.GetValue(rank, level)
        End Function

        ' 段階グループを取得
        Public Function GetStageGroup(stage As Integer) As SmeltStageGroup
            If Not _stageGroups.ContainsKey(stage) Then Return Nothing
            Return _stageGroups(stage)
        End Function
    End Class

End Namespace