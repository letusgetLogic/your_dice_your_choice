using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Object remains after scene change.
            }
            else
            {
                Destroy(gameObject); // Destroy the new instance instead of the old one.
            }
        }

        /// <summary>
        /// Starts the match by loading the Battle Arena scene.
        /// </summary>
        public void StartMatch()
        {
            ButtonClickAnimation.Instance.ScaleSize(ButtonManager.Instance.NewMatchButton);
            SceneManager.LoadScene("BattleArenaScene");
        }
    }
}
