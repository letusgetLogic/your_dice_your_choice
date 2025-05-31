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
    /// Deactivates the roll buttons.
    /// </summary>
    private void DeactivateRollButtons()
    {
        foreach (var button in AllRollButtons)
        {
            button.interactable = false;
        }
    }

    /// <summary>
    /// Activates the button.
    /// </summary>
    /// <param name="button"></param>
    public void ButtonOn(Button button)
    {
        button.interactable = true;
    }
}

