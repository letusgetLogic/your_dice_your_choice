using TMPro;
using UnityEngine;


namespace Assets.Scripts.ActionPanelPrefab
{
    public class ActionPopup : MonoBehaviour
    {
        [SerializeField] private GameObject _actionPanelObject;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Vector2 _distance;

        /// <summary>
        /// Sets the description text.
        /// </summary>
        public void SetText(string description)
        {
            _descriptionText.text = description;
        }

        /// <summary>
        /// Sets the position of the info panel.
        /// </summary>
        public void SetPosition()
        {
             gameObject.GetComponent<RectTransform>().localPosition += PopUpBehaviour.Distance(
                _actionPanelObject.transform.position,
                _distance);
        }

        /// <summary>
        /// Sets the children game objects active or inactive.
        /// </summary>
        public void SetActiveChildren(bool value)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(value);
            }
        }
    }
}
