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

        public static AllowedDiceNumber GetAllowedDiceNumber(AttackKey attackKey)
        {
            switch (attackKey)
            {
                case AttackKey.Sword:
                    return SwordBehaviour.GetAllowedDiceNumber();
            }

            throw new System.Exception("AttackType.GetAllowedDiceNumber() -> enum attackKey invalid");
        }

        public static AllowedTile GetAllowedTile(int index)
        {
            switch (index)
            {
                case 0:
                    throw new System.Exception("SwordBehaviour.GetAllowedTile() -> index = 0");

                case 1:
                    return AllowedTile.Tile_1;
            }

            throw new System.Exception("SwordBehaviour.GetAllowedTile() -> int index invalid");
        }

        public static Direction GetDirection(int index)
        {
            switch (index)
            {
                case 0:
                    throw new System.Exception("SwordBehaviour.GetDirection() -> index = 0");

                case 1:
                    return Direction.Orthogonal;
            }

            throw new System.Exception("SwordBehaviour.GetDirection() -> int index invalid");
        }

        public static readonly Dictionary<AttackKey, string> Description = new Dictionary<AttackKey, string>
        {
            { AttackKey.Sword, SwordBehaviour.Description[0] },
            
        };
    }
}
