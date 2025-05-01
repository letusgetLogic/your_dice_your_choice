using UnityEngine;
using UnityEngine.EventSystems;

public class Field : MonoBehaviour, IPointerClickHandler
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

}
