using Assets.Scripts.LevelDatas;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class ActionPanelMouseEvent : MonoBehaviour, 
        IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] [Range(0f, 1f)] private float _delayOnHoverTime = .5f;

        private IEnumerator _coroutine;
        private bool _isPopUpActionActive = false;

        /// <summary>
        /// OnPointerEnter. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (LevelManager.Instance.CurrentPhase != Phase.Battle)
                return;

            _coroutine = ShowInfo();
            StartCoroutine(_coroutine);
        }

        /// <summary>
        /// OnPointerExit.
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (LevelManager.Instance.CurrentPhase != Phase.Battle &&
                _isPopUpActionActive == false)
                return;

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
            PopUpAction.Instance.SetData(
                GetComponent<ActionPanel>().ActionData.Description, 2);
            PopUpAction.Instance.SetPosition(gameObject);

            _isPopUpActionActive = true;
        }

        /// <summary>
        /// Hides the action popup.
        /// </summary>
        private void HideInfo()
        {
            PanelManager.Instance.SetActive(PanelManager.Instance.PopUpAction, false);

            _isPopUpActionActive = false;
        }

    }
}

