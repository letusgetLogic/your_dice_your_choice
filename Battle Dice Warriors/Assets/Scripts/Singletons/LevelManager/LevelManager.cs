using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private GameObject _developTool;
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
    }

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        _developTool.SetActive(GameManager.Instance.ModusDevelopment);
       
        SetDataIndex();
        StartPhases();
    }

    /// <summary>
    /// Sets the data index based on the number of characters.
    /// </summary>
    private void SetDataIndex()
    {
        if (GameManager.Instance.CharacterAmount == 0)
            return;
        else
            _dataIndex = GameManager.Instance.CharacterAmount - 1;
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
            SetPhase(Phase.Initialization);
        }
        else
        {
            throw new System.Exception("LevelManager.Instance.Data == null");
        }
    }

    /// <summary>
    /// Sets the current phase to the specified phase.
    /// </summary>
    /// <param name="phase"></param>
    public void SetPhase(Phase phase)
    {
        CurrentPhase = phase;
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
                //MatchIntroController.Instance.Play(); * Intro Animation not running correctly in build. 
                return;

            case Phase.Initialization:
                PhaseInitialization();
                SetPhase(Phase.SetFirstTurn);
                return;

            case Phase.SetFirstTurn:
                SetFirstTurn.Instance.InitializePanels();
                SetFirstTurn.Instance.SetTurnDiceAndPanel();
                SetFirstTurn.Instance.RollTurnDice();
                return;

            case Phase.Battle:
                //MatchIntroController.Instance.gameObject.SetActive(false);
                BattleController.Instance.StartMatch();
                return;

            case Phase.MatchOver:
                PanelManager.Instance.SetPanelsInactive(true);
                ButtonManager.Instance.SetGameObjectActive(ButtonManager.Instance.EndTurnButton, false);

                _matchOver.SetActive(true);
                _matchOver.GetComponent<MatchOverController>().Congratulate(Winner.Name);
                return;

            case Phase.WaitForInput:
                ButtonManager.Instance.SetGameObjectActive(
                    ButtonManager.Instance.NewMatchButton, true);
                return;
        }
    }

    /// <summary>
    /// Phase Initialization.
    /// </summary>
    private void PhaseInitialization()
    {
        PanelManager.Instance.SetPanels();
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
                PlayerBase.Instance.Create(GameManager.Instance.PlayerLeftName, PlayerType.PlayerLeft);
                PlayerBase.Instance.Create(GameManager.Instance.PlayerRightName, PlayerType.PlayerRight);
                break;

            case MatchType.DuelAI:
                break;
        }
    }

    /// <summary>
    /// Determines the winner based on the specified losing player and 
    /// updates the <see cref="Winner"/> property.
    /// </summary>
    /// <remarks>The method retrieves the winner by invoking 
    /// the <see cref="PlayerBase.Instance.GetWinner"/> method 
    /// with the provided <paramref name="loser"/>. 
    /// Ensure that the <paramref name="loser"/> parameter is valid
    /// and corresponds to a recognized player type.</remarks>
    /// <param name="loser">The player who lost the game. 
    /// This value is used to determine the winner.</param>
    public void SubmitWinnerFrom(PlayerType loser)
    {
        Winner = PlayerBase.Instance.GetWinner(loser);
    }

}
