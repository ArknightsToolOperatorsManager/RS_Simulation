Imports RS_EquipSimulation.Common

Namespace CharacterSystem.Core

    ''' <summary>
    ''' キャラクターデータの管理を行うクラス
    ''' </summary>
    Public Class CharactersManager
        Implements IDisposable

#Region "プロパティ"
        ' 全キャラクターのデータを保持
        Private ReadOnly _characters As New Dictionary(Of Guid, CharacterData)

        ' 現在選択中のキャラクター
        Private _currentCharacterId As Guid?

        ' 現在のキャラクター
        Public ReadOnly Property CurrentCharacter As CharacterData
            Get
                If Not _currentCharacterId.HasValue Then Return Nothing
                Return _characters(_currentCharacterId.Value)
            End Get
        End Property
#End Region

#Region "キャラクター操作"
        ''' <summary>
        ''' 新規キャラクターの作成（保存後ロード）
        ''' </summary>
        ''' <param name="characterName">キャラクター名（指定がない場合は自動生成）</param>
        ''' <returns>作成されたキャラクターデータ</returns>
        Public Function CreateCharacter(Optional characterName As String = Nothing) As CharacterData
            Try
                ' 新しいGUIDの生成
                Dim newCharacterId = Guid.NewGuid()

                ' 最小構成の仮キャラクターを作成
                Dim tempCharacter = CreateMinimalCharacter(characterName)

                ' ファイルに保存
                If Not CharacterDataIO.SaveCharacterData(newCharacterId, tempCharacter) Then
                    Throw New Exception("新規キャラクターの初期保存に失敗しました")
                End If

                ' 保存したファイルからロード
                Dim character = LoadCharacter(newCharacterId)
                If character Is Nothing Then
                    Throw New Exception("新規作成したキャラクターのロードに失敗しました")
                End If

                Return character
            Catch ex As Exception
                Lib_JsonIO.LogError("キャラクター作成に失敗", ex)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' 最小構成のキャラクター作成（保存用）
        ''' </summary>
        ''' <param name="characterName">キャラクター名</param>
        ''' <returns>最小構成のキャラクターデータ</returns>
        Private Function CreateMinimalCharacter(Optional characterName As String = Nothing) As CharacterData
            Dim character = New CharacterData()

            ' 基本情報のみ設定
            If Not String.IsNullOrEmpty(characterName) Then
                character.BasicManager.CharacterName = characterName
            Else
                character.BasicManager.CharacterName = $"新規キャラクター{DateTime.Now.ToString("yyyyMMddHHmmss")}"
            End If

            ' デフォルト値を設定
            character.BasicManager.JobName = "剣士"  ' デフォルト職業
            character.BasicManager.Level = 2000      ' デフォルトレベル

            Return character
        End Function

        ''' <summary>
        ''' アプリケーション起動時の全キャラクターロード
        ''' </summary>
        ''' <returns>成功したかどうか</returns>
        Public Function LoadAllCharacters() As Boolean
            Try
                For Each characterId In CharacterDataIO.GetAllCharacterIds()
                    Dim character = LoadCharacter(characterId)
                Next
                Return True
            Catch ex As Exception
                Lib_JsonIO.LogError("キャラクター一覧のロードに失敗", ex)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' キャラクターのロード
        ''' </summary>
        ''' <param name="characterId">キャラクターID</param>
        ''' <returns>ロードされたキャラクターデータ</returns>
        Public Function LoadCharacter(characterId As Guid) As CharacterData
            Try
                ' 既にメモリ内にある場合はそれを返す
                If _characters.ContainsKey(characterId) Then
                    _currentCharacterId = characterId
                    Return _characters(characterId)
                End If

                ' ファイルからロード
                Dim character = CharacterDataIO.LoadCharacterData(characterId)
                If character Is Nothing Then
                    Return Nothing
                End If

                ' コレクションに追加
                _characters.Add(characterId, character)
                _currentCharacterId = characterId

                Return character
            Catch ex As Exception
                Lib_JsonIO.LogError("キャラクターロードに失敗", ex)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' キャラクターの切り替え
        ''' </summary>
        ''' <param name="characterId">切り替え先キャラクターのID</param>
        ''' <returns>成功したかどうか</returns>
        Public Function SwitchCharacter(characterId As Guid) As Boolean
            Try
                ' 指定されたキャラクターの存在確認
                If Not _characters.ContainsKey(characterId) AndAlso Not CharacterDataIO.CharacterExists(characterId) Then
                    Return False
                End If

                ' 現在のキャラクターをセーブ
                If _currentCharacterId.HasValue Then
                    If Not CharacterDataIO.SaveCharacterData(_currentCharacterId.Value, CurrentCharacter) Then
                        Return False
                    End If
                End If

                ' キャラクター切り替え
                If Not _characters.ContainsKey(characterId) Then
                    ' メモリになければロード
                    If LoadCharacter(characterId) Is Nothing Then
                        Return False
                    End If
                Else
                    ' 既にメモリにあれば選択状態を変更
                    _currentCharacterId = characterId
                End If

                Return True
            Catch ex As Exception
                Lib_JsonIO.LogError("キャラクター切り替えに失敗", ex)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' 現在のキャラクターの保存
        ''' </summary>
        ''' <returns>成功したかどうか</returns>
        Public Function SaveCurrentCharacter() As Boolean
            Try
                If Not _currentCharacterId.HasValue Then Return False
                Return CharacterDataIO.SaveCharacterData(_currentCharacterId.Value, CurrentCharacter)
            Catch ex As Exception
                Lib_JsonIO.LogError("現在のキャラクターの保存に失敗", ex)
                Return False
            End Try
        End Function
#End Region

#Region "情報取得"
        ''' <summary>
        ''' 全キャラクター情報の取得
        ''' </summary>
        ''' <returns>キャラクターIDと名前のタプルリスト</returns>
        Public Function GetCharacterList() As IEnumerable(Of (Guid, String))
            Dim result As New List(Of (Guid, String))

            ' 各キャラクターのIDと名前を取得
            For Each kvp In _characters
                Dim characterId As Guid = kvp.Key
                Dim character As CharacterData = kvp.Value
                Dim characterName As String = character.BasicManager.CharacterName

                ' タプルとして追加
                result.Add((characterId, characterName))
            Next

            Return result
        End Function

        ''' <summary>
        ''' 現在選択中のキャラクターID
        ''' </summary>
        ''' <returns>キャラクターID（選択がない場合はNull）</returns>
        Public Function GetCurrentCharacterId() As Guid?
            Return _currentCharacterId
        End Function
#End Region

#Region "IDisposable"
        Private _disposed As Boolean = False

        ''' <summary>
        ''' リソースの解放
        ''' </summary>
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    ' 現在のキャラクターがあれば保存
                    If _currentCharacterId.HasValue Then
                        Try
                            SaveCurrentCharacter()
                        Catch ex As Exception
                            Lib_JsonIO.LogError("終了時のキャラクター保存に失敗", ex)
                        End Try
                    End If

                    ' 全キャラクターの解放
                    For Each character In _characters.Values
                        character.Dispose()
                    Next
                    _characters.Clear()
                End If
                _disposed = True
            End If
        End Sub

        ''' <summary>
        ''' Disposeメソッド
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

End Namespace