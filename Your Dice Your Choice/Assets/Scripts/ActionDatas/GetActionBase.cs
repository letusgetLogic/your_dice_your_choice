using System.Numerics;
using Assets.Scripts.WeaponDatas;
using UnityEngine;

namespace Assets.Scripts.ActionDatas
{
    public static class GetActionBase
    {
        /// <summary>
        /// Creates an instance of ActionBase.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="characterObject"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ActionBase Create(ActionData data, GameObject characterObject)
        {
            switch (data.ActionType)
            {
                case ActionType.None:
                    return null;

                case ActionType.Move:
                    return new Movement(data, characterObject);

                case ActionType.Attack:
                    return CreateChild(data, characterObject);

                case ActionType.Defend:
                    return new Defend(data, characterObject);
            }

            throw new System.Exception("Didn't match any case in GetActionBase.Create()");
        }

        private static Attack CreateChild(ActionData data, GameObject characterObject)
        {
            switch (data.WeaponType)
            {
                case WeaponType.None:
                    throw new System.Exception("Attack.Attack() -> data.WeaponType = None");

                case WeaponType.Sword:
                    return new SwordBehaviour(data, characterObject);
            }

            throw new System.Exception("Didn't match any case in GetActionBase.CreateChild()");
        }
    }
}
