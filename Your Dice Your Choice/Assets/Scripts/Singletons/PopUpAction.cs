using TMPro;
using UnityEngine;

public class PopUpAction : MonoBehaviour
{
    public static PopUpAction Instance { get; private set; }

    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Vector2 _distance;

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
    }

    /// <summary>
    /// Sets the values default.
    /// </summary>
    private void OnEnable()
    {
        transform.SetParent(_canvasRectTransform, true);
        _description.text = "";
    }

    /// <summary>
    /// Transfers the description text to the popup.
    /// </summary>
    /// <param name="description"></param>
    /// <param name="maxHp"></param>
    /// <param name="currentHp"></param>
    /// <param name="ap"></param>
    /// <param name="dp"></param>
    public void SetData(string description)
    {
        _description.text = description;
    }

    /// <summary>
    /// Sets the position of the info panel.
    /// </summary>
    public void SetPosition(GameObject targetObject)
    {
        gameObject.GetComponent<RectTransform>().localPosition =
            PopUpBehaviour.NewWorldToLocalPosition(
                _canvasRectTransform,
                targetObject.transform.position,
                _distance);

    }
}
