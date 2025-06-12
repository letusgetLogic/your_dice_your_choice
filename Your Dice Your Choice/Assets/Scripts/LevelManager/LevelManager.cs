using System;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.LevelData;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance {  get; private set; }

    [SerializeField] private LevelData[] _dataPrefab;
    [SerializeField] private int _dataIndex;
    
    public LevelData Data => _dataPrefab[_dataIndex];

    public Phase CurrentPhase { get; private set; }
    public bool IsCheckingPhase { get; private set; }

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

        SetMatch();
    }

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        if (Data != null)
        {
            CurrentPhase = Phase.Intro;
            IsCheckingPhase = true;
            
           
            
            //TurnManager.Instance.PhaseSetFirstTurn();
        }
        else
        {
            throw new System.Exception("LevelManager.Instance.Data == null");
        }
    }

    /// <summary>
    /// Update method.
    /// </summary>
    /// <exception cref="System.Exception"></exception>
    private void Update()
    {
        if (IsCheckingPhase)
        {
            IsCheckingPhase = false;

            switch (CurrentPhase)
            {
                case Phase.None:
                    throw new System.Exception("CurrentPhase = Phase.None");

                case Phase.Intro:
                    MatchIntroManager.Instance.Play();
                    return;

                case Phase.Initialization:
                    PhaseInitialization();
                    return;

                case Phase.Battle:
                    BattleManager.Instance.StartMatch();
                    return;

            }
        }
    }

    /// <summary>
    /// Switchs to the next phase.
    /// </summary>
    public void NextPhase()
    {
        int nextEnumIndex = (int)CurrentPhase + 1;
        CurrentPhase = (Phase)nextEnumIndex;
        IsCheckingPhase = true;
    }

    /// <summary>
    /// Set the match.
    /// </summary>
    private void SetMatch()
    {
        Data.MatchType = MatchType.Duell;
    }

    /// <summary>
    /// Phase Initialization.
    /// </summary>
    private void PhaseInitialization()
    {
        FieldManager.Instance.InitializeFields();

        LevelGenerator.Instance.GenerateFrom(Data);

        CreatePlayer();

        PanelManager.Instance.ShowRollPanels();
    }

    /// <summary>
    /// Creates the players in the battle.
    /// </summary>
    /// <exception cref="System.Exception"></exception>
    private void CreatePlayer()
    {
        switch (Data.MatchType)
        {
            case MatchType.None:
                throw new System.Exception("Match Type is None.");

            case MatchType.Singleplayer:
                break;

            case MatchType.Duell:
                PlayerBase.Create(PlayerBase.PlayerLeft, "Player 1", TurnState.PlayerLeft);
                PlayerBase.Create(PlayerBase.PlayerRight, "Player 2", TurnState.PlayerRight);
                break;

            case MatchType.DuellAI:
                break;
        }
    }
}
