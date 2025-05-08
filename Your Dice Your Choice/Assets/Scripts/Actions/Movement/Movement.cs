using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Movement
    {
        private MoveDirection _direction;
        private AvaiableDiceNumber _diceNumber;
        private AvaiableMoveTiles _moveTiles;

        private Dictionary<string, string> _moveDescription = new Dictionary<string, string>
    {
        {"Move_X_Tiles", "Move in any direction X Tiles"},
        {"Move_X_+_Tiles", "Move orthogonally X Tiles"},
        {"Move_X_x_Tiles", "Move diagonally X Tiles"},

        {"Move_X_Tiles_1-3", "Move in any direction X Tiles, with Dice 1-3"},
        {"Move_X_+_Tiles_1-3", "Move orthogonally X Tiles, with Dice 1-3"},
        {"Move_X_x_Tiles_1-3", "Move diagonally X Tiles, with Dice 1-3"},

        {"Move_1_Tile_4-6", "Move in any direction 1 Tile, with Dice 4-6"},
        {"Move_1_+_Tile_4-6", "Move orthogonally 1 Tile, with Dice 4-6"},
        {"Move_1_x_Tile_4-6", "Move diagonally 1 Tile, with Dice 4-6"},
    };

        public Movement(Dictionary<string, string> moveDescription, MoveDirection direction, AvaiableDiceNumber diceNumber, AvaiableMoveTiles moveTiles)
        {
            _moveDescription = moveDescription;
            _direction = direction;
            _diceNumber = diceNumber;
            _moveTiles = moveTiles;
        }
    }
}
