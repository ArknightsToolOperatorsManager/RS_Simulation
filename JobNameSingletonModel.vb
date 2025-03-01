Imports RS_EquipSimulation.Common.Singleton

Namespace MasterSystem.Common

    Public Class JobNameSingletonModel
        Inherits SingletonBase(Of JobNameSingletonModel)

        Public Sub New()
        End Sub

        Public ReadOnly Property JobList As IReadOnlyList(Of String)
            Get
                Return JobNameManager.GetAllJobNames().ToList().AsReadOnly()
            End Get
        End Property
    End Class

End Namespace