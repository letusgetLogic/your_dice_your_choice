using System;
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

        Data = _dataPrefab[_dataIndex];
    }

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
