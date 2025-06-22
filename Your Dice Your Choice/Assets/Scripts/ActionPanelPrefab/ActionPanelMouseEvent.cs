using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

namespace Assets.Scripts.ActionPopupPrefab
{
    public class ActionPanelMouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] [Range(0f, 1f)] private float _delayOnHoverTime = .5f;

        private ActionPanel _actionPanel => GetComponent<ActionPanel>();
        private ActionPopup _actionPopup => _actionPanel.ActionPopup;
        private CharacterPanel _characterPanel => _actionPanel.CharacterPanel;

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

            _actionPopup.SetActiveChildren(true);

            if (_characterPanel.Player == TurnState.PlayerLeft)
                HideRightPanels(true);
        }

        /// <summary>
        /// Hides the action popup.
        /// </summary>
        private void HideInfo()
        {
            _actionPopup.SetActiveChildren(false);

            if (_characterPanel.Player == TurnState.PlayerLeft)
                HideRightPanels(false);
        }

        /// <summary>
        /// Hides the components of the right action panels because of text overlaying in UI.
        /// </summary>
        private void HideRightPanels(bool isHiding)
        {
            for (int i = _actionPanel.Index; i < _characterPanel.ActionPanelPrefabs.Length; i++)
            {
                if (i == _actionPanel.Index) 
                    continue;

                if (isHiding) 
                    _characterPanel.ActionPanelPrefabs[i].GetComponent<ActionPanel>().ShowComponents(false);
                else
                    _characterPanel.ActionPanelPrefabs[i].GetComponent<ActionPanel>().ShowComponents(true);
            }
        }
    }
}

