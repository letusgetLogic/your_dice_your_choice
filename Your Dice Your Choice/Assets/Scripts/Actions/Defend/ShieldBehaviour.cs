using System.Collections.Generic;

public class ShieldBehaviour
{
    private Dictionary<string, string> _defendDescription = new Dictionary<string, string>
    {
        {"Quick_Shielding", "Cover 50% Damage" },
        {"Ally_Shielding", "Cover 40% Damage for the nearby selected Ally" },
        {"The_Big_Shield", "Enlarge the Shield by 3 Tiles, which cover 30% Damage and prevent Enemies from getting through" },
        {"Shield_Throwing", "Damage the nearby Enemy and getting Shield, which cover 40% Damage" },
        {"Team_Shielding", "All nearby Allies and You getting Shields, which cover 25% Damage" },
        {"The_Burden", "Force all Enemies to attack him and cover 80% Damage" }
    };
}

