using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterBeingAttacked : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject[] _hoverColor;

    // Generator Tool
    public void DeactivateHoverColor()
    {
        foreach (var hover in _hoverColor)
            hover.SetActive(false);
    }

    /// <summary>
    /// Start method.
    /// </summary>
    private void OnEnable()
    {
        foreach (var hover in _hoverColor)
            hover.SetActive(false);
    }

    /// <summary>
    /// Hovers the mouse over the character. 
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (LevelManager.Instance.CurrentPhase != Phase.Battle)
        {
            return;
        }

        foreach (var hover in _hoverColor)
            hover.SetActive(true);
    }

    /// <summary>
    /// Mouse exits the collider.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (LevelManager.Instance.CurrentPhase != Phase.Battle)
        {
            return;
        }

        foreach (var hover in _hoverColor)
            hover.SetActive(false);
    }

    /// <summary>
    /// Clicks the character.
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (LevelManager.Instance.CurrentPhase != Phase.Battle)
        {
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            foreach (var hover in _hoverColor)
                hover.SetActive(false);

            BattleController.Instance.HandleInput(eventData.pointerClick);
        }

    }

}

