using UnityEngine;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.Action
{
    public class Attack : ActionBase
    {
         public Attack(ActionData actionData, GameObject characterObject) : base(actionData, characterObject) { }
    }
}

