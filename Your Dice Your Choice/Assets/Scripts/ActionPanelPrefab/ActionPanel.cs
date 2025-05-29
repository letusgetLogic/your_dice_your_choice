using Assets.Scripts.CharacterPrefab;
using Assets.Scripts.Action;
using TMPro;
using UnityEngine;
using Assets.Scripts.DicePrefab;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class ActionPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _actionName;

        public ActionData ActionData {  get; private set; }
        public GameObject CharacterObject { get; private set; }
        public Character Character { get; private set; }
        public string Description { get; private set; }


        /// <summary>
        /// Initializes data.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(ActionData data, GameObject character)
        {
            ActionData = data;
            CharacterObject = character;
            Character = CharacterObject.GetComponent<Character>();
            _actionName.text = data.ActionType.ToString();
            Description = data.Description;
        }


        public void ManageAction(GameObject dice)
        {
            var diceMovement = dice.GetComponent<DiceMovement>();

            if (!CheckDiceInput(dice))
            {
                diceMovement.SendBackToBase();
                return;
            }

            diceMovement.PositionsToDiceSlot(gameObject.GetComponent<RectTransform>().anchoredPosition);
        }

        private bool CheckDiceInput(GameObject dice)
        {
            var allowedDiceNumber = ActionData.AllowedDiceNumber;
            var diceNumber = dice.GetComponent<Dice>().CurrentNumber;

            return CheckDiceCondition.IsNumberAllowed(allowedDiceNumber, diceNumber);
        }
    }
}
