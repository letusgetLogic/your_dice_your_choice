using UnityEngine;

namespace Assets.Scripts.MatchIntro
{
    public class MatchIntroModel : MonoBehaviour
    {
        public static MatchIntroModel Instance { get; private set; }

        public float Act1Time => _act1Time;
        public float Act2Time => _act2Time;
        public float AnimSpeedAct1 => _animSpeedAct1;
        public float AnimSpeedAct2 => _animSpeedAct2;
        public float AnimSpeedAct3 => _animSpeedAct3;
        public float AnimFadeInTime => _animFadeInTime;
        public AnimationCurve AnimCurve1 => _animCurve1;
        public AnimationCurve AnimCurve2 => _animCurve2;
        public AnimationCurve AnimCurve3 => _animCurve3;
        public enum PlayState { None, Act1, Act2, Act3 }
        public PlayState CurrentState { get; private set; }
        public float CurrentValue { get; private set; }

        [SerializeField] private float _act1Time = 3.0f;
        [SerializeField] private float _act2Time = 3.0f;

        [SerializeField] private float _animSpeedAct1 = 1f;
        [SerializeField] private float _animSpeedAct2 = 1f;
        [SerializeField] private float _animSpeedAct3 = 1.55f;
        [SerializeField] private float _animFadeInTime = 3.0f;
        [SerializeField] private AnimationCurve _animCurve1;
        [SerializeField] private AnimationCurve _animCurve2;
        [SerializeField] private AnimationCurve _animCurve3;

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
        /// Sets the play state.
        /// </summary>
        /// <param name="state"></param>
        public void SetPlayState(PlayState state)
        {
            CurrentState = state;
        }

        /// <summary>
        /// Runs the current value.
        /// </summary>
        /// <param name="animSpeed"></param>
        public void RunCurrentValue(float animSpeed)
        {
            CurrentValue = Mathf.MoveTowards(CurrentValue, 1, animSpeed * 0.0001f / Time.deltaTime);
        }

        /// <summary>
        /// Gets the value of interpolation.
        /// </summary>
        /// <returns></returns>
        public float GetInterpolation(AnimationCurve animationCurve)
        {
            return animationCurve.Evaluate(CurrentValue);
        }

        /// <summary>
        /// Moves the text objects.
        /// </summary>
        /// <param name="animSpeed"></param>
        /// <param name="startPositionLeft"></param>
        /// <param name="endPOsitionLeft"></param>
        /// <param name="startPositionRight"></param>
        /// <param name="endPOsitionRight"></param>
        public void MoveText(RectTransform rect1, RectTransform rect2,
                            Vector2 startPositionLeft, Vector2 endPOsitionLeft,
                            Vector2 startPositionRight, Vector2 endPOsitionRight,
                            float value)
        {
            rect1.anchoredPosition = Vector2.Lerp(startPositionLeft, endPOsitionLeft, value);
            rect2.anchoredPosition = Vector2.Lerp(startPositionRight, endPOsitionRight, value);
        }

        /// <summary>
        /// Sets the default values.
        /// </summary>
        public void SetDefault()
        {
            CurrentValue = 0f;
            CurrentState = PlayState.None;
        }
    }
}
