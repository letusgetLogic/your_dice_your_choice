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
            PivotTransform = transform.Find("Pivot").gameObject.GetComponent<Transform>();
            BodyTransform = transform.Find("Pivot").Find("Character Body").gameObject.GetComponent<Transform>();
            LeftHandTransform = transform.Find("Pivot").Find("Character Left Hand").gameObject.GetComponent<Transform>();
            RightHandTransform = transform.Find("Pivot").Find("Character Right Hand").gameObject.GetComponent<Transform>();
        }
    }
}
