Namespace CharacterSystem.Common

#Region "インターフェースと基本実装"

    ' 基本実装
    Public Class StatusPointModel

        ' プロパティ
        Public Property Name As String
        Public Property Status As New StatusPoint
        Private ReadOnly _previousStatus As New StatusPoint

        ' コンストラクタ
        Public Sub New(name As String)
            Me.Name = name
            Status.Power = 0
            Status.Agility = 0
            Status.Health = 0
            Status.Knowledge = 0
            Status.Wisdom = 0
            Status.Charisma = 0
            Status.Luck = 0

            SaveCurrentState()
        End Sub

        Public Sub New()
        End Sub

        ' 現在の状態を保存
        Private Sub SaveCurrentState()
            With _previousStatus
                .Power = Status.Power
                .Agility = Status.Agility
                .Health = Status.Health
                .Knowledge = Status.Knowledge
                .Wisdom = Status.Wisdom
                .Charisma = Status.Charisma
                .Luck = Status.Luck
            End With
        End Sub

        ' 値の検証
        Public Function Validate(value As Integer) As Boolean
            Return value >= 0
        End Function

        ' 前回値に戻す
        Public Sub Reset()
            With Status
                .Power = _previousStatus.Power
                .Agility = _previousStatus.Agility
                .Health = _previousStatus.Health
                .Knowledge = _previousStatus.Knowledge
                .Wisdom = _previousStatus.Wisdom
                .Charisma = _previousStatus.Charisma
                .Luck = _previousStatus.Luck
            End With
        End Sub

        ' 値の変更を確定
        Public Sub CommitChanges()
            SaveCurrentState()
        End Sub
    End Class
#End Region

End Namespace