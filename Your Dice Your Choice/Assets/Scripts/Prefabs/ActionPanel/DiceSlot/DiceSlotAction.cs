using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DiceSlotAction : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [SerializeField][Range(0f, 1f)] private float _delayShowingInteractible = .5f;

    private ActionPanel _actionPanel => transform.parent.GetComponent<ActionPanel>();
    private PlayerType _playerType =>
        _actionPanel.CharacterObject.GetComponent<Character>().Player.PlayerType;

    private bool _canDiceBeingDropped { get; set; } = false;

    /// <summary>
    /// Mouse enters UI Element. 
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        // Only runs when

        // - the current phase is Battle,
        if (LevelManager.Instance.CurrentPhase != Phase.Battle)
            return;

        // - the current turn is the player type of this action panel,
        if (TurnManager.Instance.Turn != _playerType)
            return;

        // - the pointer is dragging a dice object,
        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Dice"))
        {
            // - the previous interactable objects are not interactible,
            BattleController.Instance.DeactivateInteractible();

            BattleController.Instance.Coroutine =
                ShowInteractible(eventData.pointerDrag);

            StartCoroutine(BattleController.Instance.Coroutine);
        }
    }

    /// <summary>
    /// Shows the interactible objects.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowInteractible(GameObject diceBeingDragged)
    {
        yield return new WaitForSeconds(_delayShowingInteractible);

        BattleController.Instance.Coroutine = null;

        var dice = diceBeingDragged.GetComponent<Dice>();

        if (_actionPanel.Action.IsValid(dice.CurrentNumber) == false)
            yield break;

        // Only runs when the dice is valid to the action.

        BattleController.Instance.CurrentAction = _actionPanel.Action;
        BattleController.Instance.SetInteractible(dice.CurrentNumber);

        if (FieldManager.Instance.InteractibleFields != null &&
            FieldManager.Instance.InteractibleFields.Count == 0)
        {
            yield break;
        }
        if (CharacterManager.Instance.InteractibleCharacters != null &&
            CharacterManager.Instance.InteractibleCharacters.Count == 0)
        {
            yield break;
        }

        _canDiceBeingDropped = true;
        Debug.Log("ShowInteractible, _canDiceBeingDropped " + _canDiceBeingDropped);
        BattleController.Instance.ShowInteractible();
    }

    /// <summary>
    /// Mouse exits UI Element. 
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (LevelManager.Instance.CurrentPhase != Phase.Battle)
            return;

        if (TurnManager.Instance.Turn != _playerType)
            return;

        _actionPanel.GetComponent<ActionPanelMouseEvent>().HidePopUp();

        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Dice"))
        {
            Debug.Log("OnPointerExit");
            BattleController.Instance.DeactivateInteractible();
        }
    }

    /// <summary>
    /// UI Element is being dropped.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop, _canDiceBeingDropped " + _canDiceBeingDropped);
        if (!_canDiceBeingDropped)
        {
            BattleController.Instance.DeactivateInteractible();
            return;
        }

        BattleController.Instance.IsDiceBeingDropped = true;
        Debug.Log("OnDrop, IsDiceBeingDropped " + BattleController.Instance.IsDiceBeingDropped);
        if (LevelManager.Instance.CurrentPhase != Phase.Battle)
            return;

        if (TurnManager.Instance.Turn != _playerType)
            return;

        var diceObject = eventData.pointerDrag;
        var dice = diceObject.GetComponent<Dice>();
        dice.SetOnActionSlot(GetComponent<RectTransform>().position);

        BattleController.Instance.ActivateSkill(dice.CurrentNumber);

        _canDiceBeingDropped = false;
        BattleController.Instance.IsDiceBeingDropped = false;
    }

}
