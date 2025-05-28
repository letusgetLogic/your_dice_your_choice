using System;
using System.Collections.Generic;
using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public static class MovementData
    {
        public static readonly Dictionary<ActionKey, string> Description = new Dictionary<ActionKey, string>
        {
            {ActionKey.Move_X_D1_6_Any, "Move in any direction X Tiles"},
            {ActionKey.Move_X_D1_6_Orthogonal, "Move orthogonally X Tiles"},
            {ActionKey.Move_X_D1_6_Diagonal, "Move diagonally X Tiles"},

            {ActionKey.Move_X_D1_3_Any, $"Move in any direction X Tiles,\\n with Dice 1-3"},
            {ActionKey.Move_X_D1_3_Orthogonal, $"Move orthogonally X Tiles,\\n with Dice 1-3"},
            {ActionKey.Move_X_D1_3_Diagonal, $"Move diagonally X Tiles,\\n with Dice 1-3"},

            {ActionKey.Move_1_D4_6_Any, $"Move in any direction 1 Tile,\\n with Dice 4-6"},
            {ActionKey.Move_1_D4_6_Orthogonal, $"Move orthogonally 1 Tile,\\n with Dice 4-6"},
            {ActionKey.Move_1_D4_6_Diagonal, $"Move diagonally 1 Tile,\\n with Dice 4-6"},
        };
    }
}
