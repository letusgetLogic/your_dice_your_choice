using UnityEngine;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.ActionDatas
{
    public class Defend : ActionBase
    {
        public static readonly string DefaultDescription = 
            "Move the dice over it to get more information";

        public Defend(ActionData data, GameObject characterObject) : 
            base(data, characterObject) { }

        public override void SetInteractible(int diceNumber)
        {

        }

        public override void ShowInteractible()
        {
            throw new System.NotImplementedException();
        }

        public override void HandleInput(GameObject fieldObject)
        {
            Debug.Log("Defend!");
        }

    }
}

