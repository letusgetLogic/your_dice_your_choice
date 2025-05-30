using UnityEngine;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.Action
{
    public class ActionMovement : IAction
    {
        public ActionData ActionData { get; set; }
        public GameObject CharacterObject { get; set; }
        public Character Character { get; set; }
        public string Description { get; set; }


        /// <summary>
        /// Initializes data.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(ActionData data, GameObject character)
        {
            ActionData = data;
            CharacterObject = character;
            Character = CharacterObject.GetComponent<Character>();
            Description = data.Description;
        }
    }
}
