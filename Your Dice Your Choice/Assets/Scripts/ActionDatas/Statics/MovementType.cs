using System.Collections.Generic;

namespace Assets.Scripts.ActionDatas
{
    public static class MovementType
    {
        public enum MovementKey
        {
            Move_Tile_X_D1_6_Any,
            Move_Tile_X_D1_6_Orthogonal,
            Move_Tile_X_D1_6_Diagonal,

            Move_Tile_X_D1_3_Any,
            Move_Tile_X_D1_3_Orthogonal,
            Move_Tile_X_D1_3_Diagonal,

            Move_Tile_1_D4_6_Any,
            Move_Tile_1_D4_6_Orthogonal,
            Move_Tile_1_D4_6_Diagonal,
        }

        public static readonly Dictionary<MovementKey, string> Description = new Dictionary<MovementKey, string>
        {
            { MovementKey.Move_Tile_X_D1_6_Any, "Move in any direction X Tiles" },
            { MovementKey.Move_Tile_X_D1_6_Orthogonal, "Move orthogonally X Tiles" },
            { MovementKey.Move_Tile_X_D1_6_Diagonal, "Move diagonally X Tiles" },

            { MovementKey.Move_Tile_X_D1_3_Any, $"Move in any direction X Tiles,\\n with Dice 1-3" },
            { MovementKey.Move_Tile_X_D1_3_Orthogonal, $"Move orthogonally X Tiles,\\n with Dice 1-3" },
            { MovementKey.Move_Tile_X_D1_3_Diagonal, $"Move diagonally X Tiles,\\n with Dice 1-3" },

            { MovementKey.Move_Tile_1_D4_6_Any, $"Move in any direction 1 Tile,\\n with Dice 4-6" },
            { MovementKey.Move_Tile_1_D4_6_Orthogonal, $"Move orthogonally 1 Tile,\\n with Dice 4-6" },
            { MovementKey.Move_Tile_1_D4_6_Diagonal, $"Move diagonally 1 Tile,\\n with Dice 4-6" },
        };
    }
}
