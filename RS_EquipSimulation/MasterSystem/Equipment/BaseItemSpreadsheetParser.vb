Imports System.IO
Imports RS_EquipSimulation.MasterSystem.Equipment
Imports Microsoft.VisualBasic.FileIO

Namespace MasterSystem.Update

    ''' <summary>
    ''' スプレッドシートデータをパースするユーティリティクラス
    ''' </summary>
    Public Class BaseItemSpreadsheetParser

        ' スプレッドシートの開始位置（D4に相当）
        Private Const StartRowIndex As Integer = 3  ' 0ベースなので4行目は3
        Private Const StartColIndex As Integer = 3  ' 0ベースなのでD列は3

        ''' <summary>
        ''' CSVデータを解析してBaseItemModelのリストに変換
        ''' </summary>
        ''' <param name="csvData">CSVデータ文字列</param>
        ''' <returns>BaseItemModelのリスト</returns>
        Public Shared Function ParseData(csvData As String) As List(Of BaseItemModel)
            Dim items = New List(Of BaseItemModel)()

            Try
                ' CSVデータを2次元配列に変換
                Dim data = ParseCsvTo2DArray(csvData)
                If data Is Nothing OrElse data.Length = 0 Then
                    Return items
                End If

                ' 定数でオフセット値を定義
                Const ITEM_BLOCK_HEIGHT As Integer = 10       ' アイテム1つ分のデータ行数

                ' 基本情報のオフセット
                Const OFFSET_NAME As Integer = 0               ' アイテム名の行オフセット
                Const OFFSET_PARTNAME As Integer = 0           ' 部位名の行オフセット
                Const OFFSET_RANK As Integer = 0               ' ランクの行オフセット
                Const OFFSET_LEVEL As Integer = 1              ' レベルの行オフセット

                ' 列オフセット
                Const COL_NAME As Integer = 0                 ' アイテム名の列オフセット（基準位置から）
                Const COL_PARTNAME As Integer = 1             ' 部位名の列オフセット
                Const COL_RANK_LEVEL As Integer = 2           ' ランク/レベルの列オフセット
                Const COL_STATS As Integer = 3                ' アイテム性能の列オフセット
                Const COL_OPTIONS_START As Integer = 4        ' オプション開始列オフセット

                ' 要求ステータスのオフセット
                Const OFFSET_REQ_START As Integer = 2          ' 要求ステータス開始行オフセット

                ' NXオプションのオフセット
                Const OFFSET_NX1_START As Integer = 2          ' NX1オプション開始行オフセット
                Const OFFSET_NX2_START As Integer = 4          ' NX2オプション開始行オフセット
                Const OFFSET_NX3_START As Integer = 6          ' NX3オプション開始行オフセット
                Const OFFSET_NX4_START As Integer = 8          ' NX4オプション開始行オフセット

                ' D4からの相対位置でデータを解析
                Dim baseRow As Integer = StartRowIndex

                ' 各アイテムを解析（行が空になるまで繰り返し）
                While baseRow < data.Length AndAlso Not String.IsNullOrEmpty(data(baseRow)(StartColIndex))
                    ' 10行分のデータをアイテムブロックとして取得
                    Dim itemBlock = ExtractItemBlock(data, baseRow, ITEM_BLOCK_HEIGHT)
                    If itemBlock Is Nothing Then Exit While

                    ' 新しいアイテムを作成
                    Dim item = New BaseItemModel()

                    ' 基本情報の設定
                    item.Name = GetCellValue(itemBlock, OFFSET_NAME, COL_NAME)
                    item.PartName = GetCellValue(itemBlock, OFFSET_PARTNAME, COL_PARTNAME)
                    item.Rank = GetCellValue(itemBlock, OFFSET_RANK, COL_RANK_LEVEL)
                    item.Level = GetCellValue(itemBlock, OFFSET_LEVEL, COL_RANK_LEVEL)

                    ' アイテム要求の設定
                    item.Requirements = ParseRequirements(itemBlock, OFFSET_REQ_START, COL_RANK_LEVEL)

                    ' アイテム性能の設定
                    item.ItemStats = ParseItemStats(itemBlock, COL_STATS)

                    ' オプションの設定
                    item.Options = ParseOptions(itemBlock, OFFSET_NAME, COL_OPTIONS_START)

                    ' Nxオプションの設定
                    item.NxOptions = New Dictionary(Of Integer, List(Of OpValue))()

                    ' NX1～NX4のオプションを処理
                    Dim nxOffsets As New Dictionary(Of Integer, Integer) From {
                        {1, OFFSET_NX1_START},
                        {2, OFFSET_NX2_START},
                        {3, OFFSET_NX3_START},
                        {4, OFFSET_NX4_START}
                    }

                    For Each nxEntry In nxOffsets
                        Dim nxLevel = nxEntry.Key
                        Dim nxRowOffset = nxEntry.Value

                        Dim nxOptions = ParseOptions(itemBlock, nxRowOffset, COL_OPTIONS_START)
                        If nxOptions.Count > 0 Then
                            item.NxOptions(nxLevel) = nxOptions
                        End If
                    Next

                    ' アイテムをリストに追加
                    items.Add(item)

                    ' 次のアイテムへ（10行単位でアイテムが配置されている）
                    baseRow += ITEM_BLOCK_HEIGHT
                End While

            Catch ex As Exception
                Debug.WriteLine($"スプレッドシートデータの解析エラー: {ex.Message}")
                Throw New Exception("スプレッドシートデータの解析に失敗しました", ex)
            End Try

            Return items
        End Function

        ''' <summary>
        ''' 指定された行から一定行数のデータブロックを抽出
        ''' </summary>
        Private Shared Function ExtractItemBlock(data As String()(), baseRow As Integer, blockHeight As Integer) As String()()
            If baseRow + blockHeight > data.Length Then
                Return Nothing
            End If

            ' 指定された行から一定行数分の2次元配列を作成
            Dim result(blockHeight - 1)() As String
            For i As Integer = 0 To blockHeight - 1
                If baseRow + i < data.Length Then
                    result(i) = data(baseRow + i)
                Else
                    result(i) = New String() {}
                End If
            Next

            Return result
        End Function

        ''' <summary>
        ''' 指定されたセルの値を取得（StartColIndexを基準とした相対位置）
        ''' </summary>
        Private Shared Function GetCellValue(itemBlock As String()(), rowOffset As Integer, colOffset As Integer) As String
            If rowOffset >= 0 AndAlso rowOffset < itemBlock.Length AndAlso
               StartColIndex + colOffset >= 0 AndAlso StartColIndex + colOffset < itemBlock(rowOffset).Length Then
                Return If(itemBlock(rowOffset)(StartColIndex + colOffset), "")
            End If
            Return ""
        End Function

        ''' <summary>
        ''' 要求ステータスを解析
        ''' </summary>
        Private Shared Function ParseRequirements(itemBlock As String()(), startRowOffset As Integer, colOffset As Integer) As Dictionary(Of String, String)
            Dim requirements = New Dictionary(Of String, String)()
            Dim reqNames = New String() {"力", "知識", "敏捷", "知恵", "健康", "威厳", "運"}

            For i As Integer = 0 To reqNames.Length - 1
                Dim rowOffset = startRowOffset + i
                Dim value = GetCellValue(itemBlock, rowOffset, colOffset)

                If Not String.IsNullOrEmpty(value) Then
                    requirements(reqNames(i)) = value
                End If
            Next

            Return requirements
        End Function

        ''' <summary>
        ''' アイテム性能を解析
        ''' </summary>
        Private Shared Function ParseItemStats(itemBlock As String()(), colOffset As Integer) As Dictionary(Of String, String)
            Dim stats = New Dictionary(Of String, String)()
            Dim statNames = New String() {"武器最小攻撃力", "武器最大攻撃力", "武器速度", "防御力"}

            For i As Integer = 0 To statNames.Length - 1
                Dim rowOffset = (i * 2) + 1  ' 性能データの行位置
                Dim value = GetCellValue(itemBlock, rowOffset, colOffset)

                If Not String.IsNullOrEmpty(value) Then
                    stats(statNames(i)) = value
                End If
            Next

            Return stats
        End Function

        ''' <summary>
        ''' オプションを解析
        ''' </summary>
        Private Shared Function ParseOptions(itemBlock As String()(), baseRowOffset As Integer, startColOffset As Integer) As List(Of OpValue)
            Dim options = New List(Of OpValue)()
            Dim col = startColOffset

            ' 列が空になるまで繰り返し
            While True
                Dim optionName = GetCellValue(itemBlock, baseRowOffset, col)
                If String.IsNullOrEmpty(optionName) Then Exit While

                Dim optionValue = GetCellValue(itemBlock, baseRowOffset + 1, col)

                options.Add(New OpValue(optionName, optionValue))

                col += 1
            End While

            Return options
        End Function

        ''' <summary>
        ''' CSVデータを2次元配列に変換
        ''' </summary>
        ''' <param name="csvData">CSVデータ文字列</param>
        ''' <returns>文字列の2次元配列</returns>
        Private Shared Function ParseCsvTo2DArray(csvData As String) As String()()
            Dim result As New List(Of String())()

            Try
                Using reader As New StringReader(csvData)
                    Using parser As New TextFieldParser(reader)
                        parser.TextFieldType = FieldType.Delimited
                        parser.SetDelimiters(",")
                        parser.HasFieldsEnclosedInQuotes = True

                        While Not parser.EndOfData
                            Dim fields = parser.ReadFields()
                            result.Add(fields)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Debug.WriteLine($"CSV解析エラー: {ex.Message}")
                Return Nothing
            End Try

            Return result.ToArray()
        End Function
    End Class

End Namespace