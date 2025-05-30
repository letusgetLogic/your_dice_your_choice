using UnityEngine;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.Action
{
    public class Movement : ActionBase
    {
        public ActionData ActionData { get; set; }
        public GameObject CharacterObject { get; set; }

        public Movement(ActionData actionData, GameObject characterObject) : base(actionData, characterObject) { }

    }
}
