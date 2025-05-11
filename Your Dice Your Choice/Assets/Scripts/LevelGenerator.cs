using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance { get; private set; }

    public GameObject FieldPrefab;
    public GameObject[] CharacterPrefab;
    public GameObject GroundTop;
    public GameObject GroundBottom;
    public GameObject GroundLeft;
    public GameObject GroundRight;

    private readonly int CharacterSpawnAreaMaxColumn = 2; // Max. value of columns of the spawn area for character. 

    private LevelData _data;

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
    public void SpawnField()
    {
        Transform spawnTransform = GetComponent<Transform>();
        Vector3 spawnPos = spawnTransform.position;

        // Length - 1 because the distance between pivot point of fields together is 1 field length less than the length of entire fields.
        // For example 9 fields have 8 distance between their pivot points.
        float halfLength = (_data.MapLength - 1) * .5f;
        float halfHeight = (_data.MapHeight - 1) * .5f;
        float startPointHorizontal = -halfLength;
        float startPointVertical = halfHeight;

        spawnPos = new Vector3(startPointHorizontal, startPointVertical, 0);

        for (int j = 0; j < _data.MapHeight; j++)
        {
            for (int i = 0; i < _data.MapLength; i++)
            {
                var field = Instantiate(FieldPrefab, spawnPos, Quaternion.identity);

                BattleManager.Instance.SetField(field, j, i);

                spawnPos.x += 1;
            }

            spawnPos.y -= 1;
            spawnPos.x = startPointHorizontal;
        }

        spawnPos = new Vector3(0, startPointVertical + 1, 0);
        Instantiate(GroundTop, spawnPos, Quaternion.identity); Debug.Log(spawnPos);

        spawnPos = new Vector3(0, -startPointVertical - 1, 0);
        Instantiate(GroundBottom, spawnPos, Quaternion.identity); Debug.Log(spawnPos);

        spawnPos = new Vector3(startPointHorizontal - 1, 0, 0);
        Instantiate(GroundLeft, spawnPos, Quaternion.identity); Debug.Log(spawnPos);

        spawnPos = new Vector3(-startPointHorizontal + 1, 0, 0);
        Instantiate(GroundRight, spawnPos, Quaternion.identity); Debug.Log(spawnPos);
    }

    /// <summary>
    /// Spawn characters.
    /// </summary>
    public void SpawnCharacter()
    {
        Vector3[] randPositions = new Vector3[_data.CharacterAmount];
        RandomizePosition(randPositions);

        for (int i = 0; i < _data.CharacterAmount; i++)
        {
            var prefab = CharacterPrefab[Random.Range(0, CharacterPrefab.Length)];

            Instantiate(prefab, randPositions[i], Quaternion.identity);

            BattleManager.Instance.SetCharacter(prefab, i);
        }
    }

    /// <summary>
    /// Randomize the position of characters.
    /// </summary>
    /// <param name="randPositions"></param>
    private void RandomizePosition(Vector3[] randPositions)
    {
        // The second array to check if the field index is already assigned. 
        Vector2[] fieldIndex = new Vector2[randPositions.Length];

        for (int h = 0; h < fieldIndex.Length; h++)
            fieldIndex[h] = new Vector2(-1, -1);


        for (int i = 0; i < randPositions.Length; i++)
        {
            int rowLength = BattleManager.Instance.Fields.GetLength(0);
            int row = Random.Range(0, rowLength);
            int col = Random.Range(0, CharacterSpawnAreaMaxColumn);
            var field = BattleManager.Instance.Fields[row, col];
            var pos = field.transform.position;
            randPositions[i] = pos;

            // Check if the field index is already assigned. 
            var tempIndex = new Vector2(row, col);

            for (int j = 0; j <= i; j++)
            {
                if (tempIndex == fieldIndex[j])
                {
                    i--;
                    break;
                }

                if (j == i) fieldIndex[j] = tempIndex;
            }
        }
    }

    /// <summary>
    /// References _data.
    /// </summary>
    public void SetData()
    {
        _data = LevelManager.Instance.Data;
    }
}
