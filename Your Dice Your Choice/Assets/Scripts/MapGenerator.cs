using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int MapLength;
    public int MapHeight;
    public GameObject PointPrefab;

    private readonly float HalfSize = 0.5f;
    private readonly float HalfSizeOfOnePoint = 0.5f;


    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        Transform spawnTransform = GetComponent<Transform>();
        Vector3 spawnPos = spawnTransform.position;

        float startPointVertical = -MapLength * HalfSize + HalfSizeOfOnePoint;
        float startPointHorizontal = MapHeight * HalfSize - HalfSizeOfOnePoint;

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
