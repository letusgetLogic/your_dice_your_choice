using Assets.Scripts.Action;
using TMPro;
using UnityEngine;
using System;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class ActionPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _actionName;

        public ActionBase Action {  get; private set; }

        /// <summary>
        /// Initializes data.
        /// </summary>
        /// <param name="actionData"></param>
        public void SetData(ActionData actionData, GameObject character)
        {
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
            switch(actionData.ActionType) 
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
    }
}
