using System.Collections;
using TMPro;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public static BattleUI Inst { get; private set; }

    [SerializeField] private TextMeshProUGUI _warningTextShader;
    [SerializeField] private TextMeshProUGUI _warningText;
    [SerializeField] private float _warningDuration = 0.2f;
    [SerializeField] private GameObject _index;


    private void Awake()
    {
        if (Inst != null)
        {
            Destroy(Inst.gameObject);
        }
        Inst = this;

#if UNITY_EDITOR
        _index.SetActive(true);
#endif
    }

    public void ShowWarning(string message)
    {
        _warningTextShader.gameObject.SetActive(true);
        _warningTextShader.text = message;
        _warningText.text = message;

        StartCoroutine(HideWarning());
    }


    private IEnumerator HideWarning()
    {
        yield return new WaitForSeconds(_warningDuration);

        _warningTextShader.gameObject.SetActive(false);
    }

}