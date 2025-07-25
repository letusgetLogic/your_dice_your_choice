using TMPro;
using UnityEngine;
using System;

public class ActionPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _actionName;
    [SerializeField] private TextMeshProUGUI _hitEndurance;
    [SerializeField] private TextMeshProUGUI _roundEndurance;
    [SerializeField] private DiceSlotAction _diceSlotAction;

    public ActionData ActionData { get; private set; }
    public ActionBase Action { get; private set; }
    public GameObject CharacterObject { get; private set; }
    public CharacterPanel CharacterPanel { get; private set; }
    public Component ActionAtribute { get; private set; }
    public DiceSlotAction DiceSlotAction => _diceSlotAction;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        _hitEndurance.gameObject.SetActive(false);
        _roundEndurance.gameObject.SetActive(false);
    }

    /// <summary>
    /// Initializes data.
    /// </summary>
    /// <param name="actionData"></param>
    public void SetData(ActionData actionData, GameObject characterObject,
                        CharacterPanel characterPanel, int index)
    {
        ActionData = actionData;
        Action = GetActionBase.Create(this, characterObject);
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

    /// <summary>
    /// Updates the endurance text based on the count and keyWord.
    /// </summary>
    /// <param name="count"></param>
    /// <param name="keyWord"></param>
    /// <exception cref="ArgumentException"></exception>
    public void UpdateEndurance(int hitEndurance, int roundEndurance)
    {
        bool isActive = hitEndurance > 0;
        _hitEndurance.gameObject.SetActive(isActive);
        _hitEndurance.text = hitEndurance.ToString();

        isActive = roundEndurance > 0;
        _roundEndurance.gameObject.SetActive(isActive);
        _roundEndurance.text = roundEndurance.ToString();
    }
}
