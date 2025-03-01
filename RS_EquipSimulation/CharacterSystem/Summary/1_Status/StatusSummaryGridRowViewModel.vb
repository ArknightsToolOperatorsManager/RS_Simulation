Imports RS_EquipSimulation.CharacterSystem.Common

Namespace CharacterSystem.Summary.Status

    ''' <summary>
    ''' ソースごとのステータス値を保持するViewModel
    ''' </summary>
    Public Class StatusSummaryGridRowViewModel
        Inherits StatusGridRowBase

        ' 表示用名称のオーバーライド（空文字の場合は合計と表示）
        Public Overrides ReadOnly Property DisplayName As String
            Get
                Return If(String.IsNullOrEmpty(RowName), "合計", RowName)
            End Get
        End Property

        ' コンストラクタ
        Public Sub New(sourceName As String, isEditable As Boolean)
            MyBase.New(sourceName, isEditable)  ' 編集不可に設定
        End Sub

        ' 空のコンストラクタ（シリアライズ用）
        Public Sub New()
            MyBase.New()
        End Sub
    End Class

End Namespace