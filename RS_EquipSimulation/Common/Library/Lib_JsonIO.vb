Imports Newtonsoft.Json
Imports System.IO

Namespace Common

    Public Class Lib_JsonIO
        ' カスタム例外クラス
        Public Class JsonLibraryException
            Inherits Exception
            Public Sub New(message As String)
                MyBase.New(message)
            End Sub
            Public Sub New(message As String, innerException As Exception)
                MyBase.New(message, innerException)
            End Sub
        End Class

        ' 設定ビルダークラス
        Public Class JsonSettingsBuilder
            Private _settings As New JsonSerializerSettings()

            Public Function WithFormatting(formatting As Formatting) As JsonSettingsBuilder
                _settings.Formatting = formatting
                Return Me
            End Function

            Public Function WithNullHandling(handling As NullValueHandling) As JsonSettingsBuilder
                _settings.NullValueHandling = handling
                Return Me
            End Function

            Public Function WithDateFormatHandling(handling As DateFormatHandling) As JsonSettingsBuilder
                _settings.DateFormatHandling = handling
                Return Me
            End Function

            Public Function WithErrorHandler(handler As EventHandler(Of Newtonsoft.Json.Serialization.ErrorEventArgs)) As JsonSettingsBuilder
                _settings.Error = handler
                Return Me
            End Function

            Public Function Build() As JsonSerializerSettings
                Return _settings
            End Function

            ' よく使う設定のプリセット
            Public Shared Function CreateDefault() As JsonSerializerSettings
                Return New JsonSettingsBuilder().
                WithFormatting(Formatting.Indented).
                WithNullHandling(NullValueHandling.Ignore).
                WithDateFormatHandling(DateFormatHandling.IsoDateFormat).
                WithErrorHandler(AddressOf HandleDeserializationError).
                Build()
            End Function
        End Class

        ' エラーログ機能
        Public Shared Sub LogError(message As String, ex As Exception)
            Try
                Dim logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}" &
                          $"Exception Type: {ex.GetType().Name}{Environment.NewLine}" &
                          $"Message: {ex.Message}{Environment.NewLine}" &
                          $"Stack Trace: {ex.StackTrace}"

                ' ログディレクトリの作成
                Dim logDir = "logs"
                If Not Directory.Exists(logDir) Then
                    Directory.CreateDirectory(logDir)
                End If

                ' 日付ベースのログファイル名
                Dim logPath = Path.Combine(logDir, $"error_{DateTime.Now:yyyyMMdd}.log")
                File.AppendAllText(logPath, logEntry & Environment.NewLine & Environment.NewLine)
            Catch logEx As Exception
                Debug.WriteLine($"ログ出力に失敗: {logEx.Message}")
            End Try
        End Sub

        ' オブジェクトをJSONファイルとして保存
        Public Shared Function SaveToJson(filePath As String,
                                    data As Object,
                                    Optional settings As JsonSerializerSettings = Nothing) As Boolean
            Try
                ' 入力パラメータの検証
                If String.IsNullOrEmpty(filePath) Then
                    Throw New ArgumentNullException(NameOf(filePath))
                End If
                If data Is Nothing Then
                    Throw New ArgumentNullException(NameOf(data))
                End If

                ' 親ディレクトリの作成
                Dim parentDir = Path.GetDirectoryName(filePath)
                If Not String.IsNullOrEmpty(parentDir) AndAlso Not Directory.Exists(parentDir) Then
                    Directory.CreateDirectory(parentDir)
                End If

                ' 設定の適用
                settings = If(settings, JsonSettingsBuilder.CreateDefault())

                ' シリアライズと保存
                Dim jsonText = JsonConvert.SerializeObject(data, settings)
                File.WriteAllText(filePath, jsonText)
                Return True

            Catch ex As UnauthorizedAccessException
                LogError($"ファイルへのアクセス権限がありません: {filePath}", ex)
                Throw New JsonLibraryException("ファイルへのアクセス権限がありません", ex)
            Catch ex As Exception
                LogError($"JSONファイルの保存に失敗: {filePath}", ex)
                Throw New JsonLibraryException("JSONファイルの保存に失敗しました", ex)
            End Try
        End Function

        ' JSONファイルからデータを読み込み
        Public Shared Function LoadDataFromJsonFile(Of T)(filePath As String,
                                                     Optional settings As JsonSerializerSettings = Nothing) As T
            Try
                ' 入力パラメータの検証
                If String.IsNullOrEmpty(filePath) Then
                    Throw New ArgumentNullException(NameOf(filePath))
                End If

                ' ファイルの存在確認
                If Not File.Exists(filePath) Then
                    Throw New FileNotFoundException($"指定されたJSONファイルが見つかりません: {filePath}")
                End If

                ' JSONファイルの読み込み
                Dim json As String = File.ReadAllText(filePath)
                If String.IsNullOrWhiteSpace(json) Then
                    Throw New JsonLibraryException("JSONファイルが空です")
                End If

                ' 設定の適用
                settings = If(settings, JsonSettingsBuilder.CreateDefault())

                ' デシリアライズ
                Return JsonConvert.DeserializeObject(Of T)(json, settings)

            Catch ex As FileNotFoundException
                LogError($"JSONファイルが見つかりません: {filePath}", ex)
                Throw New JsonLibraryException("JSONファイルが見つかりません", ex)
            Catch ex As JsonReaderException
                LogError($"JSONの形式が不正です: {filePath}", ex)
                Throw New JsonLibraryException("JSONの形式が不正です", ex)
            Catch ex As Exception
                LogError($"JSONファイルの読み込みに失敗: {filePath}", ex)
                Throw New JsonLibraryException("JSONファイルの読み込みに失敗しました", ex)
            End Try
        End Function

        ' デシリアライズ時のエラーハンドラー
        Private Shared Sub HandleDeserializationError(sender As Object, e As Newtonsoft.Json.Serialization.ErrorEventArgs)
            ' エラーをログに記録
            LogError($"デシリアライズエラー: {e.ErrorContext.Error.Message}", e.ErrorContext.Error)
            ' エラーを無視して続行
            e.ErrorContext.Handled = True
        End Sub
    End Class

End Namespace