using UnityEngine;
using Assets.Scripts.CharacterPrefab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.ActionPopupPrefab;
using Assets.Scripts.FieldPrefab;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public TextMeshProUGUI HeaderText;

    private ActionPanel _currentActionPanel;
    private GameObject _currentCharacterObject;
    public List<GameObject> InteractibleCharacters { get; private set; }

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
        ButtonManager.Instance.EndTurnButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Sets the data.
    /// </summary>
    /// <param name="actionPanel"></param>
    /// <param name="characterObject"></param>
    public void SetData(ActionPanel actionPanel, GameObject characterObject)
    {
        _currentActionPanel = actionPanel;
        _currentCharacterObject = characterObject;
    }

    /// <summary>
    /// Handles the input of player on the clicked field or enemy character.
    /// </summary>
    /// <param name="clickedObject"></param>
    public void HandleInput(GameObject clickedObject)
    {
        var action = _currentActionPanel.Action;

        action.HandleInput(clickedObject);
    }

}

