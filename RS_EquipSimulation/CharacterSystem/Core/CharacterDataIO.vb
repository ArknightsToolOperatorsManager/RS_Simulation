Imports System.IO
Imports Newtonsoft.Json
Imports RS_EquipSimulation.Common.Equipment
Imports RS_EquipSimulation.Common.Creature
Imports RS_EquipSimulation.Common
Imports RS_EquipSimulation.CharacterSystem.Operation.Basic
Imports RS_EquipSimulation.CharacterSystem.Operation.Costume
Imports RS_EquipSimulation.CharacterSystem.Operation.Creature
Imports RS_EquipSimulation.CharacterSystem.Operation.Equipment
Imports RS_EquipSimulation.CharacterSystem.Operation.Status

Namespace CharacterSystem.Core

    ''' <summary>
    ''' キャラクターデータのファイル入出力を管理するクラス
    ''' </summary>
    Public Class CharacterDataIO

#Region "パス管理"
        ''' <summary>
        ''' キャラクターデータのパス管理
        ''' </summary>
        Private Class CharacterPaths
            Private ReadOnly _characterId As Guid

            Public Sub New(characterId As Guid)
                _characterId = characterId
            End Sub

            ' キャラクターのベースディレクトリ
            Public ReadOnly Property BaseDirectory As String
                Get
                    Return Path.Combine(UserDataDirectory, "characters", _characterId.ToString())
                End Get
            End Property

            ' キャラクターファイル
            Public ReadOnly Property CharacterFile As String
                Get
                    Return Path.Combine(BaseDirectory, "character.json")
                End Get
            End Property

            ' インベントリディレクトリ
            Public ReadOnly Property InventoryDirectory As String
                Get
                    Return Path.Combine(BaseDirectory, "inventories")
                End Get
            End Property

            ' インベントリファイル取得
            Public Function GetInventoryFile(inventoryType As String) As String
                Return Path.Combine(InventoryDirectory, $"{inventoryType}.json")
            End Function

            ' ディレクトリの存在チェック
            Public Function Exists() As Boolean
                Return Directory.Exists(BaseDirectory)
            End Function
        End Class

        ''' <summary>
        ''' 共通のディレクトリ定義
        ''' </summary>
        Private Shared ReadOnly Property UserDataDirectory As String
            Get
                Return "userdata"
            End Get
        End Property
#End Region

#Region "パブリックメソッド"
        ''' <summary>
        ''' キャラクターデータの保存
        ''' </summary>
        ''' <param name="characterId">キャラクターID</param>
        ''' <param name="characterData">保存するキャラクターデータ</param>
        ''' <returns>成功したかどうか</returns>
        Public Shared Function SaveCharacterData(characterId As Guid, characterData As CharacterData) As Boolean
            Try
                Dim paths = New CharacterPaths(characterId)

                ' ディレクトリの準備
                Directory.CreateDirectory(paths.BaseDirectory)
                Directory.CreateDirectory(paths.InventoryDirectory)

                ' メインデータの保存
                If Not Lib_JsonIO.SaveToJson(paths.CharacterFile, characterData) Then
                    Return False
                End If

                ' インベントリデータの保存
                If Not SaveInventoryData(characterData, paths) Then
                    Return False
                End If

                Return True
            Catch ex As Exception
                Lib_JsonIO.LogError("キャラクターデータの保存に失敗", ex)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' キャラクターデータの読み込み
        ''' </summary>
        ''' <param name="characterId">キャラクターID</param>
        ''' <returns>ロードされたキャラクターデータ</returns>
        Public Shared Function LoadCharacterData(characterId As Guid) As CharacterData
            Try
                ' パス情報の取得
                Dim paths = New CharacterPaths(characterId)

                ' ファイルの存在チェック
                If Not File.Exists(paths.CharacterFile) Then
                    Lib_JsonIO.LogError($"キャラクターファイルが存在しません: {paths.CharacterFile}", New FileNotFoundException())
                    Return Nothing
                End If

                ' インベントリマネージャーの新規作成
                Dim equipInventory = New EquipInventoryManager()
                Dim creatureInventory = New CreatureInventoryManager()

                ' インベントリデータのロード
                LoadInventoryData(equipInventory, creatureInventory, paths)

                ' メインデータのロード
                Dim serializedData = Lib_JsonIO.LoadDataFromJsonFile(Of CharacterData)(paths.CharacterFile)
                If serializedData Is Nothing Then
                    Lib_JsonIO.LogError("キャラクターデータのデシリアライズに失敗", New InvalidOperationException())
                    Return Nothing
                End If

                ' 実際のCharacterDataオブジェクトの構築
                Dim character = New CharacterData()

                ' 各マネージャーの初期化
                InitializeManagers(character, serializedData, equipInventory, creatureInventory)

                Return character
            Catch ex As Exception
                Lib_JsonIO.LogError("キャラクターデータの読み込みに失敗", ex)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' キャラクターの存在確認
        ''' </summary>
        ''' <param name="characterId">キャラクターID</param>
        ''' <returns>存在するかどうか</returns>
        Public Shared Function CharacterExists(characterId As Guid) As Boolean
            Return New CharacterPaths(characterId).Exists()
        End Function

        ''' <summary>
        ''' 全キャラクターIDの取得
        ''' </summary>
        ''' <returns>キャラクターIDのリスト</returns>
        Public Shared Function GetAllCharacterIds() As List(Of Guid)
            Try
                Dim baseDir = Path.Combine(UserDataDirectory, "characters")
                If Not Directory.Exists(baseDir) Then Return New List(Of Guid)

                Return Directory.GetDirectories(baseDir).
                    Select(Function(dir) Path.GetFileName(dir)).
                    Where(Function(name) Guid.TryParse(name, New Guid)).
                    Select(Function(name) Guid.Parse(name)).
                    ToList()
            Catch ex As Exception
                Lib_JsonIO.LogError("キャラクターID一覧の取得に失敗", ex)
                Return New List(Of Guid)
            End Try
        End Function
#End Region

#Region "内部処理"
        ''' <summary>
        ''' シリアライズ可能なデータの抽出
        ''' </summary>
        ''' <param name="characterData">キャラクターデータ</param>
        ''' <returns>シリアライズ可能なデータ</returns>
        Private Shared Function ExtractSerializableData(characterData As CharacterData) As CharacterData
            Return New CharacterData With {
                .BasicManager = characterData.BasicManager,
                .StatusManager = characterData.StatusManager,
                .EquipmentManager = characterData.EquipmentManager,
                .CreatureManager = characterData.CreatureManager,
                .CostumeManager = characterData.CostumeManager
            }
        End Function

        ''' <summary>
        ''' インベントリデータの保存
        ''' </summary>
        ''' <param name="characterData">キャラクターデータ</param>
        ''' <param name="paths">パス情報</param>
        ''' <returns>成功したかどうか</returns>
        Private Shared Function SaveInventoryData(characterData As CharacterData, paths As CharacterPaths) As Boolean
            Try
                ' 装備インベントリの保存
                Dim equipPath = paths.GetInventoryFile("equipment")
                Dim equipData = characterData._equipmentInventory.GetInventoryData()
                If Not Lib_JsonIO.SaveToJson(equipPath, equipData) Then
                    Return False
                End If

                ' クリーチャーインベントリの保存
                Dim creaturePath = paths.GetInventoryFile("creature")
                Dim creatureData = characterData._creatureInventory.GetInventoryData()
                Return Lib_JsonIO.SaveToJson(creaturePath, creatureData)
            Catch ex As Exception
                Lib_JsonIO.LogError("インベントリデータの保存に失敗", ex)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' インベントリデータのロード
        ''' </summary>
        ''' <param name="equipInventory">装備インベントリマネージャー</param>
        ''' <param name="creatureInventory">クリーチャーインベントリマネージャー</param>
        ''' <param name="paths">パス情報</param>
        Private Shared Sub LoadInventoryData(equipInventory As EquipInventoryManager,
                                          creatureInventory As CreatureInventoryManager,
                                          paths As CharacterPaths)
            Try
                ' 装備インベントリ
                Dim equipPath = paths.GetInventoryFile("equipment")
                If File.Exists(equipPath) Then
                    Dim equipData = Lib_JsonIO.LoadDataFromJsonFile(Of List(Of EquipInventoryItem))(equipPath)
                    If equipData IsNot Nothing Then
                        equipInventory.SetInventoryData(equipData)
                    End If
                End If

                ' クリーチャーインベントリ
                Dim creaturePath = paths.GetInventoryFile("creature")
                If File.Exists(creaturePath) Then
                    Dim creatureData = Lib_JsonIO.LoadDataFromJsonFile(Of List(Of CreatureInventoryItem))(creaturePath)
                    If creatureData IsNot Nothing Then
                        creatureInventory.SetInventoryData(creatureData)
                    End If
                End If
            Catch ex As Exception
                Lib_JsonIO.LogError("インベントリデータのロードに失敗", ex)
                ' 読み込み失敗時は空のインベントリを維持
            End Try
        End Sub

        ''' <summary>
        ''' マネージャーの初期化
        ''' </summary>
        ''' <param name="character">キャラクターデータ</param>
        ''' <param name="serializedData">シリアライズデータ</param>
        ''' <param name="equipInventory">装備インベントリ</param>
        ''' <param name="creatureInventory">クリーチャーインベントリ</param>
        Private Shared Sub InitializeManagers(character As CharacterData, serializedData As CharacterData,
                                           equipInventory As EquipInventoryManager,
                                           creatureInventory As CreatureInventoryManager)
            ' 各マネージャーの初期化
            character.InitializeFromSerializedData(serializedData, equipInventory, creatureInventory)

            ' サマリーマネージャーは自動的に初期化される
            character.BasicManager.InitializeAfterDeserialize()
            character.StatusManager.InitializeAfterDeserialize()
            character.EquipmentManager.InitializeAfterDeserialize()
            character.CreatureManager.InitializeAfterDeserialize()
            character.CostumeManager.InitializeAfterDeserialize()
        End Sub
#End Region

#Region "データモデル"
        ''' <summary>
        ''' シリアライズ可能なキャラクターデータ
        ''' </summary>
        Public Class SerializedCharacterData
            Public Property BasicManager As BasicManagerModel
            Public Property StatusManager As StatusManagerModel
            Public Property EquipmentManager As EquipmentManagerModel
            Public Property CreatureManager As CreatureManagerModel
            Public Property CostumeManager As CostumeManagerModel
        End Class
#End Region
    End Class

End Namespace