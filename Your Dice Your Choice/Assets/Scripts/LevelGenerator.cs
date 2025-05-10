using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;

    public GameObject FieldPrefab;
    public GameObject[] CharacterPrefab;
    

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
        float startPointVertical = -halfLength;
        float startPointHorizontal = halfHeight;

        spawnPos = new Vector3(startPointVertical, startPointHorizontal, 0);

        for (int j = 0; j < _data.MapHeight; j++)
        {
            for (int i = 0; i < _data.MapLength; i++)
            {
                var field = Instantiate(FieldPrefab, spawnPos, Quaternion.identity);
               
                BattleManager.Instance.SetField(field, j, i);

                spawnPos.x += 1;
            }

            spawnPos.y -= 1;
            spawnPos.x = startPointVertical;
        }
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
        for (int i = 0; i < randPositions.Length; i++)
        {
            int rowLength = BattleManager.Instance.Fields.GetLength(0);
            int row = Random.Range(0, rowLength);
            int col = Random.Range(0, CharacterSpawnAreaMaxColumn);
            var field = BattleManager.Instance.Fields[row, col];
            var pos = field.transform.position;

            for (int j = 0; j < randPositions.Length; j++) // If pos is the same as the predecessor, repeat the loop.
            {
                if (randPositions[j] == pos)
                {
                    i--;
                    break;
                }

                randPositions[i] = pos;Debug.Log(randPositions[i]);
                break;
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
