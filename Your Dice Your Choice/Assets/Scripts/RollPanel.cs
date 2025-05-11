using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollPanel : MonoBehaviour
{
    public GameObject[] Dice;

    [NonSerialized] public List<GameObject> DiceOnPanel = new List<GameObject>();


    /// <summary>
    /// Shows Dice.
    /// </summary>
    /// <param name="amount"></param>
    public void ShowDice(int  amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var dice = Dice[i];
            dice.SetActive(true);
            DiceOnPanel.Add(dice);
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
            DiceOnPanel.RemoveAll(x => !x);
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
        for (int i = 0; i < DiceManager.Instance.RollFrequency; i++)
        {
            foreach (var dice in DiceOnPanel)
            {
                var diceScript = dice.GetComponent<Dice>();
                int sideIndex = UnityEngine.Random.Range(1, diceScript.DiceSide.Length);
                diceScript.InitializeSide(sideIndex);
            }

            yield return new WaitForSeconds(DiceManager.Instance.AnimTimer); 
        }
    }
}
