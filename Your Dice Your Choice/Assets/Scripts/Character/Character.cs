using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class Character : MonoBehaviour
    {
        public CharacterData Data { get; private set; }

        public GameObject Panel { get; private set; }

        /// <summary>
        /// Sets the panel null.
        /// </summary>
        private void Awake()
        {
            
        }

        /// <summary>
        /// Initialize Data.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(CharacterData data)
        {
            Data = data;
        }

        /// <summary>
        /// Initialize Panel.
        /// </summary>
        /// <param name="panel"></param>
        public void SetPanel(GameObject panel)
        {
            Panel = panel;
        }
    }
}

