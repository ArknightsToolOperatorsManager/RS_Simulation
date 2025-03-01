Imports System.Reflection
Imports RS_EquipSimulation.Common.Singleton.Internal

Namespace Common.Singleton

    ''' <summary>
    ''' Json読み出し用のSingleton基底クラス
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public MustInherit Class SingletonJsonBase(Of T)
        Inherits SingletonBase(Of T)
        Protected MustOverride ReadOnly Property FilePath As String

        ' デシリアライズ対象の変数への参照を受け取る
        Protected MustOverride Sub SetLoadedData(data As Object)
        Protected MustOverride Function GetDataType() As Type

        Protected Sub New()
            LoadFromFile()
        End Sub

        Protected Overridable Sub LoadFromFile()
            Try
                ' 読み込む型を動的に判定
                Dim dataType = GetDataType()

                ' SafeLoadMasterDataを活用して型指定でデシリアライズ
                Dim loadedData = SafeLoadMasterData(Of Object)(FilePath, dataType)

                ' 読み込んだデータをセット
                SetLoadedData(loadedData)
            Catch ex As Exception
                ' エラーハンドリング
                Throw New Exception($"JSON読み込みエラー: {FilePath}", ex)
            End Try
        End Sub

        Protected Function SafeLoadMasterData(Of TData)(filePath As String, dataType As Type) As TData
            Try
                ' LoadDataFromJsonFileのメソッド情報を取得
                Dim method = GetType(Lib_JsonIO).GetMethod(
                    "LoadDataFromJsonFile",
                    BindingFlags.Public Or BindingFlags.Static)

                ' 指定された型でジェネリックメソッドを構築
                Dim genericMethod = method.MakeGenericMethod(dataType)

                ' メソッドを呼び出し（第2引数のsettingsはOptionalなのでnullを渡す）
                Dim result = genericMethod.Invoke(
                    Nothing,
                    New Object() {filePath, Nothing})

                ' 結果を戻り値の型にキャスト
                Return DirectCast(result, TData)

            Catch ex As Exception
                ' エラーハンドリング（LoadDataFromJsonFileでもエラーログは出力されますが、
                ' アプリケーション終了処理のためここでもキャッチ）
                MessageBox.Show(
                    $"マスターデータの読み込みに失敗しました：{filePath}" & Environment.NewLine & ex.Message,
                    "致命的エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                )

                ' アプリケーション終了
                Application.Exit()
                Return Nothing
            End Try
        End Function
    End Class

    ''' <summary>
    ''' Constantデータ用のSingleton基底クラス
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public MustInherit Class SingletonConstantBase(Of T)
        Inherits Internal.SingletonDelimitedDataBase(Of T)

        Protected MustOverride ReadOnly Property ConstantData As String

        Protected Overrides Function GetRawData() As String
            Return ConstantData
        End Function
    End Class

    ''' <summary>
    ''' CSV読み出し用のSingleton基底クラス
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public MustInherit Class SingletonCSVBase(Of T)
        Inherits Internal.SingletonDelimitedDataBase(Of T)

        Protected MustOverride ReadOnly Property FilePath As String

        Protected Overrides Function GetRawData() As String
            Return System.IO.File.ReadAllText(FilePath)
        End Function
    End Class

#Region "基底クラス"
    ''' <summary>
    ''' Singleton基底クラス
    ''' このクラスを継承すると実行時に1インスタンスのみが保証される。
    ''' このクラスを継承すると他のインスタンスとライフサイクルが違うため、モデル名に「SingletonModel」を付与して明示すること。
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' 
    Public MustInherit Class SingletonBase(Of T)
        Public Shared Property _instance As T = Nothing

        Protected Sub New()
            SafeInitialize()
        End Sub

        Public Shared ReadOnly Property Instance() As T
            Get
                If _instance Is Nothing Then
                    If GetType(T).IsAbstract Then
                        Throw New InvalidOperationException($"Cannot create instance of abstract class {GetType(T).Name}")
                    End If
                    _instance = DirectCast(Activator.CreateInstance(GetType(T), True), T)
                End If
                Return _instance
            End Get
        End Property

        Protected Overridable Sub SafeInitialize()
            ' サブクラスでオーバーライドして追加の初期化処理を行う
        End Sub
    End Class
#End Region

End Namespace

Namespace Common.Singleton.Internal
    Public MustInherit Class SingletonDelimitedDataBase(Of T)
        Inherits SingletonBase(Of T)

        Protected Sub New()
            ParseData()
        End Sub

        ' 区切り文字の候補
        Protected ReadOnly Property DefaultDelimiters As String()
            Get
                Return {vbTab, ",", "|"}
            End Get
        End Property

        ' 自動判定された区切り文字を保持
        Private _detectedDelimiter As String = Nothing

        Protected ReadOnly Property Delimiter As String
            Get
                If _detectedDelimiter Is Nothing Then
                    _detectedDelimiter = DetectDelimiter(GetRawData())
                End If
                Return _detectedDelimiter
            End Get
        End Property

        Protected MustOverride Function GetRawData() As String
        Protected MustOverride Sub ProcessLine(parts() As String)

        Private Sub ParseData()
            Dim rawData = GetRawData()
            Dim lines = rawData.Trim().Split(New String() {Environment.NewLine}, StringSplitOptions.None)
            For Each line In lines
                If Not String.IsNullOrWhiteSpace(line) Then
                    Dim parts = line.Split(New String() {Delimiter}, StringSplitOptions.None)
                    ProcessLine(parts)
                End If
            Next
        End Sub

        ' 区切り文字の自動判定
        Private Function DetectDelimiter(rawData As String) As String
            ' 最初の数行のみを対象に判定
            Dim sampleLines = rawData.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries) _
                .Take(5) _
                .ToList()

            If sampleLines.Count = 0 Then
                Return DefaultDelimiters(0)  ' デフォルトを返す
            End If

            ' 各区切り文字候補での分割結果を評価
            Dim delimiterScores = DefaultDelimiters.ToDictionary(
                Function(d) d,
                Function(d) EvaluateDelimiter(sampleLines, d))

            ' 最もスコアの高い区切り文字を返す
            Return delimiterScores.OrderByDescending(Function(kvp) kvp.Value).First().Key
        End Function

        ' 区切り文字の評価
        Private Function EvaluateDelimiter(lines As List(Of String), delimiter As String) As Integer
            Dim score As Integer = 0
            Dim columnCounts = New List(Of Integer)

            For Each line In lines
                If Not String.IsNullOrWhiteSpace(line) Then
                    Dim parts = line.Split(New String() {delimiter}, StringSplitOptions.None)
                    columnCounts.Add(parts.Length)

                    ' 列数が1より大きければポイント加算
                    If parts.Length > 1 Then
                        score += 1
                    End If

                    ' 各部分の前後の空白をトリムした後も意味のある内容が残っているかチェック
                    Dim meaningfulParts = parts.Count(Function(p) Not String.IsNullOrWhiteSpace(p.Trim()))
                    If meaningfulParts > 1 Then
                        score += 1
                    End If
                End If
            Next

            ' 列数の一貫性をチェック
            If columnCounts.Count > 1 AndAlso columnCounts.Distinct().Count() = 1 Then
                score += 3
            End If

            Return score
        End Function
    End Class

End Namespace