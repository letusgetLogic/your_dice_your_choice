using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterData _data;

        public CharacterData Data => _data;

        public GameObject Panel { get; private set; }

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            //Data = _data;
            //Debug.Log(Data);
        }

        /// <summary>
        /// Sets the panel at the character and active.
        /// </summary>
        /// <param name="panel"></param>
        public void SetPanel(GameObject panel)
        {
            Panel = panel;
        }
    }
}

