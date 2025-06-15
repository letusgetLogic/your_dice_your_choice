using UnityEngine;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.Action
{
    public class Defend : ActionBase
    {
        public Defend(ActionData data, GameObject characterObject) : base(data, characterObject) { }

        public override void ShowInteractible(int diceNumber)
        {

        }

        public override void HandleInput(GameObject fieldObject)
        {
            Debug.Log("Defend!");
        }
    }
}

