using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int MapLength;
    public int MapHeight;
    public GameObject PointPrefab;

    private float _halfSize = 0.5f;
    private float _halfSizeOfOnePoint = 0.5f;


    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        Transform spawnTransform = GetComponent<Transform>();
        Vector3 spawnPos = spawnTransform.position;

        float startPointVertical = -MapLength * _halfSize + _halfSizeOfOnePoint;
        float startPointHorizontal = MapHeight * _halfSize - _halfSizeOfOnePoint;

        spawnPos = new Vector3(startPointVertical, startPointHorizontal, 0);

        for (int j = MapHeight; j > 0; j--)
        {
            for (int i = MapLength; i > 0; i--)
            {
                Instantiate(PointPrefab, spawnPos, Quaternion.identity);

                spawnPos.x += 1;
            }

            spawnPos.y -= 1;
            spawnPos.x = startPointVertical;
        }
    }

}
