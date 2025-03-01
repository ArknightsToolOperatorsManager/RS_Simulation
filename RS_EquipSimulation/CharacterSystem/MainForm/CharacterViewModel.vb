Imports System.ComponentModel
Imports RS_EquipSimulation.CharacterSystem.Core

Public Class CharacterViewModel
    Implements INotifyPropertyChanged

    ' キャラクター管理
    Private _charactersManager As CharactersManager
    Private _currentCharacter As CharacterData
    Private _characterList As List(Of (Guid, String))

    ' 選択中のインデックス
    Private _selectedCharacterIndex As Integer = -1

    ' コンストラクタ
    Public Sub New()
        ' キャラクター管理の初期化
        _charactersManager = New CharactersManager()
        _charactersManager.LoadAllCharacters()
        RefreshCharacterList()
    End Sub

    ' キャラクターリストの取得
    Public ReadOnly Property CharacterList As List(Of String)
        Get
            Return _characterList.Select(Function(c) c.Item2).ToList()
        End Get
    End Property

    ' 現在選択中のキャラクター
    Public ReadOnly Property CurrentCharacter As CharacterData
        Get
            Return _currentCharacter
        End Get
    End Property

    ' 選択中のインデックス
    Public Property SelectedCharacterIndex As Integer
        Get
            Return _selectedCharacterIndex
        End Get
        Set(value As Integer)
            If value <> _selectedCharacterIndex AndAlso value >= 0 AndAlso value < _characterList.Count Then
                _selectedCharacterIndex = value
                SwitchCharacter(value)
                OnPropertyChanged(NameOf(SelectedCharacterIndex))
            End If
        End Set
    End Property

    ' キャラクターデータ変更イベント
    Public Event CharacterDataUpdated As EventHandler

    ' プロパティ変更通知
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    ' プロパティ変更通知メソッド
    Protected Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    ' キャラクターリストの更新
    Public Sub RefreshCharacterList()
        _characterList = _charactersManager.GetCharacterList().ToList()
        OnPropertyChanged(NameOf(CharacterList))
    End Sub

    ' 新規キャラクター作成
    Public Function CreateNewCharacter(Optional characterName As String = Nothing) As Boolean
        Try
            ' 現在のキャラクターのイベントハンドラを解除
            If _currentCharacter IsNot Nothing Then
                RemoveHandler _currentCharacter.DataUpdated, AddressOf OnCharacterDataUpdated
            End If

            ' 新規キャラクターの作成（保存→ロード方式）
            Dim character = _charactersManager.CreateCharacter(characterName)

            If character IsNot Nothing Then
                _currentCharacter = character

                ' キャラクターリスト更新
                RefreshCharacterList()

                ' 新しいキャラクターを選択状態に
                Dim newIdx = _characterList.FindIndex(Function(c) c.Item2 = character.BasicManager.CharacterName)
                If newIdx >= 0 Then
                    _selectedCharacterIndex = newIdx
                    OnPropertyChanged(NameOf(SelectedCharacterIndex))
                End If

                OnPropertyChanged(NameOf(CurrentCharacter))

                ' キャラクターデータ更新イベントの登録
                AddHandler _currentCharacter.DataUpdated, AddressOf OnCharacterDataUpdated

                ' イベント発火
                RaiseEvent CharacterDataUpdated(Me, EventArgs.Empty)

                Return True
            End If

            Return False
        Catch ex As Exception
            ' エラー処理
            Debug.WriteLine($"キャラクター作成エラー: {ex.Message}")
            Return False
        End Try
    End Function

    ' キャラクター切り替え
    Private Sub SwitchCharacter(index As Integer)
        If index < 0 OrElse index >= _characterList.Count Then
            Return
        End If

        Try
            ' 現在のキャラクターのイベントハンドラを解除
            If _currentCharacter IsNot Nothing Then
                RemoveHandler _currentCharacter.DataUpdated, AddressOf OnCharacterDataUpdated
            End If

            ' キャラクターIDを取得
            Dim characterId = _characterList(index).Item1

            ' キャラクターを切り替え
            If _charactersManager.SwitchCharacter(characterId) Then
                _currentCharacter = _charactersManager.CurrentCharacter

                ' キャラクターデータ更新イベントの登録
                AddHandler _currentCharacter.DataUpdated, AddressOf OnCharacterDataUpdated

                OnPropertyChanged(NameOf(CurrentCharacter))
                RaiseEvent CharacterDataUpdated(Me, EventArgs.Empty)
            End If
        Catch ex As Exception
            ' エラー処理
            Debug.WriteLine($"キャラクター切り替えエラー: {ex.Message}")
        End Try
    End Sub

    ' 現在のキャラクターを保存
    Public Function SaveCurrentCharacter() As Boolean
        Try
            If _currentCharacter Is Nothing Then Return False

            ' 現在のキャラクターを保存
            Dim result = _charactersManager.SaveCurrentCharacter()

            ' 保存が成功したらリストを更新（名前変更に対応）
            If result Then
                RefreshCharacterList()
            End If

            Return result
        Catch ex As Exception
            ' エラー処理
            Debug.WriteLine($"キャラクター保存エラー: {ex.Message}")
            Return False
        End Try
    End Function

    ' キャラクターデータ更新イベントハンドラ
    Private Sub OnCharacterDataUpdated(sender As Object, e As EventArgs)
        ' キャラクター名が変更されたらリストも更新
        RefreshCharacterList()

        ' イベントを転送
        RaiseEvent CharacterDataUpdated(Me, e)
    End Sub

    ' 明示的なキャラクター名変更処理
    Public Sub ChangeCharacterName(newName As String)
        If _currentCharacter IsNot Nothing AndAlso Not String.IsNullOrEmpty(newName) Then
            _currentCharacter.BasicManager.CharacterName = newName
            RefreshCharacterList()

            ' 選択中のインデックスを更新
            Dim newIdx = _characterList.FindIndex(Function(c) c.Item2 = newName)
            If newIdx >= 0 AndAlso newIdx <> _selectedCharacterIndex Then
                _selectedCharacterIndex = newIdx
                OnPropertyChanged(NameOf(SelectedCharacterIndex))
            End If
        End If
    End Sub

    ' リソース解放
    Public Sub Dispose()
        Try
            ' 現在のキャラクターのイベントハンドラを解除
            If _currentCharacter IsNot Nothing Then
                RemoveHandler _currentCharacter.DataUpdated, AddressOf OnCharacterDataUpdated
            End If

            ' キャラクターマネージャーの解放
            If _charactersManager IsNot Nothing Then
                _charactersManager.Dispose()
            End If
        Catch ex As Exception
            ' エラー処理
            Debug.WriteLine($"リソース解放エラー: {ex.Message}")
        End Try
    End Sub
End Class