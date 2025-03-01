Imports Newtonsoft.Json

Namespace CharacterSystem.Operation.Costume

    ''' <summary>
    ''' コスチュームの実データを管理するクラス
    ''' </summary>
    Public Class CostumeData
        ' コスチュームステータス
        <JsonProperty("CostumeStatus")>
        Public Property CostumeStatus As Integer = 0

        ' コスチューム経験値
        <JsonProperty("CostumeExp")>
        Public Property CostumeExp As Integer = 0

        ' クレスト情報
        <JsonProperty("Crests")>
        Public Property Crests As List(Of CrestData)

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        Public Sub New()
            Crests = New List(Of CrestData)(6)

            ' 6つのクレストスロットを初期化
            For i As Integer = 0 To 5
                Crests.Add(New CrestData())
            Next
        End Sub
    End Class

    ''' <summary>
    ''' クレスト情報を管理するクラス
    ''' </summary>
    Public Class CrestData
        ' クレストの色
        <JsonProperty("Color")>
        Public Property Color As String = ""

        ' クレスト名
        <JsonProperty("Name")>
        Public Property Name As String = ""

        ' クレストのランク
        <JsonProperty("Rank")>
        Public Property Rank As String = ""

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' パラメータ付きコンストラクタ
        ''' </summary>
        Public Sub New(color As String, name As String, rank As String)
            Me.Color = color
            Me.Name = name
            Me.Rank = rank
        End Sub

        ''' <summary>
        ''' クレストが設定されているかどうか
        ''' </summary>
        Public Function IsSet() As Boolean
            Return Not String.IsNullOrEmpty(Color) AndAlso Not String.IsNullOrEmpty(Name) AndAlso Not String.IsNullOrEmpty(Rank)
        End Function
    End Class

End Namespace