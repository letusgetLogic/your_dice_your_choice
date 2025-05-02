using System.IO;
using UnityEngine;

public class DisplayPanel : MonoBehaviour
{
    public GameObject Avatar;
    public GameObject CharacterPrefab;

    /// <summary>
    /// Start method.
    /// </summary>
    void Start()
    {
        var objectInWorld = Instantiate(CharacterPrefab);
        var transform = objectInWorld.GetComponent<Transform>();
        transform.position = Vector3.zero;

        objectInWorld.transform.SetParent(Avatar.transform);
        var rectTransform = objectInWorld.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector3.zero;

    }
}
