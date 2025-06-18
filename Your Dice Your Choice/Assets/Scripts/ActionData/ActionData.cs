using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

        private void OnEnable()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }
}
