using System;
using System.Collections.Generic;
using Assets.Scripts.Action;
using Assets.Scripts.FieldPrefab;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager Instance { get; private set; }

    public GameObject[,] Fields { get; private set; }
    public List<FieldMouseEvent> DisplayedFields { get; private set; }

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
    /// Initializes the size of the array Fields.
    /// </summary>
    public void InitializeFields()
    {
        int mapHeight = LevelManager.Instance.Data.MapHeight;
        int mapLength = LevelManager.Instance.Data.MapLength;

        Fields = new GameObject[mapHeight, mapLength];
    }

    /// <summary>
    /// Sets the component FieldMouseEvent enabled true/false.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="value"></param>
    private void SetEnabled(FieldMouseEvent component, bool value)
    {
        component.enabled = value;
    }

    /// <summary>
    /// Initializes the index of the array Fields and sets the index to the field.
    /// </summary>
    /// <param name="characterPrefab"></param>
    public void SetField(GameObject field, int j, int i)
    {
        Fields[j, i] = field;

        Vector2Int index = new Vector2Int(j, i);
        field.GetComponent<Field>().SetIndex(index);

        var mouseEvent = field.GetComponent<FieldMouseEvent>();
        SetEnabled(mouseEvent, false);
    }

    /// <summary>
    /// Shows the interactible fields.
    /// </summary>
    /// <param name="characterFieldIndexOrigin"></param>
    /// <param name="actionDirections"></param>
    /// <param name="directionRange"></param>
    public void ShowField(Vector2Int characterFieldIndexOrigin, Vector2Int[] actionDirections, int directionRange)
    {
        DisplayedFields = new();

        foreach (Vector2Int actionDirection in actionDirections)
        {
            var fieldIndex = characterFieldIndexOrigin;
            fieldIndex += actionDirection * directionRange;

            if (fieldIndex.x < 0 || fieldIndex.x >= LevelManager.Instance.Data.MapHeight)
                continue;
            if (fieldIndex.y < 0 || fieldIndex.y >= LevelManager.Instance.Data.MapLength)
                continue;
            if (IsAnyObstacleInWay(characterFieldIndexOrigin, actionDirection, directionRange))
                continue;

            var mouseEvent = Fields[fieldIndex.x, fieldIndex.y].GetComponent<FieldMouseEvent>();
            SetEnabled(mouseEvent, true);
            DisplayedFields.Add(mouseEvent);
        }
    }

    /// <summary>
    /// Is any obstacle in the way?
    /// </summary>
    /// <param name="characterFieldIndexOrigin"></param>
    /// <param name="actionDirection"></param>
    /// <param name="directionRange"></param>
    /// <returns></returns>
    private bool IsAnyObstacleInWay(Vector2Int characterFieldIndexOrigin, Vector2Int actionDirection, int directionRange)
    {
        for (int i = 1; i <= directionRange; i++)
        {
            var fieldIndex = characterFieldIndexOrigin;
            fieldIndex += actionDirection * i;

            var field = Fields[fieldIndex.x, fieldIndex.y].GetComponent<Field>();
            
            if (field.IsAnyObstacleOnField() == true)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Deactivates the other displayed fields.
    /// </summary>
    /// <param name="clickedField"></param>
    public void DeactivateFields()
    {
        foreach (var mouseEvent in DisplayedFields)
        {
            SetEnabled(mouseEvent, false);
        }

        DisplayedFields.Clear();
    }
}
