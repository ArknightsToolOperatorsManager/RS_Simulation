Imports Newtonsoft.Json

Public Class OpValue
    <JsonProperty("OP名")>
    Public Property OptionName As String
    <JsonProperty("数値")>
    Public Property OptionValue As String
    Public Sub New(name As String, value As String)
        Me.OptionName = name
        Me.OptionValue = value
    End Sub
End Class

