using UnityEngine;

public class CharacterBorderColor : MonoBehaviour
{
    [SerializeField] private GameObject[] _borders;
    [SerializeField] private float _animSpeedAct = 1f;
    [SerializeField] private float _colorMaxR = 0.6f;
    [SerializeField] private float _colorMinR = 0.1f;
    [SerializeField] private float _scaleMax = 1.2f;
    [SerializeField] private float _scaleMin = 1f;
    [SerializeField] private AnimationCurve _animCurve;

    private enum LightenState
    {
        None,
        LightenUp,
        LightenDown
    }
    private LightenState _lightenState = LightenState.None;

    private float _currentValue = 0f;

    /// <summary>
    /// OnEnable method.
    /// </summary>
    private void OnEnable()
    {
        _lightenState = LightenState.LightenUp;
    }

    /// <summary>
    /// FixedUpdate method.
    /// </summary>
    private void FixedUpdate()
    {
        LightenBorderUp();
        LightenBorderDown();
    }

    /// <summary>
    /// OnDisable method.
    /// </summary>
    private void OnDisable()
    {
        _lightenState = LightenState.None;
        SetBorderColorR(_colorMinR);
        SetBorderScale(_scaleMin);
    }

    /// <summary>
    /// Lightens the border up.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void LightenBorderUp()
    {
        if (_lightenState == LightenState.LightenUp)
        {
            if (_currentValue == 1)
            {
                _lightenState = LightenState.LightenDown;
                return;
            }

            Interpolate(1f);
        }
    }

    /// <summary>
    /// Lightens the border down.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void LightenBorderDown()
    {
        if (_lightenState == LightenState.LightenDown)
        {
            if (_currentValue == 0)
            {
                _lightenState = LightenState.LightenUp;
                return;
            }

            Interpolate(0f);
        }
    }

    /// <summary>
    /// Interolates the value and sets the border color and scale.
    /// </summary>
    /// <param name="target"></param>
    private void Interpolate(float target)
    {
        _currentValue = Mathf.MoveTowards(
            _currentValue, target, _animSpeedAct * 0.0001f / Time.deltaTime);

        float dimValue =
            Mathf.Lerp(_colorMinR, _colorMaxR, _animCurve.Evaluate(_currentValue));
        SetBorderColorR(dimValue);

        float scaleValue =
            Mathf.Lerp(_scaleMin, _scaleMax, _animCurve.Evaluate(_currentValue));
        SetBorderScale(scaleValue);
    }

    /// <summary>
    /// Sets the R color of the borders.
    /// </summary>
    /// <param name="rValue"></param>
    private void SetBorderColorR(float rValue)
    {
        foreach (var item in _borders)
        {
            var spriteRenderer = item.GetComponent<SpriteRenderer>();
            spriteRenderer.color =
                new Color(rValue, spriteRenderer.color.g, spriteRenderer.color.b);
        }
    }

    /// <summary>
    /// Sets the scale.
    /// </summary>
    /// <param name="rValue"></param>
    private void SetBorderScale(float value)
    {
        foreach (var item in _borders)
        {
            item.transform.localScale = new Vector3(value, value, value);
        }
    }

}

