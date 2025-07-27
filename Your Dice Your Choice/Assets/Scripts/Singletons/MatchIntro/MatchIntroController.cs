using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Tilemaps;

public class MatchIntroController : MonoBehaviour
{
    public static MatchIntroController Instance { get; private set; }

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

        MatchIntroView.Instance.SetDefault();
        MatchIntroModel.Instance.SetPlayState(MatchIntroModel.PlayState.None);
    }

    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        switch (MatchIntroModel.Instance.CurrentState)
        {
            case MatchIntroModel.PlayState.None:
                return;

            case MatchIntroModel.PlayState.Act1:
                PlayAct1();
                return;

            case MatchIntroModel.PlayState.Act2:
                PlayAct2();
                return;

            case MatchIntroModel.PlayState.Act3:
                PlayAct3();
                return;

            case MatchIntroModel.PlayState.Act4:
                PlayAct4();
                return;
        }
    }

    /// <summary>
    /// Plays the intro.
    /// </summary>
    public void Play()
    {
        MatchIntroView.Instance.SetText();

        MatchIntroView.Instance.SetTextArrayActive(true);

        MatchIntroModel.Instance.SetPlayState(MatchIntroModel.PlayState.Act1);

        StartCoroutine(SetAct2());
    }

    /// <summary>
    /// Sets Act 2.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetAct2()
    {
        yield return new WaitForSeconds(MatchIntroModel.Instance.Act1Time);

        MatchIntroModel.Instance.SetPlayState(MatchIntroModel.PlayState.Act2);

        StartCoroutine(SetAct3());
    }

    /// <summary>
    /// Sets Act 3.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetAct3()
    {
        yield return new WaitForSeconds(MatchIntroModel.Instance.Act2Time);

        MatchIntroModel.Instance.SetPlayState(MatchIntroModel.PlayState.Act3);
    }

    /// <summary>
    /// Sets Act 3.
    /// </summary>
    /// <returns></returns>
    private void SetAct4()
    {
        MatchIntroModel.Instance.SetPlayState(MatchIntroModel.PlayState.Act4);
    }

    /// <summary>
    /// Plays the act 1.
    /// </summary>
    private void PlayAct1()
    {
        MatchIntroModel.Instance.RunCurrentValue(
            MatchIntroModel.Instance.AnimSpeedAct1);
        float value = MatchIntroModel.Instance.GetInterpolation(
            MatchIntroModel.Instance.AnimCurve1);

        MatchIntroModel.Instance.MoveText(
            MatchIntroView.Instance.LeftIntroShaderRect,
            MatchIntroView.Instance.RightIntroShaderRect,
            MatchIntroView.Instance.StartPositionLeftAct1,
            MatchIntroView.Instance.StartPositionLeftAct2,
            MatchIntroView.Instance.StartPositionRightAct1,
            MatchIntroView.Instance.StartPositionRightAct2,
            value);

        MatchIntroView.Instance.FadeIn(MatchIntroModel.Instance.AnimFadeInTime);

        if (value >= 1)
        {
            MatchIntroModel.Instance.SetDefault();
        }
    }

    /// <summary>
    /// Plays the act 2.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void PlayAct2()
    {
        MatchIntroModel.Instance.RunCurrentValue(
            MatchIntroModel.Instance.AnimSpeedAct2);
        float value = MatchIntroModel.Instance.GetInterpolation(
            MatchIntroModel.Instance.AnimCurve2);

        MatchIntroModel.Instance.MoveText(
            MatchIntroView.Instance.LeftIntroShaderRect,
            MatchIntroView.Instance.RightIntroShaderRect,
            MatchIntroView.Instance.StartPositionLeftAct2,
            MatchIntroView.Instance.EndPositionLeftAct2,
            MatchIntroView.Instance.StartPositionRightAct2,
            MatchIntroView.Instance.EndPositionRightAct2,
            value);

        if (value >= 1)
        {
            SetFirstTurn.Instance.SetTurnDiceAndPanel();
            MatchIntroModel.Instance.SetDefault();
            LevelManager.Instance.SetPhase(Phase.Initialization);
        }
    }

    /// <summary>
    /// Plays the act 3.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void PlayAct3()
    {
        MatchIntroModel.Instance.RunCurrentValue(
            MatchIntroModel.Instance.AnimSpeedAct3);
        float value = MatchIntroModel.Instance.GetInterpolation(
            MatchIntroModel.Instance.AnimCurve3);

        MatchIntroView.Instance.DimDownForeground(1 - value);

        float ratio = Mathf.Lerp(0, 1, value);

        SetFirstTurn.Instance.ScaleUpPanelsAndTurnDice(ratio);

        if (ratio >= 1)
        {
            MatchIntroView.Instance.SetForegroundActive(false);
            MatchIntroModel.Instance.SetDefault();
            SetFirstTurn.Instance.RollTurnDice();
            SetAct4();
        }
    }

    /// <summary>
    /// Plays the act 4.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void PlayAct4()
    {
        MatchIntroModel.Instance.RunCurrentValue(
            MatchIntroModel.Instance.AnimSpeedAct4);
        float value = MatchIntroModel.Instance.GetInterpolation(
            MatchIntroModel.Instance.AnimCurve4);

        float ratio = Mathf.Lerp(0, 1, value);

        SetFirstTurn.Instance.ScaleUpPlayDice(ratio);

        if (ratio >= 1)
        {
            MatchIntroModel.Instance.SetPlayState(MatchIntroModel.PlayState.None);
        }
    }

    /// <summary>
    /// Sets the intro inactive.
    /// </summary>
    public void SetIntroInactive()
    {
        MatchIntroView.Instance.SetIntroInactive();

        MatchIntroModel.Instance.SetPlayState(MatchIntroModel.PlayState.None);
    }

    /// <summary>
    /// Ends this phase.
    /// </summary>
    public void EndPhase()
    {
        LevelManager.Instance.SetPhase(Phase.Battle);
    }

}

