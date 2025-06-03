using Assets.Scripts.Action;
using Assets.Scripts.DicePrefab;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ActionPanelPrefab.DiceSlotPrefab
{
    public class DiceSlotAction : MonoBehaviour, IDropHandler
    {
        private Transform actionPanelTransform => transform.parent.parent;

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
                var diceMovement = dice.GetComponent<DiceMovement>();
                var diceNumber = dice.GetComponent<Dice>().CurrentNumber;
                var actionPanel = actionPanelTransform.GetComponent<ActionPanel>();
                var allowedDiceNumber = actionPanel.ActionData.AllowedDiceNumber;

                if (CheckDiceCondition.IsNumberAllowed(allowedDiceNumber, diceNumber) == false)
                {
                    return;
                }

                diceMovement.PositionsTo(GetComponent<RectTransform>().position);

                var character = actionPanel.CharacterPanel.Character;
                var characterFieldIndex = character.FieldIndex;
                var actionDirections = actionPanel.ActionDirections;

                FieldManager.Instance.ShowField(characterFieldIndex, actionDirections, diceNumber);
            }
        }
    }
}
