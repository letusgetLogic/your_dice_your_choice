using UnityEngine;
using UnityEngine.EventSystems;

public class Field : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// On mouse click event.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 fieldPos = transform.position;
        Debug.Log("Click!");
        Debug.Log(fieldPos);
    }

    /// <summary>
    /// On mouse hover event.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // do mouse hover stuff
    }

    /// <summary>
    /// On mouse exit event.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // reset to normal
    }
}
