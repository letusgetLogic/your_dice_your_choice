using System.Collections;
using Assets.Scripts.DicePrefab;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceDragEvent : DiceManager, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private float _diceMovementDelay = 0.01f;

    /// <summary>
    /// Triggers event at the beginning of drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        SetAlphaDown();
        SetBlocksRaycasts(false);
    }

    /// <summary>
    /// Triggers event while drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        var rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition += eventData.delta / MyCanvas().scaleFactor;
    }

    /// <summary>
    /// Triggers event at the end of drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        SetAlphaDefault();
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

