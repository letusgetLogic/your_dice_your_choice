using System;
using Assets.Scripts.ActionDatas;
using UnityEngine;

namespace Assets.Scripts.CharacterDatas
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableData/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public CharacterType Type;

        public float HP;
        public float AP;
        public float DP;

        public GameObject WeaponRight;
        public GameObject WeaponLeft;

        public ActionDatas.ActionData[] ActionData = new ActionDatas.ActionData[4];
    }
}
