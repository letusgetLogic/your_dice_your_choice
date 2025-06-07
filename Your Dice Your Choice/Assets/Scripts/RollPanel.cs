using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DicePrefab;

public class RollPanel : MonoBehaviour
{
    public GameObject[] AllDice;
    public GameObject[] VisibleDice { get; private set; }

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
    /// Sets the dice active true/false.
    /// </summary>
    /// <param name="dice"></param>
    /// <param name="value"></param>
    private void SetInteractionFor(GameObject[] dice, bool value)
    {
        foreach (GameObject d in dice)
        {
            d.GetComponent<DiceManager>().SetDragEventEnable(value);
        }
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
    /// Rolls dice.
    /// </summary>
    public void RollDice()
    {
        StartCoroutine(AnimateDiceRoll()); 
    }

    /// <summary>
    /// Shows all dice per roll.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AnimateDiceRoll()
    {
        for (int i = 0; i < _rollFrequency; i++)
        {
            foreach (var dice in VisibleDice)
            {
                var diceScript = dice.GetComponent<Dice>();
                int sideIndex = UnityEngine.Random.Range(1, diceScript.DiceSide.Length);
                diceScript.InitializeSide(sideIndex);
            }

            yield return new WaitForSeconds(_animTimer); 
        }

        SetInteractionFor(VisibleDice, true);
    }

    /// <summary>
    /// Sets the slot on the panel null.
    /// </summary>
    /// <param name="index"></param>
    public void SetNull(int index)
    {
        VisibleDice[index] = null;
    }

    /// <summary>
    /// Sets the dice on the slot on the panel.
    /// </summary>
    /// <param name="index"></param>
    public void SetInstance(GameObject dice, int index)
    {
        VisibleDice[index] = dice;
    }
}
