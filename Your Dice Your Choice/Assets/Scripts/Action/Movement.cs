using UnityEngine;
using Assets.Scripts.CharacterPrefab;
using Assets.Scripts.FieldPrefab;

namespace Assets.Scripts.Action
{
    public class Movement : ActionBase
    {
        public Movement(ActionData actionData, GameObject characterObject) : base(actionData, characterObject) { }

        public override void HandleInput(GameObject fieldObject)
        {
            var pos = fieldObject.transform.position;
            CharacterObject.GetComponent<CharacterMovement>().MoveTo(pos);
        }
    }
}
