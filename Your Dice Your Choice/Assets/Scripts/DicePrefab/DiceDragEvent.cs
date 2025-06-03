using Assets.Scripts.DicePrefab;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceDragEvent : DiceManager, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _alphaValue = 0.6f;

    private CanvasGroup _canvasGroup;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Triggers event at the beginning of drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {

        _canvasGroup.alpha = _alphaValue;
        _canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Triggers event while drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        var rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    /// <summary>
    /// Triggers event at the end of drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        Debug.Log("IsDiceOnSlot " + IsDiceOnSlot);
        if (IsDiceOnSlot)
        {
            SetDragEventEnable(false);
            return;
        }
        
        var diceMovement = GetComponent<DiceMovement>();
        diceMovement.SendBackToBase();
    }

}

