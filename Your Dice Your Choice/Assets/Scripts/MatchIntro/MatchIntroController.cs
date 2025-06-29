using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.MatchIntro
{
    public class MatchIntroController : MonoBehaviour
    {
        public static MatchIntroController Instance { get; private set; }

        [SerializeField] private float _act1Time = 3.0f;
        [SerializeField] private float _act2Time = 3.0f;

        [SerializeField] private float _animSpeedAct1 = 0.0001f;
        [SerializeField] private float _animSpeedAct2 = 0.0001f;
        [SerializeField] private float _animSpeedAct3 = 0.00015f;
        [SerializeField] private float _animFadeInTime = 3.0f;
        [SerializeField] private AnimationCurve _animCurve1;

        private enum PlayStates { None, Act1, Act2, Act3 }
        private PlayStates _playStates;

        private float _current;

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

            _playStates = PlayStates.None;
        }

        /// <summary>
        /// Update method.
        /// </summary>
        private void Update()
        {
            switch (_playStates)
            {
                case PlayStates.None:
                    return;

                case PlayStates.Act1:
                    PlayAct1();
                    return;

                case PlayStates.Act2:
                    PlayAct2();
                    return;

                case PlayStates.Act3:
                    PlayAct3();
                    return;
            }
        }

        /// <summary>
        /// Plays the intro.
        /// </summary>
        public void Play()
        {
            MatchIntroView.Instance.SetText();

            SetIntroActive();
        }

        /// <summary>
        /// Plays the act 1.
        /// </summary>
        private void PlayAct1()
        {
            _current = Mathf.MoveTowards(_current, 1, _animSpeedAct1 / Time.deltaTime);
            float value = _animCurve1.Evaluate(_current);

            MatchIntroModel.Instance.MoveText(
                MatchIntroView.Instance.LeftIntroShaderRect, MatchIntroView.Instance.RightIntroShaderRect,
                MatchIntroView.Instance.StartPositionLeftAct1, MatchIntroView.Instance.StartPositionLeftAct2,
                MatchIntroView.Instance.StartPositionRightAct1, MatchIntroView.Instance.StartPositionRightAct2,
                value);

            MatchIntroView.Instance.FadeIn(_animFadeInTime);

            if (value >= 1)
            {
                _current = 0f;
                _playStates = PlayStates.None;
            }
        }

        /// <summary>
        /// Plays the act 2.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void PlayAct2()
        {
            _current = Mathf.MoveTowards(_current, 1, _animSpeedAct2 / Time.deltaTime);
            float value = _animCurve1.Evaluate(_current);

            MatchIntroModel.Instance.MoveText(
                MatchIntroView.Instance.LeftIntroShaderRect, MatchIntroView.Instance.RightIntroShaderRect,
                MatchIntroView.Instance.StartPositionLeftAct2, MatchIntroView.Instance.EndPositionLeftAct2,
                MatchIntroView.Instance.StartPositionRightAct2, MatchIntroView.Instance.EndPositionRightAct2,
                value);

            if (value >= 1)
            {
                SetFirstTurn.Instance.SetDiceAndPanel();
                LevelManager.Instance.NextPhase();

                _current = 0f;
                _playStates = PlayStates.None;
            }
        }

        /// <summary>
        /// Plays the act 3.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void PlayAct3()
        {
            _current = Mathf.MoveTowards(_current, 1, _animSpeedAct3 / Time.deltaTime);
            float value = _animCurve1.Evaluate(_current);

            MatchIntroView.Instance.DimDownForeground(1 - value);

            float ratio = Mathf.Lerp(0, 1, value);

            SetFirstTurn.Instance.ScaleUp(ratio);

            if (ratio >= 1)
            {
                MatchIntroView.Instance.SetForegroundActive(false);

                SetFirstTurn.Instance.RollTurnDice();

                _current = 0f;
                _playStates = PlayStates.None;
            }
        }

        /// <summary>
        /// Sets the intro active.
        /// </summary>
        private void SetIntroActive()
        {
            MatchIntroView.Instance.SetTextArrayActive(true);

            _playStates = PlayStates.Act1;

            StartCoroutine(SetAct2());
        }

        /// <summary>
        /// Sets Act 2.
        /// </summary>
        /// <returns></returns>
        private IEnumerator SetAct2()
        {
            yield return new WaitForSeconds(_act1Time);
            _playStates = PlayStates.Act2;

            StartCoroutine(SetAct3());
        }

        /// <summary>
        /// Sets Act 3.
        /// </summary>
        /// <returns></returns>
        private IEnumerator SetAct3()
        {
            yield return new WaitForSeconds(_act2Time);

            _playStates = PlayStates.Act3;
        }

        /// <summary>
        /// Sets the intro inactive.
        /// </summary>
        public void SetIntroInactive()
        {
            MatchIntroView.Instance.SetIntroInactive();

            _playStates = PlayStates.None;
        }

        /// <summary>
        /// Ends this phase.
        /// </summary>
        public void EndPhase()
        {
            LevelManager.Instance.NextPhase();
        }

    }
}
