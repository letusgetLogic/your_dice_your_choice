using Assets.Scripts.LevelDatas;
using Assets.Scripts.MatchIntro;
using Assets.Scripts.MatchOver;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private GameObject _destroyEditorWeapon;
    [SerializeField] private GameObject _matchOver;

    [SerializeField] private LevelData[] _dataPrefab;
    [SerializeField] private int _dataIndex;

    public LevelData Data => _dataPrefab[_dataIndex];

    public Phase CurrentPhase { get; private set; }
    public Player Winner { get; private set; }

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

        Destroy(_destroyEditorWeapon);
    }

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        Data.MatchType = MatchType.Duel;
        StartPhases();
    }

    /// <summary>
    /// Starts the phases.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    private void StartPhases()
    {
        if (Data != null)
        {
            CurrentPhase = Phase.Intro;

            OnPhase();
        }
        else
        {
            throw new System.Exception("LevelManager.Instance.Data == null");
        }
    }

    /// <summary>
    /// Switchs to the next phase.
    /// </summary>
    public void NextPhase()
    {
        int nextEnumIndex = (int)CurrentPhase + 1;
        CurrentPhase = (Phase)nextEnumIndex;
        OnPhase();
    }

    /// <summary>
    /// Runs method in the current phase.
    /// </summary>
    /// <exception cref="System.Exception"></exception>
    private void OnPhase()
    {
        switch (CurrentPhase)
        {
            case Phase.None:
                throw new System.Exception("CurrentPhase = Phase.None");

            case Phase.Intro:
                MatchIntroController.Instance.Play();
                return;

            case Phase.Initialization:
                PhaseInitialization();
                return;

            case Phase.Battle:
                BattleManager.Instance.StartMatch();
                return;

            case Phase.MatchOver:
                _matchOver.SetActive(true);
                MatchOverController.Instance.Congratulate(Winner);
                return;

            case Phase.WaitForInput:
                ButtonManager.Instance.SetActive(ButtonManager.Instance.NewMatchButton, true);
                return;
        }
    }

    /// <summary>
    /// Phase Initialization.
    /// </summary>
    private void PhaseInitialization()
    {
        FieldManager.Instance.InitializeFields();

        MapGenerator.Instance.GenerateMapFrom(Data);

        CreatePlayer();
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

            case MatchType.Duel:
                PlayerBase.Instance.Create("Player 1", PlayerType.PlayerLeft);
                PlayerBase.Instance.Create("Player 2", PlayerType.PlayerRight);
                break;

            case MatchType.DuelAI:
                break;
        }
    }

    /// <summary>
    /// Determines the winner based on the specified losing player and updates the <see cref="Winner"/> property.
    /// </summary>
    /// <remarks>The method retrieves the winner by invoking the <see cref="PlayerBase.Instance.GetWinner"/>
    /// method with the provided <paramref name="loser"/>. Ensure that the <paramref name="loser"/> parameter is valid
    /// and corresponds to a recognized player type.</remarks>
    /// <param name="loser">The player who lost the game. This value is used to determine the winner.</param>
    public void SubmitWinnerFrom(PlayerType loser)
    {
        Winner = PlayerBase.Instance.GetWinner(loser);
    }
}
