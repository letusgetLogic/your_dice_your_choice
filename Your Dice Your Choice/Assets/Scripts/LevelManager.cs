using System;
using Assets.Scripts;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private LevelData[] _dataPrefab;
    [SerializeField] private int _dataIndex;

    public LevelData Data { get; private set; }

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
        DontDestroyOnLoad(Instance);

        Data = _dataPrefab[_dataIndex];
    }

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        if (Data != null)
        {
            PhaseInitialization();
        }
        else
        {
            throw new System.Exception("LevelManager.Instance.Data == null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Phase Initialization.
    /// </summary>
    private void PhaseInitialization()
    {
        LevelGenerator.Instance.SetData();

        BattleManager.Instance.HideAllPanel();

        BattleManager.Instance.InitializeFields();
        BattleManager.Instance.InitializeCharacter();

        LevelGenerator.Instance.SpawnField();
        LevelGenerator.Instance.SpawnCharacter();

        BattleManager.Instance.SetAction();
    }
}
