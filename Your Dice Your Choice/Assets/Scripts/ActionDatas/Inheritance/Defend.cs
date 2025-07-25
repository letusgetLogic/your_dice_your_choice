﻿using UnityEngine;
public abstract class Defend : ActionBase
{
    public static readonly string DefaultDescription =
        "Move the dice over here to get more information";

    public AllowedDiceNumber AllowedDiceNumber { get; protected set; }
    public int HitEndurance { get; protected set; }
    public int RoundEndurance { get; protected set; }

    public Defend(ActionPanel actionPanel, GameObject characterObject) :
        base(actionPanel, characterObject)
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

    public override void ProcessInput(GameObject fieldObject)
    {}

    public override void UpdateHitEnduranceForDefend()
    {
        CountDownHitEndurance();
    }

    // <summary>
    /// Counts down the HitEndurance and resets if it reaches zero.
    /// </summary>
    private void CountDownHitEndurance()
    {
        if (HitEndurance > 0)
        {
            HitEndurance--;
        }
        if (HitEndurance == 0)
        {
            characterObject.GetComponent<CharacterDefense>().SetDefault();
            RoundEndurance = 0;
        }
        actionPanel.UpdateEndurance(HitEndurance, RoundEndurance);
    }

    /// <summary>
    /// Counts down the RoundEndurance and resets if it reaches zero.
    /// </summary>
    public override void CountDownRoundEndurance(PlayerType lastTurn)
    {
        var playerType = character.Player.PlayerType;

        if (playerType != lastTurn)
        {
            if (RoundEndurance > 0)
            {
                RoundEndurance--;
            }
            if (RoundEndurance == 0)
            {
                characterObject.GetComponent<CharacterDefense>().SetDefault();
                HitEndurance = 0;
            }
            actionPanel.UpdateEndurance(HitEndurance, RoundEndurance);
        }
    }
}
