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

        public override void HandleInput(GameObject fieldObject)
        {
            CharacterObject.GetComponent<CharacterMovement>().MoveTo(fieldObject);
        }

         public override void ShowInteractible(int diceNumber)
        {
            FieldManager.Instance.ShowField(
                _character.FieldIndex,
                ActionDirections,
                GetIntFromAllowedTile.Get(Data.AllowedTile, diceNumber));
        }
    }
}
