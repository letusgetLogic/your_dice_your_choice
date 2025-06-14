using UnityEngine;

namespace Assets.Scripts.CharacterPrefab
{
    public class Character : MonoBehaviour
    {
        public TurnState Player { get; private set; }
        public CharacterData Data { get; private set; }

        public float OriginHP { get; private set; }
        public float OriginAP { get; private set; }
        public float OriginDP { get; private set; }

        public GameObject Panel { get; private set; }

        public Vector2Int FieldIndex { get; private set; }

        /// <summary>
        /// Initialize Data.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(TurnState player, CharacterData data)
        {
            Player = player;
            Data = data;
            OriginHP = data.HP;
            OriginAP = data.AP;
            OriginDP = data.DP;
        }

        /// <summary>
        /// Initializes Panel.
        /// </summary>
        /// <param name="panel"></param>
        public void SetPanel(GameObject panel) 
        { 
            Panel = panel; 
        }

        /// <summary>
        /// Initializes FieldIndex.
        /// </summary>
        /// <param name="fieldIndex"></param>
        public void SetFieldIndex(Vector2Int fieldIndex) 
        { 
            FieldIndex = fieldIndex; 
        }
    }
}

