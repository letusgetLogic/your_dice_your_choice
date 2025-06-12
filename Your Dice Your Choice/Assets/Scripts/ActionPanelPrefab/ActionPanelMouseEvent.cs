using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Assets.Scripts.Action;
using Assets.Scripts.DicePrefab;
using Assets.Scripts.Actions;
using System.Collections;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class ActionPanelMouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Range(0f, 1f)] private float _delayTime;

        private ActionPanel _actionPanel => GetComponent<ActionPanel>();
        private ActionDescriptionPanel _actionDescriptionPanel => _actionPanel.ActionDescriptionPanel;
        private CharacterPanel _characterPanel => _actionPanel.CharacterPanel;

        /// <summary>
        /// Mouse enters the collider of a game object. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            StartCoroutine(ShowInfo());
        }

        /// <summary>
        /// Mouse exits the collider of a game object.
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            StopCoroutine(ShowInfo());

            _actionDescriptionPanel.SetActiveChildren(false);

            if (_characterPanel.Player == TurnState.PlayerLeft)
                HideRightPanels(false);
        }

        private IEnumerator ShowInfo()
        {
            yield return new WaitForSeconds(_delayTime);

            _actionDescriptionPanel.SetActiveChildren(true);

            if (_characterPanel.Player == TurnState.PlayerLeft)
                HideRightPanels(true);
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

