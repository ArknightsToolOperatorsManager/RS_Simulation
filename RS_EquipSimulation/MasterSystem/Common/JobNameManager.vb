Imports RS_EquipSimulation.MasterSystem.Common.Internal

Namespace MasterSystem.Common

    Public Enum JobEnum
        Knight
        Warrior
        Archer
        Lancer
        Wizard
        Wolfman
        Bishop
        Angel
        Tamer
        Summoner
        Thief
        Monk
        Princess
        LittleWitch
        Necromancer
        Demon
        Spirit
        Champion
        Opticalist
        BeastMan
        Maid
        Sorceress
        Musketeer
        Alchemist
        Captain
        Canonia
    End Enum


    Public Enum StatusEnum
        Power
        Agility
        Health
        Knowledge
        Wisdom
        Charisma
        Luck
    End Enum

    Public Module JobNameManager
        ' 職業名の対応を管理する静的ディクショナリ
        Private ReadOnly _jobNames As New Dictionary(Of JobEnum, String) From {
            {JobEnum.Knight, "剣士"},
            {JobEnum.Warrior, "戦士"},
            {JobEnum.Archer, "アーチャー"},
            {JobEnum.Lancer, "ランサー"},
            {JobEnum.Wizard, "ウィザード"},
            {JobEnum.Wolfman, "ウルフマン"},
            {JobEnum.Bishop, "ビショップ"},
            {JobEnum.Angel, "追放天使"},
            {JobEnum.Tamer, "テイマー"},
            {JobEnum.Summoner, "サマナー"},
            {JobEnum.Thief, "シーフ"},
            {JobEnum.Monk, "武道家"},
            {JobEnum.Princess, "プリンセス"},
            {JobEnum.LittleWitch, "リトルウィッチ"},
            {JobEnum.Necromancer, "ネクロマンサー"},
            {JobEnum.Demon, "悪魔"},
            {JobEnum.Spirit, "霊術師"},
            {JobEnum.Champion, "闘士"},
            {JobEnum.Opticalist, "光奏師"},
            {JobEnum.BeastMan, "獣人"},
            {JobEnum.Maid, "メイド"},
            {JobEnum.Sorceress, "黒魔術師"},
            {JobEnum.Musketeer, "マスケッティア"},
            {JobEnum.Alchemist, "アルケミスト"},
            {JobEnum.Captain, "キャプテン"},
            {JobEnum.Canonia, "キャノニア"}
        }

        ' 職業の組み合わせを管理
        Private ReadOnly _jobPairs As New Dictionary(Of String, (JobEnum, JobEnum)) From {
            {"Warrior_Knight", (JobEnum.Warrior, JobEnum.Knight)},
            {"Archer_Lancer", (JobEnum.Archer, JobEnum.Lancer)},
            {"Wizard_Wolfman", (JobEnum.Wizard, JobEnum.Wolfman)},
            {"Bishop_Angel", (JobEnum.Bishop, JobEnum.Angel)},
            {"Tamer_Summoner", (JobEnum.Tamer, JobEnum.Summoner)},
            {"Sheif_Monk", (JobEnum.Thief, JobEnum.Monk)},
            {"Princess_LittleWitch", (JobEnum.Princess, JobEnum.LittleWitch)},
            {"Necromancer_Demon", (JobEnum.Necromancer, JobEnum.Demon)},
            {"Spirit_Champion", (JobEnum.Spirit, JobEnum.Champion)},
            {"Opticalist_BeastMan", (JobEnum.Opticalist, JobEnum.BeastMan)},
            {"Maid_Sorceress", (JobEnum.Maid, JobEnum.Sorceress)},
            {"Masuketeer_Alchemist", (JobEnum.Musketeer, JobEnum.Alchemist)},
            {"Captain_Canonia", (JobEnum.Captain, JobEnum.Canonia)}
        }

        ' JobEnumから日本語名を取得
        Public Function GetJobName(job As JobEnum) As String
            Return _jobNames(job)
        End Function

        ' 日本語名からJobEnumを取得
        Public Function GetJobEnum(name As String) As JobEnum?
            For Each pair In _jobNames
                If pair.Value = name Then
                    Return pair.Key
                End If
            Next
            Return Nothing
        End Function

        ' すべての職業名を取得
        Public Function GetAllJobNames() As IList(Of String)
            Return _jobNames.Values.ToList
        End Function

        ' ペアの取得
        Public Function GetJobDataKey(job As JobEnum) As (String, Boolean)
            Dim jobPair = _jobPairs.FirstOrDefault(Function(x) x.Value.Item1 = job OrElse x.Value.Item2 = job)

            If jobPair.Key Is Nothing Then
                Return (String.Empty, False) ' キーが見つからない場合の処理
            End If

            ' Item1と一致ならfalse、Item2と一致ならtrue
            Dim isItem2 = (jobPair.Value.Item2 = job)

            Return (jobPair.Key, isItem2)
        End Function

        Public Function GetJobDataByName(jobName As String) As JobData
            ' 日本語名から対応するJobEnumを取得
            Dim jobEnum = GetJobEnum(jobName)
            If Not jobEnum.HasValue Then
                Return Nothing
            End If

            ' 職業キーを取得
            Dim jobKeyPair = GetJobDataKey(jobEnum.Value)
            If String.IsNullOrEmpty(jobKeyPair.Item1) Then  ' タプルの最初の項目のチェック
                Return Nothing
            End If

            ' リフレクションでプロパティの値を取得
            Dim propertyInfo = GetType(JobInitialData).GetProperty(jobKeyPair.Item1)
            Dim jobData = If(propertyInfo IsNot Nothing,
                  DirectCast(propertyInfo.GetValue(Nothing), JobData),
                  Nothing)

            ' jobDataがnullでなければReverseを更新
            If jobData IsNot Nothing Then
                jobData.Reverse = jobData.Reverse And jobKeyPair.Item2
            End If

            Return jobData
        End Function
    End Module

End Namespace

Namespace MasterSystem.Common.Internal

    Public Class JobData
        Public Property HP As Long
        Public Property CP As Long
        Public Property Sex As String
        Public Property JobStatus As StatusPoint
        Public Property AutoRise As StatusEnum?
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
                .AutoRise = StatusEnum.Power,
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
                .AutoRise = StatusEnum.Agility,
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
                .AutoRise = StatusEnum.Knowledge,
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
                .AutoRise = StatusEnum.Charisma,
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
                .AutoRise = StatusEnum.Wisdom,
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
                .AutoRise = StatusEnum.Luck,
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
                .AutoRise = StatusEnum.Health,
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
                .AutoRise = Nothing,
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
                .AutoRise = Nothing,
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
                .AutoRise = Nothing,
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
                .AutoRise = Nothing,
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
                .AutoRise = Nothing,
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
                .AutoRise = Nothing,
                .Reverse = False
            }
            End Get
        End Property
    End Module

End Namespace
