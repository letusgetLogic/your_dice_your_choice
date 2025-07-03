using Assets.Scripts.CharacterPrefab;
using UnityEngine;

namespace Assets.Scripts.FieldPrefab
{
    public class Field : MonoBehaviour
    {
        public Vector2Int Index { get; private set; }
        public GameObject CharacterObject { get; private set; }

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
            if (!collision.CompareTag("Character"))
                return;
            
            _count++;
            CharacterObject = collision.transform.root.gameObject;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Character"))
                return;
                
            _count--;
            CharacterObject = null;
        }

        /// <summary>
        /// Checks OnTrigger counter.
        /// </summary>
        /// <returns></returns>
        public bool IsAnyObstacleOnField()
        {
            if (_count == 0)
                return false;

            return true;
        }

        /// <summary>
        /// Gets the enemy character.
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <returns></returns>
        public GameObject EnemyObject(PlayerType currentPlayer)
        {
            if (CharacterObject == null) 
                return null;   

            var character = CharacterObject.GetComponent<Character>();

            if (character.Player != currentPlayer)
            {
                return CharacterObject;
            }
            
            return null;
        }
    }
}
