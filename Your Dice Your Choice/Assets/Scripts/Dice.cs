using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dice : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Sprite[] DiceSide;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _alphaValue = .6f;

    [HideInInspector] public int CurrentNumber { get; private set; }

    private CanvasGroup _canvasGroup;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        var defaultImage = gameObject.GetComponent<Image>();
        defaultImage.sprite = DiceSide[6];
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>   
    /// Initializes dice side.
    /// </summary>
    /// <param name="sideIndex"></param>
    public void InitializeSide(int sideIndex)
    {
        var currentImage = gameObject.GetComponent<Image>();
        currentImage.sprite = DiceSide[sideIndex];
        CurrentNumber = sideIndex;
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
    }
}

