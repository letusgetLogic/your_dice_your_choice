using static Assets.Scripts.ActionDatas.MovementType;
using System.Collections.Generic;

namespace Assets.Scripts.WeaponDatas
{
    public static class WeaponGetDescription
    {
        public static readonly Dictionary<WeaponType, string[]> Description = new Dictionary<WeaponType, string[]>
        {
            { WeaponType.Sword, 
                new string[] 
                {
                    "None",
                    "Dice 1: Hit orthogonally 1 Tile",
                } 
            },
           
        };
    }
}
