using System.Collections.Generic;
using Assets.Scripts.CharacterDatas;
using Assets.Scripts.CharacterPrefab;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance { get; private set; }

    [SerializeField] private GameObject _fieldPrefab;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private CharacterData[] _characterData;
    [SerializeField] private GameObject _groundTop;
    [SerializeField] private GameObject _groundBottom;
    [SerializeField] private GameObject _groundLeft;
    [SerializeField] private GameObject _groundRight;

    private readonly int CharacterSpawnAreaMaxColumn = 2; // Max. value of columns of the spawn area for character. 

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    /// <summary>
    /// Generates level from the data.
    /// </summary>
    public void GenerateFrom(LevelData levelData)
    {
        // Length - 1 because the distance between pivot point of fields together is 1 field length
        // less than the length of entire fields.
        // For example 9 fields have 8 distance between their pivot points.
        float halfLength = (levelData.MapLength - 1) *  .5f;
        float halfHeight = (levelData.MapHeight - 1) * .5f;
        float startPointHorizontal = -halfLength;
        float startPointVertical = halfHeight;

        SpawnCoverGrounds(startPointHorizontal, startPointVertical);
        SpawnFields(levelData, startPointHorizontal, startPointVertical);
    }

    /// <summary>
    /// Spawns cover grounds.
    /// </summary>
    /// <param name="startPointHorizontal"></param>
    /// <param name="startPointVertical"></param>
    private void SpawnCoverGrounds(float startPointHorizontal, float startPointVertical)
    {
        Instantiate(_groundTop, new Vector3(0, startPointVertical + 1, 0), Quaternion.identity);
        Instantiate(_groundBottom, new Vector3(0, -startPointVertical - 1, 0), Quaternion.identity);
        Instantiate(_groundLeft, new Vector3(startPointHorizontal - 1, 0, 0), Quaternion.identity);
        Instantiate(_groundRight, new Vector3(-startPointHorizontal + 1, 0, 0), Quaternion.identity);
    }

    /// <summary>
    /// Spawns fields.
    /// </summary>
    /// <param name="levelData"></param>
    /// <param name="startPointHorizontal"></param>
    /// <param name="startPointVertical"></param>
    private void SpawnFields(LevelData levelData, float startPointHorizontal, float startPointVertical)
    {
        Vector3 spawnPos = new Vector3(startPointHorizontal, startPointVertical, 0);

        for (int j = 0; j < levelData.MapHeight; j++)
        {
            for (int i = 0; i < levelData.MapLength; i++)
            {
                var field = Instantiate(_fieldPrefab, spawnPos, Quaternion.identity);

                FieldManager.Instance.SetField(field, j, i);

                spawnPos.x += 1;
            }

            spawnPos.y -= 1;
            spawnPos.x = startPointHorizontal;
        }
    }

    /// <summary>
    /// Instantiate the characters for the corresponding player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public List<GameObject> CreateCharactersFor(PlayerType player)
    {
        var tempList = new List<GameObject>();

        // The array of random positions.
        var randomIndexes = new Vector2Int[LevelManager.Instance.Data.CharacterAmount];
        var randomPositions = new Vector3[LevelManager.Instance.Data.CharacterAmount];

        RandomizeIndexes(player, randomIndexes);
        GetSpawnPositions(randomPositions, randomIndexes);

        for (int i = 0; i < LevelManager.Instance.Data.CharacterAmount; i++)
        {
            var characterObject = Instantiate(_characterPrefab, randomPositions[i], Quaternion.identity);
            var character = characterObject.GetComponent<Character>();

            // Data
            var characterData = _characterData[Random.Range(0, _characterData.Length)];
            character.SetData(player, characterData, randomIndexes[i]);
            
            // Weapon
            var characterGetWeapon = characterObject.GetComponent<CharacterGetWeapon>();
            characterGetWeapon.SetWeaponToLeftHand(character);
            characterGetWeapon.SetWeaponToRightHand(character);

            // Color
            var characterColor = characterObject.GetComponent<CharacterColor>();
            characterColor.SetColor(PLayerColor(player));

            // Panel
            var characterPanelObject = PanelManager.Instance.GetPanel(player, i, characterObject);
            character.SetPanel(characterPanelObject);

            tempList.Add(characterObject);
        }

        return tempList;
    }

    /// <summary>
    /// Randomize the position of characters.
    /// </summary>
    /// <param name="randomIndexes"></param>
    private void RandomizeIndexes(PlayerType player, Vector2Int[] randomIndexes)
    {
        // The array to check if the field index is already assigned. 
        var fieldIndex = new Vector2[randomIndexes.Length];

        for (int h = 0; h < fieldIndex.Length; h++)
            fieldIndex[h] = new Vector2(-1, -1); // Initializes for each index a null value.

        int rowAmount = FieldManager.Instance.Fields.GetLength(0);

        for (int i = 0; i < randomIndexes.Length; i++)
        {
            int row = Random.Range(0, rowAmount);
            int col = RandomizeColumn(player);
            
            randomIndexes[i] = new Vector2Int(row, col);

            // Checks if the field index is already assigned. 
            var currentFieldIndex = new Vector2(row, col);

            for (int j = 0; j <= i; j++) 
            {
                if (currentFieldIndex == fieldIndex[j]) // ex. for the first loop, fieldIndex[0] = Vector2(-1,-1) => false.
                {
                    i--; // The main for-loop repeats this loop.
                    break;
                }

                if (j == i) // only sets in the last loop.
                { 
                    fieldIndex[j] = currentFieldIndex; // ex. fieldIndex[0] = Vector2(row, col).
                }
            }
        }
    }

    /// <summary>
    /// Randomizes the column index.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private int RandomizeColumn(PlayerType player)
    {
        if (player == PlayerType.PlayerLeft)
        {
            return Random.Range(0, CharacterSpawnAreaMaxColumn);
        }
        else if (player == PlayerType.PlayerRight)
        {
            int colAmount = FieldManager.Instance.Fields.GetLength(1);
            return Random.Range(colAmount - CharacterSpawnAreaMaxColumn, colAmount);
        }

        return -1;
    }

    /// <summary>
    /// Gets the spawn positions.
    /// </summary>
    private void GetSpawnPositions(Vector3[] randomPositions, Vector2Int[] randomIndexes)
    {
        for (int i = 0; i < randomIndexes.Length; i++)
        {
            var field = FieldManager.Instance.Fields[randomIndexes[i].x, randomIndexes[i].y];
            randomPositions[i] = field.transform.position;
        }
    }

    /// <summary>
    /// Return the color of the corresponding player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private Color PLayerColor(PlayerType player)
    {
        if (player == PlayerType.PlayerLeft)
        {
            return PanelManager.Instance.NameTextLeft.color;
        }
        else if (player == PlayerType.PlayerRight)
        {
            return PanelManager.Instance.NameTextRight.color;
        }

        return default;
    }
}
