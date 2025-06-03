using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.DicePrefab
{
    public class DiceMovement : DiceManager
    {
        [SerializeField] private float _animSpeed = 0.0001f;
        [SerializeField] private AnimationCurve _animCurve;

        
        private RectTransform _rectTransform => GetComponent<RectTransform>();
        private Vector2 _basePosition;
        private Vector2 _currentPosition;

        private bool _isRunning = false;
        private float _currentValue = 0f;

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
            Debug.Log("Dice Movement IsDiceOnSlot " + IsDiceOnSlot);
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
                    Debug.Log("_rectTransform.anchoredPosition " + _rectTransform.anchoredPosition);
                    _currentValue = 0f;
                    _isRunning = false;
                    SetDragEventEnable(true);
                    return;
                }

                _currentValue = Mathf.MoveTowards(_currentValue, 1, _animSpeed / Time.deltaTime);
                var lerpPos = Vector2.Lerp(_currentPosition, _basePosition, _animCurve.Evaluate(_currentValue));
                _rectTransform.anchoredPosition = lerpPos;
            }
        }
        public void SendBackToBase()
        {
            Debug.Log("_basePosition " + _basePosition);
            _currentPosition = _rectTransform.anchoredPosition;
            _isRunning = true;
            SetDragEventEnable(false);
            Debug.Log("set _isRunning " + _isRunning);
           
        }

        /// <summary>
        /// Positions to the dice slot in the action panel.
        /// </summary>
        public void PositionsTo(Vector2 pos)
        {
            Debug.Log("diceSlot pos " + pos);
            _rectTransform.position = pos;
            Debug.Log("IsDiceOnSlot = true ");
            IsDiceOnSlot = true;

            var dice = GetComponent<Dice>();
            var rollPanel = dice.RollPanel.GetComponent<RollPanel>();
            rollPanel.SetNull(dice.IndexOnPanel);
        }
    }
}
