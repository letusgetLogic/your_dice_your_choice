using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

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

                GetComponent<ActionPanel>().ManageAction(dice);
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
    }
}

