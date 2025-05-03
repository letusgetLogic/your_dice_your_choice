using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollPanelDisplay : MonoBehaviour
{
    public GameObject DicePrefab;
    public RectTransform[] DiceSlot;

    [SerializeField] private int _diceAmount;  

    [NonSerialized] public List<GameObject> DiceOnPanel = new List<GameObject>();

    /// <summary>
    /// Start method.
    /// </summary>
    void Start()
    {
        for (int i = 0; i < _diceAmount; i++)
        {
            var position = GetMiddlePoint(DiceSlot[i]);
            var dice = Instantiate(DicePrefab, position, Quaternion.identity);
            DiceOnPanel.Add(dice);
        }
    }

    private Vector3 GetMiddlePoint(RectTransform slot)
    {
        return Camera.main.ScreenToWorldPoint(slot.position);
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
