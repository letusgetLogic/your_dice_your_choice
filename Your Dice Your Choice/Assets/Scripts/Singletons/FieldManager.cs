using System;
using System.Collections.Generic;
using Assets.Scripts.ActionDatas;
using Assets.Scripts.FieldPrefab;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager Instance { get; private set; }

    public GameObject[,] Fields { get; private set; }
    public List<GameObject> InteractibleFields { get; private set; }

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
    /// Initializes the index of the array Fields and sets the index to the field.
    /// </summary>
    /// <param name="characterPrefab"></param>
    public void SetField(GameObject fieldObject, int j, int i)
    {
        Fields[j, i] = fieldObject;

        Vector2Int index = new Vector2Int(j, i);
        fieldObject.GetComponent<Field>().SetIndex(index);

        var field = fieldObject.GetComponent<Field>();
        field.SetEnabled(field.GetComponent<FieldMouseEvent>(), false);

    }

    /// <summary>
    /// Adds the interactible fields in the list InteractibleFields.
    /// </summary>
    /// <param name="characterFieldIndexOrigin"></param>
    /// <param name="actionDirections"></param>
    /// <param name="directionRange"></param>
    public void SetInteractibleFields(Vector2Int characterFieldIndexOrigin,
        Vector2Int[] actionDirections, int directionRange)
    {
        if (InteractibleFields != null)
        {
            DeactivateFields();
        }

        InteractibleFields = new();

        foreach (Vector2Int actionDirection in actionDirections)
        {
            var fieldIndex = characterFieldIndexOrigin;
            fieldIndex += actionDirection * directionRange;

            // Check if the field index is within bounds
            if (IsTargetOutOfRange(fieldIndex))
                continue;

            // Check if there is an obstacle in the way
            if (IsAnyObstacleInWay(
                characterFieldIndexOrigin, actionDirection, directionRange))
                continue;

            var fieldObject = Fields[fieldIndex.x, fieldIndex.y];

            InteractibleFields.Add(fieldObject);
        }
    }

    /// <summary>
    /// Shows the interactible fields.
    /// </summary>
    /// <param name="characterFieldIndexOrigin"></param>
    /// <param name="actionDirections"></param>
    /// <param name="directionRange"></param>
    public void ShowInteractibleFields()
    {
        foreach (var fieldObject in InteractibleFields)
        {
            var field = fieldObject.GetComponent<Field>();
            field.SetEnabled(field.GetComponent<FieldMouseEvent>(), true);
            Debug.Log($"Field {fieldObject.GetComponent<Field>().Index} FieldMouseEvent enabled true\n");
        }
    }

    /// <summary>
    /// Checks every field between character and target field.
    /// </summary>
    /// <param name="characterFieldIndexOrigin"></param>
    /// <param name="actionDirection"></param>
    /// <param name="directionRange"></param>
    /// <returns></returns>
    private bool IsAnyObstacleInWay(Vector2Int characterFieldIndexOrigin,
        Vector2Int actionDirection, int directionRange)
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
    /// Deactivates the interactable fields and sets the InteractableFields to null.
    /// </summary>
    /// <param name="clickedField"></param>
    public void DeactivateFields()
    {
        if (InteractibleFields == null)
            return;

        if (InteractibleFields.Count == 0)
        {
            InteractibleFields = null; Debug.Log("InteractibleFields set to null");
            return;
        }

        // Deactivates the interaction of each field in InteractibleFields
        foreach (var fieldObject in InteractibleFields)
        {
            var field = fieldObject.GetComponent<Field>();
            field.SetEnabled(field.GetComponent<FieldMouseEvent>(), false);
            Debug.Log($"Field {fieldObject.GetComponent<Field>().Index} FieldMouseEvent enabled false\n");
        }

        InteractibleFields = null;
        Debug.Log("InteractibleFields set to null");
    }

    /// <summary>
    /// Check if the field index is within bounds
    /// </summary>
    /// <param name="fieldIndex"></param>
    /// <returns></returns>
    public bool IsTargetOutOfRange(Vector2Int fieldIndex)
    {
        if (fieldIndex.x < 0 || fieldIndex.x >= LevelManager.Instance.Data.MapHeight ||
            fieldIndex.y < 0 || fieldIndex.y >= LevelManager.Instance.Data.MapLength)
            return true;

        return false;
    }
}
