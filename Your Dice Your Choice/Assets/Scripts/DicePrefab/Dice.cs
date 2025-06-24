using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DicePrefab
{
    public class Dice : MonoBehaviour
    {
        public static readonly int MaxNumber = 6;

        public Sprite[] DiceSide;
        public int CurrentNumber { get; private set; }
        public int IndexOnPanel { get; private set; }
        public GameObject RollPanel { get; private set; }

        [SerializeField] private int _defaultNumber = 6;

        public int DefaultNumber => _defaultNumber;

        /// <summary>
        /// Start method.
        /// </summary>
        private void Start()
        {
            InitializeSide(_defaultNumber);
        }

        /// <summary>   
        /// Initializes the dice side.
        /// </summary>
        /// <param name="sideIndex"></param>
        public void InitializeSide(int sideIndex)
        {
            var currentImage = gameObject.GetComponent<Image>();
            currentImage.sprite = DiceSide[sideIndex];
            CurrentNumber = sideIndex;
        }

        /// <summary>
        /// Initializes the roll panel and its index.
        /// </summary>
        /// <param name="index"></param>
        public void InitializeIndexOf(GameObject rollPanel, int index)
        {
            RollPanel = rollPanel;
            IndexOnPanel = index;
        }

    }
}
