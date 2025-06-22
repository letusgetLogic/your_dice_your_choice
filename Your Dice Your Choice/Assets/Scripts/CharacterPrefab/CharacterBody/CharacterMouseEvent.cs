using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.CharacterPrefab;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEditor.U2D.Animation;
using Assets.Scripts.ActionPopupPrefab;

namespace Assets.Scripts.CharacterPrefab.CharacterBody
{
    public class CharacterMouseEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField][Range(0f, 1f)] private float _delayOnHoverTime = .5f;

        private GameObject _characterObject;
        private Character _character;
        private CharacterPanelHint _panelHint;
        private Color _color;
        private IEnumerator _coroutine;

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
            _coroutine = ShowInfo();
            StartCoroutine(_coroutine);
        }

        /// <summary>
        /// Mouse exits the collider.
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
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

            CharacterPopup.Instance.SetPosition(_characterObject);

            CharacterPopup.Instance.TransferValues(
                _character.Name,
                _color,
                _character.OriginHP,
                _character.Data.HP,
                _character.Data.AP,
                _character.Data.DP);

            CharacterPopup.Instance.gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the action description label.
        /// </summary>
        private void HideInfo()
        {
            CharacterPopup.Instance.gameObject.SetActive(false);
            CharacterPopup.Instance.SetDefault();
        }
    }
}
