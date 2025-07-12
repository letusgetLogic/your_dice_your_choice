using System.Collections;
using Assets.Scripts.DicePrefab;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceDragEvent : DiceDisplay, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    
    /// <summary>
    /// Triggers event at the beginning of drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        SetAlphaDown();
        SetBlocksRaycasts(false);
        SetScale();
    }

    /// <summary>
    /// Triggers event while drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        UpdatePosition(eventData);
    }

    /// <summary>
    /// Triggers event at the end of drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        SetDefault();
        SetBlocksRaycasts(true);

        StartCoroutine(MoveDice());
    }

    /// <summary>
    /// Moves the dice.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveDice()
    {
        yield return new WaitForEndOfFrame();

        var diceMovement = GetComponent<DiceMovement>();
        diceMovement.SendBackToBase();
    }
}

