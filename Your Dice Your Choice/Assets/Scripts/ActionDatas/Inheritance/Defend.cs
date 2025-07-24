using UnityEngine;
public abstract class Defend : ActionBase
{
    public static readonly string DefaultDescription =
        "Move the dice over here to get more information";

    public AllowedDiceNumber AllowedDiceNumber { get; protected set; }

    public Defend(ActionData data, GameObject characterObject) :
        base(data, characterObject)
    { }

    public override bool IsValid(int diceNumber)
    {
        return CheckDiceCondition.IsNumberValid(AllowedDiceNumber, diceNumber);
    }

    public override abstract void SetDataPopUp(int diceNumber);

    public override void SetInteractible(int diceNumber)
    {}

    public override void ShowInteractible()
    {}

    public override void HandleInput(GameObject fieldObject)
    {}
}
