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
        private ActionPanel _actionPanel => _actionPanelTransform.gameObject.GetComponent<ActionPanel>();

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

                SetDiceOnSlot(dice);
                
                FieldManager.Instance.ShowField(
                    _actionPanel.CharacterPanel.Character.FieldIndex, 
                    _actionPanel.ActionDirections,
                    GetIntFromAllowedTile.Get(_actionPanel.ActionData.AllowedTile, dice.GetComponent<Dice>().CurrentNumber));
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

        /// <summary>
        /// Sets the dice on the slot, deactivates the drag event and sets the canvas group default.
        /// </summary>
        /// <param name="dice"></param>
        private void SetDiceOnSlot(GameObject dice)
        {
            var diceMovement = dice.GetComponent<DiceMovement>();
            diceMovement.PositionsTo(GetComponent<RectTransform>().position);
            
            var diceManager = dice.GetComponent<DiceManager>();

            diceManager.SetDragEventEnable(false);
            diceManager.SetAlphaDefault();
            diceManager.SetBlocksRaycasts(true);
        }
    }
}
