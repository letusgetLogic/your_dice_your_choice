using UnityEngine;
using Assets.Scripts.CharacterPrefab;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.Action
{
    public class Movement : ActionBase
    {
        public Vector2Int[] ActionDirections { get; private set; }

        public Movement(ActionData data, GameObject characterObject) : base(data, characterObject)
        {
            ActionDirections = GetVector2FromDirection.Get(data.Direction);
        }

         public override void SetDisplayedFields(int diceNumber)
        {
            FieldManager.Instance.SetDisplayedFields(
                character.FieldIndex,
                ActionDirections,
                GetIntFromAllowedTile.Get(Data.AllowedTile, diceNumber));
        }

        public override void HandleInput(GameObject fieldObject)
        {
            CharacterObject.GetComponent<CharacterMovement>().MoveTo(fieldObject);
        }

    }
}
