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

        public void ManageAction(GameObject actionPanel, GameObject dice)
        {

        }

        private bool CheckDiceInput(ActionData actionData, GameObject dice)
        {
            var allowedDiceNumber = actionData.AllowedDiceNumber;
            var diceNumber = dice.GetComponent<Dice>().CurrentNumber;

            return CheckDiceCondition.IsNumberAllowed(allowedDiceNumber, diceNumber);
        }
    }
}
