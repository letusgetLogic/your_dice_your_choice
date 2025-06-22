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
    public static BattleManager Instance;

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
        ButtonManager.Instance.ButtonOn(ButtonManager.Instance.RollButtonLeft);
        ButtonManager.Instance.ButtonOn(ButtonManager.Instance.RollButtonRight);
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
    /// Sets the interactible enmey characters.
    /// </summary>
    /// <param name="characterFieldIndexOrigin"></param>
    /// <param name="actionDirections"></param>
    /// <param name="directionRange"></param>
    public void SetInteractibleEnemyCharacters(Vector2Int characterFieldIndexOrigin, Vector2Int[] actionDirections, int directionRange)
    {
        InteractibleCharacters = new();

        foreach (Vector2Int actionDirection in actionDirections)
        {
            var fieldIndex = characterFieldIndexOrigin;
            fieldIndex += actionDirection * directionRange;

            if (fieldIndex.x < 0 || fieldIndex.x >= LevelManager.Instance.Data.MapHeight)
                continue;
            if (fieldIndex.y < 0 || fieldIndex.y >= LevelManager.Instance.Data.MapLength)
                continue;

            var enemyObject = EnemyCharacter(characterFieldIndexOrigin, actionDirection, directionRange);

            if (enemyObject == null)
            {
                continue;
            }

            InteractibleCharacters.Add(enemyObject);
        }
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


    /// <summary>
    /// Checks enemy between character and target field.
    /// </summary>
    /// <param name="characterFieldIndexOrigin"></param>
    /// <param name="actionDirection"></param>
    /// <param name="directionRange"></param>
    /// <returns></returns>
    private GameObject EnemyCharacter(Vector2Int characterFieldIndexOrigin, Vector2Int actionDirection, int directionRange)
    {
        var enemyObject = new GameObject();

        for (int i = 1; i <= directionRange; i++)
        {
            var fieldIndex = characterFieldIndexOrigin;
            fieldIndex += actionDirection * i;

            if (fieldIndex.x < 0 || fieldIndex.x >= LevelManager.Instance.Data.MapHeight)
                continue;
            if (fieldIndex.y < 0 || fieldIndex.y >= LevelManager.Instance.Data.MapLength)
                continue;

            var field = FieldManager.Instance.Fields[fieldIndex.x, fieldIndex.y].GetComponent<Field>();

            enemyObject = field.EnemyObject(TurnManager.Instance.Turn);

            if (enemyObject != null)
                return enemyObject;
        }

        return null;
    }

}

