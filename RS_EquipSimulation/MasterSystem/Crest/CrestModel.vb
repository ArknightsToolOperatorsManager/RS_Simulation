Namespace MasterSystem.Crest

    ' クレストの定義を管理するクラス
    Public Class CrestModel
        ' クレスト名（オプション名）
        Public Property CrestName As String

        ' 色
        Public Property Color As String

        ' ランクごとの値
        Private ReadOnly _rankValues As New Dictionary(Of String, Integer)

        ' コンストラクタ
        Public Sub New(color As String, crestName As String)
            Me.Color = color
            Me.CrestName = crestName
        End Sub

        ' ランク値の設定
        Public Sub SetRankValue(rank As String, value As Integer)
            _rankValues(rank) = value
        End Sub

        ' ランク値の取得
        Public Function GetRankValue(rank As String) As Integer
            If _rankValues.ContainsKey(rank) Then
                Return _rankValues(rank)
            End If
            Return 0
        End Function

        ' 利用可能なランクの取得（値が設定されているもののみ）
        Public Function GetAvailableRanks() As List(Of String)
            Return _rankValues.Keys.ToList()
        End Function
    End Class

End Namespace