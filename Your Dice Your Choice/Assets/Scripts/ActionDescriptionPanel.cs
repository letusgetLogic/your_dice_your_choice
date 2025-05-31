using Assets.Scripts.ActionPanelPrefab;
using TMPro;
using UnityEngine;


namespace Assets.Scripts
{
    public class ActionDescriptionPanel : MonoBehaviour
    {
        public static ActionDescriptionPanel Instance { get; private set; }

        [SerializeField] private RectTransform _canvasRectTransform;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Vector2 _distance;

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        public void SetText(GameObject actionPanelObject)
        {
            string text = actionPanelObject.GetComponent<ActionPanel>().ActionData.Description;
            _descriptionText.text = text;
        }

        /// <summary>
        /// Sets the position of the info panel.
        /// </summary>
        public void SetPosition(GameObject actionPanelObject)
        {
            var panelPos = actionPanelObject.transform.position;

            var pos = _canvasRectTransform.InverseTransformPoint(panelPos);

            gameObject.GetComponent<RectTransform>().localPosition = (Vector2)pos + Distance(pos);
        }

        /// <summary>
        /// Return the distance to the character.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private Vector2 Distance(Vector3 pos)
        {
            Vector2 distance = new();

            distance.x = _distance.x * Direction(pos).x;

            return distance;
        }

        /// <summary>
        /// Return the direction of the distance.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private Vector2 Direction(Vector3 pos)
        {
            Vector2 dir = new();

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
