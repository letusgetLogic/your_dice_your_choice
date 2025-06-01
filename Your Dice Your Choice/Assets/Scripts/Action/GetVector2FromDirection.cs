using System.Numerics;
using UnityEngine;

namespace Assets.Scripts.Action
{
    public static class GetVector2FromDirection
    {
        private static readonly Vector2Int[] DirVector = new[]
        {
            // (vertical, horizontal)
            new Vector2Int(1, -1),  new Vector2Int(1, 0),  new Vector2Int(1, 1),
            new Vector2Int(0, -1),  new Vector2Int(0, 0),  new Vector2Int(0, 1),
            new Vector2Int(-1, -1), new Vector2Int(-1, 0), new Vector2Int(-1, 1),
        };

        /// <summary>
        /// Gets Vertor2[] of directions.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static Vector2Int[] Get(Direction direction)
        {
            Vector2Int[] v2list = new Vector2Int[DirVector.Length];

            switch (direction)
            {
                case Direction.None:
                    return null;

                case Direction.Any:
                    return DirVector;

                case Direction.Orthogonal:
                    v2list[0] = DirVector[2];
                    v2list[1] = DirVector[4];
                    v2list[2] = DirVector[6];
                    v2list[3] = DirVector[8];
                    return v2list;

                case Direction.Diagonal:
                    v2list[0] = DirVector[1];
                    v2list[1] = DirVector[3];
                    v2list[2] = DirVector[7];
                    v2list[3] = DirVector[9];
                    return v2list;
            }

            return null;
        }
    }
}
