﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : Defend
{
    public ShieldBehaviour(ActionPanel actionPanel, GameObject characterObject) :
       base(actionPanel, characterObject)
    {
        AllowedDiceNumber = AllowedDiceNumber.D1_6;
    }

    private static readonly string[] description = new string[]
    {
            DefaultDescription,
            "Dice 1: Increase DP by 100% for 1 round. It doesn't stack.",
            "Dice 2: Increase DP by 100% for 2 hits. It doesn't stack.",
            "Dice 3: Reduces damage by 30% for 3 rounds. It doesn't stack.",
            "Dice 4: Reduces damage by 40% for 2 hits. It doesn't stack.",
            "Dice 5: Reduces damage by 55% for 1 round. It doesn't stack.",
            "Dice 6: Increase DP by 260% for 1 round. It doesn't stack.",
    };
  
    private readonly ShieldSkill[] shieldSkills = new ShieldSkill[]
    {
        // dpP., dmg, hit, round, buffText
        new(0,    0,   0,    0, ""),          // Default
        new(100,  0,   0,    1, "(+100% DP)"),// Dice 1
        new(100,  0,   2,    0, "(+100% DP)"),// Dice 2
        new(0,   30,   0,    3, "(-30% dmg)"),// Dice 3
        new(0,   40,   2,    0, "(-40% dmg)"),// Dice 4
        new(0,   55,   0,    1, "(-55% dmg)"),// Dice 5
        new(260,  0,   0,    1, "(+260% DP)") // Dice 6
    };

    public override void SetDataPopUp(int index)
    {
        if (index == 0 && ActiveSkillIndex != 0)
        {
            PopUpAction.Instance.SetData(description[ActiveSkillIndex]);
            return;
        }
        PopUpAction.Instance.SetData(description[index]);
    }

    public override void ActivateSkill(int diceNumber)
    {
        ActiveSkillIndex = diceNumber;
        var skill = shieldSkills[diceNumber];
        var characterDefend = character.GetComponent<CharacterDefense>();

        if (skill.Percentage > 0)
        {
            float buffDP = characterDefend.CurrentDP * skill.Percentage * 0.01f;
            characterDefend.CurrentBuffDP = buffDP;
            characterDefend.CurrentDP = characterDefend.CurrentDP + buffDP;
            characterDefend.CurrentBuffType = CharacterDefense.BuffType.DP;
        }
        else if (skill.DamageReduction > 0)
        {
            characterDefend.CurrentDamageReduction = skill.DamageReduction;
            characterDefend.CurrentBuffType = CharacterDefense.BuffType.DamageReduction;
        }
        characterDefend.CurrentBuffDPText = skill.BuffText;

        HitEndurance = skill.HitEndurance;
        RoundEndurance = skill.RoundEndurance;

        if (HitEndurance > 0)
            IsHitCrucial = true;
        else
            IsHitCrucial = false;

        actionPanel.UpdateEndurance(skill.HitEndurance, skill.RoundEndurance);
    
    }

        //private Dictionary<string, string> _defendDescription = new Dictionary<string, string>
        //    {
        //        {"Quick_Shielding", "Cover 50% Damage" },
        //        {"Ally_Shielding", "Cover 40% Damage for the nearby selected Ally" },
        //        {"The_Big_Shield", "Enlarge the Shield by 3 Tiles, which cover 30% Damage and prevent Enemies from getting through" },
        //        {"Shield_Throwing", "Damage the nearby Enemy and getting Shield, which cover 40% Damage" },
        //        {"Team_Shielding", "All nearby Allies and You getting Shields, which cover 25% Damage" },
        //        {"The_Burden", "Force all Enemies to attack him and cover 80% Damage" }
        //    };
    }
