using System;
using Assets.Scripts.DicePrefab;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Assets.Scripts.CharacterPrefab;
using Assets.Scripts.LevelDatas;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class DiceSlotAction : MonoBehaviour, 
        IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {
        [SerializeField][Range(0f, 1f)] private float _delayShowingInteractible = .5f;

        private ActionPanel _actionPanel => transform.parent.GetComponent<ActionPanel>();
        private PlayerType _playerType => 
            _actionPanel.CharacterObject.GetComponent<Character>().PlayerType;

        private bool _canDiceBeingDropped { get; set; } = false;

        /// <summary>
        /// Mouse enters UI Element. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (LevelManager.Instance.CurrentPhase != Phase.Battle)
                return;

            if (_playerType != TurnManager.Instance.Turn)
                return;

            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Dice"))
            {
                // If the player don't use the previous action and
                // the interactable are still showed,
                // deactivate the interactable of the previous action panel.
                BattleManager.Instance.DeactivateInteractible();

                BattleManager.Instance.Coroutine = 
                    ShowInteractible(eventData.pointerDrag);
                
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

            BattleManager.Instance.Coroutine = null;

            var dice = diceBeingDragged.GetComponent<Dice>();
            if (_actionPanel.Action.IsValid(dice.CurrentNumber) == false)
                yield break;

            BattleManager.Instance.CurrentAction = _actionPanel.Action;
            BattleManager.Instance.ShowInteractible(dice.CurrentNumber, _actionPanel);

            // Both lists are null.
            if (FieldManager.Instance.InteractibleFields == null && 
                CharacterManager.Instance.InteractibleCharacters == null) 
                yield break;

            // It doesn't have any interactactible objects.
            if (FieldManager.Instance.InteractibleFields != null && 
                FieldManager.Instance.InteractibleFields.Count == 0)
                yield break;

            if (CharacterManager.Instance.InteractibleCharacters != null &&
                CharacterManager.Instance.InteractibleCharacters.Count == 0)
                yield break;

            _canDiceBeingDropped = true;
        }

        /// <summary>
        /// Mouse exits UI Element. 
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (LevelManager.Instance.CurrentPhase != Phase.Battle)
                return;

            if (_playerType != TurnManager.Instance.Turn)
                return;

            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Dice"))
            {
                BattleManager.Instance.DeactivateInteractible();
            }
        }

        /// <summary>
        /// UI Element is being dropped.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {
            if (!_canDiceBeingDropped)
            {
                BattleManager.Instance.DeactivateInteractible();
                return;
            }

            BattleManager.Instance.IsDiceBeingDropped = true;

            if (LevelManager.Instance.CurrentPhase != Phase.Battle)
                return;

            if (_playerType != TurnManager.Instance.Turn)
                return;

            var diceObject = eventData.pointerDrag;
            var dice = diceObject.GetComponent<Dice>();
            dice.SetOnActionSlot(GetComponent<RectTransform>().position);

            _canDiceBeingDropped = false;
            BattleManager.Instance.IsDiceBeingDropped = false;
        }

    }
}
