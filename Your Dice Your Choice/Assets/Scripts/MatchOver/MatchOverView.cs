using UnityEngine;

namespace Assets.Scripts.MatchOver
{
    public class MatchOverView : MonoBehaviour
    {
        public static MatchOverView Instance { get; private set; }

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

    }
}
