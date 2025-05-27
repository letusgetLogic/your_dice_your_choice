using Assets.Scripts.Actions;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionData", menuName = "Scriptable Objects/ActionData")]
public class ActionData : ScriptableObject
{
    public ActionKey ActionKey;
    public ActionType ActionType;
    public AvaiableDiceNumber DiceNumber;
    public Direction Direction;
    public AvaiableTiles Tiles;
}
