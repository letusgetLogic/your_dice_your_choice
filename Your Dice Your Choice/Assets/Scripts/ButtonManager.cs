using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance { get; private set; }

    public Button EndTurnButton;
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
        EndTurnButton.gameObject.SetActive(false);
        DeactivateRollButtons();
    }

    /// <summary>
    /// Calls when end button is clicked.
    /// </summary>
    public void OnEndButton()
    {
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
    /// gameObject.SetActive true/false.
    /// </summary>
    /// <param name="button"></param>
    public void SetActive(Button button, bool value)
    {
        button.gameObject.SetActive(value);
    }
    
    /// <summary>
    /// button.interactable = true/false.
    /// </summary>
    /// <param name="button"></param>
    public void SetInteractible(Button button, bool value)
    {
        button.interactable = value;
    }

}

