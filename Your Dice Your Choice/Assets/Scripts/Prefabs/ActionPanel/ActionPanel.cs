using Assets.Scripts.ActionDatas;
using TMPro;
using UnityEngine;
using System;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class ActionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _behindLayer;
        [SerializeField] private TextMeshProUGUI _actionName;
        [SerializeField] private ActionPopup _actionPopup;
        [SerializeField] private DiceSlotAction _diceSlotAction;

        public ActionBase Action { get; private set; }
        public GameObject CharacterObject { get; private set; }
        public CharacterPanel CharacterPanel { get; private set; }
        public int Index {  get; private set; }
        public ActionPopup ActionPopup => _actionPopup;
        public DiceSlotAction DiceSlotAction => _diceSlotAction;

        /// <summary>
        /// Initializes data.
        /// </summary>
        /// <param name="actionData"></param>
        public void SetData(ActionDatas.ActionData actionData, GameObject characterObject, 
                            CharacterPanel characterPanel, int index)
        {
            Action = GetActionBase.Create(actionData, characterObject);
            CharacterObject = characterObject;
            CharacterPanel = characterPanel;
            Index = index;
            _actionName.text = actionData.ActionType.ToString();
        }

        /// <summary>
        /// Hides/shows the components because of text overlaying in UI.
        /// </summary>
        public void ShowComponents(bool value)
        {
            _behindLayer.SetActive(value);
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
}
