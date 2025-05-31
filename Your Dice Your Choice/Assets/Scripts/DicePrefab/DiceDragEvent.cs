using Assets.Scripts.DicePrefab;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceDragEvent : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _alphaValue = .6f;

    public bool IsDiceOnSlot { get; private set; }

    private CanvasGroup _canvasGroup;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        IsDiceOnSlot = false;
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
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if (IsDiceOnSlot == true)
        {
            IsDiceOnSlot = false;
            return;
        }
        
        Debug.Log("endDrag set _isRunning ");
        var diceMovement = GetComponent<DiceMovement>();
        diceMovement.SendBackToBase();
    }

    /// <summary>
    /// Sets IsDiceOnSlot true/false.
    /// </summary>
    /// <param name="value"></param>
    public void SetDiceOnSlot(bool value)
    {
        IsDiceOnSlot = value;
    }
}

