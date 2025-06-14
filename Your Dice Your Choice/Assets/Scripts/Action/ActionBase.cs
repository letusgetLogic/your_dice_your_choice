using UnityEngine;
using Assets.Scripts.CharacterPrefab;
using Assets.Scripts.ActionPanelPrefab;
using Assets.Scripts.DicePrefab;

namespace Assets.Scripts.Action
{
    public abstract class ActionBase
    {
        public ActionData Data { get;  set; }
        public Vector2Int[] ActionDirections { get; private set; }
        public GameObject CharacterObject { get;  set; }

        private Character _character => CharacterObject.GetComponent<Character>();

        public ActionBase(ActionData data, GameObject characterObject)
        {
            Data = data;
            CharacterObject = characterObject;
            ActionDirections = GetVector2FromDirection.Get(data.Direction);
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

        public virtual void ShowInteractible(int diceNumber)
        {
            FieldManager.Instance.ShowField(
                _character.FieldIndex,
                ActionDirections,
                GetIntFromAllowedTile.Get(Data.AllowedTile, diceNumber));
        }

        /// <summary>
        /// Handles the input of player.
        /// </summary>
        /// <param name="fieldObject"></param>
        public abstract void HandleInput(GameObject fieldObject);
    }
}
