using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.CharacterPrefab;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEditor.U2D.Animation;

namespace Assets.Scripts.CharacterPrefab.CharacterBody
{
    public class CharacterMouseEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private GameObject _characterObject;
        private Character _character;
        private CharacterPanelHint _panelHint;
        private Color _color;

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Start()
        {
            _characterObject = transform.root.gameObject;
            _character = transform.root.GetComponent<Character>();
            _panelHint = _character.Panel.GetComponent<CharacterPanelHint>();
            _color = _characterObject.GetComponent<CharacterColor>().PlayerColor;
        }

        /// <summary>
        /// Clicks the character.
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnPointerClick(PointerEventData eventData)
        {
            _panelHint.StartHintAnim();
        }

        /// <summary>
        /// Hovers the mouse over the character. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            CharacterInfoPanel.Instance.SetPosition(_characterObject);

            CharacterInfoPanel.Instance.TransferValues(
                _character.Data.Type.ToString(),
                _color,
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
