using System;
using UnityEngine;

public class BattleFieldManager : MonoBehaviour
{
    public static BattleFieldManager Instance;

    public GameObject[,] Fields;
    public GameObject[] CharacterPrefabs;


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

}
