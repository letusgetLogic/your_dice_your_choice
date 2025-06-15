using UnityEngine;

namespace Assets.Scripts.FieldPrefab
{
    public class Field : MonoBehaviour
    {
        public Vector2Int Index { get; private set; }

        private int _count = 0;

        /// <summary>
        /// Sets the field index.
        /// </summary>
        /// <param name="index"></param>
        public void SetIndex(Vector2Int index) 
        { 
            this.Index = index; 
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _count++;
            Debug.Log($"Field {Index} have {_count} Obstacle.");
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _count--;
            Debug.Log($"Field {Index} have {_count} Obstacle.");
        }

        /// <summary>
        /// Is any obstacle on the field?
        /// </summary>
        /// <returns></returns>
        public bool IsAnyObstacleOnField()
        {
            if (_count == 0)
                return false;

            return true;
        }

    }
}
