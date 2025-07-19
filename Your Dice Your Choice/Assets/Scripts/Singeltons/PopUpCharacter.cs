using Assets.Scripts;
using TMPro;
using UnityEngine;

public class PopUpCharacter : MonoBehaviour
{
    public static PopUpCharacter Instance { get; private set; }

    [SerializeField] private RectTransform   _canvasRectTransform;
    [SerializeField] private TextMeshProUGUI _name;

    [SerializeField] private TextMeshProUGUI _maxHp;
    [SerializeField] private TextMeshProUGUI _currentHp;

    [SerializeField] private TextMeshProUGUI _ap;
    [SerializeField] private TextMeshProUGUI _buffAp;

    [SerializeField] private TextMeshProUGUI _dp;
    [SerializeField] private TextMeshProUGUI _buffDp;

    [SerializeField] private Vector2         _distance;

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
        _name.text = "";
        _name.color = Color.white;
        _maxHp.text = "";
        _currentHp.text = "";

        _buffAp.gameObject.SetActive(false);
        _ap.text = "";
        _buffAp.text = "";

        _buffAp.gameObject.SetActive(false);
        _dp.text = "";
        _buffDp.text = "";
    }

    /// <summary>
    /// Transfers the values to the info panel.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="maxHp"></param>
    /// <param name="currentHp"></param>
    /// <param name="ap"></param>
    /// <param name="dp"></param>
    public void SetData(string name, Color color, 
                        float maxHp, float currentHp, 
                        float ap, float buffAp, 
                        float dp, float buffDp)
    {
        _name.text = name;
        _name.color = color;

        _maxHp.text = maxHp.ToString();
        _currentHp.text = currentHp.ToString();

        _buffAp.gameObject.SetActive(buffAp != 0);
        _ap.text = ap.ToString();
        _buffAp.text = $"(+{buffAp})";

        _buffDp.gameObject.SetActive(buffDp != 0);
        _dp.text = dp.ToString();
        _buffDp.text = $"(+{buffDp})";
    }

    /// <summary>
    /// Sets the position of the info panel.
    /// </summary>
    public void SetPosition(GameObject characterObject)
    {
        gameObject.GetComponent<RectTransform>().localPosition = PopUpBehaviour.NewWorldToLocalPosition(
            _canvasRectTransform,
            characterObject.transform.position,
            _distance);
    }

}
