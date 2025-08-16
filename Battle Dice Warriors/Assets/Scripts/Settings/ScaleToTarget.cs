using System;
using UnityEngine;

public class ScaleToTarget : MonoBehaviour
    {
    [SerializeField] private Vector3 _target = new(0, 0, 0);
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private AnimationCurve _curve;

    private bool _isScaling = false;
    private bool _isScalingToTarget = false;

    private Vector3 _originScale;
    private Vector3 _targetScale;

    private float _currentValue;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        _originScale = GetComponent<RectTransform>().localScale;
    }

    /// <summary>
    /// External button triggers.
    /// </summary>
    public void Trigger()
    {
        _currentValue = 0;
        _isScalingToTarget = !_isScalingToTarget;

        if (_isScalingToTarget)
            _targetScale = _target;
        else
            _targetScale = _originScale;

        _isScaling = true;
    }

    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        if (_isScaling)
        {
            ScaleTo(_targetScale);
        }
    }

    /// <summary>
    /// Scale to target.
    /// </summary>
    /// <param name="targetScale"></param>
    private void ScaleTo(Vector3 targetScale)
    {
        var currentScale = GetComponent<RectTransform>().localScale;
        var distance = currentScale - targetScale;

        _currentValue = Mathf.MoveTowards(
            _currentValue, 1, _speed *0.0001f / Time.deltaTime);

        float value = _curve.Evaluate(_currentValue);

        GetComponent<RectTransform>().localScale = 
            Vector3.Lerp(currentScale, targetScale, value);

        if (value >= 1)
        {
            _isScaling = false;
        }
    }
}

