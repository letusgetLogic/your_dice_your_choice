using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int MapLength;
    public int MapHeight;
    public GameObject PointPrefab;
    public float Ratio;
    public float HalfSizeOfOnePoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Obsolete]
    void Start()
    {
        Transform spawnTransform = GetComponent<Transform>();
        Vector3 spawnPos = spawnTransform.position;

        float startPointVertical = -MapLength * Ratio + HalfSizeOfOnePoint;
        float startPointHorizontal = MapHeight * Ratio - HalfSizeOfOnePoint;

        spawnPos = new Vector3(startPointVertical, startPointHorizontal, 0);

        //LineRenderer lineRenderer = PointPrefab.GetComponent<LineRenderer>();

        Vector2Int[] dir = new[]
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0)
        };


        for (int j = MapLength; j > 0; j--)
        {
            for (int i = MapHeight; i > 0; i--)
            {
                Instantiate(PointPrefab, spawnPos, Quaternion.identity); Debug.Log(spawnPos);

                //lineRenderer.SetColors(Color.black, Color.black);
                //lineRenderer.SetPosition(0, new Vector3(spawnPos.x, spawnPos.y, spawnPos.z));

                spawnPos.x += 1;

                //lineRenderer.SetPosition(1, new Vector3(spawnPos.x, spawnPos.y, spawnPos.z));
            }

            spawnPos.y -= 1;
            spawnPos.x = startPointVertical;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
