using System;
using Assets.Scripts.Action;
using Assets.Scripts.DicePrefab;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ActionPanelPrefab.DiceSlotPrefab
{
    public class DiceSlotAction : MonoBehaviour, IDropHandler
    {
        private Transform _actionPanelTransform => transform.parent.parent;
        private ActionPanel _actionPanel => _actionPanelTransform.GetComponent<ActionPanel>();

        /// <summary>
        /// Mouse button is released.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop");
            var dice = eventData.pointerDrag;

            if (dice.CompareTag("Dice"))
            {
                if (IsValid(dice) == false) return; 

                var diceManager = dice.GetComponent<DiceManager>();
                diceManager.SetIsDiceOnSlot(true);
                Debug.Log("IsDiceOnSlot " + diceManager.IsDiceOnSlot);

                var diceMovement = dice.GetComponent<DiceMovement>();
                diceMovement.PositionsTo(GetComponent<RectTransform>().position);

                FieldManager.Instance.ShowField(
                    _actionPanel.CharacterPanel.Character.FieldIndex, 
                    _actionPanel.ActionDirections, 
                    dice.GetComponent<Dice>().CurrentNumber);
            }
        }

        /// <summary>
        /// Checks the dice condition, appended to the dice's valid number.
        /// </summary>
        /// <param name="dice"></param>
        /// <exception cref="NotImplementedException"></exception>
        private bool IsValid(GameObject dice)
        {
            var allowedDiceNumber = _actionPanel.ActionData.AllowedDiceNumber;
            var diceNumber = dice.GetComponent<Dice>().CurrentNumber;

            return CheckDiceCondition.IsNumberValid(allowedDiceNumber, diceNumber);
        }
    }
}
