using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    public RectTransform StartPositionLeftAct2;
    public RectTransform StartPositionRightAct2; 
    public RectTransform EndPositionLeftAct2;
    public RectTransform EndPositionRightAct2;

    public GameObject ForegroundTilemap;
    

    [SerializeField] private float _act1Time = 3f;
    [SerializeField] private float _act2Time = 3f;

    [SerializeField] private float _animSpeedAct1 = 0.0001f;
    [SerializeField] private float _animSpeedAct2 = 0.0001f;
    [SerializeField] private float _animSpeedAct3 = 0.001f;
    [SerializeField] private float _animFadeInTime = 2f;
    [SerializeField] private AnimationCurve _animCurve1;

    private readonly string PlayerNameLeft = "Player 1";
    private readonly string PlayerNameRight = "Player 2";

    private enum PlayStates { None, Act1 , Act2, Act3 }
    private PlayStates _playStates;


    private TextMeshProUGUI[] _textArray;

    private Vector2 _startPositionLeftAct1 => LeftIntroShaderRect.anchoredPosition;
    private Vector2 _startPositionRightAct1 => RightIntroShaderRect.anchoredPosition;
    private Vector2 _startPositionLeftAct2 => StartPositionLeftAct2.anchoredPosition;
    private Vector2 _startPositionRightAct2 => StartPositionRightAct2.anchoredPosition;
    private Vector2 _endPositionLeftAct2 => EndPositionLeftAct2.anchoredPosition;
    private Vector2 _endPositionRightAct2 => EndPositionRightAct2.anchoredPosition;

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


        _current = 0f;

        InitializeArray();
        SetIntroInactive();
    }

    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        PlayAct1();
        PlayAct2();
        PlayAct3();
    }

    /// <summary>
    /// Plays the act 1.
    /// </summary>
    private void PlayAct1()
    {
        if (_playStates == PlayStates.Act1)
        {
            _current = Mathf.MoveTowards(_current, 1, _animSpeedAct1 / Time.deltaTime);

            LeftIntroShaderRect.anchoredPosition = Vector2.Lerp(_startPositionLeftAct1, _startPositionLeftAct2, _animCurve1.Evaluate(_current));
            RightIntroShaderRect.anchoredPosition = Vector2.Lerp(_startPositionRightAct1, _startPositionRightAct2, _animCurve1.Evaluate(_current));

            FadeIn();

            if (_animCurve1.Evaluate(_current) >= 1)
            {
                _current = 0f;
                _playStates = PlayStates.None;
            }
        }
    }

    /// <summary>
    /// Plays the act 2.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void PlayAct2()
    {
        if (_playStates == PlayStates.Act2)
        {
            _current = Mathf.MoveTowards(_current, 1, _animSpeedAct2 / Time.deltaTime);

            LeftIntroShaderRect.anchoredPosition = Vector2.Lerp(_startPositionLeftAct2, _endPositionLeftAct2, _animCurve1.Evaluate(_current));
            RightIntroShaderRect.anchoredPosition = Vector2.Lerp(_startPositionRightAct2, _endPositionRightAct2, _animCurve1.Evaluate(_current));

            var value = _animCurve1.Evaluate(_current);
            Debug.Log(value);

            if (value >= 1)
            {
                _current = 0f;

                TurnManager.Instance.SetDiceAndPanel();

                LevelManager.Instance.NextPhase();

                _playStates = PlayStates.None;
            }
        }
    }
    
    /// <summary>
    /// Plays the act 3.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void PlayAct3()
    {
        if (_playStates == PlayStates.Act3)
        {
            _current = Mathf.MoveTowards(_current, 1, _animSpeedAct3 / Time.deltaTime);

            ForegroundTilemap.GetComponent<Tilemap>().color = new Color(1, 1, 1, 1 - _animCurve1.Evaluate(_current));

            float ratio = Mathf.Lerp(0, 1, _animCurve1.Evaluate(_current));

            TurnManager.Instance.ScaleUp(ratio);

            if (ratio >= 1)
            {
                _current = 0f;
               
                TurnManager.Instance.RollDice();

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
    /// Plays the intro.
    /// </summary>
    public void Play()
    {
        LeftIntroText.text = PlayerNameLeft;
        LeftIntroShaderText.text = PlayerNameLeft;
        RightIntroText.text = PlayerNameRight;
        RightIntroShaderText.text = PlayerNameRight;

        SetIntroActive();
    }

    /// <summary>
    /// Sets the intro active.
    /// </summary>
    private void SetIntroActive()
    {
        foreach (var item in _textArray)
        {
            item.gameObject.SetActive(true);
            item.alpha = 0f;
        }

        _playStates = PlayStates.Act1;

        StartCoroutine(SetAct2());  
    }

    /// <summary>
    /// Sets Act 2.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetAct2()
    {
        yield return new WaitForSeconds(_act1Time);

        _playStates = PlayStates.Act2;

        StartCoroutine(SetAct3());
    }
    
    /// <summary>
    /// Sets Act 3.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetAct3()
    {
        yield return new WaitForSeconds(_act2Time);

        _playStates = PlayStates.Act3;
    }

    /// <summary>
    /// Ends this phase.
    /// </summary>
    public void EndPhase()
    {
        MatchIntroManager.Instance.SetIntroInactive();
        LevelManager.Instance.NextPhase();
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

    /// <summary>
    /// Sets the intro inactive.
    /// </summary>
    public void SetIntroInactive()
    {
        _playStates = PlayStates.None;

        foreach (var item in _textArray)
        {
            item.gameObject.SetActive(false);
        }

        LeftIntroShaderRect.anchoredPosition = _startPositionLeftAct1;
        RightIntroShaderRect.anchoredPosition = _startPositionRightAct1;
    }
}

