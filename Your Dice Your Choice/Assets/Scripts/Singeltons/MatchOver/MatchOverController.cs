using Assets.Scripts.MatchIntro;
using UnityEngine;

namespace Assets.Scripts.MatchOver
{
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

            MatchOverView.Instance.SetDefault();
            MatchOverModel.Instance.SetPlayState(MatchOverModel.PlayState.None);
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
        public void Congratulate(Player player)
        {
            MatchOverView.Instance.SetText(player.Name);

            MatchOverView.Instance.SetTextArrayActive(true);

            MatchOverModel.Instance.SetPlayState(MatchOverModel.PlayState.Act1);
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
                MatchOverModel.Instance.AnimFadeInTime);

            if (value >= 1)
            {
                MatchOverModel.Instance.SetDefault();
                MatchOverModel.Instance.SetPlayState(MatchOverModel.PlayState.Act2);
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
               MatchOverModel.Instance.AnimFadeInTime);

            if (value >= 1)
            {
                MatchOverModel.Instance.SetDefault();
                LevelManager.Instance.NextPhase();
            }
        }

    }
}
