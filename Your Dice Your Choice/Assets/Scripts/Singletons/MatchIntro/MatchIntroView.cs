using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MatchIntroView : MonoBehaviour
{
    public static MatchIntroView Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _playerNameLeft;
    [SerializeField] private TextMeshProUGUI _playerNameRight;

    [SerializeField] private TextMeshProUGUI _leftIntroShaderText;
    [SerializeField] private TextMeshProUGUI _leftIntroText;
    [SerializeField] private TextMeshProUGUI _versusIntroShaderText;
    [SerializeField] private TextMeshProUGUI _versusIntroText;
    [SerializeField] private TextMeshProUGUI _rightIntroShaderText;
    [SerializeField] private TextMeshProUGUI _rightIntroText;
    
    [SerializeField] private RectTransform _leftIntroShaderRect;
    [SerializeField] private RectTransform _rightIntroShaderRect;
    [SerializeField] private RectTransform _startPositionLeftAct2;
    [SerializeField] private RectTransform _startPositionRightAct2; 
    [SerializeField] private RectTransform _endPositionLeftAct2;
    [SerializeField] private RectTransform _endPositionRightAct2;
    
    [SerializeField] private GameObject _foregroundTilemap;

    public RectTransform LeftIntroShaderRect => _leftIntroShaderRect;
    public RectTransform RightIntroShaderRect => _rightIntroShaderRect;

    public Vector2 StartPositionLeftAct1 => _leftIntroShaderRect.anchoredPosition;
    public Vector2 StartPositionRightAct1 => _rightIntroShaderRect.anchoredPosition;
    public Vector2 StartPositionLeftAct2 => _startPositionLeftAct2.anchoredPosition;
    public Vector2 StartPositionRightAct2 => _startPositionRightAct2.anchoredPosition;
    public Vector2 EndPositionLeftAct2 => _endPositionLeftAct2.anchoredPosition;
    public Vector2 EndPositionRightAct2 => _endPositionRightAct2.anchoredPosition;

    private TextMeshProUGUI[] _textArray => new[]
    {
        _leftIntroShaderText,
        _leftIntroText,
        _versusIntroShaderText,
        _versusIntroText,
        _rightIntroShaderText,
        _rightIntroText,
    };

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
    }

    /// <summary>
    /// Sets the text of textMeshProUGUI.
    /// </summary>
    /// <param name="textMeshProUGUI"></param>
    /// <param name="value"></param>
    public void SetText()
    {
        _playerNameLeft.text = GameManager.Instance.PlayerLeftName;
        _playerNameRight.text = GameManager.Instance.PlayerRightName;

        _leftIntroShaderText.text = GameManager.Instance.PlayerLeftName;
        _leftIntroText.text = GameManager.Instance.PlayerLeftName;
        _rightIntroShaderText.text = GameManager.Instance.PlayerRightName;
        _rightIntroText.text = GameManager.Instance.PlayerRightName;
    }

    /// <summary>
    /// Sets the elements in _textArray active true/false.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="value"></param>
    public void SetTextArrayActive(bool value)
    {
        foreach (var item in _textArray)
        {
            item.gameObject.SetActive(value);
            item.alpha = 0f;
        }
    }

    /// <summary>
    /// Fades in the text components. 
    /// </summary>
    public void FadeIn(float animTime)
    {
        foreach (var item in _textArray)
        {
            if (item.alpha < 1f)
                item.alpha += animTime * Time.deltaTime;
            else
                item.alpha = 1f;
        }
    }

    /// <summary>
    /// Sets _foregroundTilemap active true/false.
    /// </summary>
    /// <param name="value"></param>
    public void SetForegroundActive(bool value)
    {
        _foregroundTilemap.SetActive(value);
    }

    /// <summary>
    /// Dims up the battleground.
    /// </summary>
    /// <param name="value"></param>
    public void DimDownForeground(float alphaValue)
    {
        _foregroundTilemap.GetComponent<Tilemap>().color = new Color(1, 1, 1, alphaValue);
    }

    /// <summary>
    /// Sets the values default.
    /// </summary>
    public void SetIntroInactive()
    {
        SetTextArrayActive(false);

        _leftIntroShaderRect.anchoredPosition = StartPositionLeftAct1;
        _rightIntroShaderRect.anchoredPosition = StartPositionRightAct1;
    }

    /// <summary>
    /// Sets the values default.
    /// </summary>
    public void SetDefault()
    {
        SetIntroInactive();
        SetForegroundActive(true);
        DimDownForeground(1.0f);
    }
}

