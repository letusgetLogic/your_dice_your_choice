using UnityEngine;

namespace Assets.Scripts.MatchIntro
{
    public class MatchIntroModel : MonoBehaviour
    {
        public static MatchIntroModel Instance { get; private set; }

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
