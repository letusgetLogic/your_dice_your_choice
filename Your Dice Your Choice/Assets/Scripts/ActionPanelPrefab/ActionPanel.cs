using Assets.Scripts.ActionDatas;
using TMPro;
using UnityEngine;
using System;
using Assets.Scripts.ActionPopupPrefab.DiceSlotPrefab;

namespace Assets.Scripts.ActionPopupPrefab
{
    public class ActionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _behindLayer;
        [SerializeField] private TextMeshProUGUI _actionName;
        [SerializeField] private GameObject _actionPopupPrefab;

        public ActionBase Action { get; private set; }
        public GameObject CharacterObject { get; private set; }
        public CharacterPanel CharacterPanel { get; private set; }
        public int Index {  get; private set; }
        public ActionPopup ActionPopup
           => _actionPopupPrefab.GetComponent<ActionPopup>();

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
        /// Deactivates ActionPanelMouseEvent.
        /// </summary>
        public void DeactivateMouseEvent()
        {
            GetComponent<ActionPanelMouseEvent>().enabled = false;
        }
    }
}
