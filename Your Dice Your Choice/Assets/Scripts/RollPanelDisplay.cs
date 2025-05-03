using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollPanelDisplay : MonoBehaviour
{
    public GameObject DicePrefab;
    public GameObject[] DiceSlot;

    [SerializeField] private int _diceAmount;  

    [NonSerialized] public List<GameObject> DiceOnPanel = new List<GameObject>();

    /// <summary>
    /// Start method.
    /// </summary>
    void Start()
    {
        for (int i = 0; i < _diceAmount; i++)
        {
            var dice = Instantiate(DicePrefab, DiceSlot[i].transform.position, Quaternion.identity);
            DiceOnPanel.Add(dice);
        }
    }

    /// <summary>
    /// Rolls dice.
    /// </summary>
    public void RollDice()
    {
        foreach(var dice in DiceOnPanel)
        {
            var diceScript = dice.GetComponent<Dice>();
            diceScript.InitializeSide(UnityEngine.Random.Range(1, diceScript.DiceSide.Length));
        }
    }
}
