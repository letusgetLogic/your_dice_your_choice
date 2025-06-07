using UnityEngine;
using Assets.Scripts.CharacterPrefab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public TextMeshProUGUI HeaderText;

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
    /// 
    /// </summary>
    public void StartMatch()
    {

    }

}

