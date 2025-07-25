using UnityEngine;
using UnityEngine.EventSystems;

public class SetRectLocalPositionClick : MonoBehaviour, 
    IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    //private bool _isHeld = false;
    private Vector3 _originallocalPosition;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        _originallocalPosition = GetComponent<RectTransform>().localPosition;
    }

    /// <summary>
    /// OnPointerDown method to handle button press events.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //_isHeld = true;
        GetComponent<RectTransform>().localPosition = Vector3.zero;
    }

    /// <summary>
    /// OnPointerUp method to handle button release events.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        //_isHeld = false;
        GetComponent<RectTransform>().localPosition = _originallocalPosition;
    }

    /// <summary>
    /// OnPointerExit method to handle pointer exit events while holding the button.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //_isHeld = false;
        GetComponent<RectTransform>().localPosition = _originallocalPosition;
    }

    //private void Update()
    //{
    //    if (_isHeld)
    //    {

    //    }
    //}
}
