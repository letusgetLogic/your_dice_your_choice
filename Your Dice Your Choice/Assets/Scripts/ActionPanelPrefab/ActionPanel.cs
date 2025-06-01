using Assets.Scripts.Action;
using TMPro;
using UnityEngine;
using System;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class ActionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _behindLayer;
        [SerializeField] private TextMeshProUGUI _actionName;
        [SerializeField] private GameObject _actionDescriptionPanelPrefab;

        public ActionBase Action { get; private set; }
        public ActionData ActionData { get; private set; }
        public CharacterPanel CharacterPanel { get; private set; }
        public Vector2Int[] ActionDirections { get; private set; }
        public int Index {  get; private set; }

        public ActionDescriptionPanel ActionDescriptionPanel
           => _actionDescriptionPanelPrefab.GetComponent<ActionDescriptionPanel>();


        /// <summary>
        /// Initializes data.
        /// </summary>
        /// <param name="actionData"></param>
        public void SetData(ActionData actionData, GameObject character, CharacterPanel characterPanel, int index)
        {
            ActionData = actionData;
            CharacterPanel = characterPanel;
            Index = index;

            ActionDirections = GetVector2FromDirection.Get(ActionData.Direction);

            _actionName.text = actionData.ActionType.ToString();
            Action = Create(actionData, character);
        }

        /// <summary>
        /// Creates the instace of Action.
        /// </summary>
        /// <param name="actionData"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private ActionBase Create(ActionData actionData, GameObject character)
        {
            switch (actionData.ActionType)
            {
                case ActionType.None:
                    return null;
                case ActionType.Move:
                    return new Movement(actionData, character);
                case ActionType.Attack:
                    return new Attack(actionData, character);
                case ActionType.Defend:
                    return new Defend(actionData, character);
            }

            return null;
        }

        /// <summary>
        /// Hides/shows the components because of text overlaying in UI.
        /// </summary>
        public void ShowComponents(bool value)
        {
            _behindLayer.SetActive(value);
        }
    }
}
