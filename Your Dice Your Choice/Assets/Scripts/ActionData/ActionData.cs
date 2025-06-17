using UnityEngine;

namespace Assets.Scripts.Action
{
    [CreateAssetMenu(fileName = "ActionData", menuName = "ScriptableData/ActionData")]
    public class ActionData : ScriptableObject
    {
        public ActionType ActionType;

        public MovementType.MovementKey MovementKey;
        public AllowedTile AllowedTile;
        public AllowedDiceNumber AllowedDiceNumber;
        public Direction Direction;

        public string Description = "";
    }
}
