using UnityEngine;

namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterComponents : MonoBehaviour
    {
        public Transform PivotTransform { get; protected set; }
        public Transform BodyTransform { get; protected set; }
        public Transform LeftHandTransform { get; protected set; }
        public Transform RightHandTransform { get; protected set; }


        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            PivotTransform = transform.Find("Pivot");
            BodyTransform = transform.Find("Pivot").Find("Character Body");
            LeftHandTransform = transform.Find("Pivot").Find("Character Left Hand");
            RightHandTransform = transform.Find("Pivot").Find("Character Right Hand");
        }
    }
}
