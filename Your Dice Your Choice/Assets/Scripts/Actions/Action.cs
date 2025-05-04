using System.Collections.Generic;

class Action
{
    private ActionType _type;
    private float _weightingValue;
    private string _description;
   

    public Action(ActionType type, float weightingValue, string description)
    {
        _type = type;
        _weightingValue = weightingValue;
        _description = description;
    }

    
}

