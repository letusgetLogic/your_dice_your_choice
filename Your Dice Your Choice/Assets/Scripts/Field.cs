using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Field : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject FoggyPanel;
    public GameObject AnimationZoom;
    public GameObject AnimationHover;
    public GameObject AnimationClick;

    [SerializeField] private float _animTimer = 0.1f;

    private bool _isClicking = false;

    private void Start()
    {
        FoggyPanel.SetActive(true);
        AnimationZoom.SetActive(true);
    }

    /// <summary>
    /// On mouse click event.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        _isClicking = true;
        AnimationHover.SetActive(false);
        AnimationClick.SetActive(true);
        StartCoroutine(DisableAnimClick());
    }

    /// <summary>
    /// Disable animation click.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisableAnimClick()
    {
        yield return new WaitForSeconds(_animTimer);
        FoggyPanel.SetActive(false);
        AnimationClick.SetActive(false);
        var field = gameObject.GetComponent<Field>();
        field.enabled = false;
    }

    /// <summary>
    /// On mouse hover event.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isClicking)
        {
            AnimationZoom.SetActive(false);
            AnimationHover.SetActive(true);
        }
    }

    /// <summary>
    /// On mouse exit event.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isClicking)
        {
            AnimationHover.SetActive(false);
            AnimationZoom.SetActive(true);
        }
    }
}
