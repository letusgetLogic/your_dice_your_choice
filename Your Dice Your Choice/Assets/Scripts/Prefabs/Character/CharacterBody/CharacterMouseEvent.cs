using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.CharacterPrefab;
using System.Collections;
using System;
using UnityEngine.UI;
using Assets.Scripts.ActionPanelPrefab;

namespace Assets.Scripts.CharacterPrefab.CharacterBody
{
    public class CharacterMouseEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float       _delayOnHoverTime = 0.5f;
        [SerializeField] private GameObject  _hoverColor;

        private IEnumerator _coroutine;

        private bool        _isBeingAttacked = false;

        private bool        _isShowing = false;

        // Generator Tool
        public void DeactivateHoverColor()
        {
            _hoverColor.SetActive(false);
        }

        /// <summary>
        /// Start method.
        /// </summary>
        private void Start()
        {
            _hoverColor.SetActive(false);
        }

        /// <summary>
        /// Update method.
        /// </summary>
        private void Update()
        {
            if (_isShowing)
            {
                var character = transform.root.GetComponent<Character>();

                CharacterPopup.Instance.SetData(
                    character.Name,
                    character.GetComponent<CharacterColor>().PlayerColor,
                    character.Data.HP,
                    character.GetComponent<CharacterHealth>().CurrentHP,
                    character.CurrentAP,
                    character.CurrentDP);
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
                _hoverColor.SetActive(false);
                
                BattleManager.Instance.HandleInput(eventData.pointerClick);
                _isBeingAttacked = false;
                return;
            }

            var character = transform.root.GetComponent<Character>();
            var panelHint = character.Panel.GetComponent<CharacterPanelHint>();
            panelHint.StartHintAnim();
        }

        /// <summary>
        /// Hovers the mouse over the character. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isBeingAttacked)
            {
                _hoverColor.SetActive(true);
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
                _hoverColor.SetActive(false);
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
            CharacterPopup.Instance.SetPosition(transform.root.gameObject);
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
