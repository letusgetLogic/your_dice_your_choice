using System;
using Assets.Scripts.ActionDatas;
using Assets.Scripts.DicePrefab;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class DiceSlotAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {
        [SerializeField][Range(0f, 1f)] private float _delayOnHoverTime = .5f;

        private ActionPanel _actionPanel => transform.parent.parent.GetComponent<ActionPanel>();
        private PlayerType _playerType => _actionPanel.CharacterObject.GetComponent<Character>().PlayerType;

        private IEnumerator _coroutine;

        private bool _canInteractibleBeDeactivated = false;

        /// <summary>
        /// Mouse enters UI Element. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_playerType != TurnManager.Instance.Turn)
                return;

            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Dice"))
            {
                _coroutine = ShowInteractible(eventData.pointerDrag);
                StartCoroutine(_coroutine);
            }
        }

        /// <summary>
        /// Mouse exits UI Element. 
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            StartCoroutine(DelayDeactivate());
        }

        /// <summary>
        /// WaitForEndOfFrame and runs the method "DeactivateInteractable()".
        /// </summary>
        /// <returns></returns>
        private IEnumerator DelayDeactivate()
        {
            yield return new WaitForSeconds(1f);

            if (_canInteractibleBeDeactivated)
            {
                Debug.Log("DiceSlotAction.OnPointerExit !!!");
                _actionPanel.Action.DeactivateInteractible();

                var action = _actionPanel.Action;
                action.SetDescriptionOf(_actionPanel, 0);
            }
        }

        /// <summary>
        /// Shows the interactible objects.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShowInteractible(GameObject diceBeingDragged)
        {
            yield return new WaitForSeconds(_delayOnHoverTime);

            var action = _actionPanel.Action;
            var dice = diceBeingDragged.GetComponent<Dice>();

            if (action.IsValid(dice.CurrentNumber) == false)
                yield break;

            action.SetDescriptionOf(_actionPanel, dice.CurrentNumber);
            action.SetInteractible(dice.CurrentNumber);
            action.ShowInteractible();
            _canInteractibleBeDeactivated = true;
        }

        /// <summary>
        /// UI Element is being dropped.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {
            _canInteractibleBeDeactivated = false;

            if (_playerType != TurnManager.Instance.Turn)
                return;

            var diceObject = eventData.pointerDrag;

            if (diceObject.CompareTag("Dice"))
            {
                var action = _actionPanel.Action;
                var dice = diceObject.GetComponent<Dice>();

                if (action.IsValid(dice.CurrentNumber) == false)
                    return;

                action.SetInteractible(dice.CurrentNumber);

                if (FieldManager.Instance.InteractibleFields.Count == 0 &&
                    CharacterManager.Instance.InteractibleCharacters.Count == 0)
                    return;

                dice.SetOnActionSlot(GetComponent<RectTransform>().position);

                action.ShowInteractible();
                action.ActivateSkill(dice.CurrentNumber);

                BattleManager.Instance.SetData(_actionPanel);
            }
        }

    }
}
