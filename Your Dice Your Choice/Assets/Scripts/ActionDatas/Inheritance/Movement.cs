using Assets.Scripts.ActionPanelPrefab;
using Assets.Scripts.CharacterPrefab;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.ActionDatas
{
    public class Movement : ActionBase
    {
        public Vector2Int[] ActionDirections { get; private set; }

        public Movement(ActionData data, GameObject characterObject) : 
            base(data, characterObject)
        {
            ActionDirections = GetVector2IntFromDirection.Get(data.Direction);
        }
        
        public override void SetInteractible(int diceNumber)
        {
            FieldManager.Instance.SetInteractibleFields(
                character.FieldIndex,
                ActionDirections,
                GetIntFromAllowedTile.Get(Data.AllowedTile, diceNumber));
        }
        public override void ShowInteractible()
        {
            FieldManager.Instance.ShowInteractibleFields();
        }

        public override void HandleInput(GameObject fieldObject)
        {
            if (fieldObject.CompareTag("Field") == false)
            {
                Debug.LogWarning("The clicked object is not a field.");
                return;
            }

            CharacterObject.GetComponent<CharacterMovement>().MoveTo(fieldObject);
        }

    }
}
