using UnityEngine;
public abstract class ActionBase
{
    public ActionData Data { get; private set; }
    public GameObject CharacterObject { get; private set; }
    protected Character character => CharacterObject.GetComponent<Character>();

    /// <summary>
    /// Sets data when the constructor has been created.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="characterObject"></param>
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
    /// Sets the description of the action for popup based on dice number or not.
    /// </summary>
    /// <param name="diceNumber"></param>
    public virtual void SetDataPopUp(int diceNumber)
    {
        PopUpAction.Instance.SetData(Data.Description);
    }

    /// <summary>
    /// Sets the interactible objects.
    /// </summary>
    /// <param name="diceNumber"></param>
    public abstract void SetInteractible(int diceNumber);

    /// <summary>
    /// Shows the interactible objects.
    /// </summary>
    public abstract void ShowInteractible();

    /// <summary>
    /// Activates the skill of the action.
    /// </summary>
    public virtual void ActivateSkill(int diceNumber)
    { }

    /// <summary>
    /// Handles the input of player.
    /// </summary>
    /// <param name="fieldObject"></param>
    public abstract void HandleInput(GameObject fieldObject);

}

