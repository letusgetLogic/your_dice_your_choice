
using Assets.Scripts.Action;
using TMPro;
using UnityEngine;
using Assets.Scripts.DicePrefab;
using Unity.VisualScripting;
using System;


namespace Assets.Scripts.ActionPanelPrefab
{
    public class ActionPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _actionName;

        public IAction action {  get; private set; }

        /// <summary>
        /// Initializes data.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(ActionData data, GameObject character)
        {
            _actionName.text = data.ActionType.ToString();
            Create(data, character);
        }

        private void Create(ActionData data, GameObject character)
        {
            switch(data.ActionType) 
            {
                case ActionType.None:
                    throw new Exception("ActionType = None");
                case ActionType.Move:
                    action = new ActionMovement();
                    return;
                case ActionType.Attack:
                    Description = "Attack";
                    return;
                case ActionType.Defend:
                    Description = "Defend";
                    return;
            }
        }

        /// <summary>
        /// Manages action.
        /// </summary>
        /// <param name="dice"></param>
        public void ManageAction(GameObject dice)
        {
            var diceMovement = dice.GetComponent<DiceMovement>();
            var allowedDiceNumber = ActionData.AllowedDiceNumber;
            var diceNumber = dice.GetComponent<Dice>().CurrentNumber;


            if (CheckDiceCondition.IsNumberAllowed(allowedDiceNumber, diceNumber) == false)
            {
                diceMovement.SendBackToBase();
                return;
            }

            diceMovement.PositionsTo(gameObject.GetComponent<RectTransform>().anchoredPosition);
        }
    }
}
