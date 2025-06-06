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

        private DiceManager _diceManager => GetComponent<DiceManager>();
        private RectTransform _rectTransform => GetComponent<RectTransform>();
        private Dice _dice => GetComponent<Dice>();
        private RollPanel _rollPanel => _dice.RollPanel.GetComponent<RollPanel>();

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
                    _diceManager.SetDragEventEnable(true);
                    return;
                }

                _currentValue = Mathf.MoveTowards(_currentValue, 1, _animSpeed / Time.deltaTime);
                var lerpPos = Vector2.Lerp(_currentPosition, _basePosition, _animCurve.Evaluate(_currentValue));
                _rectTransform.anchoredPosition = lerpPos;
            }
        }
        public void SendBackToBase()
        {
            if (_diceManager.DiceOnEndDrag)
            Debug.Log("_basePosition " + _basePosition);
            _currentPosition = _rectTransform.anchoredPosition;
            _isRunning = true;
            _diceManager.SetDragEventEnable(false);
            Debug.Log("set _isRunning " + _isRunning);
           
        }

        /// <summary>
        /// Positions to the dice slot in the action panel.
        /// </summary>
        public void PositionsTo(Vector2 pos)
        {
            Debug.Log("diceSlot pos " + pos);
            _rectTransform.position = pos;

            _rollPanel.SetNull(_dice.IndexOnPanel);
        }
    }
}
