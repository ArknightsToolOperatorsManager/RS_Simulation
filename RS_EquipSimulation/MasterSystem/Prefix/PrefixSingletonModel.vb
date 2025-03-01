Imports RS_EquipSimulation.Common.Singleton
Imports RS_EquipSimulation.MasterSystem.Prefix
Imports RS_EquipSimulation.MasterSystem.Common

Public Class PrefixSingletonModel
    Inherits SingletonCSVBase(Of PrefixSingletonModel)

    Private _prefixList As List(Of PrefixModel)

    Protected Overrides Sub SafeInitialize()
        _prefixList = New List(Of PrefixModel)
    End Sub

    Protected Overrides ReadOnly Property FilePath As String
        Get
            Return InputFileConfig.PrefixFilePath
        End Get
    End Property

    Protected Overrides Sub ProcessLine(parts() As String)
        If parts.Length = 7 Then
            Dim prefix As New PrefixModel With {
                .Name = parts(0),
                .Level = parts(1),
                .Category = parts(2),
                .Rank = parts(3),
                .BonusLevel = Convert.ToInt32(parts(4)),
                .Op = parts(5),
                .Value = parts(6)
            }
            _prefixList.Add(prefix)
        End If
    End Sub

    ' カテゴリ一覧を取得
    Public Function GetCategories() As List(Of String)
        Return _prefixList.
            Select(Function(p) p.Category).
            Distinct().
            OrderBy(Function(c) c).
            ToList()
    End Function
    Public Function FilterPrefixesByLevel(level As Integer) As IEnumerable(Of PrefixModel)
        Return GetByLevel(level).OrderBy(Function(p) p.Name)
    End Function

    Public Function GetPrefixLevels(prefixName As String) As List(Of String)
        Return GetLevelsByName(prefixName)
    End Function

    ' 指定カテゴリの称号一覧を取得
    Public Function GetPrefixesByCategory(category As String, Optional maxLevel As Integer? = Nothing) As List(Of PrefixModel)
        Dim query = _prefixList.Where(Function(p) p.Category = category)

        If maxLevel.HasValue Then
            query = query.Where(Function(p) Integer.Parse(p.Level) <= maxLevel.Value)
        End If

        Return query.OrderBy(Function(p) p.Level).ToList()
    End Function

    ' 称号を名前で取得
    Public Function GetPrefixByName(name As String) As PrefixModel
        Return _prefixList.FirstOrDefault(Function(p) p.Name = name)
    End Function

    ' レベルに基づいた称号フィルタリング
    Public Function GetPrefixesByLevel(level As Integer) As List(Of PrefixModel)
        Return _prefixList.Where(Function(p) Integer.Parse(p.Level) <= level).ToList()
    End Function

    ' オプション名で称号を検索
    Public Function FindPrefixesByOption(opName As String) As List(Of String)
        Return _prefixList.Where(Function(p) p.Op = opName).
                Select(Function(p) p.Level).
                ToList()
    End Function

    ''' <summary>
    ''' 名前で称号を検索
    ''' </summary>
    Public Function FindByName(name As String) As PrefixModel
        Return _prefixList.FirstOrDefault(Function(p) p.Name = name)
    End Function

    ''' <summary>
    ''' レベルで称号をフィルタリング
    ''' </summary>
    Public Function GetByLevel(level As Integer) As IEnumerable(Of PrefixModel)
        Return _prefixList.Where(Function(p) p.Level <= level)
    End Function

    ''' <summary>
    ''' 系統で称号をフィルタリング
    ''' </summary>
    Public Function GetByCategory(category As String) As List(Of String)
        Return _prefixList.Where(Function(p) p.Category = category).
                Select(Function(p) p.Name).
                ToList()
    End Function

    ''' <summary>
    ''' 称号別のレベルを取得
    ''' </summary>
    Public Function GetLevelsByName(name As String) As List(Of String)
        Return _prefixList.Where(Function(p) p.Name = name).
                   Select(Function(p) p.Level).
                   ToList()
    End Function

End Class