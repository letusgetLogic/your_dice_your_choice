using System.Collections.Generic;
using Assets.Scripts.Actions;
using UnityEngine;

public class Action : MonoBehaviour
{
    private ActionType _type;
   
    public Action(ActionType type)
    {
        _type = type;
    }

}

