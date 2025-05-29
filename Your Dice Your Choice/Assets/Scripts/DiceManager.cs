using System.Collections.Generic;
using System;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; }

    public GameObject RollPanelLeft;
    public GameObject RollPanelRight;
    public GameObject TurnDiceLeft;
    public GameObject TurnDiceRight;

    [SerializeField] private int _diceAmount = 4;
    [SerializeField] private int _rollFrequency = 10;
    [SerializeField] private float _animTimer = 0.25f;

    public int DiceAmount => _diceAmount;
    public int RollFrequency => _rollFrequency;
    public float AnimTimer => _animTimer;


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
    /// Start method.
    /// </summary>
    private void Start()
    {
        HideDiceOnPanel(RollPanelLeft);
        HideDiceOnPanel(RollPanelRight);
        TurnDiceLeft.SetActive(false);
        TurnDiceRight.SetActive(false);
    }

    /// <summary>
    /// Hides Dice on Panel.
    /// </summary>
    /// <param name="panel"></param>
    private void HideDiceOnPanel(GameObject panel)
    {
        var rollPanel = panel.GetComponent<RollPanel>();
        rollPanel.HideAllDice();
    }
    
    /// <summary>
    /// Hides Dice on Panel.
    /// </summary>
    /// <param name="panel"></param>
    private void ShowDiceOnPanel(GameObject panel)
    {
        var rollPanel = panel.GetComponent<RollPanel>();
        rollPanel.HideAllDice();
    }


}
