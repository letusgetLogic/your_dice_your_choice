using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class ActionPanelMouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] [Range(0f, 1f)] private float _delayOnHoverTime = .5f;

        private IEnumerator _coroutine;

        /// <summary>
        /// OnPointerEnter. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            _coroutine = ShowInfo();
            StartCoroutine(_coroutine);
        }

        /// <summary>
        /// OnPointerExit.
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            StopCoroutine(_coroutine);
            HideInfo();
        }

        /// <summary>
        /// Shows the action popup.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShowInfo()
        {
            yield return new WaitForSeconds(_delayOnHoverTime);

            PanelManager.Instance.SetActive(PanelManager.Instance.PopUpAction, true);
        }

        /// <summary>
        /// Hides the action popup.
        /// </summary>
        private void HideInfo()
        {
            PanelManager.Instance.SetActive(PanelManager.Instance.PopUpAction, false);
        }

    }
}

