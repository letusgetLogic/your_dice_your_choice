using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject FieldPrefab;

    private readonly float HalfSizeOfOneField = 0.5f;

    private LevelData _data;


    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        if (LevelManager.Instance.Data != null)
        { 
            _data = LevelManager.Instance.Data;
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
        BattleFieldManager.Instance.Fields = new GameObject[_data.MapHeight, _data.MapLength];

        Transform spawnTransform = GetComponent<Transform>();
        Vector3 spawnPos = spawnTransform.position;

        float halfLength = _data.MapLength * .5f;
        float halfHeight = _data.MapHeight * .5f;
        float startPointVertical = -halfLength + HalfSizeOfOneField;
        float startPointHorizontal = halfHeight - HalfSizeOfOneField;

        spawnPos = new Vector3(startPointVertical, startPointHorizontal, 0);

        for (int j = 0; j < _data.MapHeight; j++)
        {
            for (int i = 0; i < _data.MapLength; i++)
            {
                var field = Instantiate(FieldPrefab, spawnPos, Quaternion.identity);

                BattleFieldManager.Instance.Fields[j, i] = field;

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

    }
}
