using System;
using System.Collections.Generic;
using Assets.Scripts.ActionPanelPrefab;
using Assets.Scripts.CharacterPrefab;
using UnityEngine;

namespace Assets.Scripts.ActionDatas
{
    public class SwordBehaviour : Attack
    {
        public static readonly string[] Description = new string[]
        {
            Attack.DefaultDescription,
            "Dice 1: Attack with 100% AP",
            "Dice 2: Attack with 200% AP",
            "Dice 3: Attack with 300% AP",
            "Dice 4: Attack with 400% AP",
            "Dice 5: Attack with 500% AP",
            "Dice 6: Attack with 600% AP",
        };

        private int _indexInList { get; set; }

        public SwordBehaviour(ActionData data, GameObject characterObject) : base(data, characterObject)
        {
            AllowedDiceNumber = AllowedDiceNumber.D1_6;
        }

        public override void SetDescriptionOf(ActionPanel actionPanel, int index)
        {
            actionPanel.ActionPopup.SetText(Description[index]);
        }

        public override void SetInteractible(int diceNumber)
        {
            CharacterManager.Instance.SetInteractibleEnemyCharacters(
               character.FieldIndex,
               GetVector2IntFromDirection.Get(GetDirection(diceNumber)),
               Range(diceNumber));
        }

        public override void ActivateSkill(int diceNumber)
        {
            float buffedAP = character.CurrentAP + Buff(character.CurrentAP, diceNumber);
            character.SetAP(buffedAP);
        }

        private float Buff(float ap, int index)
        {
            switch (index)
            {
                case 0:
                    throw new System.Exception("SwordBehaviour.Buff() -> index = 0");

                case 1:
                    return 0f;

                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    return ap * index;
            }

            throw new System.Exception("SwordBehaviour.Buff() -> int index invalid");
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

