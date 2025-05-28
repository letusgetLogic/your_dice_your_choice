using System;
using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableData/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public CharacterType Type;

        public float HP;
        public float AP;
        public float DP;

        public Weapon WeaponRight;
        public Weapon WeaponLeft;

        public ActionData[] ActionData = new ActionData[4];
    }
}
