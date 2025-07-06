using System.Collections.Generic;
using System;
using UnityEngine;

public class DiceDisplay : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _alphaValue = 0.6f;

    private CanvasGroup _canvasGroup => GetComponent<CanvasGroup>();
    
    /// <summary>
    /// Sets the alpha at the default value.
    /// </summary>
    public void SetAlphaDefault()
    {
        _canvasGroup.alpha = 1f;
    }

    /// <summary>
    /// Sets the alpha at the defined value.
    /// </summary>
    public void SetAlphaDown()
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
    /// Accesses the canvas reference.
    /// </summary>
    /// <returns></returns>
    public Canvas MyCanvas()
    {
        return _canvas;
    }
}
