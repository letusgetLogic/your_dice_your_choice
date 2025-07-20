using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance { get; private set; }

    public Button EndTurnButton;
    public Button NewMatchButton;
    public Button[] AllRollButtons;
    public Button RollButtonLeft;
    public Button RerollButtonLeft;
    public Button RollButtonRight;
    public Button RerollButtonRight;

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
        SetActive(EndTurnButton, false);
        DeactivateRollButtons();
    }

    /// <summary>
    /// Calls when end button is clicked.
    /// </summary>
    public void OnEndButton()
    {
        ButtonClickAnimation.Instance.ScaleSize(EndTurnButton);

        FieldManager.Instance.DeactivateFields();
        CharacterManager.Instance.DeactivateCharacters();
        TurnManager.Instance.SwitchTurn();
    }

    /// <summary>
    /// Deactivates the roll buttons.
    /// </summary>
    private void DeactivateRollButtons()
    {
        foreach (var button in AllRollButtons)
        {
            SetInteractible(button, false);
        }
    }

    /// <summary>
    /// Sets the button active true/false.
    /// </summary>
    /// <param name="button"></param>
    public void SetActive(Button button, bool value)
    {
        button.gameObject.SetActive(value);
    }

    /// <summary>
    /// Sets the button interactable true/false.
    /// </summary>
    /// <param name="button"></param>
    public void SetInteractible(Button button, bool value)
    {
        button.interactable = value;
    }

}

