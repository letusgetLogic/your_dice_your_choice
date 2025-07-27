using System;
using TMPro;
using UnityEngine;

public class MatchOverView : MonoBehaviour
{
    public static MatchOverView Instance { get; private set; }

    [SerializeField] 
    private TextMeshProUGUI _matchStateShaderText;
    public RectTransform MatchStateShaderRect => _matchStateShaderRect;

    [SerializeField] private TextMeshProUGUI _matchStateText;
    [SerializeField] private TextMeshProUGUI _winnerStateShaderText;
    [SerializeField] private TextMeshProUGUI _winnerStateText;

    [SerializeField] private RectTransform _matchStateShaderRect;
    [SerializeField] private RectTransform _endPosition;

    public Vector2 StartPosition => _matchStateShaderRect.anchoredPosition;
    public Vector2 EndPosition => _endPosition.anchoredPosition;

    public TextMeshProUGUI[] MatchStateArray => new[]
    {
            _matchStateShaderText,
            _matchStateText
        };

    public TextMeshProUGUI[] WinnerStateArray => new[]
    {
            _winnerStateShaderText,
            _winnerStateText
        };

    private TextMeshProUGUI[] _textArray => new[]
    {
            _matchStateShaderText,
            _matchStateText,
            _winnerStateShaderText,
            _winnerStateText,
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
    public void SetText(string playerName)
    {
        _winnerStateShaderText.text = $"<b>{playerName}</b> has won the match!";
        _winnerStateText.text = $"<b>{playerName}</b> has won the match!";
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
            Debug.Log($"{item.name} is set to active: {value}");  // Debug log to indicate active state change
            Debug.Log($"{item.name} alpha set to: {item.alpha}");  // Debug log to indicate alpha value
        }
    }

    /// <summary>
    /// Fades in the text components. 
    /// </summary>
    public void FadeIn(TextMeshProUGUI[] textMeshProUGUIs, float animSpeed)
    {
        foreach (var item in textMeshProUGUIs)
        {
            if (item.alpha < 0.9f)
                item.alpha += animSpeed * Time.deltaTime;
            else
            {
                item.alpha = 1f;
            }
        }
    }
}
