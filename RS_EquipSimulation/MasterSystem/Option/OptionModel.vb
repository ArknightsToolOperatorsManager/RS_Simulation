Namespace MasterSystem.Option

    Public Class OptionModel
        Public Property OptionName As String
        Public Property OptionType As String

        Public Sub New(name As String, type As String)
            OptionName = name
            OptionType = type
        End Sub
    End Class

End Namespace
