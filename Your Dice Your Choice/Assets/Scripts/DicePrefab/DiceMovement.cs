using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.DicePrefab
{
    public class DiceMovement : MonoBehaviour
    {
        [SerializeField] private float _animSpeed = .0001f;
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
                    _currentValue = 0f;
                    _isRunning = false;

                    if (TurnManager.Instance.Turn == PlayerType.None)
                        return;

                    _diceManager.SetDragEventEnable(true);

                    return;
                }

                _currentValue = Mathf.MoveTowards(_currentValue, 1, _animSpeed / Time.deltaTime);

                if (_currentValue > 0.9f)
                { 
                    _rectTransform.anchoredPosition = _basePosition;
                    return;
                }

                var lerpPos = Vector2.Lerp(_currentPosition, _basePosition, _animCurve.Evaluate(_currentValue));
                _rectTransform.anchoredPosition = lerpPos;
            }
        }

        /// <summary>
        /// Sends the dice back to the roll panel.
        /// </summary>
        public void SendBackToBase()
        {
            _currentPosition = _rectTransform.anchoredPosition;
            _isRunning = true;
            _diceManager.SetDragEventEnable(false);
        }

        /// <summary>
        /// Positions to the dice slot in the action panel.
        /// </summary>
        public void PositionsTo(Vector2 pos)
        {
            _rectTransform.position = pos;

            //_rollPanel.SetNull(_dice.IndexOnPanel);
        }
    }
}
