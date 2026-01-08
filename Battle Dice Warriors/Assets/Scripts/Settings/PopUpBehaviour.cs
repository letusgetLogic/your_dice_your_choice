using UnityEngine;

public class PopUpBehaviour
{
    /// <summary>
    /// Returns the position of the popup with distance to non-UI target object.
    /// </summary>
    /// <param name="canvasRect"></param>
    /// <param name="targetWorldPos"></param>
    /// <param name="distanceV2"></param>
    /// <returns></returns>
    public static (Vector2, int) RectLocalPositionFromTargetDirToCenterCanvasWithDistance(RectTransform canvasRect, Vector3 targetWorldPos, Vector2 distanceV2)
    {
        var targetLocalPos = canvasRect.InverseTransformPoint(targetWorldPos);
        Vector3  distance = Distance(targetLocalPos, distanceV2);
        return (targetLocalPos + distance, (int)distance.normalized.x);
    }

    /// <summary>
    /// Return the distance to target, which shows to the center of window.
    /// </summary>
    /// <param name="targetLocalPos"> target</param>
    /// <param name="distanceV2"> distance to be setted</param>
    /// <returns></returns>
    private static Vector2 Distance(Vector2 targetLocalPos, Vector2 distanceV2)
    {
        int dir = HoriDirectionDefaultLeft(targetLocalPos.x);
        Vector2 distance = new();

        distance.x = distanceV2.x * dir;
        distance.y = distanceV2.y /**  VertiDirection(targetLocalPos).y*/;

        return distance;
    }

    /// <summary>
    /// Return the horizontal direction to target.
    /// Left (1 -> default) and Right (-1)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private static int HoriDirectionDefaultLeft(float x)
    {
        switch (x)
        {
            case <= 0f: return 1;
            case > 0f: return -1; 
        }

        return 0;
    }

}
