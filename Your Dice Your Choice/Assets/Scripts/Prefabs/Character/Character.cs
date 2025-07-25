﻿using UnityEngine;

[ExecuteAlways]
public class Character : MonoBehaviour
{
    [SerializeField]
    private CharacterMouseEvent _characterMouseEvent;
    public CharacterMouseEvent CharacterMouseEvent => _characterMouseEvent;

    [SerializeField]
    private CharacterBeingAttacked _characterBeingAttacked;
    public CharacterBeingAttacked CharacterBeingAttacked => _characterBeingAttacked;

    public Player Player { get; private set; }
    public CharacterData Data { get; private set; }
    public string Name { get; private set; }
    public CharacterPanel Panel { get; private set; }
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
        _characterBeingAttacked.DeactivateHoverColor();

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
    public void SetData(Player player, CharacterData data, Vector2Int fieldIndex)
    {
        Player = player;
        Data = data;
        Name = CharacterName.GetName();

        GetComponent<CharacterHealth>().SetData();
        GetComponent<CharacterAttack>().SetData();
        GetComponent<CharacterDefense>().SetData();

        SetFieldIndex(fieldIndex);

        gameObject.name = $"Character {Name}";
    }

    /// <summary>
    /// Initializes Panel.
    /// </summary>
    /// <param name="panel"></param>
    public void SetPanel(CharacterPanel panel)
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
    /// Sets the character interactible false, when hp = 0.
    /// </summary>
    public void SetInteractibleFalse()
    {
        gameObject.tag = "Obstacle";
        GetComponent<CharacterState>().SetDownState();
        Panel.SetActionInactive();

        var field = FieldManager.Instance.Fields[FieldIndex.x, FieldIndex.y].GetComponent<Field>();
        field.SetCharacterObjectNull();

        Player.RemoveCharacter(gameObject);
    }

    /// <summary>
    /// Sets the component enabled true/false.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="value"></param>
    public void SetComponentEnabled(Component component, bool value)
    {
        if (component is Behaviour behaviour)
        {
            behaviour.enabled = value;
        }
    }
}


