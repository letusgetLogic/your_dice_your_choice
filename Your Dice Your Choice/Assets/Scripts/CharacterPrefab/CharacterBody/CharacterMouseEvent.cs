using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.CharacterPrefab;
using System.Collections;
using System;
using UnityEngine.UI;
using Assets.Scripts.ActionPopupPrefab;

namespace Assets.Scripts.CharacterPrefab.CharacterBody
{
    public class CharacterMouseEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField][Range(0f, 1f)] private float _delayOnHoverTime = .5f;

        private GameObject _characterObject;
        private Character _character;
        private CharacterPanelHint _panelHint;
        private CharacterBorderColor _borderColor;
        private Color _color;
        private IEnumerator _coroutine;

        private bool _isBeingAttacked = false;

        private GameObject _hoverColor => transform.root.GetComponent<CharacterComponents>().HoverColor;

        private bool _isShowing = false;

        /// <summary>
        /// Start method.
        /// </summary>
        private void Start()
        {
            _characterObject = transform.root.gameObject;
            _character = _characterObject.GetComponent<Character>();
            _panelHint = _character.Panel.GetComponent<CharacterPanelHint>();
            _borderColor = _characterObject.GetComponent <CharacterBorderColor>();
            _color = _characterObject.GetComponent<CharacterColor>().PlayerColor;
            _hoverColor.SetActive(false);
        }

        /// <summary>
        /// Update method.
        /// </summary>
        private void Update()
        {
            if (_isShowing)
            {
                CharacterPopup.Instance.TransferValues(
                    _character.Name,
                    _color,
                    _character.Data.HP,
                    _character.CharacterHealth.CurrentHP,
                    _character.CurrentAP,
                    _character.CurrentDP);
            }
        }

        /// <summary>
        /// Clicks the character.
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isBeingAttacked)
            {
                _hoverColor.SetActive(false );
                CharacterManager.Instance.DeactivateCharacters();
                BattleManager.Instance.HandleInput(eventData.pointerClick);
                _isBeingAttacked = false;
                return;
            }

            _panelHint.StartHintAnim();
        }

        /// <summary>
        /// Hovers the mouse over the character. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isBeingAttacked)
            {
                _hoverColor.SetActive(true );
                return;
            }

            _coroutine = ShowInfo();
            StartCoroutine(_coroutine);
        }

        /// <summary>
        /// Mouse exits the collider.
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isBeingAttacked)
            {
                _hoverColor.SetActive(false ) ;
                return;
            }

            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            HideInfo();
        }

        /// <summary>
        /// Shows the action description label.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShowInfo()
        {
            yield return new WaitForSeconds(_delayOnHoverTime);

            _isShowing = true;
            CharacterPopup.Instance.SetPosition(_characterObject);
            CharacterPopup.Instance.gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the action description label.
        /// </summary>
        private void HideInfo()
        {
            _isShowing = false;
            CharacterPopup.Instance.gameObject.SetActive(false);
            CharacterPopup.Instance.SetDefault();
        }

        /// <summary>
        /// Sets _isBeingAttacked.
        /// </summary>
        /// <param name="value"></param>
        public void SetIsBeingAttacked(bool value)
        {
            _isBeingAttacked = value;
        }
    }
}
