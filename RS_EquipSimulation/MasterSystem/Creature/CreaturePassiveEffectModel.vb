Namespace MasterSystem.Creature

	Public Class CreaturePassiveEffectModel
		Public Property Name As String
		Public Property Rank As String
		Public Property Effects As Dictionary(Of String, List(Of String))

		Public Sub New(name As String, rank As String)
			Me.Name = name
			Me.Rank = rank
			Me.Effects = New Dictionary(Of String, List(Of String))()
		End Sub

		Public Sub AddEffect(effectName As String, values As List(Of String))
			Me.Effects(effectName) = values
		End Sub

		Public Function GetEffectValues(effectName As String) As List(Of String)
			If Me.Effects.ContainsKey(effectName) Then
				Return Me.Effects(effectName)
			End If
			Return New List(Of String)()
		End Function
	End Class

End Namespace
