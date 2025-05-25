using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Movement
    {
        private MoveDirection _direction;
        private AvaiableDiceNumber _diceNumber;
        private AvaiableMoveTiles _moveTiles;

       

        public Movement(Dictionary<string, string> moveDescription, MoveDirection direction, AvaiableDiceNumber diceNumber, AvaiableMoveTiles moveTiles)
        {
            _direction = direction;
            _diceNumber = diceNumber;
            _moveTiles = moveTiles;
        }
    }
}
