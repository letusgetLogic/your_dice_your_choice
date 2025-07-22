using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DicePrefab
{
    public class Dice : MonoBehaviour
    {
        public static int MaxNumber => 6;

        [SerializeField] private int _defaultNumber = 6;
        public int DefaultNumber => _defaultNumber;
        public int CurrentNumber { get; private set; }
        public GameObject RollPanel { get; private set; }
        public int IndexOnPanel { get; private set; }

        private DiceDisplay _diceDisplay;
        private DiceMovement _diceMovement;


        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            _diceDisplay = GetComponent<DiceDisplay>();
            _diceMovement = GetComponent<DiceMovement>();
        }

        /// <summary>   
        /// Initializes the dice side.
        /// </summary>
        /// <param name="sideIndex"></param>
        public void InitializeSide(int sideIndex)
        {
            _diceDisplay.SetImage(sideIndex);
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
        /// Sets the dice on the slot, deactivates the drag event and sets the canvas group default.
        /// </summary>
        public void SetOnActionSlot(Vector3 pos)
        {
            SetComponentEnabled(GetComponent<DiceDragEvent>(), false);

            _diceMovement.PositionsTo(pos);

            _diceDisplay.SetDefault();
            _diceDisplay.SetBlocksRaycasts(true);
        }


        /// <summary>
        /// Sets the component enabled true/false.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="value"></param>
        public void SetComponentEnabled(Component component, bool value)
        {
            if (component is Behaviour behaviour)
            {
                behaviour.enabled = value;
            }
        }
    }
}
