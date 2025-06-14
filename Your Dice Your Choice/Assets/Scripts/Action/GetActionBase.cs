using System.Numerics;
using UnityEngine;

namespace Assets.Scripts.Action
{
    public static class GetActionBase
    {
        /// <summary>
        /// Creates an instance of ActionBase.
        /// </summary>
        /// <param name="actionData"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ActionBase Create(ActionData actionData, GameObject character)
        {
            switch (actionData.ActionType)
            {
                case ActionType.None:
                    return null;
                case ActionType.Move:
                    return new Movement(actionData, character);
                case ActionType.Attack:
                    return new Attack(actionData, character);
                case ActionType.Defend:
                    return new Defend(actionData, character);
            }

            throw new System.Exception("Didn't match any case GetActionBase");
        }
    }
}
