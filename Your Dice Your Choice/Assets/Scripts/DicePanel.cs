using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DicePanel : MonoBehaviour, IDropHandler
{
    public TextMeshProUGUI ActionNameText;
    public GameObject DiceSlot;

    public void OnDrop(PointerEventData eventData)
    {
       
        if (eventData.pointerDrag != null)
        {
            var item = eventData.pointerDrag;
            item.transform.SetParent(DiceSlot.transform);
            item.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            //var slotPos = DiceSlot.GetComponent<RectTransform>().anchoredPosition;

            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = slotPos;
            //Debug.Log(slotPos);
        }
    }
}

