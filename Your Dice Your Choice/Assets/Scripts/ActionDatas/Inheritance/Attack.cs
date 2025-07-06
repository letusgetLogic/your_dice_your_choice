using UnityEngine;
using Assets.Scripts.CharacterPrefab;
using Assets.Scripts.ActionPopupPrefab;

namespace Assets.Scripts.ActionDatas
{
    public abstract class Attack : ActionBase
    {
        public AllowedDiceNumber AllowedDiceNumber { get; protected set; }

        public Attack(ActionData data, GameObject characterObject) : base(data, characterObject) 
        {
            
        }

        public override bool IsValid(int diceNumber)
        {
            return CheckDiceCondition.IsNumberValid(AllowedDiceNumber, diceNumber);
        }

        public override abstract void SetDescriptionOf(ActionPopup actionPopup, int index);

        public override abstract void SetInteractible(int diceNumber);

        public override abstract void ShowInteractible();

        public abstract override void DeactivateInteractible();

        public override void HandleInput(GameObject clickedCharacterBody)
        {
            GameObject enemyObject = clickedCharacterBody.transform.root.gameObject;

            float damage = characterData.AP - enemyObject.GetComponent<Character>().Data.DP;

            enemyObject.GetComponent<CharacterHealth>().TakeDamage(damage);

            Debug.Log("Attack!");
        }


       
    }
}

