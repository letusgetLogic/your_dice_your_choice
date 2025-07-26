using UnityEngine;

public class MatchOverModel : MonoBehaviour
{
    public static MatchOverModel Instance { get; private set; }

    [SerializeField] 
    private float _animSpeedAct1 = 1f;
    public float AnimSpeedAct1 => _animSpeedAct1;

    [SerializeField] 
    private float _animSpeedAct2 = 1f;
    public float AnimSpeedAct2 => _animSpeedAct2;

    [SerializeField] 
    private float _animFadeInSpeed = 2.5f;
    public float AnimFadeInSpeed => _animFadeInSpeed;

    [SerializeField] 
    private AnimationCurve _animCurve1;
    public AnimationCurve AnimCurve1 => _animCurve1;

    [SerializeField] 
    private AnimationCurve _animCurve2;
    public AnimationCurve AnimCurve2 => _animCurve2;

    public enum PlayState { None, Act1, Act2 }
    public PlayState CurrentState { get; private set; }
    public float CurrentValue { get; private set; }

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
    /// Sets the play state.
    /// </summary>
    /// <param name="state"></param>
    public void SetPlayState(PlayState state)
    {
        CurrentState = state;
    }

    /// <summary>
    /// Runs the current value.
    /// </summary>
    /// <param name="animSpeed"></param>
    public void RunCurrentValue(float animSpeed)
    {
        CurrentValue = Mathf.MoveTowards(CurrentValue, 1, animSpeed * 0.0001f / Time.deltaTime);
    }

    /// <summary>
    /// Gets the value of interpolation.
    /// </summary>
    /// <returns></returns>
    public float GetInterpolation(AnimationCurve animationCurve)
    {
        return animationCurve.Evaluate(CurrentValue);
    }

    /// <summary>
    /// Moves the text objects.
    /// </summary>
    /// <param name="animSpeed"></param>
    /// <param name="startPositionTop"></param>
    /// <param name="endPositionTop"></param>
    /// <param name="startPositionBottom"></param>
    /// <param name="endPositionBottom"></param>
    public void MoveText(RectTransform rect,
                        Vector2 startPosition, Vector2 endPosition,
                        float value)
    {
        rect.anchoredPosition = Vector2.Lerp(startPosition, endPosition, value);
    }

    /// <summary>
    /// Sets the default values.
    /// </summary>
    public void SetDefault()
    {
        CurrentValue = 0f;
        CurrentState = PlayState.None;
    }
}