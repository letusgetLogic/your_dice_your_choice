using System.Numerics;
using UnityEngine;

namespace Assets.Scripts.ActionDatas
{
    public static class GetIntFromAllowedTile
    {
        /// <summary>
        /// Gets int of allowedTile and dice number.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static int Get(AllowedTile allowedTile, int diceNumber)
        {
            switch (allowedTile)
            {
                case AllowedTile.None:
                    throw new System.Exception("GetIntFromAllowedTile AllowedTile.None");

                case AllowedTile.Tile_X:
                    return diceNumber;
                    
                case AllowedTile.Tile_1_3:
                    return diceNumber;
                    
                case AllowedTile.Tile_1:
                    return 1;
            }

            throw new System.Exception("Didn't match any case GetIntFromAllowedTile");
        }
    }
}
