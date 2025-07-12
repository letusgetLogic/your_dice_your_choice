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
        public int DefaultNumber => _defaultNumber;

        [SerializeField] private int _defaultNumber = 6;


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

        /// <summary>
        /// Sets the component enabled true/false.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="value"></param>
        public void SetEnabled(Component component, bool value)
        {
            if (component is Behaviour behaviour)
            {
                behaviour.enabled = value;
            }
        }

        /// <summary>
        /// Sets the dice on the slot, deactivates the drag event and sets the canvas group default.
        /// </summary>
        public void SetOnActionSlot(Vector3 pos)
        {
            SetEnabled(GetComponent<DiceDragEvent>(), false);

            var diceMovement = GetComponent<DiceMovement>();
            diceMovement.PositionsTo(pos);

            var diceDisplay = GetComponent<DiceDisplay>();
            diceDisplay.SetDefault();
            diceDisplay.SetBlocksRaycasts(true);
        }

    }
}
