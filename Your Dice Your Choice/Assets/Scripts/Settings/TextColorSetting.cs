using TMPro;
using UnityEngine;

public class TextColorSetting : MonoBehaviour
{
    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().color = TextColorManager.Instance.DefaultTextColor;
        Debug.Log($"{gameObject.name} is set color");
    }
}
