using UnityEngine;

namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterRotation : MonoBehaviour
    {
        private Transform _bodyTransform => GetComponent<CharacterComponents>().BodyTransform;

        /// <summary>
        /// Rotate the body transform.
        /// </summary>
        public void RotateBodyTransform(int number)
        {
            var rotation = _bodyTransform.rotation;
            rotation.z += number;
            _bodyTransform.rotation = rotation;
        }
    }
}

