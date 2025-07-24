using UnityEngine;

public class CharacterDefense : MonoBehaviour
    {
    public float CurrentDP { get; private set; }
    public float BuffDP { get; private set; }
    public int HitEndurance { get; private set; }
    public int RoundEndurance { get; private set; }
    private float _originalDP => GetComponent<Character>().Data.DP;

    /// <summary>
    /// Initialize Data.
    /// </summary>
    /// <param name="data"></param>
    public void SetData()
    {
        CurrentDP = _originalDP;
    }

    /// <summary>
    /// Sets the value of defense points.
    /// </summary>
    /// <param name="value"></param>
    public void SetDP(float value)
    {
        CurrentDP = value;
    }

    /// <summary>
    /// Sets the value of defense points.
    /// </summary>
    /// <param name="value"></param>
    public void SetBuffDP(float value, int hit, int round)
    {
        BuffDP = value;
        HitEndurance = hit;
        RoundEndurance = round;
    }

    /// <summary>
    /// Counts down the HitEndurance and resets if it reaches zero.
    /// </summary>
    public void CountDownHitEndurance()
    {
        if (HitEndurance > 0)
        {
            HitEndurance--;
        }
        if (HitEndurance == 0)
        {
            BuffDP = 0f;
            CurrentDP = _originalDP;
            RoundEndurance = 0;
        }
    }

    /// <summary>
    /// Counts down the RoundEndurance and resets if it reaches zero.
    /// </summary>
    public void CountDownRoundEndurance()
    {
        if (RoundEndurance > 0)
        {
            RoundEndurance--;
        }
        if (RoundEndurance == 0)
        {
            BuffDP = 0f;
            CurrentDP = _originalDP;
            HitEndurance = 0;
        }
    }
}

