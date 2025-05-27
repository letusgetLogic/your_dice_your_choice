using System.Collections.Generic;
using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    [CreateAssetMenu(fileName = "MovementData", menuName = "ScriptableData/MovementData", order = 2)]
    public class MovementData : ScriptableObject
    {
       
        
        public Dictionary<ActionKey, string> Description = new Dictionary<ActionKey, string>
        {
            {ActionKey.Mx_D1_6_Any, "Move in any direction X Tiles"},
            {ActionKey.Mx_D1_6_Orthogonal, "Move orthogonally X Tiles"},
            {ActionKey.Mx_D1_6_Diagonal, "Move diagonally X Tiles"},

            {ActionKey.Mx_D1_3_Any, "Move in any direction X Tiles, with Dice 1-3"},
            {ActionKey.Mx_D1_3_Orthogonal_, "Move orthogonally X Tiles, with Dice 1-3"},
            {ActionKey.Mx_D1_3_Diagonal, "Move diagonally X Tiles, with Dice 1-3"},

            {ActionKey.M1_D4_6_Any, "Move in any direction 1 Tile, with Dice 4-6"},
            {ActionKey.M1_D4_6_Orthogonal, "Move orthogonally 1 Tile, with Dice 4-6"},
            {ActionKey.M1_D4_6_Diagonal, "Move diagonally 1 Tile, with Dice 4-6"},
        };
    }
}
