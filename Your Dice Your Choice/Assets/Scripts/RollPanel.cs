using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DicePrefab;

public class RollPanel : MonoBehaviour
{
    public GameObject[] Dice;
    public GameObject[] DiceOnPanel { get; private set; }

    [SerializeField] private int _rollFrequency = 10;
    [SerializeField] private float _animTimer = 0.25f;
    [SerializeField] private int _diceAmount = 4;


    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        DiceOnPanel = new GameObject[_diceAmount];
    }

    /// <summary>
    /// Shows Dice.
    /// </summary>
    /// <param name="amount"></param>
    public void ShowDice()
    {
        for (int i = 0; i < _diceAmount; i++)
        {
            var dice = Dice[i];
            dice.SetActive(true);
            
            DiceOnPanel[i] = dice;
            
            dice.GetComponent<Dice>().InitializeIndexOf(gameObject, i);
        }
    }

    /// <summary>
    /// Hide all Dice.
    /// </summary>
    /// <param name="amount"></param>
    public void HideAllDice()
    {
        for (int i = 0; i < Dice.Length; i++)
        {
            var dice = Dice[i];
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
            foreach (var dice in DiceOnPanel)
            {
                var diceScript = dice.GetComponent<Dice>();
                int sideIndex = UnityEngine.Random.Range(1, diceScript.DiceSide.Length);
                diceScript.InitializeSide(sideIndex);
            }

            yield return new WaitForSeconds(_animTimer); 
        }
    }

    /// <summary>
    /// Sets the slot on the panel null.
    /// </summary>
    /// <param name="index"></param>
    public void SetNull(int index)
    {
        DiceOnPanel[index] = null;
    }

    /// <summary>
    /// Sets the dice on the slot on the panel.
    /// </summary>
    /// <param name="index"></param>
    public void SetInstance(GameObject dice, int index)
    {
        DiceOnPanel[index] = dice;
    }
}
