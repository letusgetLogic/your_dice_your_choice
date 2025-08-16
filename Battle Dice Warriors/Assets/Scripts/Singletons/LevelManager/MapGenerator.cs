using System;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; private set; }

    [SerializeField] private GameObject _linePrefabHorizontal;
    [SerializeField] private GameObject _linePrefabVertical;
    [SerializeField] private GameObject _fieldPrefab;


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
    /// Generates map from the data.
    /// </summary>
    public void GenerateMapFrom(LevelData levelData)
    {
        // Length - 1 because the distance between pivot point of fields together is
        // 1 field length less than the length of entire fields.
        // For example 9 fields have 8 distance between their pivot points.
        float halfLength = (levelData.MapLength - 1) * 0.5f;
        float halfHeight = (levelData.MapHeight - 1) * 0.5f;
        float startPointHorizontal = -halfLength - 1;
        float startPointVertical = halfHeight + 1;

        SpawnFields(levelData, startPointHorizontal, startPointVertical);
    }

    /// <summary>
    /// Spawns fields.
    /// </summary>
    /// <param name="levelData"></param>
    /// <param name="startPointHorizontal"></param>
    /// <param name="startPointVertical"></param>
    private void SpawnFields(LevelData levelData, 
        float startPointHorizontal, float startPointVertical)
    {
        Vector3 spawnPos = new(startPointHorizontal, startPointVertical, 0);

        for (int j = -1; j < levelData.MapHeight; j++)
        {
            for (int i = -1; i < levelData.MapLength; i++)
            {
                if (j == -1) // draw horizontal lines for the border of battle ground
                {
                    if (i == -1)
                    {
                        // pos(-1,-1) draw nothing
                    }
                    else
                        Instantiate(_linePrefabHorizontal, spawnPos, Quaternion.identity);
                }
                else
                {
                    if (i == -1) // draw vertical lines for the border of battle ground
                    {
                        Instantiate(_linePrefabVertical, spawnPos, Quaternion.identity);
                    }
                    else
                    {
                        var field = Instantiate(_fieldPrefab, spawnPos, Quaternion.identity);
                        FieldManager.Instance.SetField(field, j, i);
                    }
                }

                spawnPos.x += 1;
            }

            spawnPos.y -= 1;
            spawnPos.x = startPointHorizontal;
        }
    }

}
