using UnityEngine;

namespace Assets.Scripts.FieldPrefab
{
    public class FieldComponents : MonoBehaviour
    {
        public FieldMouseEvent MouseEvent {  get; private set; }

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            MouseEvent = GetComponent<FieldMouseEvent>();
        }

        /// <summary>
        /// Sets the component enabled true/false.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="value"></param>
        public void SetEnabled(Component component, bool value)
        {
            if (component is Behaviour behaviour)
            {
                behaviour.enabled = value;
            }
        }
    }
}
