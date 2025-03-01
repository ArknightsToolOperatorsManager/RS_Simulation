Imports RS_EquipSimulation.Common.Singleton

Namespace MasterSystem.Crest

    Public Class CrestSingletonModel
        Inherits SingletonConstantBase(Of CrestSingletonModel)

        ' クレストデータの管理
        Private _crests As List(Of CrestModel)

        ' カラーの一覧を保持
        Private _colors As HashSet(Of String)

        ' ランクの一覧を保持（順序を保持するためList）
        Private _ranks As List(Of String)

        Protected Overrides ReadOnly Property ConstantData As String
            Get
                Return CrestData
            End Get
        End Property

        Public Sub New()
        End Sub

        Protected Overrides Sub SafeInitialize()
            If _colors Is Nothing Then
                _colors = New HashSet(Of String)()
            End If
            If _ranks Is Nothing Then
                _ranks = New List(Of String) From {
                    "下級", "中級", "上級", "最上級", "究極", "超級I", "超級II"
                }
            End If
            If _crests Is Nothing Then
                _crests = New List(Of CrestModel)()
            End If
        End Sub

        ' 行データの処理（タブ区切りデータを想定）
        Protected Overrides Sub ProcessLine(parts() As String)
            ' データ行の形式チェック（色とクレスト名は必須）
            If parts.Length < 3 Then Return

            Dim color As String = parts(0).Trim()
            Dim crestName As String = parts(1).Trim()

            ' 空行や見出し行をスキップ
            If String.IsNullOrWhiteSpace(color) OrElse String.IsNullOrWhiteSpace(crestName) Then Return

            ' 色の登録
            _colors.Add(color)

            ' クレストモデルの作成
            Dim crest As New CrestModel(color, crestName)

            ' ランク値の設定
            For i As Integer = 2 To Math.Min(parts.Length - 1, _ranks.Count + 1)
                Dim value As Integer
                Dim rankIndex = i - 2

                If rankIndex < _ranks.Count AndAlso Integer.TryParse(parts(i), value) Then
                    crest.SetRankValue(_ranks(rankIndex), value)
                End If
            Next

            ' クレストリストに追加
            _crests.Add(crest)
        End Sub

        ' カラー一覧の取得
        Public Function GetColorList() As List(Of String)
            Return _colors.ToList()
        End Function

        ' ランク一覧の取得
        Public Function GetRankList() As List(Of String)
            Return _ranks.ToList()
        End Function

        ' 色でクレストをフィルタリング
        Public Function GetCrestsByColor(color As String) As List(Of CrestModel)
            Return _crests.Where(Function(c) c.Color = color).ToList()
        End Function

        ' 色でクレスト名のみを取得（コンボボックス表示用）
        Public Function GetCrestNamesByColor(color As String) As List(Of String)
            Return GetCrestsByColor(color).Select(Function(c) c.CrestName).ToList()
        End Function

        ' 色とクレスト名でクレストを取得
        Public Function GetCrest(color As String, crestName As String) As CrestModel
            Return _crests.FirstOrDefault(Function(c) c.Color = color AndAlso c.CrestName = crestName)
        End Function

        ' 色とクレスト名から、利用可能なランク一覧を取得（クレストとランクの選択連動用）
        Public Function GetAvailableRanks(color As String, crestName As String) As List(Of String)
            Dim crest = GetCrest(color, crestName)
            If crest IsNot Nothing Then
                Return crest.GetAvailableRanks()
            End If
            Return New List(Of String)
        End Function

        ' 色、クレスト名、ランクで値を取得（計算時用）
        Public Function GetValue(color As String, crestName As String, rank As String) As Integer
            Dim crest = GetCrest(color, crestName)
            If crest IsNot Nothing Then
                Return crest.GetRankValue(rank)
            End If
            Return 0
        End Function
    End Class

    Public Module Constant_CrestData
        Public Const CrestData As String = "
紫	全ステータス上昇	20	35	50	65	80		
紫	CP獲得ボーナス	14	18	22	26	30		
紫	ステ低下抵抗			3	6	9		
紫	属性抵抗低下抵抗			3	6	9		
紫	被ダメージ減少				2	4		
紫	最終ダメ				3	6		
紫	物理限界増加				2	4		
紫	魔法限界増加				2	4		
赤	ダメージ	20	35	50	65			
赤	PVP攻撃力	8	14	20	26	32	35	45
赤	攻撃速度	2	4	6	8	10	15	30
赤	強打率	2	4	6	8	10		
赤	攻撃命中	2	4	6	8			
赤	致命打率	2	4	6	8			
赤	魔法致命打率	2	4	6	8			
赤	クリダメ	2	4	6	8			
赤	ダブクリダメ	2	4	6	8			
赤	火強化	10	20	30	40			
赤	水強化	10	20	30	40			
赤	風強化	10	20	30	40			
赤	大地強化	10	20	30	40			
赤	光強化	10	20	30	40			
赤	闇強化	10	20	30	40			
赤	火弱化	5	10	15	20			
赤	水弱化	5	10	15	20			
赤	風弱化	5	10	15	20			
赤	大地弱化	5	10	15	20			
赤	光弱化	5	10	15	20			
赤	闇弱化	5	10	15	20			
赤	全属性弱化					27		
赤	全属性強化					52		
赤	致命打抵抗減少	2	4	6	8	10		
青	防御効率	20	45	75	105	135		
青	PVP防御力	8	14	20	26	33	35	45
青	HP効率	4	7	10	13	17	30	50
青	被クリダメ減少	2	4	6	8			
青	被ダブクリダメ減少	2	4	6	8			
青	攻撃回避	2	4	6	8	10		
青	火抵抗	8	14	20	26			
青	水抵抗	8	14	20	26			
青	風抵抗	8	14	20	26			
青	大地抵抗	8	14	20	26			
青	光抵抗	8	14	20	26			
青	闇抵抗	8	14	20	26			
青	移動速度	5	10	15	20			
黄	力上昇	50	75	100	125	150		
黄	敏捷上昇	50	75	100	125			
黄	健康上昇	50	75	100	125	150	120	150
黄	知識上昇	50	75	100	125	150		
黄	知恵上昇	50	75	100	125			
黄	威厳上昇	50	75	100	125			
黄	運上昇	50	75	100	125	150		
"
    End Module

End Namespace