Imports System.ComponentModel

Namespace CharacterSystem.Summary.Status

    Public Class StatusSummaryRowViewModel
        Implements INotifyPropertyChanged

        ' 表示名とプロパティ名
        Public Property DisplayName As String
        Public Property PropertyName As String

        ' 各ソースの値
        Private ReadOnly _sourceValues As New Dictionary(Of String, Integer)

        ' コンストラクタ
        Public Sub New(displayName As String, propertyName As String)
            Me.DisplayName = displayName
            Me.PropertyName = propertyName
        End Sub

        ' ソース値の更新
        Public Sub UpdateSourceValues(manager As StatusSummaryManagerModel)
            ' 現在の値をクリア
            _sourceValues.Clear()

            ' 各ソースの値を設定
            For Each source In manager.SourceStatus.Keys
                Dim sourceStats = manager.SourceStatus(source)
                Dim value = GetPropertyValue(sourceStats, PropertyName)
                _sourceValues(source) = value
            Next

            ' 更新通知
            OnPropertyChanged("SourceValues")
        End Sub

        ' ソース値の取得
        Public Function GetSourceValue(sourceName As String) As Integer
            If _sourceValues.ContainsKey(sourceName) Then
                Return _sourceValues(sourceName)
            End If
            Return 0
        End Function

        ' StatusPointからプロパティ値を取得するヘルパーメソッド
        Private Function GetPropertyValue(stats As StatusPoint, propertyName As String) As Integer
            Dim prop = stats.GetType().GetProperty(propertyName)
            If prop IsNot Nothing Then
                Return CInt(prop.GetValue(stats))
            End If
            Return 0
        End Function

        ' PropertyChangedイベント
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class

End Namespace