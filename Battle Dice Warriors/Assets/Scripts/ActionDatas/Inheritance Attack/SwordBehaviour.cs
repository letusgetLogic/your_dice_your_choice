﻿using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordBehaviour : Attack
{
    public SwordBehaviour(ActionPanel actionPanel, GameObject characterObject) :
        base(actionPanel, characterObject)
    {
        AllowedDiceNumber = AllowedDiceNumber.D1_6;
    }

    private static readonly float belowVariedPercentage = 15f;
    private static readonly float aboveVariedPercentage = 25;
    private static readonly string variedValueDescription = 
        $"* AP can vary during the attack within the range between {belowVariedPercentage}% below and {aboveVariedPercentage}% above";

    public static readonly string[] Description = new string[]
    {
            DefaultDescription,
            "Dice 1: Hit orthogonally a opponent",
            "Dice 2: Hit orthogonally a opponent with 20% AP Buff",
            "Dice 3: Hit orthogonally a opponent with 50% AP Buff",
            "Dice 4: Hit orthogonally a opponent with 100% AP Buff",
            "Dice 5: Hit orthogonally a opponent with 170% AP Buff",
            "Dice 6: Hit orthogonally a opponent with 260% AP Buff",
    };

    private static readonly SwordSkill[] swordSkills = new SwordSkill[]
    {
        // Direction,           Range, %, Hit, Round, BuffAPText
        new(Direction.None,       0,   0,   0, 0, ""),
        new(Direction.Orthogonal, 1,   0,   1, 0, ""),
        new(Direction.Orthogonal, 1,   20,  1, 0, "(+20% AP)"),
        new(Direction.Orthogonal, 1,   50,  1, 0, "(+50% AP)"),
        new(Direction.Orthogonal, 1,   100, 1, 0, "(+100% AP)"),
        new(Direction.Orthogonal, 1,   170, 1, 0, "(+170% AP)"),
        new(Direction.Orthogonal, 1,   260, 1, 0, "(+260% AP)"),
    };
    
    public override void SetDataPopUp(int index)
    {
        PopUpAction.Instance.SetData(Description[index]);
    }

    public override void SetInteractible(int diceNumber)
    {
        CharacterManager.Instance.SetInteractibleEnemyCharacters();

        var skill = swordSkills[diceNumber];
        var actionDirections = GetVector2IntFromDirection.Get(
            skill.Direction);

        foreach (Vector2Int actionDirection in actionDirections)
        {
            var enemyObject = FindTarget(
                character.FieldIndex,
                actionDirection,
                skill.Range);

            if (enemyObject == null)
                continue;

            CharacterManager.Instance.AddCharacter(enemyObject);
        }
    }

    /// <summary>
    /// Finds the first target within a specified range and direction from the given origin field index.
    /// </summary>
    /// <remarks>This method iterates through fields in the specified direction and range, skipping fields
    /// that are out of bounds.  The search stops as soon as a valid target is found. If no target is found within the
    /// range, the method returns <see langword="null"/>.</remarks>
    /// <param name="characterFieldIndexOrigin">The starting field index of the character, represented as a 2D grid coordinate.</param>
    /// <param name="actionDirection">The direction in which to search for the target, represented as a 2D vector.</param>
    /// <param name="range">The maximum number of fields to search in the specified direction. Must be a positive integer.</param>
    /// <param name="objectManager">An object that defines the logic for identifying the target within a field.</param>
    /// <returns>The first <see cref="GameObject"/> found within the specified range and direction that matches the criteria
    /// defined by  <paramref name="objectManager"/>. Returns <see langword="null"/> if no target is found.</returns>
    public GameObject FindTarget(Vector2Int characterFieldIndexOrigin,
       Vector2Int actionDirection, int range)
    {
        for (int i = 1; i <= range; i++)
        {
            var fieldIndex = characterFieldIndexOrigin;
            fieldIndex += actionDirection * i;

            if (FieldManager.Instance.IsTargetOutOfMap(fieldIndex))
                return null;

            var field = FieldManager.Instance.Fields[fieldIndex.x, fieldIndex.y].
            GetComponent<Field>();

            GameObject target = field.EnemyObject(character.Player.PlayerType);

            if (target == null)
                continue;

            return target;
        }

        return null;
    }

    public override void ActivateSkill(int diceNumber)
    {
        var skill = swordSkills[diceNumber];
        var characterAttack = character.GetComponent<CharacterAttack>();

        float buffAP = characterAttack.CurrentAP * skill.Percentage * 0.01f;
        characterAttack.CurrentBuffAP = buffAP; 
        characterAttack.CurrentAP = characterAttack.CurrentAP + buffAP;
        characterAttack.CurrentBuffAPText = skill.BuffAPText;
        characterAttack.InfoText = variedValueDescription;

        actionPanel.UpdateEndurance(skill.HitEndurance, skill.RoundEndurance);
    }

    public override float VariedAP(float ap)
    {
        float variedPercentage = UnityEngine.Random.Range(-belowVariedPercentage * 0.01f, aboveVariedPercentage * 0.01f);
        Debug.Log($"Varied AP: {ap} * {variedPercentage}");
        return ap * variedPercentage;
    }


    //private static Dictionary<string, string> _attackDescription = new Dictionary<string, string>
    //{
    //    {"Solid Thrust", "Hit orthogonally 1 Tile, with Dice 1" },
    //    {"Long Thrust", "Hit diagonally 1 Tile, with Dice 2" },
    //    {"Silver Swing", "Hit orthogonally 3 Tiles with 75% Damage, with Dice 3" },
    //    {"The 4 Stiches", "Hit all orthogonal Tiles or all diagonal Tiles with 100% Damage, with Dice 4" },
    //    {"Stunning Strike", "Hit and stun in any direction 1 Tile, with Dice 5" },
    //    {"The Giant Sword", "Hit in any direction 2 Tiles with 200% Damage, with Dice 6" },
    //};
}


