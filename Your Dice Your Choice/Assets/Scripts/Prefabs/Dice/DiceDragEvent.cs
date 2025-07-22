using System;
using System.Collections;
using Assets.Scripts.DicePrefab;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceDragEvent : MonoBehaviour, 
    IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private float _delayEndDrag = 0.1f;

    /// <summary>
    /// Triggers event at the beginning of drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            var diceDisplay = GetComponent<DiceDisplay>();
            diceDisplay.SetAlphaDown();
            diceDisplay.SetBlocksRaycasts(false);
            diceDisplay.SetScale();
        }
       
    }

    /// <summary>
    /// Triggers event while drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            var diceDisplay = GetComponent<DiceDisplay>();
            diceDisplay.UpdatePosition(eventData);
        }
       
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

        StartCoroutine(WaitForEndDrag());
        
    }

    /// <summary>
    /// Waits for a delay before sending the dice back to base.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForEndDrag()
    {
        yield return new WaitForSeconds(_delayEndDrag);

        BattleManager.Instance.SendDiceBackToBase(
            GetComponent<DiceMovement>());
    }
}

