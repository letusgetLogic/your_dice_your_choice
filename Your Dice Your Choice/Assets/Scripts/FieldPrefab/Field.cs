using UnityEngine;

namespace Assets.Scripts.FieldPrefab
{
    public class Field : MonoBehaviour
    {
        public Vector2Int Index { get; private set; }

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            GetComponent<FieldMouseEvent>().enabled = false;
        }

        /// <summary>
        /// Sets the field index.
        /// </summary>
        /// <param name="index"></param>
        public void SetIndex(Vector2Int index) 
        { 
            this.Index = index; 
        }
    }
}
