using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Assets.Scripts.DicePanel;

public class DicePanel : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
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

    /// <summary>
    /// Hovers the mouse over the character. 
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        ActionDescriptionPanel.Instance.SetPosition(ActionDescriptionPanel.Instance.gameObject);
        ActionDescriptionPanel.Instance.gameObject.SetActive(true);
    }

    /// <summary>
    /// Mouse exits the collider.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        ActionDescriptionPanel.Instance.gameObject.SetActive(false);
        ActionDescriptionPanel.Instance.SetDefault();
    }
}

