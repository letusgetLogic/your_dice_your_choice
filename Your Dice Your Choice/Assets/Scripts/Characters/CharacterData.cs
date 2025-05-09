using UnityEngine;

namespace Assets.Scripts.Characters
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableData/CharacterData", order = 1)]
    public class CharacterData : ScriptableObject
    {
        public TurnState Player;
        public CharacterType Type;

        public float HP;
        public float AP;
        public float DP;

        public Weapon Weapon1;
        public Weapon Weapon2;

        public ActionType[] ActionTypes = new ActionType[4];
    }
}
