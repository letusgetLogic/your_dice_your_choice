using UnityEngine;

namespace Assets.Scripts.Action
{
    [CreateAssetMenu(fileName = "ActionData", menuName = "ScriptableData/ActionData")]
    public class ActionData : ScriptableObject
    {
        public ActionKey ActionKey;
        public ActionType ActionType;
        public AllowedTile AllowedTile;
        public AllowedDiceNumber AllowedDiceNumber;
        public Direction Direction;
        public string Description = "...run method SetDescription() on Inspector...";

        [ContextMenu("Set Description")]
        public void SetDescription()
        {
            switch (ActionType)
            {
                case ActionType.None:
                    Description = "None";
                    return;
                case ActionType.Move:
                    Description = MovementData.Description[ActionKey];
                    return;
                case ActionType.Attack:
                    Description = "Attack";
                    return;
                case ActionType.Defend:
                    Description = "Defend";
                    return;
            }
        }

    }
}
