using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public bool acceptDrops = true;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        if (acceptDrops)
        {
            // Re-parent to this slot and center it
            dropped.transform.SetParent(transform);
            dropped.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            Debug.Log("Drop rejected: Slot doesn't accept items.");
        }
    }
}
