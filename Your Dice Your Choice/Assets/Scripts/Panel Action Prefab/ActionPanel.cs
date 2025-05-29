using Assets.Scripts.Characters;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace Assets.Scripts.ActionPanel
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

    }
}
