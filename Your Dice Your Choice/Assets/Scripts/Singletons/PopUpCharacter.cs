using Assets.Scripts;
using Assets.Scripts.CharacterPrefab;
using TMPro;
using UnityEngine;

public class PopUpCharacter : MonoBehaviour
{
    public static PopUpCharacter Instance { get; private set; }

    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private TextMeshProUGUI _name;

    [SerializeField] private TextMeshProUGUI _maxHp;
    [SerializeField] private TextMeshProUGUI _currentHp;

    [SerializeField] private TextMeshProUGUI _ap;
    [SerializeField] private TextMeshProUGUI _buffAp;

    [SerializeField] private TextMeshProUGUI _dp;
    [SerializeField] private TextMeshProUGUI _buffDp;

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
    public void SetData(Character character)
    {
        _name.text = character.Name;
        _name.color = character.GetComponent<CharacterColor>().PlayerColor;

        _maxHp.text = character.Data.HP.ToString();
        _currentHp.text = character.GetComponent<CharacterHealth>().CurrentHP.ToString();

        _ap.text = character.CurrentAP.ToString();
        _buffAp.text = $"(+{character.BuffAP.ToString()})";
        _buffAp.gameObject.SetActive(character.BuffAP != 0);

        _dp.text = character.CurrentDP.ToString();
        _buffDp.text = $"(+{character.BuffDP.ToString()})";
        _buffDp.gameObject.SetActive(character.BuffDP != 0);
    }
   
    /// <summary>
    /// Sets the position of the info panel.
    /// </summary>
    public void SetPosition(GameObject characterObject)
    {
        gameObject.GetComponent<RectTransform>().localPosition =
            PopUpBehaviour.NewWorldToLocalPosition(
                _canvasRectTransform,
                characterObject.transform.position,
                _distance);
    }

}
