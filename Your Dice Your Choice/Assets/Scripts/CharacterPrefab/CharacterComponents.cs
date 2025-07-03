using UnityEngine;

namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterComponents : MonoBehaviour
    {
        public Transform PivotTransform { get; protected set; }
        public Transform BodyPivotTransform { get; protected set; }
        public Transform BodyTransform { get; protected set; }
        public Transform LeftHandTransform { get; protected set; }
        public Transform RightHandTransform { get; protected set; }

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            PivotTransform = transform.Find("Pivot");
            BodyPivotTransform = PivotTransform.Find("Body Pivot");
            BodyTransform = BodyPivotTransform.Find("Character Body");
            LeftHandTransform = BodyTransform.Find("Character Left Hand");
            RightHandTransform = BodyTransform.Find("Character Right Hand");
        }
    }
}
