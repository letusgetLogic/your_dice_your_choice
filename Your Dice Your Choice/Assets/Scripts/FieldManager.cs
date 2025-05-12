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
    /// Initializes the array Fields.
    /// </summary>
    public void InitializeFields()
    {
        int mapHeight = LevelManager.Instance.Data.MapHeight;
        int mapLength = LevelManager.Instance.Data.MapLength;

        Fields = new GameObject[mapHeight, mapLength];
    }

    /// <summary>
    /// Initializes the array Character.
    /// </summary>
    /// <param name="characterPrefab"></param>
    public void SetField(GameObject field, int j, int i)
    {
        Fields[j, i] = field;
    }

}
