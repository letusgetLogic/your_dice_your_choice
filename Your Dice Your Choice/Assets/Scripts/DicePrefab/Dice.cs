using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DicePrefab
{
    public class Dice : MonoBehaviour
    {
        public static readonly int MaxNumber = 6;

        public Sprite[] DiceSide;

        [SerializeField] private int _defaultDiceIndex = 6;

        [HideInInspector] public int CurrentNumber { get; private set; }


        /// <summary>
        /// Start method.
        /// </summary>
        private void Start()
        {
            var defaultImage = gameObject.GetComponent<Image>();
            defaultImage.sprite = DiceSide[_defaultDiceIndex];
        }

        /// <summary>   
        /// Initializes dice side.
        /// </summary>
        /// <param name="sideIndex"></param>
        public void InitializeSide(int sideIndex)
        {
            var currentImage = gameObject.GetComponent<Image>();
            currentImage.sprite = DiceSide[sideIndex];
            CurrentNumber = sideIndex;
        }
    }
}
