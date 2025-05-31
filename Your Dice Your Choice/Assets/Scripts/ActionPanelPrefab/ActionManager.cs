using UnityEngine;
using Assets.Scripts.Action;
using Assets.Scripts.DicePrefab;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class ActionManager : MonoBehaviour
    {
        /// <summary>
        /// Manages action.
        /// </summary>
        /// <param name="dice"></param>
        public void ManageAction(GameObject dice)
        {
            var diceMovement = dice.GetComponent<DiceMovement>();
            var actionPanel = GetComponent<ActionPanel>();
            var allowedDiceNumber = actionPanel.Action.ActionData.AllowedDiceNumber;
            var diceNumber = dice.GetComponent<Dice>().CurrentNumber;

            if (CheckDiceCondition.IsNumberAllowed(allowedDiceNumber, diceNumber) == false)
            {
                diceMovement.SendBackToBase();
                return;
            }

            diceMovement.PositionsTo(gameObject.GetComponent<RectTransform>().anchoredPosition);
           
        }

        public void Handle(ActionData actionData, GameObject character)
        {
            
        }
       
    }
}
