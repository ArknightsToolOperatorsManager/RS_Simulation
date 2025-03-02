Imports System.IO
Imports System.Net.Http
Imports RS_EquipSimulation.Common
Imports RS_EquipSimulation.MasterSystem.Common
Imports RS_EquipSimulation.MasterSystem.Equipment

Namespace MasterSystem.Update

    ''' <summary>
    ''' マスターデータの更新処理を管理するクラス
    ''' スプレッドシートからのデータ取得と更新を担当
    ''' </summary>
    Public Class MasterDataUpdateManager
        Private Shared _instance As MasterDataUpdateManager
        Private _isUpdating As Boolean = False
        Private _lastUpdateTime As DateTime = DateTime.MinValue
        Private _updateTask As Task = Nothing
        Private _fileWatcher As FileSystemWatcher = Nothing
        Private _lockObj As New Object()

        ' スプレッドシートの公開URL (CSV形式)
        Private ReadOnly _spreadsheetCsvUrl As String = "https://docs.google.com/spreadsheets/d/1yymYktji2jvuUFc7kvOziNkfJALtfyr_hOZYFnT5Uu8/export?format=csv&gid=0"

        ' シングルトンインスタンス
        Public Shared ReadOnly Property Instance As MasterDataUpdateManager
            Get
                If _instance Is Nothing Then
                    _instance = New MasterDataUpdateManager()
                End If
                Return _instance
            End Get
        End Property

        ' 更新中フラグ
        Public ReadOnly Property IsUpdating As Boolean
            Get
                Return _isUpdating
            End Get
        End Property

        ' 最終更新時刻
        Public ReadOnly Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
        End Property

        ' コンストラクタ
        Private Sub New()
            ' 初期化処理
            InitializeFileWatcher()
            LoadUpdateMetadata()
        End Sub

        ' ファイル監視の初期化
        Private Sub InitializeFileWatcher()
            Try
                Dim directory = Path.GetDirectoryName(InputFileConfig.BaseItemFilePath)
                Dim fileName = Path.GetFileName(InputFileConfig.BaseItemFilePath)

                _fileWatcher = New FileSystemWatcher(directory, fileName) With {
                    .NotifyFilter = NotifyFilters.LastWrite Or NotifyFilters.FileName,
                    .EnableRaisingEvents = True
                }

                ' ファイル変更イベントの登録
                AddHandler _fileWatcher.Changed, AddressOf OnItemsFileChanged
                AddHandler _fileWatcher.Created, AddressOf OnItemsFileChanged
            Catch ex As Exception
                Debug.WriteLine($"ファイル監視の初期化に失敗: {ex.Message}")
            End Try
        End Sub

        ' 更新メタデータの読み込み
        Private Sub LoadUpdateMetadata()
            Try
                Dim metadataPath = GetUpdateMetadataPath()
                If File.Exists(metadataPath) Then
                    Dim metadata = Lib_JsonIO.LoadDataFromJsonFile(Of Dictionary(Of String, Object))(metadataPath)
                    If metadata.ContainsKey("LastUpdateTime") Then
                        _lastUpdateTime = Convert.ToDateTime(metadata("LastUpdateTime"))
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine($"更新メタデータの読み込みに失敗: {ex.Message}")
            End Try
        End Sub

        ' 更新メタデータパスの取得
        Private Function GetUpdateMetadataPath() As String
            Return Path.Combine(
                Path.GetDirectoryName(InputFileConfig.BaseItemFilePath),
                "update_metadata.json")
        End Function

        ' 更新メタデータの保存
        Private Sub SaveUpdateMetadata()
            Try
                Dim metadataPath = GetUpdateMetadataPath()
                Dim metadata = New Dictionary(Of String, Object) From {
                    {"LastUpdateTime", _lastUpdateTime},
                    {"UpdateStatus", "Success"}
                }

                Lib_JsonIO.SaveToJson(metadataPath, metadata)
            Catch ex As Exception
                Debug.WriteLine($"更新メタデータの保存に失敗: {ex.Message}")
            End Try
        End Sub

        ' アプリケーション起動時に呼び出すメソッド
        Public Sub InitializeOnStartup()
            Try
                ' items.jsonが存在する場合、items_current.jsonとしてコピー
                PrepareMasterDataFiles()

                ' 更新条件を満たす場合、非同期で更新処理を開始
                If ShouldUpdateData() Then
                    CheckForUpdatesAsync()
                End If
            Catch ex As Exception
                Debug.WriteLine($"初期化処理に失敗: {ex.Message}")
            End Try
        End Sub

        ' マスターデータファイルの準備
        Private Sub PrepareMasterDataFiles()
            Dim originalPath = InputFileConfig.BaseItemFilePath
            Dim currentPath = GetCurrentFilePath()

            If File.Exists(originalPath) Then
                ' items.jsonをitems_current.jsonとしてコピー
                File.Copy(originalPath, currentPath, True)
            End If
        End Sub

        ' 現在使用中のファイルパスを取得
        Private Function GetCurrentFilePath() As String
            Dim directory = Path.GetDirectoryName(InputFileConfig.BaseItemFilePath)
            Dim fileName = Path.GetFileNameWithoutExtension(InputFileConfig.BaseItemFilePath)
            Return Path.Combine(directory, $"{fileName}_current.json")
        End Function

        ' 更新条件を判定
        Private Function ShouldUpdateData() As Boolean
            ' 最終更新から24時間以上経過している場合に更新
            Return (DateTime.Now - _lastUpdateTime).TotalHours >= 24
        End Function

        ' 非同期更新処理の開始
        Public Sub CheckForUpdatesAsync()
            SyncLock _lockObj
                If _isUpdating Then Return
                _isUpdating = True
            End SyncLock

            _updateTask = Task.Run(AddressOf UpdateMasterDataAsync)
        End Sub

        ' マスターデータの非同期更新処理
        Private Async Function UpdateMasterDataAsync() As Task
            Try
                ' スプレッドシートからデータ取得
                Dim itemsData = Await FetchAndParseSpreadsheetData()
                If itemsData IsNot Nothing AndAlso itemsData.Any() Then
                    ' 一時ファイルに保存
                    Dim tempFilePath = GetTempFilePath()
                    Lib_JsonIO.SaveToJson(tempFilePath, itemsData)

                    ' 成功したら正式なファイルに移動
                    File.Delete(InputFileConfig.BaseItemFilePath)
                    File.Move(tempFilePath, InputFileConfig.BaseItemFilePath)

                    ' 更新時刻を記録
                    _lastUpdateTime = DateTime.Now
                    SaveUpdateMetadata()

                    ' 更新イベント発行
                    RaiseEvent MasterDataUpdated(Me, New EventArgs())
                End If
            Catch ex As Exception
                Debug.WriteLine($"マスターデータ更新エラー: {ex.Message}")
                LogUpdateError(ex)
            Finally
                SyncLock _lockObj
                    _isUpdating = False
                End SyncLock
            End Try
        End Function

        ' 一時ファイルパスの取得
        Private Function GetTempFilePath() As String
            Dim directory = Path.GetDirectoryName(InputFileConfig.BaseItemFilePath)
            Dim fileName = Path.GetFileNameWithoutExtension(InputFileConfig.BaseItemFilePath)
            Return Path.Combine(directory, $"{fileName}_temp.json")
        End Function

        ' スプレッドシートデータの取得と解析
        Private Async Function FetchAndParseSpreadsheetData() As Task(Of List(Of BaseItemModel))
            Try
                ' CSVデータの取得
                Dim csvData = Await FetchSpreadsheetDataAsCSV(_spreadsheetCsvUrl)
                If String.IsNullOrEmpty(csvData) Then
                    Throw New Exception("スプレッドシートデータの取得に失敗しました")
                End If

                ' CSVデータを解析してBaseItemModelのリストに変換
                Return ParseSpreadsheetData(csvData)
            Catch ex As Exception
                Debug.WriteLine($"スプレッドシートデータの取得と解析に失敗: {ex.Message}")
                Throw
            End Try
        End Function

        ' スプレッドシートからCSVデータを取得
        Private Async Function FetchSpreadsheetDataAsCSV(url As String) As Task(Of String)
            Using client As New HttpClient()
                client.Timeout = TimeSpan.FromSeconds(30)
                Return Await client.GetStringAsync(url)
            End Using
        End Function

        ' スプレッドシートデータの解析
        Private Function ParseSpreadsheetData(csvData As String) As List(Of BaseItemModel)
            ' GASスクリプトと同様の解析ロジックを実装
            Dim items = New List(Of BaseItemModel)()

            ' この部分は実際のスプレッドシート構造に合わせて実装する必要があります
            ' GASのロジックを参考に、CSVデータからBaseItemModelリストを生成

            ' 実装例（詳細なパース処理は別メソッドに実装）
            items = BaseItemSpreadsheetParser.ParseData(csvData)

            Return items
        End Function

        ' items.jsonファイル変更イベントハンドラ
        Private Sub OnItemsFileChanged(sender As Object, e As FileSystemEventArgs)
            Try
                ' ファイル操作が完了するまで少し待機
                Task.Delay(500).Wait()

                ' items.jsonをitems_current.jsonにコピー
                File.Copy(InputFileConfig.BaseItemFilePath, GetCurrentFilePath(), True)

                ' BaseItemSingletonModelの更新通知
                RaiseEvent ItemsFileUpdated(Me, New EventArgs())
            Catch ex As Exception
                Debug.WriteLine($"ファイル更新処理エラー: {ex.Message}")
            End Try
        End Sub

        ' エラーログ記録
        Private Sub LogUpdateError(ex As Exception)
            Try
                Dim logDir = Path.Combine(
                    Path.GetDirectoryName(InputFileConfig.BaseItemFilePath),
                    "logs")

                If Not Directory.Exists(logDir) Then
                    Directory.CreateDirectory(logDir)
                End If

                Dim logPath = Path.Combine(logDir, $"update_error_{DateTime.Now:yyyyMMdd}.log")
                Dim logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - スプレッドシート更新エラー" & Environment.NewLine &
                              $"エラー: {ex.Message}" & Environment.NewLine &
                              $"スタックトレース: {ex.StackTrace}" & Environment.NewLine & Environment.NewLine

                File.AppendAllText(logPath, logEntry)
            Catch logEx As Exception
                Debug.WriteLine($"エラーログ記録に失敗: {logEx.Message}")
            End Try
        End Sub

        ' リソース解放
        Public Sub Dispose()
            If _fileWatcher IsNot Nothing Then
                _fileWatcher.EnableRaisingEvents = False
                RemoveHandler _fileWatcher.Changed, AddressOf OnItemsFileChanged
                RemoveHandler _fileWatcher.Created, AddressOf OnItemsFileChanged
                _fileWatcher.Dispose()
                _fileWatcher = Nothing
            End If
        End Sub

        ' マスターデータ更新イベント
        Public Event MasterDataUpdated As EventHandler

        ' items.jsonファイル更新イベント
        Public Event ItemsFileUpdated As EventHandler
    End Class

End Namespace