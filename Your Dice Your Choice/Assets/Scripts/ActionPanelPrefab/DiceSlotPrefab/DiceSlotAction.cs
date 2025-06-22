using System;
using Assets.Scripts.ActionDatas;
using Assets.Scripts.DicePrefab;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Unity.VisualScripting;

namespace Assets.Scripts.ActionPopupPrefab.DiceSlotPrefab
{
    public class DiceSlotAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {
        [SerializeField][Range(0f, 1f)] private float _delayOnHoverTime = .5f;

        private Transform _actionPanelTransform => transform.parent.parent;
        private ActionPanel _actionPanel => _actionPanelTransform.gameObject.GetComponent<ActionPanel>();

        private IEnumerator _coroutine;

        private bool _isDeactivatingInteractible = false;

        /// <summary>
        /// Mouse enters UI Element. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            _coroutine = ShowInteractible(eventData.pointerDrag);
            StartCoroutine(_coroutine);
        }

        /// <summary>
        /// Mouse exits UI Element. 
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            StopCoroutine(_coroutine);

            if (_isDeactivatingInteractible)
            {
                _actionPanel.Action.DeactivateInteractible();
            }
        }

        /// <summary>
        /// Shows the interactible objects.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShowInteractible(GameObject diceBeingDragged)
        {
            yield return new WaitForSeconds(_delayOnHoverTime);

            if (diceBeingDragged != null && diceBeingDragged.CompareTag("Dice"))
            {
                var action = _actionPanel.Action;
                var dice = diceBeingDragged.GetComponent<Dice>();

                if (action.IsValid(dice.CurrentNumber) == false) 
                    yield break;

                action.SetDescriptionOf(_actionPanel.ActionPopup, dice.CurrentNumber);
                action.SetInteractible(dice.CurrentNumber);
                action.ShowInteractible();

                _isDeactivatingInteractible = true;
            }
        }

        /// <summary>
        /// UI Element is being dropped.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {
            var diceObject = eventData.pointerDrag;

            if (diceObject.CompareTag("Dice"))
            {
                var action = _actionPanel.Action;
                var dice = diceObject.GetComponent<Dice>();

                if (action.IsValid(dice.CurrentNumber) == false) 
                    return;

                action.SetInteractible(dice.CurrentNumber);

                if (FieldManager.Instance.InteractibleFields.Count == 0)
                    return;

                _isDeactivatingInteractible = false;
                       
                SetDiceOnSlot(diceObject);

                action.ShowInteractible();

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
