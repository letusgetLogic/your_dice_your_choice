using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject FieldPrefab;
    public GameObject[] CharacterPrefab;
    

    private readonly int CharacterSpawnAreaMaxColumn = 2; // Max. value of columns of the spawn area for character. 

    private LevelData _data;


    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        if (LevelManager.Instance.Data != null)
        { 
            _data = LevelManager.Instance.Data;

            BattleManager.Instance.HideAllPanel();

            BattleManager.Instance.InitializeFields();
            BattleManager.Instance.InitializeCharacter();

            SpawnField();
            SpawnCharacter();
        }
        else
        {
            throw new System.Exception("LevelManager.Instance.Data == null");
        }
    }

    /// <summary>
    /// Spawn fields.
    /// </summary>
    private void SpawnField()
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
    /// Spawn Character.
    /// </summary>
    private void SpawnCharacter()
    {
        for (int i = 0; i < _data.CharacterAmount; i++)
        {
            var prefab = CharacterPrefab[Random.Range(0, CharacterPrefab.Length)];
            BattleManager.Instance.SetCharacter(prefab, i);
            
            Instantiate(prefab, GetPosition(), Quaternion.identity);
        }
    }

    /// <summary>
    /// Get random position on field.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetPosition()
    {
        int rowLength = BattleManager.Instance.Fields.GetLength(0);
        int row = Random.Range(0, rowLength);
        int col = Random.Range(0, CharacterSpawnAreaMaxColumn);
        var field = BattleManager.Instance.Fields[row, col];
        var pos = field.transform.position;
       
        return pos;
    }
}
