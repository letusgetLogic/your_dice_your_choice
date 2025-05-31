using Assets.Scripts;
using Assets.Scripts.FieldPrefab;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager Instance { get; private set; }

    public GameObject[,] Fields { get; private set; }

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
    /// Initializes the size of the array Fields.
    /// </summary>
    public void InitializeFields()
    {
        int mapHeight = LevelManager.Instance.Data.MapHeight;
        int mapLength = LevelManager.Instance.Data.MapLength;

        Fields = new GameObject[mapHeight, mapLength];
    }

    /// <summary>
    /// Initializes the index of the array Fields and sets the index to the field.
    /// </summary>
    /// <param name="characterPrefab"></param>
    public void SetField(GameObject field, int j, int i)
    {
        Fields[j, i] = field;

        Vector2Int index = new Vector2Int(j, i);
        field.GetComponent<Field>().SetIndex(index);
        field.GetComponent<FieldMouseEvent>().HideComponents();
        field.GetComponent<FieldMouseEvent>().enabled = false;
    }

    public void ShowField(Vector2Int characterFieldIndex, Vector2Int[] actionDirections, int directionRange)
    {
        foreach (Vector2Int actionDirection in actionDirections)
        {
            characterFieldIndex += actionDirection * directionRange;
            
            if (characterFieldIndex.x < 0 || characterFieldIndex.x >= LevelManager.Instance.Data.MapHeight)
                return;
            if (characterFieldIndex.y < 0 || characterFieldIndex.y >= LevelManager.Instance.Data.MapLength)
                return;

            Fields[characterFieldIndex.y, characterFieldIndex.x].GetComponent<FieldMouseEvent>().enabled = true; 
        }
    }
}
