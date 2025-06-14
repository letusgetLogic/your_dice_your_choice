using UnityEngine;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.Action
{
    public class Attack : ActionBase
    {
         public Attack(ActionData data, GameObject characterObject) : base(data, characterObject) { }


        public override void HandleInput(GameObject fieldObject)
        {
            Debug.Log("Attack!");
        }
    }
}

