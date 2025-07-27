using System;
using System.Collections;
using UnityEngine;

public class MatchOverController : MonoBehaviour
{
    public static MatchOverController Instance { get; private set; }

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
    /// Update method.
    /// </summary>
    private void Update()
    {
        switch (MatchOverModel.Instance.CurrentState)
        {
            case MatchOverModel.PlayState.None:
                return;

            case MatchOverModel.PlayState.Act1:
                PlayAct1();
                return;

            case MatchOverModel.PlayState.Act2:
                PlayAct2();
                return;
        }
    }

    /// <summary>
    /// Plays the intro.
    /// </summary>
    public void Congratulate(string playerName)
    {
        MatchOverModel.Instance.SetPlayState(MatchOverModel.PlayState.None);
        MatchOverView.Instance.SetText(playerName);
        MatchOverView.Instance.SetTextArrayActive(true);
        StartCoroutine(SetAct());
    }

    /// <summary>
    /// Delays the act setting wo waiting the start method in TextColorSeting.cs.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetAct()
    {
        yield return new WaitForSeconds(0.1f);

        MatchOverModel.Instance.SetPlayState(MatchOverModel.PlayState.Act1);
    }

    /// <summary>
    /// Delays the act 2 setting after the first act is done.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetAct2()
    {
        yield return new WaitForSeconds(MatchOverModel.Instance.AnimDuration);

        MatchOverModel.Instance.SetPlayState(MatchOverModel.PlayState.Act2);
    }

    /// <summary>
    /// Plays the act 1.
    /// </summary>
    private void PlayAct1()
    {
        MatchOverModel.Instance.RunCurrentValue(MatchOverModel.Instance.AnimSpeedAct1);
        float value = MatchOverModel.Instance.GetInterpolation(MatchOverModel.Instance.AnimCurve1);

        MatchOverModel.Instance.MoveText(
            MatchOverView.Instance.MatchStateShaderRect,
            MatchOverView.Instance.StartPosition,
            MatchOverView.Instance.EndPosition,
            value);

        MatchOverView.Instance.FadeIn(
            MatchOverView.Instance.MatchStateArray,
            MatchOverModel.Instance.AnimFadeInSpeed);

        if (value >= 1)
        {
            MatchOverModel.Instance.SetDefault();
            StartCoroutine(SetAct2());
        }
    }

    /// <summary>
    /// Plays the act 2.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void PlayAct2()
    {
        MatchOverModel.Instance.RunCurrentValue(MatchOverModel.Instance.AnimSpeedAct2);
        float value = MatchOverModel.Instance.GetInterpolation(MatchOverModel.Instance.AnimCurve2);

        MatchOverView.Instance.FadeIn(
           MatchOverView.Instance.WinnerStateArray,
           MatchOverModel.Instance.AnimFadeInSpeed);

        if (value >= 1)
        {
            MatchOverModel.Instance.SetDefault();
            LevelManager.Instance.SetPhase(Phase.WaitForInput);
        }
    }

}

