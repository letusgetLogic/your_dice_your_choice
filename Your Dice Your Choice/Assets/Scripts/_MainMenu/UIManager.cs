using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _playerLeftName;
    [SerializeField] private TextMeshProUGUI _playerRightName;
    [SerializeField] private TextMeshProUGUI _characterAmount;

    [SerializeField] private TMP_InputField _inputFieldLeft;
    [SerializeField] private TMP_InputField _inputFieldRight;
    [SerializeField] private int _charName;

    [SerializeField] private TMP_Dropdown _characterAmountDropDown;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _randomizeButton;


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
    /// Start method.
    /// </summary>
    private void Start()
    {
        _inputFieldLeft.characterLimit = _charName;
        _inputFieldRight.characterLimit = _charName;

        _inputFieldLeft.onValueChanged.AddListener(
            delegate 
            { 
                ValueChangeCheck(_inputFieldLeft); 
            });
        _inputFieldRight.onValueChanged.AddListener(
            delegate 
            { 
                ValueChangeCheck(_inputFieldRight); 
            });

        _playButton.onClick.AddListener(OnBattleButton);
        _quitButton.onClick.AddListener(OnQuitButtonClick);
        _randomizeButton.onClick.AddListener(OnRandomizeButtonClick);
    }

    /// <summary>
    /// OnRandomizeButtonClick method to randomize the character amount dropdown value.
    /// </summary>
    private void OnRandomizeButtonClick()
    {
        int rand = UnityEngine.Random.Range(0, _characterAmountDropDown.options.Count);
        _characterAmountDropDown.value = rand;
    }

    /// <summary>
    /// Checks the value of the input field and limits it to a specified character length.
    /// </summary>
    /// <param name="inputField"></param>
    private void ValueChangeCheck(TMP_InputField inputField)
    {
        string value = inputField.text;
        if (value.Length > _charName)
        {
            inputField.text = value.Substring(0, _charName);
        }
    }

    /// <summary>
    /// Battle button click handler.
    /// </summary>
    private void OnBattleButton()
    {
        if (string.IsNullOrEmpty(_inputFieldLeft.text))
        {
            _inputFieldLeft.text = GameManager.Instance.PlayerLeftName;
        }

        if (string.IsNullOrEmpty(_inputFieldRight.text))
        {
            _inputFieldRight.text = GameManager.Instance.PlayerRightName; 
        }

        GameManager.Instance.SetLevelData(
            _inputFieldLeft.text, _inputFieldRight.text, Int32.Parse(_characterAmount.text));

        GameManager.Instance.LoadScene("BattleArenaScene");
    }

    /// <summary>
    /// Closes the game.
    /// </summary>
    private void OnQuitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
    }
}
