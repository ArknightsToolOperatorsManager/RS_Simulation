Imports Newtonsoft.Json

Namespace MasterSystem.Equipment

    Public Class BaseItemModel
        <JsonProperty("アイテム名")>
        Public Property Name As String

        <JsonProperty("部位名")>
        Public Property PartName As String

        <JsonProperty("アイテムランク")>
        Public Property Rank As String

        <JsonProperty("アイテムレベル")>
        Public Property Level As String

        <JsonProperty("アイテム要求")>
        Public Property Requirements As Dictionary(Of String, String)

        <JsonProperty("アイテム性能")>
        Public Property ItemStats As Dictionary(Of String, String)

        <JsonProperty("オプション")>
        Public Property Options As List(Of OpValue)

        <JsonProperty("Nxオプション")>
        Public Property NxOptions As Dictionary(Of Integer, List(Of OpValue))

        Public Sub New()
            Name = ""
            PartName = ""
            Rank = ""
            Level = ""
            Requirements = New Dictionary(Of String, String)
            ItemStats = New Dictionary(Of String, String)
            Options = New List(Of OpValue)
            NxOptions = New Dictionary(Of Integer, List(Of OpValue))
        End Sub
    End Class

End Namespace

