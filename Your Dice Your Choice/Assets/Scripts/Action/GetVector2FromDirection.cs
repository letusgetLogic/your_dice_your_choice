using System.Numerics;

namespace Assets.Scripts.Action
{
    public static class GetVector2FromDirection
    {
        private static Vector2[] dirVector = new[]
        {
            // (vertical, horizontal)
            new Vector2(1, -1),  new Vector2(1, 0),  new Vector2(1, 1),
            new Vector2(0, -1),  new Vector2(0, 0),  new Vector2(0, 1),
            new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1, 1),
        };

        /// <summary>
        /// Gets Vertor2[] of directions.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static Vector2[] Get(Direction direction)
        {
            Vector2[] v2list = new Vector2[dirVector.Length];

            switch (direction)
            {
                case Direction.None:
                    throw new System.Exception("Direction = None");
                case Direction.Any:
                    return dirVector;
                case Direction.Orthogonal:
                    v2list[0] = dirVector[2];
                    v2list[1] = dirVector[4];
                    v2list[2] = dirVector[6];
                    v2list[3] = dirVector[8];
                    return v2list;
                case Direction.Diagonal:
                    v2list[0] = dirVector[1];
                    v2list[1] = dirVector[3];
                    v2list[2] = dirVector[7];
                    v2list[3] = dirVector[9];
                    return v2list;
            }

            return default(Vector2[]);
        }
    }
}
