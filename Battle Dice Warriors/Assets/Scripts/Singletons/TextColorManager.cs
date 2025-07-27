using TMPro;
using UnityEngine;

public class TextColorManager : MonoBehaviour
    {
    public static TextColorManager Instance { get; private set; }

    public Color DefaultTextColor { get; private set; }

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

        DefaultTextColor = GetComponent<TextMeshProUGUI>().color;
    }

}

