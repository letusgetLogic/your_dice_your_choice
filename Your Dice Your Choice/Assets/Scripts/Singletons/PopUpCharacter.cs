using System;
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
        transform.SetParent(_canvasRectTransform, true);

        _name.text = "";
        _name.color = Color.white;
        _maxHp.text = "";
        _currentHp.text = "";

        _buffAp.gameObject.SetActive(false);
        _ap.text = "";
        _buffAp.text = "";

        _buffDp.gameObject.SetActive(false);
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
        _ap.color = Color.white;

        _name.text = character.Name;
        _name.color = character.GetComponent<CharacterColor>().PlayerColor;

        _maxHp.text = character.Data.HP.ToString("0");
        _currentHp.text = character.GetComponent<CharacterHealth>().CurrentHP.ToString("0");

        UpdateAP(character);
        UpdateDP(character);
    }

    /// <summary>
    /// Updates attack points.
    /// </summary>
    /// <param name="character"></param>
    private void UpdateAP(Character character)
    {
        var characterAttack = character.GetComponent<CharacterAttack>();

        if (characterAttack.BuffAP > 0)
        {
            _ap.color = Color.green;
            _buffAp.gameObject.SetActive(true);
            _buffAp.text = $"(+{characterAttack.BuffAP.ToString("0")})";
        }
        else
            _buffAp.gameObject.SetActive(false);

        _ap.text = characterAttack.CurrentAP.ToString("0");
    }

    /// <summary>
    /// Updates defense points.
    /// </summary>
    /// <param name="character"></param>
    private void UpdateDP(Character character)
    {
        var characterDefense = character.GetComponent<CharacterDefense>();

        if (characterDefense.BuffDP > 0)
        {
            _dp.color = Color.green;
            _buffDp.gameObject.SetActive(true);
            _buffDp.text = $"(+{characterDefense.BuffDP.ToString("0")})";
        }
        else
            _buffDp.gameObject.SetActive(false);

        _dp.text = characterDefense.CurrentDP.ToString("0");
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
