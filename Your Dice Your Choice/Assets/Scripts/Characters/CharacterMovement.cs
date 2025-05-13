using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private Transform _pivotTransform;
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private Transform _leftHandTransform;
        [SerializeField] private Transform _rightHandTransform;

        private Transform _characterTransform;

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
