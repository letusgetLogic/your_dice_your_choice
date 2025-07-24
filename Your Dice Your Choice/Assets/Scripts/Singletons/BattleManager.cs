using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public ActionBase CurrentAction { get; set; }
    public IEnumerator Coroutine { get; set; }
    public bool IsDiceBeingDropped { get; set; } = false;
    public enum BattleState
    {
        None,
        PhaseRoll,
        PhaseAction,
    }
    public BattleState State { get; set; } = BattleState.None;

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
        ButtonManager.Instance.SetGameObjectActive(ButtonManager.Instance.EndTurnButton, true);
    }

    /// <summary>
    /// Sets the interactable objects in lists and shows the PopUpAction. 
    /// </summary>
    /// <param name="diceNumber"></param>
    /// <param name="actionPanel"></param>
    public void SetInteractible(int diceNumber)
    {
        CurrentAction.SetInteractible(diceNumber);
    }

    /// <summary>
    /// Shows the interactible objects.
    /// </summary>
    /// <param name="diceNumber"></param>
    /// <param name="actionPanel"></param>
    public void ShowInteractible()
    {
        CurrentAction.ShowInteractible();
    }

    /// <summary>
    /// Activates the skill of the current action based on the given dice number.
    /// </summary>
    /// <param name="diceNumber"></param>
    public void ActivateSkill(int diceNumber)
    {
        CurrentAction.ActivateSkill(diceNumber);
    }

    /// <summary>
    /// Deactivates the interactable objects and Sets the coroutine null.
    /// </summary>
    public void DeactivateInteractible()
    {
        FieldManager.Instance.DeactivateFields();
        CharacterManager.Instance.DeactivateCharacters();
        SetCoroutineNull();
    }

    /// <summary>
    /// Stops coroutine if necessary, setting it to null.
    /// </summary>
    private void SetCoroutineNull()
    {
        // Ensure that the coroutine is not null before stopping it.
        if (Coroutine != null)
        {
            StopCoroutine(Coroutine);
            Coroutine = null;
        }
    }

    /// <summary>
    /// DiceDragEvent.OnEndDrag() method calls this method to send the dice back to 
    /// its base position, when dice is not being dropped.
    /// </summary>
    /// <param name="diceMovement"></param>
    public void SendDiceBackToBase(DiceMovement diceMovement)
    {Debug.Log("SendDiceBackToBase called, IsDiceBeingDropped " + IsDiceBeingDropped);
        if (IsDiceBeingDropped)
            return;

        diceMovement.SendBackToBase();
    }

    /// <summary>
    /// Handles the input of player on the clicked field or enemy character.
    /// </summary>
    /// <param name="clickedObject"></param>
    public void HandleInput(GameObject clickedObject)
    {
        DeactivateInteractible();

        CurrentAction.HandleInput(clickedObject);
        CurrentAction = null;
    }

    /// <summary>
    /// Ends the match.
    /// </summary>
    /// <param name="loser"></param>
    public void EndMatch(PlayerType loser)
    {
        LevelManager.Instance.SubmitWinnerFrom(loser);
        PanelManager.Instance.SetPanelsInactive(true);
        ButtonManager.Instance.SetGameObjectActive(ButtonManager.Instance.EndTurnButton, false);
        LevelManager.Instance.NextPhase();
    }
}

