using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Assets.Scripts.Action;
using Assets.Scripts.DicePrefab;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class ActionPanelMouseEvent : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// Mouse button is released
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.tag == "Dice")
            {
                var dice = eventData.pointerDrag;

                ManageAction(dice);
            }
        }

        /// <summary>
        /// Mouse enters the collider of a game object. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            ActionDescriptionPanel.Instance.SetPosition(eventData.pointerEnter);
            ActionDescriptionPanel.Instance.SetText(eventData.pointerEnter);
            ActionDescriptionPanel.Instance.gameObject.SetActive(true);
        }

        /// <summary>
        /// Mouse exits the collider of a game object.
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            ActionDescriptionPanel.Instance.gameObject.SetActive(false);
        }


        /// <summary>
        /// Manages action.
        /// </summary>
        /// <param name="dice"></param>
        private void ManageAction(GameObject dice)
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
    }
}

