using UnityEngine;

[CreateAssetMenu(fileName = "ActionData", menuName = "Scriptable Objects/ActionData")]
public class ActionData : ScriptableObject
{
    public ActionType ActionType;
    public AvaiableDiceNumber DiceNumber;
    public Action Action;
}
