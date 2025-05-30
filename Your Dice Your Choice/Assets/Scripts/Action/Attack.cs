using UnityEngine;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.Action
{
    public class Attack : ActionBase
    {
        public ActionData ActionData { get; set; }
        public GameObject CharacterObject { get; set; }

        public Attack(ActionData actionData, GameObject characterObject) : base(actionData, characterObject) { }
    }
}

