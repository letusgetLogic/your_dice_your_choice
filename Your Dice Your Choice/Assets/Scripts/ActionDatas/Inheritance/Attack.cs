using UnityEngine;
using Assets.Scripts.CharacterPrefab;
using Assets.Scripts.ActionPanelPrefab;

namespace Assets.Scripts.ActionDatas
{
    public abstract class Attack : ActionBase
    {
        public static readonly string DefaultDescription = "Move the dice over it to get more information";

        public AllowedDiceNumber AllowedDiceNumber { get; protected set; }

        public Attack(ActionData data, GameObject characterObject) : base(data, characterObject) 
        {
            
        }

        public override bool IsValid(int diceNumber)
        {
            return CheckDiceCondition.IsNumberValid(AllowedDiceNumber, diceNumber);
        }

        public override abstract void SetDescriptionOf(ActionPanel actionPanel, int diceNumber);

        public override abstract void SetInteractible(int diceNumber);

        public override void ShowInteractible()
        {
            CharacterManager.Instance.ShowInteractibleCharacters();
        }

        public override void DeactivateInteractible()
        {
            CharacterManager.Instance.DeactivateCharacters();
        }

        public override void HandleInput(GameObject clickedCharacterBody)
        {
            GameObject enemyObject = clickedCharacterBody.transform.root.gameObject;

            float damage = character.CurrentAP - enemyObject.GetComponent<Character>().CurrentDP;

            enemyObject.GetComponent<CharacterHealth>().TakeDamage(damage);

            this.SetDefault();
        }

        public override void SetDefault()
        {
            character.SetDefault();
        }
    }
}

