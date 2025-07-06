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
            "Dice 2",
            "Dice 3",
            "Dice 4",
            "Dice 5",
            "Dice 6",
        };


        public SwordBehaviour(ActionData data, GameObject characterObject) : base(data, characterObject) 
        {
            AllowedDiceNumber = AllowedDiceNumber.D1_6;
        }

        public override void SetDescriptionOf(ActionPopup actionPopup, int index)
        {
            actionPopup.SetText(Description[index]);
        }

        public override void SetInteractible(int diceNumber)
        {
            CharacterManager.Instance.SetInteractibleEnemyCharacters(
               CharacterObject.GetComponent<Character>().FieldIndex,
               GetVector2IntFromDirection.Get(GetDirection(diceNumber)),
               Range(diceNumber));
        }
        
        public override void ShowInteractible()
        {
            CharacterManager.Instance.ShowInteractibleCharacters();
        }

        public override void DeactivateInteractible()
        {
            CharacterManager.Instance.DeactivateCharacters();
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
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
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
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
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

