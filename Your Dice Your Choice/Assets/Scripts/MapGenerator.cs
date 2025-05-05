using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int MapLength;
    public int MapHeight;
    public GameObject FieldPrefab;

    private readonly float HalfSizeOfOneField = 0.5f;


    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        FieldManager.Instance.Fields = new GameObject[MapHeight, MapLength];

        Transform spawnTransform = GetComponent<Transform>();
        Vector3 spawnPos = spawnTransform.position;

        float halfLength = MapLength * .5f;
        float halfHeight = MapHeight * .5f;
        float startPointVertical = -halfLength + HalfSizeOfOneField;
        float startPointHorizontal = halfHeight - HalfSizeOfOneField;

        spawnPos = new Vector3(startPointVertical, startPointHorizontal, 0);

        for (int j = 0; j < MapHeight; j++)
        {
            for (int i = 0; i < MapLength; i++)
            {
                var field = Instantiate(FieldPrefab, spawnPos, Quaternion.identity);

                FieldManager.Instance.Fields[j, i] = field;

                spawnPos.x += 1;
            }

            spawnPos.y -= 1;
            spawnPos.x = startPointVertical;
        }
    }

}
