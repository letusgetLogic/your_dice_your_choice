using System;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : Defend
{
    public static readonly string[] Description = new string[]
    {
            Defend.DefaultDescription,
            "Dice 1: Increase DP by 10% for 1 round.\nIt expires when attacked.",
            "Dice 2: Increase DP by 20% for 1 round.\nIt expires when attacked.",
            "Dice 3: Increase DP by 30% for 1 round.\nIt expires when attacked.",
            "Dice 4: Increase DP by 40% for 1 round.\nIt expires when attacked.",
            "Dice 5: Increase DP by 50% for 1 round.\nIt expires when attacked.",
            "Dice 6: Increase DP by 60% for 1 round.\nIt expires when attacked.",
    };

    public ShieldBehaviour(ActionData data, GameObject characterObject) :
       base(data, characterObject)
    {
        AllowedDiceNumber = AllowedDiceNumber.D1_6;
    }

    public override void SetDataPopUp(int index)
    {
        PopUpAction.Instance.SetData(Description[index]);
    }

    public override void ActivateSkill(int diceNumber)
    {
        var characterDefend = character.GetComponent<CharacterDefense>();    

        float buffedDP = Buff(characterDefend.CurrentDP, diceNumber);

        characterDefend.SetBuffDP(buffedDP - characterDefend.CurrentDP, 1, 2);
        characterDefend.SetDP(buffedDP);
    }

    private float Buff(float dp, int index)
    {
        switch (index)
        {
            case 0:
                throw new System.Exception("ShieldBehaviour.Buff() -> index = 0");

            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                var buffedAP = (float)Math.Round(dp * (1 + index * 0.1f), 2);
                return buffedAP;
        }

        throw new System.Exception("ShieldBehaviour.Buff() -> int index invalid");
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
