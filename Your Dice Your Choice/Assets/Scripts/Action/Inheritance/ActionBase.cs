using UnityEngine;
using Assets.Scripts.CharacterPrefab;
using Assets.Scripts.ActionPanelPrefab;
using Assets.Scripts.DicePrefab;

namespace Assets.Scripts.Action
{
    public abstract class ActionBase
    {
        public ActionData Data { get;  set; }
        
        public GameObject CharacterObject { get;  set; }

        protected Character _character => CharacterObject.GetComponent<Character>();

        public ActionBase(ActionData data, GameObject characterObject)
        {
            Data = data;
            CharacterObject = characterObject;
        }

        /// <summary>
        /// Checks the dice condition, appended to the dice's valid number.
        /// </summary>
        /// <param name="dice"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual bool IsValid(int diceNumber)
        {
            return CheckDiceCondition.IsNumberValid(Data.AllowedDiceNumber, diceNumber);
        }

        /// <summary>
        /// Shows the interactible objects.
        /// </summary>
        /// <param name="diceNumber"></param>
        public abstract void ShowInteractible(int diceNumber);

        /// <summary>
        /// Handles the input of player.
        /// </summary>
        /// <param name="fieldObject"></param>
        public abstract void HandleInput(GameObject fieldObject);
    }
}
