Imports RS_EquipSimulation.CharacterSystem.Common

Namespace CharacterSystem.Operation.Status

    ' StatusFormで使用する行ViewModel
    Public Class StatusGridRowViewModel
        Inherits StatusGridRowBase

        ' ステータスデータへの参照
        Private ReadOnly _statusData As StatusPointModel

        Public Sub New(rowName As String, isEditable As Boolean, statusData As StatusPointModel)
            MyBase.New(rowName, isEditable)  ' 編集可能をTrueに設定
            _statusData = statusData
            SetFromStatusPoint(statusData.Status)
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        ' ステータスモデルを更新
        Public Sub UpdateStatusModel()
            WriteToStatusPoint(_statusData.Status)
            _statusData.CommitChanges()
        End Sub

        Public Function ApplyChanges(propertyName As String, value As Integer) As Boolean
            ' プロパティ名に基づいて該当するプロパティを更新
            Select Case propertyName
                Case "Power"
                    Power = value
                Case "Agility"
                    Agility = value
                Case "Health"
                    Health = value
                Case "Knowledge"
                    Knowledge = value
                Case "Wisdom"
                    Wisdom = value
                Case "Charisma"
                    Charisma = value
                Case "Luck"
                    Luck = value
                Case Else
                    Return False
            End Select

            ' モデルに変更を適用
            UpdateStatusModel()
            Return True
        End Function
    End Class

End Namespace