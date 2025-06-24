using System;
using System.Collections;
using System.ComponentModel;
using Assets.Scripts.DicePrefab;
using Assets.Scripts.LevelDatas;
using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public PlayerType Turn { get; private set; }
    public PlayerType[] TurnStates { get; private set; }
    public GameObject[] VisibleDice { get; private set; }

    public GameObject PlayerPanelLeft;
    public GameObject PlayerPanelRight;

    [SerializeField] private GameObject _turnDiceLeft;
    [SerializeField] private GameObject _turnDiceRight;

    [SerializeField] private GameObject _setTurnShaderObject;
    [SerializeField] private GameObject _setTurnObject;
    [SerializeField] private TextMeshProUGUI _setTurnShaderText;
    [SerializeField] private TextMeshProUGUI _setTurnText;

    [SerializeField] private int _rollFrequency = 10;
    [SerializeField] private float _rollTimer = 0.25f;

    private Vector3 _startScale;

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

        Turn = PlayerType.None;

        TurnStates = new PlayerType[]
        {
            PlayerType.PlayerLeft,
            PlayerType.PlayerRight
        };

        _setTurnShaderObject.SetActive(false);
        _setTurnObject.SetActive(false);

        _startScale = _turnDiceLeft.GetComponent<RectTransform>().localScale;
    }

    /// <summary>
    /// Sets the turn dice and the player panel at the start state.
    /// </summary>
    public void SetDiceAndPanel()
    {
        _turnDiceLeft.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        _turnDiceRight.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        _turnDiceLeft.SetActive(true);
        _turnDiceRight.SetActive(true);

        PlayerPanelLeft.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        PlayerPanelRight.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// Rolls dice.
    /// </summary>
    public void RollDice()
    {
        VisibleDice = new GameObject[]
        {
                _turnDiceLeft,
                _turnDiceRight,
        };

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
            foreach (var dice in VisibleDice)
            {
                var diceScript = dice.GetComponent<Dice>();
                int sideIndex = UnityEngine.Random.Range(1, diceScript.DiceSide.Length);
                diceScript.InitializeSide(sideIndex);
            }

            yield return new WaitForSeconds(_rollTimer);
        }

        int numberLeft = _turnDiceLeft.GetComponent<Dice>().CurrentNumber;
        int numberRight = _turnDiceRight.GetComponent<Dice>().CurrentNumber;

        if (numberLeft == numberRight)
            StartCoroutine(Reroll());
        else
        {
            var turnState = numberLeft > numberRight ? PlayerType.PlayerLeft : PlayerType.PlayerRight;
            StartCoroutine(SetFirstTurn(turnState));
        }
    }

    /// <summary>
    /// Checks the turn dice.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Reroll()
    {
        yield return new WaitForSeconds(1);

        StartCoroutine(AnimateDiceRoll());
    }

    /// <summary>
    /// Sets the first turn.
    /// </summary>
    /// <param name="turnState"></param>
    /// <returns></returns>
    private IEnumerator SetFirstTurn(PlayerType turnState)
    {
        yield return new WaitForSeconds(1);

        MatchIntroManager.Instance.LeftIntroShaderText.gameObject.SetActive(false);
        MatchIntroManager.Instance.RightIntroShaderText.gameObject.SetActive(false);

        SwitchTurn(turnState);

        StartCoroutine(EndMatchIntro());
    }

    private IEnumerator EndMatchIntro()
    {
        yield return new WaitForSeconds(1);

        _turnDiceLeft.SetActive(false);
        _turnDiceRight.SetActive(false);

        MatchIntroManager.Instance.EndPhase();
    }

    /// <summary>
    /// Scales up the alpha values. 
    /// </summary>
    public void ScaleUp(float ratio)
    {
        Vector3 diceScale = _startScale * ratio;
        Vector3 panelScale = new Vector3(ratio, ratio, ratio);

        if (ratio >= 1)
        {
            _turnDiceLeft.GetComponent<RectTransform>().localScale = _startScale;
            return;
        }

        _turnDiceLeft.GetComponent<RectTransform>().localScale = diceScale;
        _turnDiceRight.GetComponent<RectTransform>().localScale = diceScale;

        PlayerPanelLeft.GetComponent<RectTransform>().localScale = panelScale;
        PlayerPanelRight.GetComponent<RectTransform>().localScale = panelScale;
    }

    /// <summary>
    /// Switchs turn.
    /// </summary>
    /// <param name="state"></param>
    public void SwitchTurn()
    {
        var lastTurn = Turn;
        Turn = PlayerType.None;
        SetOthers(lastTurn, GetOtherTurnFrom(lastTurn));
    }

    /// <summary>
    /// Switchs turn for the next player.
    /// </summary>
    /// <param name="lastState"></param>
    public void SwitchTurn(PlayerType nextTurn)
    {
        SetOthers(GetOtherTurnFrom(nextTurn), nextTurn);
    }

    /// <summary>
    /// Sets others.
    /// </summary>
    /// <param name="lastState"></param>
    private void SetOthers(PlayerType lastTurn, PlayerType nextTurn)
    {
        Player lastPlayer = null;
        Player nextPlayer = null;

        if (PlayerBase.Instance.PlayerLeft.PlayerType == lastTurn)
        {
            lastPlayer = PlayerBase.Instance.PlayerLeft;
            nextPlayer = PlayerBase.Instance.PlayerRight;
        }
        else if (PlayerBase.Instance.PlayerRight.PlayerType == lastTurn)
        {
            lastPlayer = PlayerBase.Instance.PlayerRight;
            nextPlayer = PlayerBase.Instance.PlayerLeft;
        }

        DeactivateRollPanel(lastPlayer);
        SetTurnText(nextPlayer, nextTurn);
    }

    /// <summary>
    /// Deactivates the roll panel for player in last turn.
    /// </summary>
    /// <param name="player"></param>
    private void DeactivateRollPanel(Player opponent)
    {
        if (LevelManager.Instance.CurrentPhase == Phase.Battle)
        {
            opponent.RollPanel.SetInteractionFor(VisibleDice, false);
            opponent.RollPanel.SetRollButton(false);
            opponent.RollPanel.SendBackToBase(opponent.RollPanel.VisibleDice);
            opponent.RollPanel.SetDefaultNumber(opponent.RollPanel.VisibleDice);
        }
    }

    /// <summary>
    /// Sets and shows text.
    /// </summary>
    /// <param name="nextPlayer"></param>
    private void SetTurnText(Player nextPlayer, PlayerType nextTurn)
    {
        _setTurnShaderText.text = nextPlayer.Name + " is turn!";
        _setTurnText.text = nextPlayer.Name + " is turn!";
        _setTurnShaderObject.SetActive(true);
        _setTurnObject.SetActive(true);

        StartCoroutine(EndTurnText(nextPlayer, nextTurn));
    }

    /// <summary>
    /// Hides text.
    /// </summary>
    /// <returns></returns>
    private IEnumerator EndTurnText(Player nextPlayer, PlayerType nextTurn)
    {
        yield return new WaitForSeconds(1);

        _setTurnShaderText.text = "";
        _setTurnText.text = "";
        _setTurnShaderObject.SetActive(false);
        _setTurnObject.SetActive(false);

        SetTurn(nextPlayer, nextTurn);
    }

    /// <summary>
    /// Sets turn.
    /// </summary>
    /// <param name="state"></param>
    private void SetTurn(Player nextPlayer, PlayerType nextTurn)
    {
        Turn = nextTurn;

        ActivateRollPanel(nextPlayer);
        ButtonManager.Instance.SetInteractible(ButtonManager.Instance.EndTurnButton, true);
    }

    /// <summary>
    /// Activates the roll panel for player in this turn.
    /// </summary>
    /// <param name="player"></param>
    private void ActivateRollPanel(Player player)
    {
        player.RollPanel.ShowDice();
        player.RollPanel.SetRollButton(true);
    }

    /// <summary>
    /// Returns other turn.
    /// </summary>
    /// <param name="targetTurn"></param>
    /// <returns></returns>
    private PlayerType GetOtherTurnFrom(PlayerType targetTurn)
    {
        return targetTurn == TurnStates[0] ? TurnStates[1] : TurnStates[0];
    }

}
