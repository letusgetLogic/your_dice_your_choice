using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelHint : MonoBehaviour
{
    [SerializeField] private float _animLightenUpTime = 0.5f;
    [SerializeField] private float _animSpeedAct = 0.0001f;
    [SerializeField] private float _colorMaxRbg = 100f;
    [SerializeField] private AnimationCurve _animCurve1;

    private Image _image;
    private bool _isRunning = false;
    private float _current = 0f;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        _image = gameObject.GetComponent<Image>();
    }

    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        LightenPanelUp();
    }

    /// <summary>
    /// Starts the hint animation.
    /// </summary>
    public void StartHintAnim()
    {
        _isRunning = true;
        StartCoroutine(EndHintAnim());
    }

    /// <summary>
    /// Ends the hint animation.
    /// </summary>
    /// <returns></returns>
    private IEnumerator EndHintAnim()
    {
        yield return new WaitForSeconds(_animLightenUpTime);

        _isRunning = false;
        _image.color = new Color(0f, 0f, 0f, _image.color.a);
    }

    /// <summary>
    /// Lightens the panel up.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void LightenPanelUp()
    {
        if (_isRunning)
        {
            _current = Mathf.MoveTowards(_current, 1, _animSpeedAct / Time.deltaTime);
            float dimValue = Mathf.Lerp(0f, _colorMaxRbg, _animCurve1.Evaluate(_current));

            _image.color = new Color(dimValue, dimValue, dimValue, _image.color.a);
        }
    }
}
