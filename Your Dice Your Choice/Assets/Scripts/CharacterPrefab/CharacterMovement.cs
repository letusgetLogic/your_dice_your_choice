using Assets.Scripts.FieldPrefab;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = .01f;

        private Transform _bodyPivotTransform => GetComponent<CharacterComponents>().BodyPivotTransform;

        private Vector3 _targetPosition;
        private bool _isMoving = false;


        /// <summary>
        /// FixedUpdate method.
        /// </summary>
        private void FixedUpdate()
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

        /// <summary>
        /// Sets the body pivot local position.
        /// </summary>
        public void SetBodyPivot(Vector3 pos)
        {
            _bodyPivotTransform.localPosition = pos;
        }

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

    }
}

