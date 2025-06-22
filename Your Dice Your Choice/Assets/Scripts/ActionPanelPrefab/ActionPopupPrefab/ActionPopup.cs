using Assets.Scripts.ActionPopupPrefab;
using TMPro;
using UnityEngine;


namespace Assets.Scripts.ActionPopupPrefab
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
            var panelPos = _actionPanelObject.transform.position;

            gameObject.GetComponent<RectTransform>().localPosition += Distance(panelPos);
        }

        /// <summary>
        /// Returns the distance to the action panel.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private Vector3 Distance(Vector3 pos)
        {
            Vector3 distance = new();

            distance.x = _distance.x * Direction(pos).x;
            distance.y = _distance.y;
            
            return distance;
        }

        /// <summary>
        /// Returns the direction of the distance.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private Vector3 Direction(Vector3 pos)
        {
            Vector3 dir = new();
           
            switch (pos.x)
            {
                case <= 0: dir.x = 1; break;
                case > 0: dir.x = -1; break;
            }
            
            switch (pos.y)
            {
                case <= 0: dir.y = 1; break;
                case > 0: dir.y = -1; break;
            }
            
            return dir;
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
