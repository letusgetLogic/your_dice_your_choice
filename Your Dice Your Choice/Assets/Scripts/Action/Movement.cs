using UnityEngine;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.Action
{
    public class Movement : ActionBase
    {
        public Movement(ActionData data, GameObject characterObject) : base(data, characterObject) { }

        public override void HandleInput(GameObject fieldObject)
        {
            var pos = fieldObject.transform.position;
            CharacterObject.GetComponent<CharacterMovement>().MoveTo(pos);
        }
    }
}
