using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionPanelMouseEvent : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField][Range(0f, 1f)] private float _delayOnHoverTime = .5f;
    public float DelayOnHoverTime => _delayOnHoverTime;

    private IEnumerator _coroutine;
    private PlayerType _playerType => GetComponent<ActionPanel>().
        CharacterObject.GetComponent<Character>().Player.PlayerType;

    /// <summary>
    /// OnPointerEnter. 
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (LevelManager.Instance.CurrentPhase != Phase.Battle)
            return;

        var diceNumber = 0;

        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Dice"))
        {
            diceNumber = eventData.pointerDrag.GetComponent<Dice>().CurrentNumber;
        }

        _coroutine = ShowInfo(diceNumber);
        StartCoroutine(_coroutine);
    }

    /// <summary>
    /// OnPointerExit.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (LevelManager.Instance.CurrentPhase != Phase.Battle)
            return;

        // Ensure that the coroutine is not null before stopping it.
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        HidePopUp();
    }

    /// <summary>
    /// Shows the action popup.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowInfo(int diceNumber)
    {
        yield return new WaitForSeconds(_delayOnHoverTime);

        PanelManager.Instance.SetActive(PanelManager.Instance.PopUpActionObject, true);

        GetComponent<ActionPanel>().Action.SetDataPopUp(diceNumber);
        PopUpAction.Instance.SetPosition(gameObject);
    }

    /// <summary>
    /// Hides the action popup.
    /// </summary>
    public void HidePopUp()
    {
        PanelManager.Instance.SetActive(PanelManager.Instance.PopUpActionObject, false);
    }

}
