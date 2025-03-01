Namespace Common.Creature

    Public Class CreatureInventoryItem
        ' 基本情報
        Public ReadOnly Property Name As String
        Public ReadOnly Property ItemId As Guid

        ' レアリティ情報
        Public ReadOnly Property BaseRarity As String     ' ベースレアリティ（N, R, HR, SR, LR, GR）
        Public ReadOnly Property EnhancedRarity As String ' 強化後レアリティ

        ' パッシブスキル情報
        Public ReadOnly Property MainPassive As PassiveSkill
        Public ReadOnly Property SubPassive As New List(Of PassiveSkill)

        ' コンストラクタ
        Public Sub New(name As String,
                      itemId As Guid,
                      baseRarity As String,
                      enhancedRarity As String,
                      mainPassive As PassiveSkill,
                      sub1Passive As PassiveSkill,
                      sub2Passive As PassiveSkill,
                      sub3Passive As PassiveSkill)
            Me.Name = name
            Me.ItemId = itemId
            Me.BaseRarity = baseRarity
            Me.EnhancedRarity = enhancedRarity
            Me.MainPassive = mainPassive
            If sub1Passive IsNot Nothing Then
                SubPassive.Add(sub1Passive)
            End If
            If sub2Passive IsNot Nothing Then
                SubPassive.Add(sub2Passive)
            End If
            If sub3Passive IsNot Nothing Then
                SubPassive.Add(sub3Passive)
            End If
        End Sub
    End Class

    ' パッシブスキルを表すクラス
    Public Class PassiveSkill
        Public ReadOnly Property Name As String          ' パッシブスキル名
        Public ReadOnly Property PassiveRank As String   ' パッシブランク（一般/上級）
        Public ReadOnly Property PassiveLevel As Integer ' パッシブレベル
        Public ReadOnly Property AwakeningValue As Integer ' 覚醒値

        ' コンストラクタ
        Public Sub New(name As String, passiveRank As String, passiveLevel As Integer, awakeningValue As Integer)
            Me.Name = name
            Me.PassiveRank = passiveRank
            Me.PassiveLevel = passiveLevel
            Me.AwakeningValue = awakeningValue
        End Sub

        ' 合計値を返すユーティリティメソッド
        Public Function GetTotalValue() As Integer
            Return PassiveLevel + AwakeningValue
        End Function

        ' 表示用文字列を生成するユーティリティメソッド
        Public Function GetDisplayValue() As String
            Return $"{PassiveLevel}+{AwakeningValue}"
        End Function
    End Class

End Namespace