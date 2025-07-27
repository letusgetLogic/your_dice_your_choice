using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public static class GetVector2IntFromDirection
{
    private static readonly Vector2Int[] DirVector = new[]
    {
            // Vector2Int(row index, column index)
            new Vector2Int(-1, -1),  new Vector2Int(-1, 0),  new Vector2Int(-1, 1),
            new Vector2Int(0, -1),  new Vector2Int(0, 0),  new Vector2Int(0, 1),
            new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(1, 1),
        };

    public static readonly Vector2Int ZeroPoint = DirVector[4];

    /// <summary>
    /// Gets Vertor2[] of directions.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public static Vector2Int[] Get(Direction direction)
    {
        List<Vector2Int> v2list = new List<Vector2Int>();

        switch (direction)
        {
            case Direction.None:
                throw new System.Exception("GetVector2FromDirection Direction.None");

            case Direction.Any:

                for (int i = 0; i < DirVector.Length; i++)
                {
                    if (DirVector[i] == ZeroPoint) continue;

                    v2list.Add(DirVector[i]);
                }
                return v2list.ToArray();

            case Direction.Orthogonal:
                v2list.Add(DirVector[1]);
                v2list.Add(DirVector[3]);
                v2list.Add(DirVector[5]);
                v2list.Add(DirVector[7]);
                return v2list.ToArray();

            case Direction.Diagonal:
                v2list.Add(DirVector[0]);
                v2list.Add(DirVector[2]);
                v2list.Add(DirVector[6]);
                v2list.Add(DirVector[8]);
                return v2list.ToArray();
        }

        throw new System.Exception("Didn't match any case GetVector2FromDirection");
    }
}

