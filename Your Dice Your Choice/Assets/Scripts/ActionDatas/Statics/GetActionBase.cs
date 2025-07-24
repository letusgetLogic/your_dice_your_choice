using System;
using System.Numerics;
using UnityEngine;

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
                return CreateAttackChild(data, characterObject);

            case ActionType.Defend:
                return CreateDefendChild(data, characterObject);
        }

        throw new System.Exception("Didn't match any case in GetActionBase.Create()");
    }

    /// <summary>
    /// Create an instance of Attack attach to the weapon.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="characterObject"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    private static Attack CreateAttackChild(ActionData data, GameObject characterObject)
    {
        switch (data.WeaponType)
        {
            case WeaponType.None:
                throw new System.Exception("GetActionBase.CreateAttackChild() " +
                    "-> data.WeaponType = None");

            case WeaponType.Sword:
                return new SwordBehaviour(data, characterObject);
        }

        throw new System.Exception("Didn't match any case in GetActionBase.CreateAttackChild()");
    }

    /// <summary>
    /// Create an instance of Defend attach to the weapon.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="characterObject"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    private static Defend CreateDefendChild(ActionData data, GameObject characterObject)
    {
        switch (data.WeaponType)
        {
            case WeaponType.None:
                throw new System.Exception("\"GetActionBase.CreateDefendChild() " +
                    "-> data.WeaponType = None");

            case WeaponType.Shield:
                return new ShieldBehaviour(data, characterObject);
        }

        throw new System.Exception("Didn't match any case in GetActionBase.CreateDefendChild()");
    }

}

