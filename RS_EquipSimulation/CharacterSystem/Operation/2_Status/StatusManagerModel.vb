Imports Newtonsoft.Json
Imports RS_EquipSimulation.CharacterSystem.Common

Namespace CharacterSystem.Operation.Status

#Region "管理モデル"
    Public Class StatusManagerModel
        Inherits OperationManagerBase

        ' ステータスデータの管理
        <JsonProperty("StatusPoints")>
        Private ReadOnly _statusPoints As New Dictionary(Of String, StatusPointModel)

        ''' <summary>
        ''' デシリアライズ用の空のコンストラクタ
        ''' </summary>
        Public Sub New()
            MyBase.New(Nothing)
            ' 基本データの初期化
            AddStatus("振り分け")
            AddStatus("その他")
        End Sub

        ''' <summary>
        ''' 通常のコンストラクタ
        ''' </summary>
        ''' <param name="optManager">オプション管理クラス</param>
        Public Sub New(optManager As OptionManagerModel)
            MyBase.New(optManager)
            ' 基本データの初期化
            AddStatus("振り分け")
            AddStatus("その他")
        End Sub

        ''' <summary>
        ''' デシリアライズ後の初期化
        ''' </summary>
        Protected Overrides Sub OnInitializeAfterDeserialize()
            MyBase.OnInitializeAfterDeserialize()
            ' 必要に応じて初期化処理を追加
            If _statusPoints Is Nothing OrElse Not _statusPoints.ContainsKey("振り分け") OrElse Not _statusPoints.ContainsKey("その他") Then
                _statusPoints.Clear()
                AddStatus("振り分け")
                AddStatus("その他")
            End If
            ' オプション更新通知
            NotifyChanged()
        End Sub

        ''' <summary>
        ''' ステータスの追加
        ''' </summary>
        ''' <param name="name">ステータス名</param>
        Public Sub AddStatus(name As String)
            _statusPoints(name) = New StatusPointModel(name)
        End Sub

        ''' <summary>
        ''' ステータスの取得
        ''' </summary>
        ''' <param name="name">ステータス名</param>
        ''' <returns>ステータスデータモデル</returns>
        Public Function GetStatus(name As String) As StatusPointModel
            If _statusPoints.ContainsKey(name) Then
                Return _statusPoints(name)
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' すべてのステータスを取得
        ''' </summary>
        ''' <returns>ステータス名とデータモデルの辞書</returns>
        Public Function GetAllStatus() As Dictionary(Of String, StatusPointModel)
            Return New Dictionary(Of String, StatusPointModel)(_statusPoints)
        End Function

        ''' <summary>
        ''' 振り分けステータスの取得
        ''' </summary>
        ''' <returns>振り分けステータス</returns>
        Public Function GetAllocationStatus() As StatusPoint
            Return _statusPoints("振り分け").Status
        End Function

        ''' <summary>
        ''' その他ステータスの取得
        ''' </summary>
        ''' <returns>その他ステータス</returns>
        Public Function GetOtherStatus() As StatusPoint
            Return _statusPoints("その他").Status
        End Function

        ''' <summary>
        ''' ステータス値の検証
        ''' </summary>
        ''' <param name="name">ステータス名</param>
        ''' <param name="propertyName">プロパティ名</param>
        ''' <param name="value">値</param>
        ''' <returns>有効かどうか</returns>
        Public Function ValidateValue(name As String, propertyName As String, value As Integer) As Boolean
            ' プロパティ名が有効なステータスプロパティか確認
            Dim validProperties = {"Power", "Agility", "Health", "Knowledge", "Wisdom", "Charisma", "Luck"}
            If Not validProperties.Contains(propertyName) Then
                Return False
            End If

            ' 値の検証
            Return value >= 0
        End Function

        ''' <summary>
        ''' 振り分けステータスの合計取得
        ''' </summary>
        ''' <returns>合計値</returns>
        Public Function GetAllocationStatusTotal() As Integer
            Dim allocationStatus = GetAllocationStatus()
            Return allocationStatus.Power +
                   allocationStatus.Agility +
                   allocationStatus.Health +
                   allocationStatus.Knowledge +
                   allocationStatus.Wisdom +
                   allocationStatus.Charisma +
                   allocationStatus.Luck
        End Function

        ''' <summary>
        ''' ステータス変更後の処理
        ''' </summary>
        Public Sub AfterChangedStatus()
            NotifyChanged()
        End Sub

        ''' <summary>
        ''' オプション更新処理
        ''' </summary>
        Protected Overrides Sub UpdateOptionValues()
            MyBase.UpdateOptionValues()
            ' オプションの更新処理は必要に応じて実装
        End Sub
    End Class
#End Region

End Namespace