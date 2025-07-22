using Assets.Scripts.DicePrefab;
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
        [SerializeField][Range(0f, 1f)] private float _delayOnHoverTime = .5f;
        public float DelayOnHoverTime => _delayOnHoverTime;

        private IEnumerator _coroutine;
        private bool _isPopUpActionActive = false;
        public bool IsPopUpActionActive
        {
            get => _isPopUpActionActive;
            set
            {
                _isPopUpActionActive = value;
            }
        }

        /// <summary>
        /// OnPointerEnter. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (LevelManager.Instance.CurrentPhase != Phase.Battle)
                return;

            var diceNumber = 0;

            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Dice"))
            {
                diceNumber = eventData.pointerDrag.GetComponent<Dice>().CurrentNumber;
            }

            _coroutine = ShowInfo(diceNumber);
            StartCoroutine(_coroutine);
        }

        /// <summary>
        /// OnPointerExit.
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            // Ensure that the coroutine is not null before stopping it.
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            HidePopUp();
        }

        /// <summary>
        /// Shows the action popup.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShowInfo(int diceNumber)
        {
            yield return new WaitForSeconds(_delayOnHoverTime);

            PanelManager.Instance.SetActive(PanelManager.Instance.PopUpActionObject, true);

            GetComponent<ActionPanel>().Action.SetDataPopUp(diceNumber);
            PopUpAction.Instance.SetPosition(gameObject);

            _isPopUpActionActive = true;
        }

        /// <summary>
        /// Hides the action popup.
        /// </summary>
        public void HidePopUp()
        {
            PanelManager.Instance.SetActive(PanelManager.Instance.PopUpActionObject, false);

            _isPopUpActionActive = false;
        }

    }
}

