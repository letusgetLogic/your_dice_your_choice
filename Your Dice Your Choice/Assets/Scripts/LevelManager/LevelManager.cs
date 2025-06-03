using System;
using System.Collections;
using Assets.Scripts;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance {  get; private set; }

    [SerializeField] private LevelData[] _dataPrefab;
    [SerializeField] private int _dataIndex;
    
    public LevelData Data => _dataPrefab[_dataIndex];

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
            MatchIntroManager.Instance.Play();
            StartCoroutine(PhaseInitialization());
            
            //TurnManager.Instance.PhaseSetFirstTurn();
        }
        else
        {
            throw new System.Exception("LevelManager.Instance.Data == null");
        }

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
    private IEnumerator PhaseInitialization()
    {
        yield return new WaitForSeconds(MatchIntroManager.Instance.IntroTime);

        MatchIntroManager.Instance.SetIntroInactive();

        FieldManager.Instance.InitializeFields();

        LevelGenerator.Instance.GenerateFrom(Data);

        CreatePlayer();

        PanelManager.Instance.ShowRollPanels();

        ButtonManager.Instance.ButtonOn(ButtonManager.Instance.RollButtonLeft);
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
