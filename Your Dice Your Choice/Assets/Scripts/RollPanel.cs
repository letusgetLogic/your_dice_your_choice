using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DicePrefab;
using UnityEngine.UI;
using Assets.Scripts;

public class RollPanel : MonoBehaviour
{
    public GameObject[] AllDice;
    public GameObject[] VisibleDice { get; private set; }

    public Button RollButton;

    [SerializeField] private int _rollFrequency = 10;
    [SerializeField] private float _animTimer = 0.25f;
    [SerializeField] private int _diceAmount = 4;


    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        SetInteractionFor(AllDice, false);
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
            var dice = AllDice[i];
            dice.SetActive(true);

            VisibleDice[i] = dice;

            dice.GetComponent<Dice>().InitializeIndexOf(gameObject, i);
        }
    }

    /// <summary>
    /// Hide all Dice.
    /// </summary>
    /// <param name="amount"></param>
    public void HideAllDice()
    {
        for (int i = 0; i < AllDice.Length; i++)
        {
            var dice = AllDice[i];
            dice.SetActive(false);
        }
    }

    /// <summary>
    /// Button call.
    /// </summary>
    public void Roll()
    {
        SetRollButton(false);

        RollDice.Instance.Roll(VisibleDice, _rollFrequency, _animTimer,
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
            var diceComponents = diceObject.GetComponent<DiceComponents>();
            diceComponents.SetEnabled(diceComponents.DragEvent, false);
        }
    }

    /// <summary>
    /// Sets roll button interactible true/false.
    /// </summary>
    /// <param name="value"></param>
    public void SetRollButton(bool value)
    {
        ButtonManager.Instance.SetInteractible(RollButton, value);
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
