using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.DicePrefab
{
    public class DiceMovement : MonoBehaviour
    {
        public GameObject DiceSlot;

        [SerializeField] private float _animTime = 0.5f;
        [SerializeField] private float _animSpeed;
        [SerializeField] private AnimationCurve _animCurve;

        private Vector2 _basePosition;
        private Vector2 _currentPosition;

        private bool _isRunning = false;
        private float _current = 0f;

        /// <summary>
        /// Start method.
        /// </summary>
        private void Start()
        {
            if (_basePosition == null)
                _basePosition = GetComponent<RectTransform>().anchoredPosition;
        }

        /// <summary>
        /// Update method.
        /// </summary>
        private void Update()
        {
            LerpMovement();
        }

        public void SendBackToBase()
        {
            _currentPosition = GetComponent<RectTransform>().anchoredPosition;

            _isRunning = true;
        }

        /// <summary>
        /// Lerps the movement of the dice.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void LerpMovement()
        {
            if (_isRunning)
            {
                if (GetComponent<RectTransform>().anchoredPosition == _basePosition)
                {
                    _isRunning = false;
                    return;
                }

                _current = Mathf.MoveTowards(_current, 1, _animSpeed / Time.deltaTime);
                var lerpPos = Vector2.Lerp(_currentPosition, _basePosition, _animCurve.Evaluate(_current));
                GetComponent<RectTransform>().anchoredPosition = lerpPos;
            }
        }

        /// <summary>
        /// Positions to the dice slot.
        /// </summary>
        public void PositionsToDiceSlot()
        {
            transform.SetParent(DiceSlot.transform);
            GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            
        }
    }
}
