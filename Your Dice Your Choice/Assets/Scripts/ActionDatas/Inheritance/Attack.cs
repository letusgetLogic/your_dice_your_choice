using Assets.Scripts.ActionPanelPrefab;
using Assets.Scripts.CharacterPrefab;
using System;
using UnityEngine;

namespace Assets.Scripts.ActionDatas
{
    public abstract class Attack : ActionBase
    {
        public static readonly string DefaultDescription = 
            "Move the dice over here to get more information";

        public AllowedDiceNumber AllowedDiceNumber { get; protected set; }

        public Attack(ActionData data, GameObject characterObject) : 
            base(data, characterObject) 
        {}

        public override bool IsValid(int diceNumber)
        {
            return CheckDiceCondition.IsNumberValid(AllowedDiceNumber, diceNumber);
        }

        public override abstract void SetDataPopUp(int diceNumber);

        public override abstract void SetInteractible(int diceNumber);

        public override void ShowInteractible()
        {
            CharacterManager.Instance.ShowInteractibleCharacters();
        }

        public override void HandleInput(GameObject clickedCharacterBody)
        {
            if (clickedCharacterBody.CompareTag("Character") == false)
            {
                Debug.LogWarning("The clicked object is not a character body.");
                return;
            }

            GameObject enemyObject = clickedCharacterBody.transform.root.gameObject;

            float damage = 
                character.CurrentAP - enemyObject.GetComponent<Character>().CurrentDP;
            Debug.Log($"Character {character.Name} has CurrentAP {character.CurrentAP} and BuffAP {character.BuffAP}.");
            Debug.Log($"Character {character.Name} makes {damage} damage. ");
            enemyObject.GetComponent<CharacterHealth>().TakeDamage(damage);

            this.SetDefault();
        }

        /// <summary>
        /// Sets the default values for the action.
        /// </summary>
        public virtual void SetDefault()
        {
            character.SetDefault();
        }
    }
}

