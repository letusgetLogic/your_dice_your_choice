using UnityEngine;
using UnityEngine.TextCore.Text;

public class Movement : ActionBase
{
    public Vector2Int[] ActionDirections { get; private set; }

    public Movement(ActionPanel actionPanel, GameObject characterObject) :
        base(actionPanel, characterObject)
    {
        ActionDirections = GetVector2IntFromDirection.Get(base.actionPanel.ActionData.Direction);
    }

    public override void SetInteractible(int diceNumber)
    {
        FieldManager.Instance.SetInteractibleFields(
            character.FieldIndex,
            ActionDirections,
            GetIntFromAllowedTile.Get(actionPanel.ActionData.AllowedTile, diceNumber));
    }
    public override void ShowInteractible()
    {
        FieldManager.Instance.ShowInteractibleFields();
    }

    public override void ProcessInput(GameObject fieldObject)
    {
        if (fieldObject.CompareTag("Field") == false)
        {
            Debug.LogWarning("The clicked object is not a field.");
            return;
        }

        characterObject.GetComponent<CharacterMovement>().MoveTo(fieldObject);
    }

}
