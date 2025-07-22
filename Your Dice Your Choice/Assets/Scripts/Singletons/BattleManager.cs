using Assets.Scripts;
using Assets.Scripts.ActionDatas;
using Assets.Scripts.ActionPanelPrefab;
using Assets.Scripts.CharacterPrefab;
using Assets.Scripts.DicePrefab;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public ActionBase CurrentAction { get; set; }
    public IEnumerator Coroutine { get; set; }
    public bool IsDiceBeingDropped { get; set; } = false;

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
        ButtonManager.Instance.SetActive(ButtonManager.Instance.EndTurnButton, true);
    }

    /// <summary>
    /// Checks if the coroutine is running and stops it if necessary, setting it to null.
    /// </summary>
    public void CheckCoroutine()
    {
        // Ensure that the coroutine is not null before stopping it.
        if (Coroutine != null)
        {
            StopCoroutine(Coroutine);
            Coroutine = null;
        }
    }

    /// <summary>
    /// Shows the interactible objects.
    /// </summary>
    /// <param name="diceNumber"></param>
    /// <param name="actionPanel"></param>
    public void ShowInteractible(int diceNumber, ActionPanel actionPanel)
    {
        if (CurrentAction == null)
            return;

        CurrentAction.ShowPopUpAction(diceNumber, actionPanel);
        CurrentAction.SetInteractible(diceNumber);
        CurrentAction.ShowInteractible();
        CurrentAction.ActivateSkill(diceNumber);
    }

    /// <summary>
    /// Deactivates the interactible objects. Being called from the DiceSlotAction script.
    /// </summary>
    /// <param name="actionPanel"></param>
    public void DeactivateInteractible()
    {
        if (CurrentAction == null) 
            return;
        
        CurrentAction.SetDefault();
        CurrentAction.DeactivateInteractible();
        CurrentAction = null;
    }

    /// <summary>
    /// DiceDragEvent.OnEndDrag() method calls this method to send the dice back to 
    /// its base position, when dice is not being dropped.
    /// </summary>
    /// <param name="diceMovement"></param>
    public void SendDiceBackToBase(DiceMovement diceMovement)
    {
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
        CurrentAction.DeactivateInteractible();

        var action = CurrentAction;
        action.HandleInput(clickedObject);

        CurrentAction = null;
    }

    /// <summary>
    /// Ends the match.
    /// </summary>
    /// <param name="loser"></param>
    public void EndMatch(PlayerType loser)
    {
        LevelManager.Instance.SubmitWinnerFrom(loser);
        ButtonManager.Instance.SetActive(ButtonManager.Instance.EndTurnButton, false);
        LevelManager.Instance.NextPhase();
    }
}

