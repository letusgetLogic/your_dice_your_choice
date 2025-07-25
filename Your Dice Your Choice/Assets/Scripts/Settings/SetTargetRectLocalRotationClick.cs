using UnityEngine;
using UnityEngine.EventSystems;

public class SetTargetRectLocalRotationClick : MonoBehaviour, 
    IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private Quaternion _rotationOnClick = Quaternion.Euler(0, 0, 45);
    [SerializeField] private RectTransform _rotatedTarget;

    //private bool _isHeld = false;
    private Quaternion _originallocalRotation;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        _originallocalRotation = _rotatedTarget.GetComponent<RectTransform>().localRotation;
    }

    /// <summary>
    /// OnPointerDown method to handle button press events.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //_isHeld = true;
        _rotatedTarget.localRotation = _rotationOnClick;
    }

    /// <summary>
    /// OnPointerUp method to handle button release events.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        //_isHeld = false;
        _rotatedTarget.localRotation = _originallocalRotation;
    }

    /// <summary>
    /// OnPointerExit method to handle pointer exit events while holding the button.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //_isHeld = false;
        _rotatedTarget.localRotation = _originallocalRotation;
    }

    //private void Update()
    //{
    //    if (_isHeld)
    //    {

    //    }
    //}
}
