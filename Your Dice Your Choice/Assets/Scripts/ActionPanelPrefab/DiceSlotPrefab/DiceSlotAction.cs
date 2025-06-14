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
            var diceObject = eventData.pointerDrag;

            if (diceObject.CompareTag("Dice"))
            {
                var action = _actionPanel.Action;
                var dice = diceObject.GetComponent<Dice>();

                if (action.IsValid(dice.CurrentNumber) == false) return;

                SetDiceOnSlot(diceObject);

                action.ShowInteractible(dice.CurrentNumber);

                BattleManager.Instance.SetData(_actionPanel, _actionPanel.CharacterObject);
            }
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
