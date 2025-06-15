using System;
using Assets.Scripts.Action;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.CharacterPrefab
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableData/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public CharacterType Type;

        public float HP;
        public float AP;
        public float DP;

        public WeaponType WeaponRight;
        public WeaponType WeaponLeft;

        public ActionType[] ActionData = new ActionType[4];
    }
}
