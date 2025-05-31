using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Assets.Scripts.Action;
using Assets.Scripts.DicePrefab;
using Assets.Scripts.Actions;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class ActionPanelMouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// Mouse enters the collider of a game object. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            var actionPanel = eventData.pointerEnter;

            ActionDescriptionPanel.Instance.SetPosition(actionPanel);
            ActionDescriptionPanel.Instance.SetText(actionPanel);
            ActionDescriptionPanel.Instance.SetActiveChildren(true);
        }

        /// <summary>
        /// Mouse exits the collider of a game object.
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            ActionDescriptionPanel.Instance.SetActiveChildren(false);
        }
    }
}

