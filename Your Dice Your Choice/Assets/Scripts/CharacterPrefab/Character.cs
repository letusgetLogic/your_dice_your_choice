using UnityEngine;
using Assets.Scripts.CharacterDatas;
using Assets.Scripts.CharacterPrefab.CharacterBody;

namespace Assets.Scripts.CharacterPrefab
{
    public class Character : MonoBehaviour
    {
        public CharacterMouseEvent CharacterMouseEvent;

        public PlayerType Player { get; private set; }
        public CharacterData Data { get; private set; }
        public string Name { get; private set; }
        public float CurrentAP { get; private set; }
        public float CurrentDP { get; private set; }
        public GameObject Panel { get; private set; }
        public Vector2Int FieldIndex { get; private set; }

        // Generator Tool
        public CharacterData CharacterData { get => _dataInstance; }

        [SerializeField]
        private CharacterData _data;

        private CharacterData _dataInstance;

        [HideInInspector]
        public bool SettingsFoldout;

        public void OnSettingsUpdate()
        {
            Data = _dataInstance;
        }
        // 

        /// <summary>
        /// Initialize Data.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(PlayerType player, CharacterData data, Vector2Int fieldIndex)
        {
            Player = player;
            Data = data;
            Name = data.Type.ToString();
            
            GetComponent<CharacterHealth>().SetData();
            CurrentAP = data.AP;
            CurrentDP = data.DP;

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
        /// Sets the value of attack points.
        /// </summary>
        /// <param name="value"></param>
        public void SetAP(float value)
        {
            CurrentAP = value;
        }

        /// <summary>
        /// Sets the default values.
        /// </summary>
        public void SetDefault()
        {
            CurrentAP = Data.AP;
        }

        /// <summary>
        /// Sets the character interactible false, when hp = 0.
        /// </summary>
        public void SetInteractibleFalse()
        {
            gameObject.tag = "Obstacle";
            GetComponent<CharacterState>().SetDownState();
            Panel.GetComponent<CharacterPanel>().SetActionInactive();
        }
    }
}

