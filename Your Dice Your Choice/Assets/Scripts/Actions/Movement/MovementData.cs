using System.Collections.Generic;
using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    [CreateAssetMenu(fileName = "MovementData", menuName = "ScriptableData/MovementData", order = 2)]
    public class MovementData : ScriptableObject
    {
        public MovementType MovementType;
        public AvaiableDiceNumber DiceNumber;
        public MoveDirection Direction;
        public AvaiableMoveTiles MoveTiles;
        
        public Dictionary<MovementType, string> Description = new Dictionary<MovementType, string>
        {
            {MovementType.D1_6_Any_Mx, "Move in any direction X Tiles"},
            {MovementType.D1_6_Orthogonal_Mx, "Move orthogonally X Tiles"},
            {MovementType.D1_6_Diagonal_Mx, "Move diagonally X Tiles"},

            {MovementType.D1_3_Any_Mx, "Move in any direction X Tiles, with Dice 1-3"},
            {MovementType.D1_3_Orthogonal_Mx, "Move orthogonally X Tiles, with Dice 1-3"},
            {MovementType.D1_3_Diagonal_Mx, "Move diagonally X Tiles, with Dice 1-3"},

            {MovementType.D4_6_Any_M1, "Move in any direction 1 Tile, with Dice 4-6"},
            {MovementType.D4_6_Orthogonal_M1, "Move orthogonally 1 Tile, with Dice 4-6"},
            {MovementType.D4_6_Diagonal_M1, "Move diagonally 1 Tile, with Dice 4-6"},
        };
    }
}
