using TMPro;
using UnityEngine;
using System;

public class ActionPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _actionName;
    [SerializeField] private DiceSlotAction _diceSlotAction;

    public ActionData ActionData { get; private set; }
    public ActionBase Action { get; private set; }
    public GameObject CharacterObject { get; private set; }
    public CharacterPanel CharacterPanel { get; private set; }
    public DiceSlotAction DiceSlotAction => _diceSlotAction;

    /// <summary>
    /// Initializes data.
    /// </summary>
    /// <param name="actionData"></param>
    public void SetData(ActionData actionData, GameObject characterObject,
                        CharacterPanel characterPanel, int index)
    {
        ActionData = actionData;
        Action = GetActionBase.Create(actionData, characterObject);
        CharacterObject = characterObject;
        CharacterPanel = characterPanel;
        _actionName.text = actionData.ActionType.ToString();
    }

    /// <summary>
    /// Sets the component enabled true/false.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="value"></param>
    public void SetEnabled(Component component, bool value)
    {
        if (component is Behaviour behaviour)
        {
            behaviour.enabled = value;
        }
    }

}
