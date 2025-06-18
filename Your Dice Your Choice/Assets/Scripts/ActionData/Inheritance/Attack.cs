using UnityEngine;
using Assets.Scripts.CharacterPrefab;
using Unity.VisualScripting;

namespace Assets.Scripts.Action
{
    public class Attack : ActionBase
    {
         public Attack(ActionData data, GameObject characterObject) : base(data, characterObject) { }

        public override void SetDisplayedFields(int diceNumber)
        {
           
        }

        public override void HandleInput(GameObject fieldObject)
        {
            Debug.Log("Attack!");
        }
    }
}

