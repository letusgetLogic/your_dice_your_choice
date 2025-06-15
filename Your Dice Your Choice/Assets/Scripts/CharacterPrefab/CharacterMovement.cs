using Assets.Scripts.FieldPrefab;
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
        public void MoveTo(GameObject fieldObject)
        {
            _targetPosition = fieldObject.transform.position;
            _isMoving = true;

            var character = GetComponent<Character>();
            var field = fieldObject.GetComponent<Field>();
            character.SetFieldIndex(field.Index);
        }

        /// <summary>
        /// FixedUpdate method.
        /// </summary>
        public void FixedUpdate()
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

