using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceDisplay : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _alphaValue = 0.6f;
    [SerializeField] private float _scaleSize = 1.1f;

    private CanvasGroup _canvasGroup => GetComponent<CanvasGroup>();
    private Vector3 _originScale;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        _originScale = transform.localScale;
    }

    
    /// <summary>
    /// Sets the default values.
    /// </summary>
    public void SetDefault()
    {
        _canvasGroup.alpha = 1f;
        transform.localScale = _originScale;
    }

    /// <summary>
    /// Sets the alpha at the defined value.
    /// </summary>
    protected void SetAlphaDown()
    {
        _canvasGroup.alpha = _alphaValue;
    }
    
    /// <summary>
    /// Sets blocksRaycasts true/false.
    /// </summary>
    public void SetBlocksRaycasts(bool value)
    {
        _canvasGroup.blocksRaycasts = value;
    }

    /// <summary>
    /// Updates the dice position.
    /// </summary>
    /// <param name="eventData"></param>
    protected void UpdatePosition(PointerEventData eventData)
    {
        var rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    /// <summary>
    /// Sets the scale of dice.
    /// </summary>
    protected void SetScale()
    {
        var rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.localScale *= _scaleSize;
    }
}
