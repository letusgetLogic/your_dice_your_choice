using System.Collections.Generic;
using Assets.Scripts.CharacterDatas;
using Assets.Scripts.CharacterPrefab;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; private set; }

    [SerializeField] private GameObject _fieldPrefab;
    [SerializeField] private GameObject _groundTop;
    [SerializeField] private GameObject _groundBottom;
    [SerializeField] private GameObject _groundLeft;
    [SerializeField] private GameObject _groundRight;

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
        float startPointHorizontal = -halfLength;
        float startPointVertical = halfHeight;

        SpawnCoverGrounds(startPointHorizontal, startPointVertical);
        SpawnFields(levelData, startPointHorizontal, startPointVertical);
    }

    /// <summary>
    /// Spawns cover grounds.
    /// </summary>
    /// <param name="startPointHorizontal"></param>
    /// <param name="startPointVertical"></param>
    private void SpawnCoverGrounds(float startPointHorizontal, float startPointVertical)
    {
        Instantiate(
            _groundTop, new Vector3(0, startPointVertical + 1, 0), Quaternion.identity);
        Instantiate(
            _groundBottom, new Vector3(0, -startPointVertical - 1, 0), Quaternion.identity);
        Instantiate(
            _groundLeft, new Vector3(startPointHorizontal - 1, 0, 0), Quaternion.identity);
        Instantiate(
            _groundRight, new Vector3(-startPointHorizontal + 1, 0, 0), Quaternion.identity);
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
        Vector3 spawnPos = new Vector3(startPointHorizontal, startPointVertical, 0);

        for (int j = 0; j < levelData.MapHeight; j++)
        {
            for (int i = 0; i < levelData.MapLength; i++)
            {
                var field = Instantiate(_fieldPrefab, spawnPos, Quaternion.identity);

                FieldManager.Instance.SetField(field, j, i);

                spawnPos.x += 1;
            }

            spawnPos.y -= 1;
            spawnPos.x = startPointHorizontal;
        }
    }

}
