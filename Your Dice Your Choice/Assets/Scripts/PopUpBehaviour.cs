using UnityEngine;

namespace Assets.Scripts
{
    public class PopUpBehaviour
    {
        /// <summary>
        /// Returns the position of the popup with distance to non-UI target object.
        /// </summary>
        /// <param name="canvasRect"></param>
        /// <param name="targetWorldPos"></param>
        /// <param name="distanceV2"></param>
        /// <returns></returns>
        public static Vector3 NewWorldToLocalPosition(RectTransform canvasRect, Vector3 targetWorldPos, Vector2 distanceV2)
        {
            var targetLocalPos = canvasRect.InverseTransformPoint(targetWorldPos);
            return targetLocalPos + Distance(targetLocalPos, distanceV2);
        }

        /// <summary>
        /// Return the distance.
        /// </summary>
        /// <param name="targetLocalPos"></param>
        /// <returns></returns>
        private static Vector3 Distance(Vector3 targetLocalPos, Vector2 distanceV2)
        {
            Vector3 distance = new();

            distance.x = distanceV2.x * Direction(targetLocalPos).x;
            distance.y = distanceV2.y * Direction(targetLocalPos).y;

            return distance;
        }

        /// <summary>
        /// Return the direction of the distance.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private static Vector3 Direction(Vector3 pos)
        {
            Vector3 dir = new();

            switch (pos.x)
            {
                case < 0f: dir.x = 1; break;
                case 0f: dir.x = 0; break;
                case > 0f: dir.x = -1; break;
            }

            switch (pos.y)
            {
                case < 0f: dir.y = 1; break;
                case 0f: dir.y = 0; break;
                case > 0f: dir.y = -1; break;
            }

            return dir;
        }
    }
}
