using TMPro;
using UnityEngine;

public class TextColorSetting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _settingText;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().color = _settingText.color;
    }
}
