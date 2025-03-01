Imports System.IO

Namespace MasterSystem.Common

    Public Class InputFileConfig
        ' ファイル名の定数
        Private Shared ReadOnly Property FileNames As Dictionary(Of String, String)
            Get
                Return New Dictionary(Of String, String) From {
                {"equip", "items.json"},
                {"option", "oplist.csv"},
                {"prefix", "prefix.csv"}
            }
            End Get
        End Property

        ' 基本パス
        Public Shared ReadOnly Property BaseDirectory As String
            Get
                Return Path.Combine("inputdata")
            End Get
        End Property

        ' 個別のファイルパス取得プロパティ
        Public Shared ReadOnly Property BaseItemFilePath As String
            Get
                Return GetFilePath("equip")
            End Get
        End Property

        Public Shared ReadOnly Property PrefixFilePath As String
            Get
                Return GetFilePath("prefix")
            End Get
        End Property

        Public Shared ReadOnly Property OptionListFilePath As String
            Get
                Return GetFilePath("option")
            End Get
        End Property

        ' ファイルパス取得メソッド
        Public Shared Function GetFilePath(fileType As String) As String
            If FileNames.ContainsKey(fileType) Then
                Return Path.Combine(BaseDirectory, FileNames(fileType))
            End If
            Throw New ArgumentException($"Unknown file type: {fileType}")
        End Function

        ' ユーティリティメソッド
        Public Shared Function ValidateInputFiles() As Boolean
            Try
                Return FileNames.Values.All(Function(fileName) File.Exists(Path.Combine(BaseDirectory, fileName)))
            Catch
                Return False
            End Try
        End Function
    End Class

End Namespace
