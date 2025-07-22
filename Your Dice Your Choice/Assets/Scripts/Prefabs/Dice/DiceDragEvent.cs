using System.Collections;
using Assets.Scripts.DicePrefab;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceDragEvent : MonoBehaviour, 
    IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>
    /// Triggers event at the beginning of drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        var diceDisplay = GetComponent<DiceDisplay>();
        diceDisplay.SetAlphaDown();
        diceDisplay.SetBlocksRaycasts(false);
        diceDisplay.SetScale();
    }

    /// <summary>
    /// Triggers event while drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        var diceDisplay = GetComponent<DiceDisplay>();
        diceDisplay.UpdatePosition(eventData);
    }

    /// <summary>
    /// Triggers event at the end of drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        var diceDisplay = GetComponent<DiceDisplay>();
        diceDisplay.SetDefault();
        diceDisplay.SetBlocksRaycasts(true);

        BattleManager.Instance.SendDiceBackToBase(
            GetComponent<DiceMovement>());
    }
}

