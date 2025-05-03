using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollPanelDisplay : MonoBehaviour
{
   
    public GameObject[] Dice;

    [SerializeField] private int _diceAmount;
    [SerializeField] private int _rollFrequency;
    [SerializeField] private float _animTimer = 0.25f;

    [NonSerialized] public List<GameObject> DiceOnPanel = new List<GameObject>();

 

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < _diceAmount; i++)
        {
            var dice = Dice[i];
            dice.SetActive(true);
            DiceOnPanel.Add(dice);
        }
    }

    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        
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
}
