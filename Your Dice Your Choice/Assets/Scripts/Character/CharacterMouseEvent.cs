using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Characters;
using System.Collections;
using System;

public class CharacterMouseEvent : MonoBehaviour, /*IPointerClickHandler, */IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _characterObject;

    private Character _character;

    /// <summary>
    /// Hovers the mouse over the character. 
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetInfoPanelPosition();

        _character = _characterObject.GetComponent<Character>(); 
        
        var characterPanel = _character.Panel.GetComponent<CharacterPanel>();

        CharacterInfoPanel.Instance.TransferValues(
            characterPanel.CharacterName,
            _character.OriginHP,
            _character.Data.HP,
            _character.Data.AP,
            _character.Data.DP);

        CharacterInfoPanel.Instance.gameObject.SetActive(true);
    }

    /// <summary>
    /// Mouse exits the collider.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        CharacterInfoPanel.Instance.gameObject.SetActive(false);
        CharacterInfoPanel.Instance.SetDefault();
    }

    /// <summary>
    /// Sets the position of the info panel.
    /// </summary>
    private void SetInfoPanelPosition()
    {
        var pos = Camera.main.WorldToScreenPoint(_characterObject.transform.position);


    }

}
