using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiceDisplay : MonoBehaviour
{
    [SerializeField] private Sprite[] _diceSide;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _alphaValue = 0.6f;
    [SerializeField] private float _scaleSize = 1.1f;

    public Sprite[] DiceSide => _diceSide;
    private Vector3 _originScale;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Awake()
    {
        _originScale = GetComponent<RectTransform>().localScale;
    }

    /// <summary>
    /// Sets the dice side image based on the index.
    /// </summary>
    /// <param name="sprite"></param>
    public void SetImage(int index)
    {
        GetComponent<Image>().sprite = _diceSide[index];
    }

    /// <summary>
    /// Sets the default values.
    /// </summary>
    public void SetDefault()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        var rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.localScale = _originScale;
    }

    /// <summary>
    /// Sets the alpha at the defined value.
    /// </summary>
    public void SetAlphaDown()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = _alphaValue;
    }
    
    /// <summary>
    /// Sets blocksRaycasts true/false.
    /// </summary>
    public void SetBlocksRaycasts(bool value)
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = value;
    }

    /// <summary>
    /// Updates the dice position.
    /// </summary>
    /// <param name="eventData"></param>
    public void UpdatePosition(PointerEventData eventData)
    {
        var rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    /// <summary>
    /// Sets the scale of dice.
    /// </summary>
    public void SetScale()
    {
        var rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.localScale *= _scaleSize;
    }
}
