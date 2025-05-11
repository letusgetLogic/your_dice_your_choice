using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance { get; private set; }

    public Button EndTurnButton;
    public Button[] AllRollButtons;
    public Button L_RollButton;
    public Button L_RerollButton;
    public Button R_RollButton;
    public Button R_RerollButton;


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
    /// Deactivate the roll buttons.
    /// </summary>
    private void DeactivateRollButtons()
    {
        foreach (var button in AllRollButtons)
        {
            button.interactable = false;
        }
    }
}

