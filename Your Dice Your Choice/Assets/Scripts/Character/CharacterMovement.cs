using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        private Transform _pivotTransform;
        private Transform _bodyTransform;
        private Transform _leftHandTransform;
        private Transform _rightHandTransform;

        private Transform _characterTransform;

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            _pivotTransform = transform.parent.Find("Pivot").gameObject.GetComponent<Transform>();
            _bodyTransform = transform.parent.Find("Pivot").Find("Character Body").gameObject.GetComponent<Transform>();
            _leftHandTransform = transform.parent.Find("Pivot").Find("Character Left Hand").gameObject.GetComponent<Transform>();
            _rightHandTransform = transform.parent.Find("Pivot").Find("Character Right Hand").gameObject.GetComponent<Transform>();
        }

        /// <summary>
        /// Rotate the pivot point.
        /// </summary>
        public void RotatePivot(int number)
        {
            var rotation = _pivotTransform.rotation;
            rotation.z += number;
            _pivotTransform.rotation = rotation;
        }
    }
}
