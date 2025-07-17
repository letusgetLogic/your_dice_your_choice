using System;
using Assets.Scripts.DicePrefab;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class DiceSlotAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {
        [SerializeField][Range(0f, 1f)] private float _delayShowingInteractible = .5f;

        private ActionPanel _actionPanel => transform.parent.parent.GetComponent<ActionPanel>();
        private PlayerType _playerType => _actionPanel.CharacterObject.GetComponent<Character>().PlayerType;

        private bool _canDiceBeingDropped { get; set; } = false;

        /// <summary>
        /// Mouse enters UI Element. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_playerType != TurnManager.Instance.Turn)
                return;

            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Dice"))
            {
                // If the player don't use the previous action and the interactable are still showed,
                // deactivate the interactable of the previous action panel.
                if (BattleManager.Instance.Coroutine != null)
                {
                    BattleManager.Instance.DeactivateInteractible(_actionPanel);
                }

                BattleManager.Instance.SetCoroutine(ShowInteractible(eventData.pointerDrag));
                
                StartCoroutine(BattleManager.Instance.Coroutine);
            }
        }

        /// <summary>
        /// Shows the interactible objects.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShowInteractible(GameObject diceBeingDragged)
        {
            yield return new WaitForSeconds(_delayShowingInteractible);

            var dice = diceBeingDragged.GetComponent<DiceMovement>();
            if (_actionPanel.Action.IsValid(dice.CurrentNumber) == false)
                yield break;

            BattleManager.Instance.SetAction(_actionPanel.Action);
            BattleManager.Instance.ShowInteractible(dice.CurrentNumber, _actionPanel);

            //_canInteractibleBeingDeactivated = true;

            if (FieldManager.Instance.InteractibleFields == null && 
                CharacterManager.Instance.InteractibleCharacters == null) 
                yield break;

            // It doesn't have any interactactible objects.
            if (FieldManager.Instance.InteractibleFields.Count == 0 &&
                CharacterManager.Instance.InteractibleCharacters.Count == 0)
            {
                //BattleManager.Instance.DeactivateInteractible(_actionPanel);
                yield break;
            }

            _canDiceBeingDropped = true;
        }

        /// <summary>
        /// Mouse exits UI Element. 
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_playerType != TurnManager.Instance.Turn)
                return;

            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Dice"))
            {
                // Ensure that the coroutine is not null before stopping it.
                if (BattleManager.Instance.Coroutine != null)
                {
                    StopCoroutine(BattleManager.Instance.Coroutine);
                }
                BattleManager.Instance.DeactivateInteractible(_actionPanel);

                //BattleManager.Instance.SetCoroutine(DelayDeactivate());
                //StartCoroutine(BattleManager.Instance.Coroutine);
            }
        }

        ///// <summary>
        ///// Deactivates the interactible objects.
        ///// </summary>
        ///// <returns></returns>
        //private IEnumerator DelayDeactivate()
        //{
        //    yield return new WaitForSeconds(_delayDeactivateTime);

        //    if (_canInteractibleBeingDeactivated)
        //    {
        //        Debug.Log("DiceSlotAction.OnPointerExit !!!");
        //        BattleManager.Instance.DeactivateInteractible(_actionPanel);
        //    }

        //    BattleManager.Instance.SetCoroutine(null);
        //}

        /// <summary>
        /// UI Element is being dropped.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("DiceSlotAction.OnDrop");
            //_canInteractibleBeingDeactivated = false;

            if (_playerType != TurnManager.Instance.Turn)
                return;

            var diceObject = eventData.pointerDrag;

            if (_canDiceBeingDropped)
            {
                var dice = diceObject.GetComponent<DiceMovement>();
                dice.SetOnActionSlot(GetComponent<RectTransform>().position);
                
                _canDiceBeingDropped = false;
            }
        }

    }
}
