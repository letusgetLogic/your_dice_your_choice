using System.Collections;
using Assets.Scripts.DicePrefab;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceDragEvent : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    
    /// <summary>
    /// Triggers event at the beginning of drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
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
        Debug.Log("OnDrag");
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

