using Assets.Scripts;
using Assets.Scripts.ActionDatas;
using Assets.Scripts.ActionPanelPrefab;
using Assets.Scripts.CharacterPrefab;
using Assets.Scripts.DicePrefab;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public ActionBase CurrentAction { get; private set; }

    public IEnumerator Coroutine { get; private set; }
    public bool IsNextCoroutineActive { get; private set; }

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
    /// Sets the coroutine to be executed.
    /// </summary>
    /// <param name="coroutine">The coroutine to assign. Cannot be null.</param>
    public void SetCoroutine(IEnumerator coroutine)
    {
        Coroutine = coroutine;
    }

    /// <summary>
    /// Sets the data.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="characterObject"></param>
    public void SetData(ActionBase action)
    {
         CurrentAction = action;
        Debug.Log(action.GetType().Name + " is set as current action.");
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

        CurrentAction.SetDescriptionOf(actionPanel, diceNumber);
        CurrentAction.SetInteractible(diceNumber);
        CurrentAction.ShowInteractible();
        CurrentAction.ActivateSkill(diceNumber);
    }

    /// <summary>
    /// Deactivates the interactible objects. Being called from the DiceSlotAction script.
    /// </summary>
    /// <param name="actionPanel"></param>
    public void DeactivateInteractible(ActionPanel actionPanel)
    {
        if (CurrentAction == null) 
            return;
        
        DeactivateInteractibleOfCurrentAction();
        CurrentAction.SetDescriptionOf(actionPanel, 0);
        CurrentAction.SetDefault();
    }

    /// <summary>
    /// Handles the input of player on the clicked field or enemy character.
    /// </summary>
    /// <param name="clickedObject"></param>
    public void HandleInput(GameObject clickedObject)
    {
        DeactivateInteractibleOfCurrentAction();

        var action = CurrentAction;
        action.HandleInput(clickedObject);

        CurrentAction = null;
    }

    /// <summary>
    /// Deactivates the interactable objects of the current action.
    /// </summary>
    public void DeactivateInteractibleOfCurrentAction()
    {
        CurrentAction.DeactivateInteractible();
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

