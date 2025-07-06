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

        var fieldComponents = fieldObject.GetComponent<FieldComponents>();
        fieldComponents.SetEnabled(fieldComponents.MouseEvent, false);

    }

    /// <summary>
    /// Adds the interactible fields in the list InteractibleFields.
    /// </summary>
    /// <param name="characterFieldIndexOrigin"></param>
    /// <param name="actionDirections"></param>
    /// <param name="directionRange"></param>
    public void SetInteractibleFields(Vector2Int characterFieldIndexOrigin, Vector2Int[] actionDirections, int directionRange)
    {
        InteractibleFields = new();

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
        foreach(var fieldObject in InteractibleFields)
        {
            var fieldComponents = fieldObject.GetComponent<FieldComponents>();
            fieldComponents.SetEnabled(fieldComponents.MouseEvent, false);
        }
    }

    /// <summary>
    /// Checks every field between character and target field.
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

            if (fieldIndex.x < 0 || fieldIndex.x >= LevelManager.Instance.Data.MapHeight)
                continue;
            if (fieldIndex.y < 0 || fieldIndex.y >= LevelManager.Instance.Data.MapLength)
                continue;

            var field = Fields[fieldIndex.x, fieldIndex.y].GetComponent<Field>();
            
            if (field.IsAnyObstacleOnField() == true)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Deactivates the interactible fields.
    /// </summary>
    /// <param name="clickedField"></param>
    public void DeactivateFields()
    {
        foreach (var fieldObject in InteractibleFields)
        {
            var fieldComponents = fieldObject.GetComponent<FieldComponents>();
            fieldComponents.SetEnabled(fieldComponents.MouseEvent, false);
        }

        InteractibleFields.Clear();
    }


}
