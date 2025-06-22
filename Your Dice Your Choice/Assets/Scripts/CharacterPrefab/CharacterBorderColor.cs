using UnityEngine;

namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterBorderColor : MonoBehaviour
    {
        [SerializeField] private float _animSpeedAct = 0.0001f;
        [SerializeField] private float _colorMaxR = 0.6f;
        [SerializeField] private float _colorMinR = 0.1f;
        [SerializeField] private AnimationCurve _animCurve;

        private enum LightenState { None, LightenUp, LightenDown }
        private LightenState _lightenState = LightenState.None;

        private float _currentValue = 0f;

        private SpriteRenderer _bodyBorderSpriteRenderer;
        private SpriteRenderer _leftHandBorderSpriteRenderer;
        private SpriteRenderer _rightHandBorderSpriteRenderer;

        private SpriteRenderer[] _borderSpriteRenderers;


        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            _lightenState = LightenState.LightenUp;
        }

        /// <summary>
        /// Start method.
        /// </summary>
        private void Start()
        {
            _bodyBorderSpriteRenderer = transform.Find("Pivot").Find("Character Body").Find("Border").gameObject.GetComponent<SpriteRenderer>();
            _leftHandBorderSpriteRenderer = transform.Find("Pivot").Find("Character Left Hand").Find("Border").gameObject.GetComponent<SpriteRenderer>();
            _rightHandBorderSpriteRenderer = transform.Find("Pivot").Find("Character Right Hand").Find("Border").gameObject.GetComponent<SpriteRenderer>();

            _borderSpriteRenderers = new SpriteRenderer[]
            {
                _bodyBorderSpriteRenderer,
                _leftHandBorderSpriteRenderer,
                _rightHandBorderSpriteRenderer
            };

            this.enabled = false;
        }

        /// <summary>
        /// Update method.
        /// </summary>
        private void Update()
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

                _currentValue = Mathf.MoveTowards(_currentValue, 1f, _animSpeedAct / Time.deltaTime);
                float dimValue = Mathf.Lerp(_colorMinR, _colorMaxR, _animCurve.Evaluate(_currentValue));

                SetBorderColorR(dimValue);
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

                _currentValue = Mathf.MoveTowards(_currentValue, 0f, _animSpeedAct / Time.deltaTime);
                float dimValue = Mathf.Lerp(_colorMinR, _colorMaxR, _animCurve.Evaluate(_currentValue));

                SetBorderColorR(dimValue);
            }
        }

        /// <summary>
        /// Sets the R color of the borders.
        /// </summary>
        /// <param name="rValue"></param>
        private void SetBorderColorR(float rValue)
        {
            foreach (var item in _borderSpriteRenderers)
            {
                item.color = new Color(rValue, item.color.g, item.color.b);
            }
        }
    }
}
