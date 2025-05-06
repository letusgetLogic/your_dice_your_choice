using System.Collections.Generic;

public class SwordBehaviour
{
    private Dictionary<string, string> _attackDescription = new Dictionary<string, string>
    {
        {"Solid Thrust", "Hit orthogonally 1 Tile, with Dice 1" },
        {"Long Thrust", "Hit diagonally 1 Tile, with Dice 2" },
        {"Silver Swing", "Hit orthogonally 3 Tiles with 75% Damage, with Dice 3" },
        {"The 4 Stiches", "Hit all orthogonal Tiles or all diagonal Tiles with 100% Damage, with Dice 4" },
        {"Stunning Strike", "Hit and stun in any direction 1 Tile, with Dice 5" },
        {"The Giant Sword", "Hit orthogonally 3 Tiles or diagonally 2 Tiles with 200% Damage, with Dice 6" },
    };
}

