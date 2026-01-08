using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public static BattleController Instance { get; private set; }

    public enum BattleState
    {
        None,
        PhaseRoll,
        PhaseAction,
    }
    public BattleState State { get; set; } = BattleState.None;
    public ActionPanel CurrentPanelOfDefend { get; set; }
    //public IEnumerator Coroutine { get; set; }

    public DiceSlotAction CurrentActiveSlot { get; set; }

    public bool IsLockingAction { get; set; } = false;



    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    /// <summary>
    /// Starts the match by enabling the End Turn button.
    /// </summary>
    public void StartMatch()
    {
    }

    /// <summary>
    /// Sets the interactable objects in lists and shows the PopUpAction. 
    /// </summary>
    /// <param name="diceNumber"></param>
    /// <param name="actionPanel"></param>
    public bool SetInteractible(DiceSlotAction diceSlotAction, int diceNumber)
    {
        CurrentActiveSlot = diceSlotAction;
        return CurrentActiveSlot.Action.SetInteractible(diceNumber);
    }

    /// <summary>
    /// Shows the interactible objects.
    /// </summary>
    /// <param name="diceNumber"></param>
    /// <param name="actionPanel"></param>
    public void ShowInteractible()
    {
        CurrentActiveSlot.Action.ShowInteractible();
        IsLockingAction = true;
    }

    /// <summary>
    /// Activates the skill of the current action based on the given dice number.
    /// </summary>
    /// <param name="diceNumber"></param>
    public void ActivateSkill(int diceNumber)
    {
        CurrentActiveSlot.Action.ActivateSkill(diceNumber);
    }

    /// <summary>
    /// Deactivates the interactable objects and Sets the coroutine null.
    /// </summary>
    public void DeactivateInteractible()
    {
        FieldManager.Instance.DeactivateInteractibleFields();
        CharacterManager.Instance.DeactivateInteractibleCharacters();
        //SetCoroutineNull();
    }

    ///// <summary>
    ///// Stops coroutine if necessary, setting it to null.
    ///// </summary>
    //private void SetCoroutineNull()
    //{
    //    // Ensure that the coroutine is not null before stopping it.
    //    if (Coroutine != null)
    //    {
    //        StopCoroutine(Coroutine);
    //        Coroutine = null;
    //    }
    //}

    /// <summary>
    /// Handles the input of player on the clicked field or enemy character.
    /// </summary>
    /// <param name="clickedObject"></param>
    public void HandleInput(GameObject clickedObject)
    {
        DeactivateInteractible();

        CurrentActiveSlot.Action.ProcessInput(clickedObject);
        CurrentActiveSlot = null;
        IsLockingAction = false;
    }

    /// <summary>
    /// Updates the hit endurance for the defender character panel.
    /// </summary>
    /// <param name="characterPanel"></param>
    public void UpdateHitEnduranceForDefender(CharacterPanel characterPanel)
    {
        foreach (ActionPanel actionPanel in characterPanel.ActiveActionPanels)
        {
           actionPanel.Action.UpdateHitEnduranceForDefend();
        }
    }

    /// <summary>
    /// Ends the match.
    /// </summary>
    /// <param name="loser"></param>
    public void EndMatch(PlayerType loser)
    {
        LevelManager.Instance.SubmitWinnerFrom(loser);
       
        LevelManager.Instance.SetPhase(Phase.MatchOver);
    }
}

