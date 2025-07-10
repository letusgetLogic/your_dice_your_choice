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
        public AnimationCurve AnimationCurve => _animCurve1;
        public enum PlayState { None, Act1, Act2, Act3 }
        public PlayState CurrentState { get; private set; }
        public float CurrentValue { get; private set; }

        [SerializeField] private float _act1Time = 3.0f;
        [SerializeField] private float _act2Time = 3.0f;

        [SerializeField] private float _animSpeedAct1 = 0.0001f;
        [SerializeField] private float _animSpeedAct2 = 0.0001f;
        [SerializeField] private float _animSpeedAct3 = 0.00015f;
        [SerializeField] private float _animFadeInTime = 3.0f;
        [SerializeField] private AnimationCurve _animCurve1;

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

    }
}
