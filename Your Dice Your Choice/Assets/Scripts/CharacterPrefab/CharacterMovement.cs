using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterMovement : CharacterComponents
    {
        [SerializeField] private float _speed = .01f;
       
        private Vector3 _targetPosition;
        private bool _isMoving = false;

        /// <summary>
        /// Defines the target position and sets _isMoving true.
        /// </summary>
        /// <param name="position"></param>
        public void MoveTo(Vector3 position)
        {
            _targetPosition = position;
            _isMoving = true;
        }

        /// <summary>
        /// Update method.
        /// </summary>
        public void Update()
        {
            if (_isMoving)
            {
                if (transform.position == _targetPosition)
                {
                    _isMoving = false;
                    return;
                }

                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed);
            }
        }
    }
}

