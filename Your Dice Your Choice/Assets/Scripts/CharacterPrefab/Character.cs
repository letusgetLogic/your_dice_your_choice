using UnityEngine;
using Assets.Scripts.CharacterDatas;
using Assets.Scripts.CharacterPrefab.CharacterBody;
using UnityEngine.TextCore.Text;
using NUnit.Framework;
using System.Collections.Generic;
using Assets.Scripts.FieldPrefab;

namespace Assets.Scripts.CharacterPrefab
{
    [ExecuteAlways]
    public class Character : MonoBehaviour
    {
        public CharacterMouseEvent CharacterMouseEvent;

        public Player Player {  get; private set; }
        public PlayerType PlayerType { get; private set; }
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

        private void OnValidate()
        {
            if (_data == null)
                return;

            _dataInstance = Instantiate(_data);
            OnSettingsUpdate();
        }

        public void OnSettingsUpdate()
        {
            Data = _dataInstance;

            // Color
            CharacterMouseEvent.DeactivateHoverColor();

            // Eyes
            GetComponent<CharacterState>().SetBattleState();

            // Health
            GetComponent<CharacterHealth>().SetData();

            // Weapon
            var characterGetWeapon = GetComponent<CharacterGetWeapon>();

            if (characterGetWeapon.WeaponObjectLeft != null)
            {
                RemoveWeaponInEditor(characterGetWeapon.WeaponObjectLeft);
            } 
                
            if (characterGetWeapon.WeaponObjectRight != null)
            {
                RemoveWeaponInEditor(characterGetWeapon.WeaponObjectRight);
            }

            characterGetWeapon.SetWeaponToLeftHand(this);
            characterGetWeapon.SetWeaponToRightHand(this);
        }

        private void RemoveWeaponInEditor(GameObject weaponObject)
        {
            var destroyEditorWeapon = GameObject.Find("DestroyEditorWeapon");
            weaponObject.SetActive(false);
            weaponObject.transform.SetParent(destroyEditorWeapon.transform);
        }

        // 

       

        /// <summary>
        /// Initialize Data.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(Player player, PlayerType playerType, CharacterData data, Vector2Int fieldIndex)
        {
            Player = player;
            PlayerType = playerType;
            Data = data;
            Name = CharacterName.GetName();
            
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

            var field = FieldManager.Instance.Fields[FieldIndex.x, FieldIndex.y].GetComponent<Field>();
            field.SetCharacterObjectNull();

            Player.RemoveCharacter(gameObject);
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

