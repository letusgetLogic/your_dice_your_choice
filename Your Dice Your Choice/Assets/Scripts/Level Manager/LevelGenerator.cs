using System.Collections.Generic;
using Assets.Scripts.Characters;
using Unity.VisualScripting;
using UnityEngine;

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
    /// Spawn fields.
    /// </summary>
    public void Generate(LevelData levelData)
    {
        // Length - 1 because the distance between pivot point of fields together is 1 field length less than the length of entire fields.
        // For example 9 fields have 8 distance between their pivot points.
        float halfLength = (levelData.MapLength - 1) *  .5f;
        float halfHeight = (levelData.MapHeight - 1) * .5f;
        float startPointHorizontal = -halfLength;
        float startPointVertical = halfHeight;

        SpawnCoverGrounds(startPointHorizontal, startPointVertical);
        SpawnFields(levelData, startPointHorizontal, startPointVertical);
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
    /// Spawns cover grounds.
    /// </summary>
    /// <param name="startPointHorizontal"></param>
    /// <param name="startPointVertical"></param>
    private void SpawnCoverGrounds(float startPointHorizontal, float startPointVertical)
    {
        SpawnCoverGround(_groundTop, new Vector3(0, startPointVertical + 1, 0));
        SpawnCoverGround(_groundBottom, new Vector3(0, -startPointVertical - 1, 0));
        SpawnCoverGround(_groundLeft, new Vector3(startPointHorizontal - 1, 0, 0));
        SpawnCoverGround(_groundRight, new Vector3(-startPointHorizontal + 1, 0, 0));
    }

    /// <summary>
    /// Spawns cover ground.
    /// </summary>
    /// <param name="ground"></param>
    /// <param name="spawnPos"></param>
    private void SpawnCoverGround(GameObject ground, Vector3 spawnPos)
    {
        Instantiate(ground, spawnPos, Quaternion.identity);
    }

    /// <summary>
    /// Instantiate the characters for the corresponding player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public List<GameObject> CreateCharactersFor(TurnState player)
    {
        var tempList = new List<GameObject>();

        // The array of random positions.
        var randPositions = new Vector3[LevelManager.Instance.Data.CharacterAmount];

        RandomizePositionInArray(player, randPositions);

        for (int i = 0; i < LevelManager.Instance.Data.CharacterAmount; i++)
        {
            var characterData = _characterData[Random.Range(0, _characterData.Length)];

            var characterObject = Instantiate(_characterPrefab, randPositions[i], Quaternion.identity);

            var character = characterObject.GetComponent<Character>();
            var characterGetWeapon = characterObject.GetComponent<CharacterGetWeapon>();
            var characterColor = characterObject.GetComponent<CharacterColor>();

            var panel = CharacterPanels(player)[i];

            var characterPanel = panel.GetComponent<CharacterPanel>();

            character.SetData(characterData);

            characterGetWeapon.SetWeaponToLeftHand(character);
            characterGetWeapon.SetWeaponToRightHand(character);

            characterColor.SetColor(PLayerColor(player));

            panel.SetActive(true);
            characterPanel.SetCharacter(characterObject);
            characterPanel.SetAction();

            characterObject.GetComponent<Character>().SetPanel(panel);

            tempList.Add(characterObject);
        }

        return tempList;
    }

    /// <summary>
    /// Randomize the position of characters.
    /// </summary>
    /// <param name="randPositions"></param>
    private void RandomizePositionInArray(TurnState player, Vector3[] randPositions)
    {
        // The array to check if the field index is already assigned. 
        var fieldIndex = new Vector2[randPositions.Length];

        for (int h = 0; h < fieldIndex.Length; h++)
            fieldIndex[h] = new Vector2(-1, -1); // Initializes for each index a null value.

        int rowAmount = FieldManager.Instance.Fields.GetLength(0);

        for (int i = 0; i < randPositions.Length; i++)
        {
            int row = Random.Range(0, rowAmount);
            int col = RandomizeCol(player);
            var field = FieldManager.Instance.Fields[row, col];
            var pos = field.transform.position;

            randPositions[i] = pos;

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
    private int RandomizeCol(TurnState player)
    {
        if (player == TurnState.PlayerLeft)
        {
            return Random.Range(0, CharacterSpawnAreaMaxColumn);
        }
        else if (player == TurnState.PlayerRight)
        {
            int colAmount = FieldManager.Instance.Fields.GetLength(1);
            return Random.Range(colAmount - CharacterSpawnAreaMaxColumn, colAmount);
        }

        return -1;
    }

    /// <summary>
    /// Return the serialized panels in PanelManager for the corresponding player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private GameObject[] CharacterPanels(TurnState player)
    {
        if (player == TurnState.PlayerLeft)
        {
            return PanelManager.Instance.CharacterPanelsLeft;
        }
        else if (player == TurnState.PlayerRight)
        {
            return PanelManager.Instance.CharacterPanelsRight;
        }

        return default;
    }

    /// <summary>
    /// Return the color of the corresponding player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private Color PLayerColor(TurnState player)
    {
        if (player == TurnState.PlayerLeft)
        {
            return PanelManager.Instance.NameTextLeft.color;
        }
        else if (player == TurnState.PlayerRight)
        {
            return PanelManager.Instance.NameTextRight.color;
        }

        return default;
    }
}
