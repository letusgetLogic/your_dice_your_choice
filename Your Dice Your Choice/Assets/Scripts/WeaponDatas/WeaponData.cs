using Assets.Scripts.ActionDatas;
using UnityEngine;

namespace Assets.Scripts.WeaponDatas
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableData/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public WeaponType WeaponType;

        public AllowedTile AllowedTile;
        public AllowedDiceNumber AllowedDiceNumber;
        public Direction Direction;
    }
}
