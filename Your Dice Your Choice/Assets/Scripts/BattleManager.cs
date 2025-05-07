using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public GameObject[,] Fields { get; private set; }
    public GameObject[] Character { get; private set; }


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
    /// Start method.
    /// </summary>
    private void Start()
    {
        Character = new GameObject[LevelManager.Instance.Data.CharacterAmount];

        InitializeFields();
    }

    /// <summary>
    /// Initializes the array Character.
    /// </summary>
    /// <param name="characterPrefab"></param>
    public void SetCharacter(GameObject characterPrefab, int index)
    {
        Character[index] = characterPrefab;
    }

    /// <summary>
    /// Initializes the array Fields.
    /// </summary>
    private void InitializeFields()
    {
        int mapHeight = LevelManager.Instance.Data.MapHeight;
        int mapLength = LevelManager.Instance.Data.MapLength;

        Fields = new GameObject[mapHeight, mapLength];
    }
}

