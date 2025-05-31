using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.CharacterPrefab;
using System.Collections;
using System;
using UnityEngine.UI;

namespace Assets.Scripts.CharacterPrefab.CharacterBody
{
    public class CharacterMouseEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private GameObject _characterObject;
        private Character _character;
        private CharacterPanel _panel;
        private CharacterPanelHint _panelHint;

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Start()
        {
            _characterObject = transform.root.gameObject;
            _character = transform.root.GetComponent<Character>();
            _panel = _character.Panel.GetComponent<CharacterPanel>();
            _panelHint = _character.Panel.GetComponent<CharacterPanelHint>(); ;
        }

        /// <summary>
        /// Clicks the character.
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnPointerClick(PointerEventData eventData)
        {
            _panelHint.StartHintAnim();
            transform.root.position = new Vector2(0, 0);
        }

        /// <summary>
        /// Hovers the mouse over the character. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            CharacterInfoPanel.Instance.SetPosition(_characterObject);

            CharacterInfoPanel.Instance.TransferValues(
                _panel.CharacterName,
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
    }
}
