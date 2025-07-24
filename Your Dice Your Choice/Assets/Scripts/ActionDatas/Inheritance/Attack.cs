using System;
using UnityEngine;

public abstract class Attack : ActionBase
{
    public static readonly string DefaultDescription =
        "Move the dice over here to get more information";

    public AllowedDiceNumber AllowedDiceNumber { get; protected set; }

    public Attack(ActionData data, GameObject characterObject) :
        base(data, characterObject)
    { }

    public override bool IsValid(int diceNumber)
    {
        return CheckDiceCondition.IsNumberValid(AllowedDiceNumber, diceNumber);
    }

    public override abstract void SetDataPopUp(int diceNumber);

    public override abstract void SetInteractible(int diceNumber);

    public override void ShowInteractible()
    {
        CharacterManager.Instance.ShowInteractibleCharacters();
    }

    public override void HandleInput(GameObject clickedCharacterBody)
    {
        if (clickedCharacterBody.CompareTag("Character") == false)
        {
            Debug.LogWarning("The clicked object is not a character body.");
            return;
        }

        var characterAttack = character.GetComponent<CharacterAttack>();
        GameObject enemyObject = clickedCharacterBody.transform.root.gameObject;
        var enemyCharacter = enemyObject.GetComponent<Character>();

        float damage = characterAttack.CurrentAP - enemyCharacter.CurrentDP > 0 ?
                    characterAttack.CurrentAP - enemyCharacter.CurrentDP : 
                     0;
       Debug.Log($"Damage dealt: {damage}");
        enemyObject.GetComponent<CharacterHealth>().TakeDamage(damage);

        this.SetDefault(enemyCharacter);
    }

    /// <summary>
    /// Sets the default values for the action.
    /// </summary>
    public virtual void SetDefault(Character enemyCharacter)
    {
        var characterAttack = character.GetComponent<CharacterAttack>();
        characterAttack.CountDownHitEndurance();
        var enemyDefense = enemyCharacter.GetComponent<CharacterDefense>();
        enemyDefense.CountDownHitEndurance();
    }
}
