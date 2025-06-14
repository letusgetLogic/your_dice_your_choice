using UnityEngine;

namespace Assets.Scripts.FieldPrefab
{
    public class Field : MonoBehaviour
    {
        public Vector2Int Index { get; private set; }
        public GameObject Square;

        /// <summary>
        /// Sets the field index.
        /// </summary>
        /// <param name="index"></param>
        public void SetIndex(Vector2Int index) 
        { 
            this.Index = index; 
        }

        /// <summary>
        /// Is any obstacle on the field?
        /// </summary>
        /// <returns></returns>
        public bool IsAnyObstacleOnField()
        {
            var size = GetComponent<BoxCollider2D>().size;

            Square.transform.localScale = new Vector3(size.x, size.y, 0);
            Instantiate(Square, transform.position, Quaternion.identity);

            return Physics2D.OverlapBox(transform.position, size, 0);
        }

    }
}
