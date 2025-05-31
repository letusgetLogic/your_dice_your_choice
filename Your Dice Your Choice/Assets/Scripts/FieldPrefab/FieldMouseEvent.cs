using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Assets.Scripts.FieldPrefab
{
    public class FieldMouseEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject FoggyPanel;
        public GameObject AnimationHint;
        public GameObject AnimationClick;

        [SerializeField] private Color _onPointerEnterColor;
        [SerializeField] private float _animTimer = .1f;

        private bool _isClicking = false;

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            FoggyPanel.SetActive(true);
            AnimationHint.SetActive(true);
            AnimationClick.SetActive(false);
        }

        /// <summary>
        /// On mouse click event.
        /// </summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            _isClicking = true;
            AnimationHint.SetActive(false);
            AnimationClick.SetActive(true);
            StartCoroutine(DisableAnimClick());
        }

        /// <summary>
        /// Disable animation click.
        /// </summary>
        /// <returns></returns>
        private IEnumerator DisableAnimClick()
        {
            yield return new WaitForSeconds(_animTimer);

            FoggyPanel.SetActive(false);
            AnimationClick.SetActive(false);

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
                AnimationHint.GetComponent<SpriteRenderer>().color = _onPointerEnterColor;
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
                AnimationHint.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        /// <summary>
        /// Hides the components for the mouse event.
        /// </summary>
        public void HideComponents()
        {
            FoggyPanel.SetActive(false);
            AnimationHint.SetActive(false);
            AnimationClick.SetActive(false);
        }
    }
}
