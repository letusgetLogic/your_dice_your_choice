using UnityEngine;
using UnityEngine.EventSystems;

public class SetRectLocalScaleClick : MonoBehaviour, 
    IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private float _scaleFactor;

    //private bool _isHeld = false;
    private Vector3 _originalScale;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        _originalScale = GetComponent<RectTransform>().localScale;
    }

    /// <summary>
    /// OnPointerDown method to handle button press events.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //_isHeld = true;
        GetComponent<RectTransform>().localScale = _originalScale * _scaleFactor;
    }

    /// <summary>
    /// OnPointerUp method to handle button release events.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        //_isHeld = false;
        GetComponent<RectTransform>().localScale = _originalScale;
    }

    /// <summary>
    /// OnPointerExit method to handle pointer exit events while holding the button.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //_isHeld = false;
        GetComponent<RectTransform>().localScale = _originalScale;
    }

    //private void Update()
    //{
    //    if (_isHeld)
    //    {

    //    }
    //}
}
