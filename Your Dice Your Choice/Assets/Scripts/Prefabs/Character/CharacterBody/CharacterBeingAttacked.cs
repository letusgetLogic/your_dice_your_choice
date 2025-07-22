using Assets.Scripts.LevelDatas;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.CharacterPrefab.CharacterBody
{
    public class CharacterBeingAttacked : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private GameObject _hoverColor;

        // Generator Tool
        public void DeactivateHoverColor()
        {
            _hoverColor.SetActive(false);
        }

        /// <summary>
        /// Start method.
        /// </summary>
        private void OnEnable()
        {
            _hoverColor.SetActive(false);
        }

        /// <summary>
        /// Hovers the mouse over the character. 
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (LevelManager.Instance.CurrentPhase != Phase.Battle)
            {
                return;
            }

            _hoverColor.SetActive(true);
        }

        /// <summary>
        /// Mouse exits the collider.
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (LevelManager.Instance.CurrentPhase != Phase.Battle)
            {
                return;
            }

            _hoverColor.SetActive(false);
        }

        /// <summary>
        /// Clicks the character.
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (LevelManager.Instance.CurrentPhase != Phase.Battle)
            {
                return;
            }

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _hoverColor.SetActive(false);

                BattleManager.Instance.HandleInput(eventData.pointerClick);
            }

        }

    }
}
