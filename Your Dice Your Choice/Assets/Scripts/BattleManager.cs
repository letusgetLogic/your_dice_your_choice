using UnityEngine;
using Assets.Scripts.CharacterPrefab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.ActionPanelPrefab;
using Assets.Scripts.FieldPrefab;


public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public TextMeshProUGUI HeaderText;

    private ActionPanel _currentActionPanel;
    private GameObject _currentCharacter;

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
    /// Starts the match.
    /// </summary>
    public void StartMatch()
    {
        ButtonManager.Instance.ButtonOn(ButtonManager.Instance.RollButtonLeft);
    }

    /// <summary>
    /// Sets the current components.
    /// </summary>
    public void SetData(ActionPanel actionPanel, GameObject character)
    {
        _currentActionPanel = actionPanel;
        _currentCharacter = character;
    }

    /// <summary>
    /// Handles the input of player on the clicked field.
    /// </summary>
    /// <param name="clickedField"></param>
    public void HandleInput(GameObject clickedField)
    {
        var action = _currentActionPanel.Action;

        action.HandleInput(clickedField);
    }

}

