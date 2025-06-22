using System.Collections.Generic;
using Assets.Scripts.ActionPopupPrefab;
using Assets.Scripts.CharacterPrefab;
using UnityEngine;

namespace Assets.Scripts.ActionDatas
{
    public class SwordBehaviour : Attack
    {
        public static readonly string[] Description = new string[]
        {
            "None",
            "Dice 1: Hit orthogonally 1 Tile",
        };


        public SwordBehaviour(ActionData data, GameObject characterObject) : base(data, characterObject) 
        {
            AllowedDiceNumber = AllowedDiceNumber.D1;
        }

        public override void SetDescriptionOf(ActionPopup actionPopup, int index)
        {
            actionPopup.SetText(Description[index]);
        }

        public override void SetInteractible(int diceNumber)
        {
            BattleManager.Instance.SetInteractibleEnemyCharacters(
               CharacterObject.GetComponent<Character>().FieldIndex,
               GetVector2FromDirection.Get(GetDirection(diceNumber)),
               Range(diceNumber));
        }

        /// <summary>
        /// The attack direction.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        private Direction GetDirection(int index)
        {
            switch (index)
            {
                case 0:
                    throw new System.Exception("SwordBehaviour.GetDirection() -> index = 0");

                case 1:
                    return Direction.Orthogonal;
            }

            throw new System.Exception("SwordBehaviour.GetDirection() -> int index invalid");
        }

        /// <summary>
        /// The attack range.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        private int Range(int index)
        {
            switch (index)
            {
                case 0:
                    throw new System.Exception("SwordBehaviour.Range() -> index = 0");

                case 1:
                    return 1;
            }

            throw new System.Exception("SwordBehaviour.Range() -> int index invalid");
        }

        //private static Dictionary<string, string> _attackDescription = new Dictionary<string, string>
        //{
        //    {"Solid Thrust", "Hit orthogonally 1 Tile, with Dice 1" },
        //    {"Long Thrust", "Hit diagonally 1 Tile, with Dice 2" },
        //    {"Silver Swing", "Hit orthogonally 3 Tiles with 75% Damage, with Dice 3" },
        //    {"The 4 Stiches", "Hit all orthogonal Tiles or all diagonal Tiles with 100% Damage, with Dice 4" },
        //    {"Stunning Strike", "Hit and stun in any direction 1 Tile, with Dice 5" },
        //    {"The Giant Sword", "Hit orthogonally 3 Tiles or diagonally 2 Tiles with 200% Damage, with Dice 6" },
        //};
    }
}

