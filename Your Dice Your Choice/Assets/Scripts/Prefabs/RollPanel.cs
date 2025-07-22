using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DicePrefab;
using UnityEngine.UI;
using Assets.Scripts;

public class RollPanel : MonoBehaviour
{
    [SerializeField] private Button _rollButton;
    [SerializeField] private int _diceAmount = 4;
    [SerializeField] private GameObject[] _allDice;

    public GameObject[] VisibleDice { get; private set; }
    public Button RollButton => _rollButton;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        SetInteractionFor(_allDice, false);
        VisibleDice = new GameObject[_diceAmount];
    }

    /// <summary>
    /// Shows Dice.
    /// </summary>
    /// <param name="amount"></param>
    public void ShowDice()
    {
        for (int i = 0; i < _diceAmount; i++)
        {
            var diceObject = _allDice[i];
            diceObject.SetActive(true);
            
            VisibleDice[i] = diceObject;

            var dice = diceObject.GetComponent<Dice>();
            dice.InitializeSide(dice.DefaultNumber);
            dice.InitializeIndexOf(gameObject, i);

            var diceDisplay = diceObject.GetComponent<DiceDisplay>();
            diceDisplay.SetDefault();
        }
    }

    /// <summary>
    /// Hide all Dice.
    /// </summary>
    /// <param name="amount"></param>
    public void HideAllDice()
    {
        for (int i = 0; i < _allDice.Length; i++)
        {
            var dice = _allDice[i];
            dice.SetActive(false);
        }
    }

    /// <summary>
    /// Button call.
    /// </summary>
    public void Roll()
    {
        ButtonClickAnimation.Instance.ScaleSize(RollButton);

        ButtonManager.Instance.SetButtonInteractible(RollButton, false);

        RollDice.Instance.Roll(
            VisibleDice,
            RollDice.Instance.RollFrequency,
            RollDice.Instance.AnimTimer,
            SetInteraction);
    }

    /// <summary>
    /// Sets interaction.
    /// </summary>
    private void SetInteraction()
    {
        SetInteractionFor(VisibleDice, true);
    }

    /// <summary>
    /// Sets the dice active true/false.
    /// </summary>
    /// <param name="diceObjects"></param>
    /// <param name="value"></param>
    public void SetInteractionFor(GameObject[] diceObjects, bool value)
    {
        foreach (GameObject diceObject in diceObjects)
        {
            var dice = diceObject.GetComponent<Dice>();
            var diceDragEvent = diceObject.GetComponent<DiceDragEvent>();
            dice.SetComponentEnabled(diceDragEvent, value);
            
            var diceDisplay = diceObject.GetComponent<DiceDisplay>();
            
            if (value == false)
                diceDisplay.SetAlphaDown();
        }
    }

    /// <summary>
    /// Sends the dice back to base.
    /// </summary>
    /// <param name="diceObjects"></param>
    /// <param name="value"></param>
    public void SendBackToBase(GameObject[] diceObjects)
    {
        foreach (GameObject diceObject in diceObjects)
        {
            diceObject.GetComponent<DiceMovement>().SendBackToBase();
        }
    }

    /// <summary>
    /// Sets the default number to dice.
    /// </summary>
    /// <param name="diceObjects"></param>
    /// <param name="value"></param>
    public void SetDefaultNumber(GameObject[] diceObjects)
    {
        foreach (GameObject diceObject in diceObjects)
        {
            var dice = diceObject.GetComponent<Dice>();
            dice.InitializeSide(dice.DefaultNumber);
        }
    }

}
