using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.DicePrefab
{
    public class DiceMovement : MonoBehaviour
    {
        [SerializeField] private float _animSpeed = 0.0001f;
        [SerializeField] private AnimationCurve _animCurve;

        private RectTransform _rectTransform => GetComponent<RectTransform>();
        private Vector2 _basePosition;
        private Vector2 _currentPosition;

        private bool _isRunning = false;
        private float _current = 0f;

        /// <summary>
        /// Start method.
        /// </summary>
        private void Start()
        {
            _basePosition = _rectTransform.anchoredPosition;
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
            _currentPosition = _rectTransform.anchoredPosition;
            _isRunning = true;
            Debug.Log("set _isRunning " + _isRunning);
        }

        /// <summary>
        /// Lerps the movement of the dice.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void LerpMovement()
        {
            if (_isRunning)
            {
                if (_rectTransform.anchoredPosition == _basePosition)
                {
                    _current = 0f;
                    _isRunning = false;
                    return;
                }
                Debug.Log("_isRunning " + _isRunning);
                _current = Mathf.MoveTowards(_current, 1, _animSpeed / Time.deltaTime);
                var lerpPos = Vector2.Lerp(_currentPosition, _basePosition, _animCurve.Evaluate(_current));
                _rectTransform.anchoredPosition = lerpPos;
            }
        }

        /// <summary>
        /// Positions to the dice slot in the action panel.
        /// </summary>
        public void PositionsTo(Vector2 pos)
        {
            Debug.Log("actionManager set _isRunning " + _isRunning);
            _rectTransform.anchoredPosition = pos;

            var dice = GetComponent<Dice>();
            var rollPanel = dice.RollPanel.GetComponent<RollPanel>();
            rollPanel.SetNull(dice.IndexOnPanel);
        }
    }
}
