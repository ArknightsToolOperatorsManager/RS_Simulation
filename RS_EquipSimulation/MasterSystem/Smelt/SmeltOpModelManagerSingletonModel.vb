Imports RS_EquipSimulation.Common.Singleton
Imports RS_EquipSimulation.MasterSystem.Common
Imports RS_EquipSimulation.MasterSystem.Smelt

Public Class SmeltOpModelManagerSingletonModel
    Inherits SingletonBase(Of SmeltOpModelManagerSingletonModel)

    ' 固定部位名とモデルの対応
    Private _bodyPartNames As New Dictionary(Of String, Type) From {
            {"武器", GetType(SmeltOpWeaponSingletonModel)},
            {"首", GetType(SmeltOpNecklessSingletonModel)},
            {"頭", GetType(SmeltOpHelmetSingletonModel)},
            {"耳", GetType(SmeltOpAccessorySingletonModel)},
            {"背", GetType(SmeltOpAccessorySingletonModel)},
            {"腰", GetType(SmeltOpBeltSingletonModel)},
            {"手", GetType(SmeltOpGloveSingletonModel)},
            {"胴", GetType(SmeltOpArmerSingletonModel)},
            {"足", GetType(SmeltOpBootsSingletonModel)}
        }

    ' アイテムランクと精錬ランクの対応
    Private _rankMapping As New Dictionary(Of String, String) From {
            {"N", "下級"}, {"NU", "下級"},
            {"DX", "中級"}, {"DXU", "中級"},
            {"UM", "上級"}, {"UMU", "上級"}
        }

    ' 精錬可能な段階
    Private _availableStages As Integer() = {1, 5, 10, 15, 18}

    ' 部位に対して利用可能なオプションを取得
    Public Function GetOptionNames(partName As String, stage As Integer) As List(Of String)
        Dim model = CheckFixedPart(partName)
        If model Is Nothing Then Return Nothing

        Return DirectCast(model, ISmeltOpManager).GetOptionNames(stage)
    End Function

    ' 段階一覧を取得
    Public Function GetAvailableStages(maxLevel As Integer) As List(Of Integer)
        Return _availableStages.Where(Function(stage) stage <= maxLevel).ToList()
    End Function

    ' オプション値の取得
    Public Function GetValue(partName As String, stage As Integer, opName As String, itemRank As String, level As Integer) As Integer

        ' 基本的な入力検証
        If String.IsNullOrEmpty(partName) OrElse level < 1 OrElse level > 20 Then
            Return 0
        End If

        Dim model = CheckFixedPart(partName)
        If model Is Nothing Then Return 0

        Dim smeltRank = GetSmeltRank(itemRank)
        Return DirectCast(model, ISmeltOpManager).GetValue(stage, opName, smeltRank, level)
    End Function

    ' アイテムランクから精錬ランクへの変換
    Private Function GetSmeltRank(itemRank As String) As String
        Return If(_rankMapping.ContainsKey(itemRank), _rankMapping(itemRank), "下級")
    End Function

    ' 固定部位のチェック
    Private Function CheckFixedPart(partName As String) As Object
        For Each kvp In _bodyPartNames
            If partName.Contains(kvp.Key) Then
                Dim baseType = GetType(SingletonBase(Of)).MakeGenericType(kvp.Value)
                Dim instanceProperty = baseType.GetProperty("Instance")
                Return instanceProperty?.GetValue(Nothing)
            End If
        Next
        Return Nothing
    End Function

    Public Function GetSmeltStages(partName As String) As List(Of Integer)
        ' 部位に基づいて利用可能な精錬段階を取得
        Return GetAvailableStages(partName)
    End Function

    Public Function CalculateSmeltValue(
        partName As String,
        stage As Integer,
        opName As String,
        itemRank As String,
        smeltLevel As Integer) As Integer

        Dim convertedRank = GetSmeltRank(itemRank)
        Return GetValue(partName, stage, opName, convertedRank, smeltLevel)
    End Function

    Private Function GetAvailableStages(partName As String) As List(Of Integer)
        ' 部位に応じて利用可能な段階を返す論理を実装
        ' 現状のコードを参考に、必要に応じて拡張
        Dim stages As New List(Of Integer) From {1, 5, 10, 15, 18}
        Return stages
    End Function
End Class