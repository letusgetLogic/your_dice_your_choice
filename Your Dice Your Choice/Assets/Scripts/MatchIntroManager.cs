using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MatchIntroManager : MonoBehaviour
{
    public static MatchIntroManager Instance { get; private set; }

    public TextMeshProUGUI LeftIntroText;
    public TextMeshProUGUI VersusIntroText;
    public TextMeshProUGUI RightIntroText;
    public TextMeshProUGUI LeftIntroShaderText;
    public TextMeshProUGUI VersusIntroShaderText;
    public TextMeshProUGUI RightIntroShaderText;

    public RectTransform LeftIntroShaderRect;
    public RectTransform RightIntroShaderRect;
    public RectTransform EndPositionLeft;
    public RectTransform EndPositionRight;

    public float IntroTime = 3f;

    [SerializeField] private float _animSpeedAct1 = 0.0001f;
    [SerializeField] private float _animFadeInTime = 2f;
    [SerializeField] private AnimationCurve _animCurve1;

    private readonly string PlayerNameLeft = "Player 1";
    private readonly string PlayerNameRight = "Player 2";

    private enum PlayStates { None, Act1 , Act2 }
    private PlayStates _playStates;


    private TextMeshProUGUI[] _textArray;

    private RectTransform _startPositionLeft;
    private RectTransform _startPositionRight;

    private float _current;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;

        _startPositionLeft = LeftIntroShaderRect;
        _startPositionRight = RightIntroShaderRect;

        _current = 0f;

        InitializeArray();
        SetIntroInactive();
    }

    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        if (_playStates == PlayStates.Act1)
        {
            var startPosLeft = _startPositionLeft.anchoredPosition;
            var endPosLeft = EndPositionLeft.anchoredPosition;

            var startPosRight = _startPositionRight.anchoredPosition;
            var endPosRight = EndPositionRight.anchoredPosition;

            _current = Mathf.MoveTowards(_current, 1, _animSpeedAct1 / Time.deltaTime);

            LeftIntroShaderRect.anchoredPosition = Vector2.Lerp(startPosLeft, endPosLeft, _animCurve1.Evaluate(_current));
            RightIntroShaderRect.anchoredPosition = Vector2.Lerp(startPosRight, endPosRight, _animCurve1.Evaluate(_current));

            FadeIn();

            if (LeftIntroShaderRect.anchoredPosition == endPosLeft)
            {
                _playStates = PlayStates.None;
            }
        }
    }

    /// <summary>
    /// Initializes the text array.
    /// </summary>
    private void InitializeArray()
    {
        _textArray = new[]
        { 
            LeftIntroText, 
            VersusIntroText, 
            RightIntroText, 
            LeftIntroShaderText, 
            VersusIntroShaderText, 
            RightIntroShaderText
        };
    }

    /// <summary>
    /// Sets the intro inactive.
    /// </summary>
    public void SetIntroInactive()
    {
        _playStates = PlayStates.None;

        foreach (var item in _textArray)
        {
            item.gameObject.SetActive(false);
            item.alpha = 0f;
        }

        LeftIntroShaderRect.anchoredPosition = _startPositionLeft.anchoredPosition;
        RightIntroShaderRect.anchoredPosition = _startPositionRight.anchoredPosition;
    }

    /// <summary>
    /// Plays the intro.
    /// </summary>
    public void Play()
    {
        LeftIntroText.text = PlayerNameLeft;
        LeftIntroShaderText.text = PlayerNameLeft;
        RightIntroText.text = PlayerNameRight;
        RightIntroShaderText.text = PlayerNameRight;

        SetIntroActive();
        StartCoroutine(EndPhase());
    }

    private IEnumerator EndPhase()
    {
        yield return new WaitForSeconds(IntroTime);

        MatchIntroManager.Instance.SetIntroInactive();
        LevelManager.Instance.NextPhase();
    }

    /// <summary>
    /// Sets the intro active.
    /// </summary>
    private void SetIntroActive()
    {
        foreach (var item in _textArray)
        {
            item.gameObject.SetActive(true);
        }

        _playStates = PlayStates.Act1;
    }

    /// <summary>
    /// Fades in the intro. 
    /// </summary>
    private void FadeIn()
    {
        foreach (var item in _textArray)
        {
            if (item.alpha < 1f)
                item.alpha += _animFadeInTime * Time.deltaTime;
            else
                item.alpha = 1f;  
        }
    }
}

