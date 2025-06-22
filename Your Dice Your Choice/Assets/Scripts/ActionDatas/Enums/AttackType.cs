using System.Collections.Generic;
using Assets.Scripts.WeaponDatas;

namespace Assets.Scripts.ActionDatas
{
    public abstract class AttackType
    {
        public enum AttackKey
        {
            Sword,
        }

        public AttackType() {}

        public abstract AllowedDiceNumber GetAllowedDiceNumber();
        public abstract AllowedTile GetAllowedTile(int index);
        public abstract Direction GetDirection(int index);


        public static readonly Dictionary<AttackKey, string> Description = new Dictionary<AttackKey, string>
        {
            { AttackKey.Sword, SwordBehaviour.Description[0] },
            
        };
    }
}
