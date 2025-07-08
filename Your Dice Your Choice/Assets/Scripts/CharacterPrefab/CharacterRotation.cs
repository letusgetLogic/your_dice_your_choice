using UnityEngine;

namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterRotation : MonoBehaviour
    {
        [SerializeField] private Transform _bodyTransform;

        /// <summary>
        /// Rotate the body transform.
        /// </summary>
        public void RotateBody(int number)
        {
            var rotation = _bodyTransform.rotation;
            rotation.z += number;
            _bodyTransform.rotation = rotation;
        }
    }
}

