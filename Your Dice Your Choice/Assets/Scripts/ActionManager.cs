using Assets.Scripts.DicePrefab;
using Assets.Scripts.Action;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class ActionManager : MonoBehaviour
    {
        public static ActionManager Instance { get; private set; }

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
