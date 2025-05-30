using UnityEngine;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.Action
{
    public class Defend : ActionBase
    {
        public ActionData ActionData { get; set; }
        public GameObject CharacterObject { get; set; }

        public Defend(ActionData actionData, GameObject characterObject) : base(actionData, characterObject) { }
    }
}

