using UnityEngine;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.Action
{
    public class Defend : ActionBase
    {
        public Defend(ActionData actionData, GameObject characterObject) : base(actionData, characterObject) { }

        public override void HandleInput(GameObject fieldObject)
        {
            Debug.Log("Defend!");
        }
    }
}

