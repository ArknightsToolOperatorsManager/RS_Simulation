Imports RS_EquipSimulation.Common.Singleton

Namespace MasterSystem.Common

    Public Class JobConfigSingletonModel
        Inherits SingletonBase(Of JobConfigSingletonModel)

        Public Sub New()
        End Sub

        ' 職業名とプロパティ名のマッピング
        Private _jobMapping As Dictionary(Of String, String)

        Protected Overrides Sub SafeInitialize()
            If _jobMapping Is Nothing Then
                _jobMapping = New Dictionary(Of String, String)
            End If
        End Sub

        ' 職業名からJobDataを取得
        Public Function GetJobData(jobName As String) As JobData
            If String.IsNullOrEmpty(jobName) Then
                Return Nothing
            End If

            ' JobEnumを取得
            Dim jobEnum = JobNameManager.GetJobEnum(jobName)
            If Not jobEnum.HasValue Then
                Return Nothing
            End If

            ' JobDataKeyを取得
            Dim propertyName = JobNameManager.GetJobDataKey(jobEnum.Value)
            If String.IsNullOrEmpty(propertyName) Then
                Return Nothing
            End If

            ' リフレクションでプロパティの値を取得
            Dim propertyInfo = GetType(JobInitialData).GetProperty(propertyName)
            If propertyInfo Is Nothing Then
                Return Nothing
            End If

            Return DirectCast(propertyInfo.GetValue(Nothing), JobData)
        End Function

        ' 有効な職業名一覧を取得
        Public Function GetValidJobNames() As IEnumerable(Of String)
            Return _jobMapping.Keys.OrderBy(Function(x) x)
        End Function

        ' マッピングの検証
        Public Function ValidateMapping() As Boolean
            Return _jobMapping.Values.All(Function(propName) _
                GetType(JobInitialData).GetProperty(propName) IsNot Nothing)
        End Function

        ' 指定した職業名が有効かどうかを確認
        Public Function IsValidJobName(jobName As String) As Boolean
            Return Not String.IsNullOrEmpty(jobName) AndAlso _jobMapping.ContainsKey(jobName)
        End Function
    End Class

    Public Class JobData
        Public Property HP As Long
        Public Property CP As Long
        Public Property Sex As String
        Public Property JobStatus As StatusPoint
        Public Property AutoRise As String
        Public Property Reverse As Boolean
    End Class

    Public Module JobInitialData
        Public ReadOnly Property Warrior_Knight As JobData
            Get
                Return New JobData With {
                .HP = 50,
                .CP = 15,
                .Sex = "Male",
                .JobStatus = New StatusPoint With {
                    .Power = 25,
                    .Agility = 15,
                    .Health = 20,
                    .Knowledge = 5,
                    .Wisdom = 15,
                    .Charisma = 10,
                    .Luck = 1
                },
                .AutoRise = "Power",
                .Reverse = False
            }
            End Get
        End Property
        Public ReadOnly Property Archer_Lancer As JobData
            Get
                Return New JobData With {
                .HP = 30,
                .CP = 25,
                .Sex = "Female",
                .JobStatus = New StatusPoint With {
                    .Power = 15,
                    .Agility = 25,
                    .Health = 15,
                    .Knowledge = 10,
                    .Wisdom = 10,
                    .Charisma = 10,
                    .Luck = 5
                },
                .AutoRise = "Agility",
                .Reverse = False
            }
            End Get
        End Property
        Public ReadOnly Property Wizard_Wolfman As JobData
            Get
                Return New JobData With {
                .HP = 25,
                .CP = 35,
                .Sex = "Male",
                .JobStatus = New StatusPoint With {
                    .Power = 5,
                    .Agility = 5,
                    .Health = 10,
                    .Knowledge = 25,
                    .Wisdom = 20,
                    .Charisma = 20,
                    .Luck = 5
                },
                .AutoRise = "Knowledge",
                .Reverse = True
            }
            End Get
        End Property
        Public ReadOnly Property Bishop_Angel As JobData
            Get
                Return New JobData With {
                .HP = 40,
                .CP = 15,
                .Sex = "Male",
                .JobStatus = New StatusPoint With {
                    .Power = 20,
                    .Agility = 10,
                    .Health = 15,
                    .Knowledge = 15,
                    .Wisdom = 20,
                    .Charisma = 10,
                    .Luck = 1
                },
                .AutoRise = "Charisma",
                .Reverse = False
            }
            End Get
        End Property
        Public ReadOnly Property Tamer_Summoner As JobData
            Get
                Return New JobData With {
                .HP = 50,
                .CP = 15,
                .Sex = "Female",
                .JobStatus = New StatusPoint With {
                    .Power = 5,
                    .Agility = 10,
                    .Health = 5,
                    .Knowledge = 20,
                    .Wisdom = 20,
                    .Charisma = 20,
                    .Luck = 10
                },
                .AutoRise = "Wisdom",
                .Reverse = False
            }
            End Get
        End Property
        Public ReadOnly Property Sheif_Monk As JobData
            Get
                Return New JobData With {
                .HP = 30,
                .CP = 25,
                .Sex = "Male",
                .JobStatus = New StatusPoint With {
                    .Power = 15,
                    .Agility = 25,
                    .Health = 10,
                    .Knowledge = 10,
                    .Wisdom = 5,
                    .Charisma = 15,
                    .Luck = 10
                },
                .AutoRise = "Luck",
                .Reverse = False
            }
            End Get
        End Property
        Public ReadOnly Property Princess_LittleWitch As JobData
            Get
                Return New JobData With {
                .HP = 40,
                .CP = 20,
                .Sex = "Female",
                .JobStatus = New StatusPoint With {
                    .Power = 10,
                    .Agility = 20,
                    .Health = 15,
                    .Knowledge = 15,
                    .Wisdom = 20,
                    .Charisma = 15,
                    .Luck = 5
                },
                .AutoRise = "Health",
                .Reverse = False
            }
            End Get
        End Property
        Public ReadOnly Property Necromancer_Demon As JobData
            Get
                Return New JobData With {
                .HP = 40,
                .CP = 25,
                .Sex = "Female",
                .JobStatus = New StatusPoint With {
                    .Power = 25,
                    .Agility = 15,
                    .Health = 20,
                    .Knowledge = 5,
                    .Wisdom = 10,
                    .Charisma = 15,
                    .Luck = 1
                },
                .AutoRise = "",
                .Reverse = False
            }
            End Get
        End Property
        Public ReadOnly Property Spirit_Champion As JobData
            Get
                Return New JobData With {
                .HP = 31,
                .CP = 31,
                .Sex = "Female",
                .JobStatus = New StatusPoint With {
                    .Power = 20,
                    .Agility = 15,
                    .Health = 15,
                    .Knowledge = 15,
                    .Wisdom = 10,
                    .Charisma = 15,
                    .Luck = 1
                },
                .AutoRise = "",
                .Reverse = False
            }
            End Get
        End Property
        Public ReadOnly Property Opticalist_BeastMan As JobData
            Get
                Return New JobData With {
                .HP = 26,
                .CP = 26,
                .Sex = "Male",
                .JobStatus = New StatusPoint With {
                    .Power = 20,
                    .Agility = 10,
                    .Health = 10,
                    .Knowledge = 20,
                    .Wisdom = 10,
                    .Charisma = 10,
                    .Luck = 10
                },
                .AutoRise = "",
                .Reverse = False
            }
            End Get
        End Property
        Public ReadOnly Property Maid_Sorceress As JobData
            Get
                Return New JobData With {
                .HP = 31,
                .CP = 26,
                .Sex = "Female",
                .JobStatus = New StatusPoint With {
                    .Power = 20,
                    .Agility = 15,
                    .Health = 15,
                    .Knowledge = 5,
                    .Wisdom = 10,
                    .Charisma = 10,
                    .Luck = 20
                },
                .AutoRise = "",
                .Reverse = True
            }
            End Get
        End Property
        Public ReadOnly Property Masuketeer_Alchemist As JobData
            Get
                Return New JobData With {
                .HP = 31,
                .CP = 26,
                .Sex = "Male",
                .JobStatus = New StatusPoint With {
                    .Power = 15,
                    .Agility = 25,
                    .Health = 15,
                    .Knowledge = 5,
                    .Wisdom = 10,
                    .Charisma = 10,
                    .Luck = 10
                },
                .AutoRise = "",
                .Reverse = True
            }
            End Get
        End Property
        Public ReadOnly Property Captain_Canonia As JobData
            Get
                Return New JobData With {
                .HP = 25,
                .CP = 5,
                .Sex = "Female",
                .JobStatus = New StatusPoint With {
                    .Power = 20,
                    .Agility = 10,
                    .Health = 15,
                    .Knowledge = 10,
                    .Wisdom = 10,
                    .Charisma = 15,
                    .Luck = 10
                },
                .AutoRise = "",
                .Reverse = False
            }
            End Get
        End Property
    End Module

End Namespace