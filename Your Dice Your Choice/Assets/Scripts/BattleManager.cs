using UnityEngine;
using Assets.Scripts.CharacterPrefab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.ActionPanelPrefab;


public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public TextMeshProUGUI HeaderText;

    private ActionPanel _actionPanel;
    private GameObject _character;

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

    public void StartMatch()
    {
        ButtonManager.Instance.ButtonOn(ButtonManager.Instance.RollButtonLeft);
    }

    /// <summary>
    /// Initializes _actionPanel
    /// </summary>
    public void SetData(ActionPanel actionPanel, GameObject character)
    {
        _actionPanel = actionPanel;
        _character = character;
    }

    

}

