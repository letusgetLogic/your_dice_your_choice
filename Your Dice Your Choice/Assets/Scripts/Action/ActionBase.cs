using UnityEngine;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.Action
{
    public abstract class ActionBase
    {
        public ActionData ActionData { get;  set; }
        public GameObject CharacterObject { get;  set; }

        public ActionBase(ActionData actionData, GameObject characterObject)
        {
            ActionData = actionData;
            CharacterObject = characterObject;
        }

        public virtual void HintField() 
        {
            
        }
    }
}
