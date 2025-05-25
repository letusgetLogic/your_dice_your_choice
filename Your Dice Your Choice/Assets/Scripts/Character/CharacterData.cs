using UnityEngine;

namespace Assets.Scripts.Characters
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableData/CharacterData", order = 1)]
    public class CharacterData : ScriptableObject
    {
        public CharacterType Type;

        public float HP;
        public float AP;
        public float DP;

        public Weapon WeaponRight;
        public Weapon WeaponLeft;

        public ActionType[] ActionTypes = new ActionType[4];
    }
}
