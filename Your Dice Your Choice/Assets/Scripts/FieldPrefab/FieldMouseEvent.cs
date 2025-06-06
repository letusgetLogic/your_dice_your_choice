using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Assets.Scripts.FieldPrefab
{
    public class FieldMouseEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private  GameObject _foggyPanel;
        [SerializeField] private  GameObject _animationHint;
        [SerializeField] private GameObject _animationClick;

        [SerializeField] private Color _onPointerEnterColor;
        [SerializeField] private float _animTimer = .1f;

        private bool _isClicking = false;

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            _foggyPanel.SetActive(true);
            _animationHint.SetActive(true);
            _animationClick.SetActive(false);
        }

        /// <summary>
        /// On mouse click event.
        /// </summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            _isClicking = true;
            _animationHint.SetActive(false);
            _animationClick.SetActive(true);
            StartCoroutine(DisableAnimClick());
        }

        /// <summary>
        /// Disable animation click.
        /// </summary>
        /// <returns></returns>
        private IEnumerator DisableAnimClick()
        {
            yield return new WaitForSeconds(_animTimer);

            _foggyPanel.SetActive(false);
            _animationClick.SetActive(false);

            var boxCollider = gameObject.GetComponent<BoxCollider2D>();
            boxCollider.enabled = false;
        }

        /// <summary>
        /// On mouse hover event.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isClicking)
            {
                _animationHint.GetComponent<SpriteRenderer>().color = _onPointerEnterColor;
            }
        }

        /// <summary>
        /// On mouse exit event.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isClicking)
            {
                _animationHint.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        /// <summary>
        /// Hides the components for the mouse event.
        /// </summary>
        public void HideComponents()
        {
            _foggyPanel.SetActive(false);
            _animationHint.SetActive(false);
            _animationClick.SetActive(false);
        }
    }
}
