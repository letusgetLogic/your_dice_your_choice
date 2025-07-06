using Assets.Scripts.ActionPopupPrefab;
using Assets.Scripts.ActionPopupPrefab.DiceSlotPrefab;
using UnityEngine;

namespace Assets.Scripts.ActionPanelPrefab
{
    public class ActionPanelComponents : MonoBehaviour
    {
        [SerializeField] private DiceSlotAction _diceSlotAction;
        public DiceSlotAction DiceSlotAction => _diceSlotAction;

        public ActionPanelMouseEvent ActionPanelMouseEvent 
            => GetComponent<ActionPanelMouseEvent>();

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
    }
}
