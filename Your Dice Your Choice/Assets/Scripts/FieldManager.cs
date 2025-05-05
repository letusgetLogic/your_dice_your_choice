using System;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager Instance;

    public GameObject[,] Fields;


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

