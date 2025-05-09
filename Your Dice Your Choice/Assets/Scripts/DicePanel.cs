using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DicePanel : MonoBehaviour, IDropHandler
{
    public TextMeshProUGUI ActionName;
    public GameObject DiceSlot;



    /// <summary>
    /// On drop event.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
       
        if (eventData.pointerDrag != null)
        {
            var item = eventData.pointerDrag;
            item.transform.SetParent(DiceSlot.transform);
            item.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
    }

    
}

