using UnityEngine;
using TMPro;
using Assets.Scripts.CharacterPrefab;
using Assets.Scripts.ActionDatas;
using Assets.Scripts;
using UnityEngine.UI;
using Assets.Scripts.ActionPanelPrefab;

public class CharacterPanel : MonoBehaviour
{
    //public GameObject Avatar;
    public TextMeshProUGUI CharacterName;
    public GameObject[] ActionPanelPrefabs;

    public GameObject CharacterObject { get; private set; }
    public Character Character { get; private set; }
    public PlayerType PlayerType { get; private set; }
    public GameObject[] ActiveActionPanels { get; private set; }

    [SerializeField] private float _alphaValueInactive = 0.9f;

    /// <summary>
    /// References Object and Script Character.
    /// </summary>
    /// <param name="characterObject"></param>
    public void SetCharacter(GameObject characterObject, PlayerType player)
    {
        CharacterObject = characterObject;
        PlayerType = player;
        Character = CharacterObject.GetComponent<Character>();
        CharacterName.text = Character.Name;
    }

    /// <summary>
    /// References the action in UI.
    /// </summary>
    public void SetAction()
    {
        ActiveActionPanels = new GameObject[Character.Data.ActionData.Length];

        for (int i = 0; i < ActionPanelPrefabs.Length; i++)
        {
            // The amount of action of a character can vary.
            if (i >= Character.Data.ActionData.Length) 
            {
                ActionPanelPrefabs[i].SetActive(false);
                continue;
            }

            ActiveActionPanels[i] = ActionPanelPrefabs[i];

            var actionData = Character.Data.ActionData[i];
            ActionPanelPrefabs[i].GetComponent<ActionPanel>().SetData(actionData, CharacterObject, this, i);
        }
    }

    /// <summary>
    /// Sets the description panel for each action. 
    /// </summary>
    public void SetDescriptonPanelForAction() // Can't be in SetAction(), because the character panel's position has not yet been determined.
    {
        for (int i = 0; i < Character.Data.ActionData.Length; i++)
        {
            var actionData = Character.Data.ActionData[i];

            ActionPanelPrefabs[i].GetComponent<ActionPanel>().ActionPopup.SetPosition();
            ActionPanelPrefabs[i].GetComponent<ActionPanel>().ActionPopup.SetText(actionData.Description);
        }
    }

    /// <summary>
    /// Sets the action inactive.
    /// </summary>
    public void SetActionInactive()
    {
        foreach (var actionPanelObject in ActiveActionPanels)
        {
            var actionPanel = actionPanelObject.GetComponent<ActionPanel>();
            var diceSlotAction = actionPanel.DiceSlotAction;
            var actionPanelMouseEvent = actionPanel.GetComponent<ActionPanelMouseEvent>();

            actionPanel.SetEnabled(diceSlotAction, false);
            actionPanel.SetEnabled(actionPanelMouseEvent, false);

            var panelColor = GetComponent<Image>().color;
            panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, _alphaValueInactive);
        }
    }
}

