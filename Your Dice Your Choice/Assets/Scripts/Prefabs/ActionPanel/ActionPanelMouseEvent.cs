﻿using UnityEngine;
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

            var actionPopup = GetComponent<ActionPanel>().ActionPopup;
            actionPopup.SetActiveChildren(true);

            var characterPanel = GetComponent<ActionPanel>().CharacterPanel;
            if (characterPanel.PlayerType == PlayerType.PlayerLeft)
                HideRightPanels(true);
        }

        /// <summary>
        /// Hides the action popup.
        /// </summary>
        private void HideInfo()
        {
            var actionPopup = GetComponent<ActionPanel>().ActionPopup;
            actionPopup.SetActiveChildren(false);

            var characterPanel = GetComponent<ActionPanel>().CharacterPanel;
            if (characterPanel.PlayerType == PlayerType.PlayerLeft)
                HideRightPanels(false);
        }

        /// <summary>
        /// Hides the components of the right action panels because of text overlaying in UI.
        /// </summary>
        private void HideRightPanels(bool isHiding)
        {
            var actionPanel = GetComponent<ActionPanel>();
            var characterPanel = actionPanel.CharacterPanel; 

            for (int i = actionPanel.Index + 1; i < characterPanel.ActionPanelPrefabs.Length; i++)
            {
                if (isHiding) 
                    characterPanel.ActionPanelPrefabs[i].
                        GetComponent<ActionPanel>().ShowComponents(false);
                else
                    characterPanel.ActionPanelPrefabs[i].
                        GetComponent<ActionPanel>().ShowComponents(true);
            }
        }
    }
}

