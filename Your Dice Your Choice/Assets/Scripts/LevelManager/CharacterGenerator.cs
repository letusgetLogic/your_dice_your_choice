using System.Collections.Generic;
using Assets.Scripts.CharacterDatas;
using Assets.Scripts.CharacterPrefab;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    public static CharacterGenerator Instance { get; private set; }

    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private CharacterData[] _characterData;

    private readonly int CharacterSpawnAreaMaxColumn = 2; // Max. value of columns of the spawn area for character. 

    public List<string> CharacterNames = CharacterName.Names;

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
    /// Instantiate the characters for the corresponding player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public List<GameObject> CreateCharactersFor(Player player, PlayerType playerType)
    {
        var tempList = new List<GameObject>();

        // The array of random positions.
        var randomIndexes = new Vector2Int[LevelManager.Instance.Data.CharacterAmount];
        var randomPositions = new Vector3[LevelManager.Instance.Data.CharacterAmount];

        RandomizeIndexes(playerType, randomIndexes);
        GetSpawnPositions(randomPositions, randomIndexes);

        for (int i = 0; i < LevelManager.Instance.Data.CharacterAmount; i++)
        {
            var characterObject = Instantiate(_characterPrefab, randomPositions[i], Quaternion.identity);
            var character = characterObject.GetComponent<Character>();
            
            // Data
            var characterData = _characterData[Random.Range(0, _characterData.Length)];
            character.SetData(player, playerType, characterData, randomIndexes[i]);
            
            // Weapon
            var characterGetWeapon = characterObject.GetComponent<CharacterGetWeapon>();
            characterGetWeapon.SetWeaponToLeftHand(character);
            characterGetWeapon.SetWeaponToRightHand(character);

            // Color
            var characterColor = characterObject.GetComponent<CharacterColor>();
            characterColor.SetColor(PlayerColor(playerType), character.Name);

            // Border Color
            var characterBorderColor = characterObject.GetComponent<CharacterBorderColor>();
            character.SetEnabled(characterBorderColor, false);

            // Panel
            var characterPanelObject = PanelManager.Instance.GetPanel(playerType, i, characterObject);
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
    private Color PlayerColor(PlayerType player)
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
