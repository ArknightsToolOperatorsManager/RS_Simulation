Namespace CharacterSystem.Common


#Region "基底クラス"
    ''' <summary>
    ''' オプション提供元の基底クラス
    ''' </summary>
    Public MustInherit Class OptionProviderBase
        Private _optionManager As OptionManagerModel

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        Protected Sub New(optManager As OptionManagerModel)
            SetOptionManager(optManager)
        End Sub

        ''' <summary>
        ''' オプションマネージャーの再設定
        ''' </summary>
        ''' <param name="optManager">オプション管理クラス</param>
        Public Overridable Sub SetOptionManager(optManager As OptionManagerModel)
            _optionManager = optManager

            ' オプションマネージャーが変更されたら、更新処理も呼び出す
            If _optionManager IsNot Nothing Then
                UpdateOptionValues()
            End If
        End Sub

        ''' <summary>
        ''' 同じオプション名の値を合計してから登録
        ''' </summary>
        Protected Sub RegisterMergedOptions(sourceId As String, options As List(Of OpValue))
            If _optionManager Is Nothing Then Return

            Dim optionSingleton = OptionSingletonModel.Instance

            ' オプションを名前でグループ化
            Dim mergedOptions = options.
            GroupBy(Function(op) op.OptionName).
            Select(Function(group)
                       Dim optionName = group.Key
                       Dim optionType = optionSingleton.GetOptionType(optionName)
                       Dim isRatioType = (optionType.Contains("比率"))

                       ' 値の合計処理
                       Dim totalValue As Double = 0

                       For Each op In group
                           ' 比率タイプの場合は分数を小数点に変換
                           If isRatioType Then
                               totalValue += ConvertRatioToDecimal(op.OptionValue)
                           Else
                               ' 通常のオプションは数値に変換して合計
                               Dim value As Double
                               If Double.TryParse(op.OptionValue, value) Then
                                   totalValue += value
                               End If
                           End If
                       Next

                       ' 結果を文字列に戻す
                       Dim resultValue As String
                       If isRatioType Then
                           ' 小数点表記のまま保持
                           resultValue = totalValue.ToString()
                       Else
                           ' 通常のオプションは元の形式に合わせる（整数に戻すかどうかは要件次第）
                           resultValue = totalValue.ToString()
                       End If

                       Return New OpValue(optionName, resultValue)
                   End Function).
                Where(Function(op)
                          ' Filter out options with a value of 0
                          Dim value As Double
                          Return Not (Double.TryParse(op.OptionValue, value) AndAlso value = 0)
                      End Function).
            ToList()

            _optionManager.RegisterOptions(sourceId, mergedOptions)
        End Sub

        Private Function ConvertRatioToDecimal(ratioStr As String) As Double
            ' "1/2", "1/3" などの形式を想定
            If ratioStr.Contains("/") Then
                Dim parts = ratioStr.Split("/"c)
                If parts.Length = 2 Then
                    Dim numerator As Double
                    Dim denominator As Double

                    If Double.TryParse(parts(0), numerator) AndAlso Double.TryParse(parts(1), denominator) Then
                        If denominator <> 0 Then
                            Return numerator / denominator
                        End If
                    End If
                End If
            Else
                ' 通常の数値表記の場合はそのまま変換
                Dim value As Double
                If Double.TryParse(ratioStr, value) Then
                    Return value
                End If
            End If

            ' 変換できない場合は0を返す
            Return 0
        End Function

        ''' <summary>
        ''' オプション更新の基本メソッド
        ''' </summary>
        Protected Overridable Sub UpdateOptionValues()
            ' 派生クラスでオーバーライド
        End Sub

        ''' <summary>
        ''' 変更通知
        ''' </summary>
        Protected Overridable Sub NotifyChanged()
            UpdateOptionValues()
        End Sub
    End Class
#End Region

#Region "マネージャー"
    ''' <summary>
    ''' オプションの登録・管理を行うマネージャー
    ''' </summary>
    Public Class OptionManagerModel
        ' オプション提供元とそのオプションリストの管理
        Private _optionSources As New Dictionary(Of String, List(Of OpValue))

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        Public Sub New()
            _optionSources = New Dictionary(Of String, List(Of OpValue))
        End Sub

        ''' <summary>
        ''' オプションの登録
        ''' </summary>
        ''' <param name="sourceId">オプション提供元の識別子</param>
        ''' <param name="options">オプションのリスト</param>
        Public Sub RegisterOptions(sourceId As String, options As List(Of OpValue))
            _optionSources(sourceId) = options
#If 0 Then ' 上記代入式のみでエラーが出たら切り替える
            If _optionSources.ContainsKey(sourceId) Then
                _optionSources(sourceId) = options  ' 更新
            Else
                _optionSources.Add(sourceId, options)  ' 新規追加
            End If
#End If
        End Sub

        ''' <summary>
        ''' オプションの登録解除
        ''' </summary>
        ''' <param name="sourceId">オプション提供元の識別子</param>
        Public Sub UnregisterOptions(sourceId As String)
            If _optionSources.ContainsKey(sourceId) Then
                _optionSources.Remove(sourceId)
            End If
        End Sub

        ''' <summary>
        ''' 特定の提供元からのオプション取得
        ''' </summary>
        ''' <param name="sourceId">オプション提供元の識別子</param>
        Public Function GetOptionsFromSource(sourceId As String) As List(Of OpValue)
            If _optionSources.ContainsKey(sourceId) Then
                Return New List(Of OpValue)(_optionSources(sourceId))
            End If
            Return New List(Of OpValue)
        End Function

        ''' <summary>
        ''' 全てのオプションを取得
        ''' </summary>
        Public Function GetAllOptions() As List(Of OpValue)
            Dim allOptions As New List(Of OpValue)
            For Each options In _optionSources.Values
                allOptions.AddRange(options)
            Next
            Return allOptions
        End Function

        ''' <summary>
        ''' 登録されている提供元の一覧を取得
        ''' </summary>
        Public Function GetRegisteredSources() As List(Of String)
            Return _optionSources.Keys.ToList()
        End Function

#If DEBUG Then
        ''' <summary>
        ''' デバッグ用：登録されているオプションの詳細情報を取得
        ''' </summary>
        Public Function GetDebugInfo() As String
            Dim info As New System.Text.StringBuilder()
            info.AppendLine("--- Option Manager Debug Info ---")
            info.AppendLine($"Registered Sources: {_optionSources.Count}")

            For Each source In _optionSources
                info.AppendLine($"Source: {source.Key}")
                info.AppendLine($"Options Count: {source.Value.Count}")
                For Each op In source.Value
                    info.AppendLine($"  - {op.OptionName} ({op.OptionValue})")
                Next
            Next

            Return info.ToString()
        End Function
#End If
    End Class
#End Region

End Namespace