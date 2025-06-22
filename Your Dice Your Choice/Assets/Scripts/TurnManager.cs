using System;
using System.Collections;
using Assets.Scripts.DicePrefab;
using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public PlayerType Turn { get; private set; }
    public GameObject[] VisibleDice { get; private set; }

    public GameObject PlayerPanelLeft;
    public GameObject PlayerPanelRight;

    [SerializeField] private GameObject _turnDiceLeft;
    [SerializeField] private GameObject _turnDiceRight;

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
        Debug.Log("RollDice");
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
        Debug.Log("SetFirstTurn");
        _turnDiceLeft.SetActive(false);
        _turnDiceRight.SetActive(false);

        MatchIntroManager.Instance.EndPhase();
        SetTurn(turnState);
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
    /// Sets turn.
    /// </summary>
    /// <param name="state"></param>
    public void SetTurn(PlayerType state)
    {
        Turn = state;
        SetOthers(state);
    }

    /// <summary>
    /// Sets others.
    /// </summary>
    /// <param name="state"></param>
    private void SetOthers(PlayerType state)
    {
        Player player = null;
        Player opponent = null;

        if (PlayerBase.Instance.PlayerLeft.PlayerType == state)
        {
            player = PlayerBase.Instance.PlayerLeft;
            opponent = PlayerBase.Instance.PlayerRight;
        }
        else if (PlayerBase.Instance.PlayerRight.PlayerType == state)
        {
            player = PlayerBase.Instance.PlayerRight;
            opponent = PlayerBase.Instance.PlayerLeft;
        }

        SetTurnText(player);
        SetRollPanel(player);
    }

    private void SetTurnText(Player player)
    {
        _setTurnShaderText.text = player.Name + " is turn!";
        _setTurnObject.SetActive(true);

        StartCoroutine(TextEnd());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator TextEnd()
    {
        yield return new WaitForSeconds(2);

        _setTurnShaderText.text = "";
        _setTurnObject.SetActive(false);


    }

    /// <summary>
    /// Sets the roll panel.
    /// </summary>
    /// <param name="player"></param>
    private void SetRollPanel(Player player)
    {
        player.RollPanel.ShowDice();
        player.RollPanel.SetRollButton(true);
    }
}
