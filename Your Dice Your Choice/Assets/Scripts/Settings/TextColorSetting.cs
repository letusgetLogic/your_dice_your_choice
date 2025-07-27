using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextColorSetting : MonoBehaviour
{
    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (TextColorManager.Instance == null)
        {
            StartCoroutine(WaitForTextColorManager());
            return;
        }
        else
            GetComponent<TextMeshProUGUI>().color = TextColorManager.Instance.DefaultTextColor;
        Debug.Log($"{gameObject.name} is set color");
    }

    /// <summary>
    /// Waits for the TextColorManager to be initialized before setting the text color.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForTextColorManager()
    {
        yield return new WaitUntil(() => TextColorManager.Instance != null);

        GetComponent<TextMeshProUGUI>().color = TextColorManager.Instance.DefaultTextColor;
    }
}
