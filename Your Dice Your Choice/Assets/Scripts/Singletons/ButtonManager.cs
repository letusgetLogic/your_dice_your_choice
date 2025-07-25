using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance { get; private set; }

    [SerializeField] 
    private Button _endTurnButton;
    public Button EndTurnButton => _endTurnButton;

    [SerializeField]
    private Button _newMatchButton;
    public Button NewMatchButton => _newMatchButton;

    [SerializeField]
    private Button _menuButton;
    public Button MenuButton => _menuButton;


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
        SetGameObjectActive(EndTurnButton, false);
        SetGameObjectActive(NewMatchButton, false);
    }

    /// <summary>
    /// OnEndTurnButton method is called when the end turn button is clicked.
    /// </summary>
    public void OnEndTurnButton()
    {
        FieldManager.Instance.DeactivateFields();
        CharacterManager.Instance.DeactivateCharacters();
        TurnManager.Instance.SwitchTurn();
    }

    /// <summary>
    /// OnNewMatchButton method is called when the new match button is clicked.
    /// </summary>
    public void OnNewMatchButton()
    {
        GameManager.Instance.LoadScene("BattleArenaScene");
    }

    /// <summary>
    /// OnMenuButton method is called when the menu button is clicked.
    /// </summary>
    public void OnMenuButton()
    {
        GameManager.Instance.LoadScene("MainMenuScene");
    }

    /// <summary>
    /// Sets the button active true/false.
    /// </summary>
    /// <param name="button"></param>
    public void SetGameObjectActive(Button button, bool value)
    {
        button.gameObject.SetActive(value);
    }

    /// <summary>
    /// Sets the button interactable true/false.
    /// </summary>
    /// <param name="button"></param>
    public void SetButtonInteractible(Button button, bool value)
    {
        button.interactable = value;
    }

}

