using Assets.Scripts.CharacterPrefab;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance {  get; private set; }

    public GameObject Canvas;
    public TextMeshProUGUI NameTextLeft;
    public TextMeshProUGUI NameTextRight;
    public GameObject[] CharacterPanelsLeft;
    public GameObject[] CharacterPanelsRight;
    public GameObject RollPanelLeft;
    public GameObject RerollPanelLeft; 
    public GameObject RollPanelRight;
    public GameObject RerollPanelRight;
    public GameObject CharacterInfoPanel;
    public GameObject ActionDescriptionPanel;

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
    /// Hides all character panels.
    /// </summary>
    public void HideAllPanel()
    {
        foreach (GameObject panel in CharacterPanelsLeft)
        {
            panel.gameObject.SetActive(false);
        }

        foreach (GameObject panel in CharacterPanelsRight)
        {
            panel.gameObject.SetActive(false);
        }

        RollPanelLeft.SetActive(false);
        RerollPanelLeft.SetActive(false);
        RollPanelRight.SetActive(false);
        RerollPanelRight.SetActive(false);

        // Set the inactive panel in the scene active to create the singleton instance.
        CharacterInfoPanel.SetActive(true); 
        CharacterInfoPanel.SetActive(false);
        ActionDescriptionPanel.SetActive(true);
        ActionDescriptionPanel.SetActive(false);
    }

    /// <summary>
    /// Shows the roll and reroll panels.
    /// </summary>
    public void ShowRollPanels()
    {
        RollPanelLeft.SetActive(true);
        DiceManager.Instance.ShowDiceOnPanel(RollPanelLeft);
        RerollPanelLeft.SetActive(true);

        RollPanelRight.SetActive(true);
        DiceManager.Instance.ShowDiceOnPanel(RollPanelRight);
        RerollPanelRight.SetActive(true);
    }
}
