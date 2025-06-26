using System;
using System.Collections;
using System.ComponentModel;
using Assets.Scripts.DicePrefab;
using Assets.Scripts.LevelDatas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public PlayerType Turn { get; private set; }
    public PlayerType[] TurnStates { get; private set; }

    [SerializeField] private GameObject _setTurnShaderObject;
    [SerializeField] private GameObject _setTurnObject;
    [SerializeField] private TextMeshProUGUI _setTurnShaderText;
    [SerializeField] private TextMeshProUGUI _setTurnText;

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

        TurnStates = new PlayerType[]
        {
            PlayerType.PlayerLeft,
            PlayerType.PlayerRight
        };

        Turn = PlayerType.None;

        _setTurnShaderObject.SetActive(false);
        _setTurnObject.SetActive(false);
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
    /// Deactivates the roll panel for the last player.
    /// </summary>
    /// <param name="player"></param>
    private void DeactivateRollPanel(Player lastPlayer)
    {
        if (LevelManager.Instance.CurrentPhase == Phase.Battle)
        {
            lastPlayer.RollPanel.SetInteractionFor(lastPlayer.RollPanel.VisibleDice, false);
            lastPlayer.RollPanel.SetRollButton(false);
            lastPlayer.RollPanel.SendBackToBase(lastPlayer.RollPanel.VisibleDice);
            lastPlayer.RollPanel.SetDefaultNumber(lastPlayer.RollPanel.VisibleDice);
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
