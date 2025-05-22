using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Characters;

namespace Assets.Scripts
{
    public class Field : MonoBehaviour/*, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler*/
    {
        public GameObject FoggyPanel;
        public GameObject AnimationZoom;
        public GameObject AnimationHover;
        public GameObject AnimationClick;

        [SerializeField] private float _animTimer = .1f;

        private bool _isClicking = false;

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            FoggyPanel.SetActive(false);
            AnimationZoom.SetActive(false);
        }

        ///// <summary>
        ///// On mouse click event.
        ///// </summary>
        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    _isClicking = true;
        //    AnimationHover.SetActive(false);
        //    AnimationClick.SetActive(true);
        //    StartCoroutine(DisableAnimClick());
        //}

        ///// <summary>
        ///// Disable animation click.
        ///// </summary>
        ///// <returns></returns>
        //private IEnumerator DisableAnimClick()
        //{
        //    yield return new WaitForSeconds(_animTimer);

        //    FoggyPanel.SetActive(false);
        //    AnimationClick.SetActive(false);

        //    var boxCollider = gameObject.GetComponent<BoxCollider2D>();
        //    boxCollider.enabled = false;
        //}

        ///// <summary>
        ///// On mouse hover event.
        ///// </summary>
        ///// <param name="eventData"></param>
        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    if (!_isClicking)
        //    {
        //        AnimationZoom.SetActive(false);
        //        AnimationHover.SetActive(true);
        //    }
        //}

        ///// <summary>
        ///// On mouse exit event.
        ///// </summary>
        ///// <param name="eventData"></param>
        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    if (!_isClicking)
        //    {
        //        AnimationHover.SetActive(false);
        //        AnimationZoom.SetActive(true);
        //    }
        //}

        //public void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other.gameObject.tag == "Player")
        //    {
        //        var otherScript = other.gameObject.GetComponent<Character>();
        //    }
        //}
    }
}
