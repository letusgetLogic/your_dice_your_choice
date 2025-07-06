using UnityEngine;
using Assets.Scripts.CharacterDatas;
using Assets.Scripts.CharacterPrefab.CharacterBody;

namespace Assets.Scripts.CharacterPrefab
{
    public class Character : MonoBehaviour
    {
        public PlayerType Player { get; private set; }
        public CharacterData Data { get; private set; }
        public string Name { get; private set; }

        public float CurrentHP { get; private set; }
        public float CurrentAP { get; private set; }
        public float CurrentDP { get; private set; }

        public GameObject Panel { get; private set; }

        public Vector2Int FieldIndex { get; private set; }

        /// <summary>
        /// Initialize Data.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(PlayerType player, CharacterData data, Vector2Int fieldIndex)
        {
            Player = player;
            Data = data;
            Name = data.Type.ToString();
            
            CurrentHP = data.HP;
            CurrentAP = data.AP;
            CurrentDP = data.DP;

            GetComponent<CharacterHealth>().SetHealthSlider();
            SetFieldIndex(fieldIndex);
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
            Debug.Log($"Character {Name} is on the field {FieldIndex}.");
        }

        /// <summary>
        /// Sets the value of the attribute.
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public void SetAttributeValue(float attribute, float value)
        {
            attribute = value;
        }
    }
}

