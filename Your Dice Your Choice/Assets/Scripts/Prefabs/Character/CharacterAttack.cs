using System;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public float CurrentAP { get; private set; }
    public float BuffAP { get; private set; }
    public int HitEndurance { get; private set; }
    public int RoundEndurance { get; private set; }
    private float _originalAP => GetComponent<Character>().Data.AP;

    /// <summary>
    /// Initialize Data.
    /// </summary>
    /// <param name="data"></param>
    public void SetData()
    {
        CurrentAP = _originalAP;
    }

    /// <summary>
    /// Sets the value of attack points.
    /// </summary>
    /// <param name="value"></param>
    public void SetAP(float value)
    {
        CurrentAP = value;
    }

    /// <summary>
    /// Sets the value of attack points.
    /// </summary>
    /// <param name="value"></param>
    public void SetBuffAP(float value, int hit, int round)
    {
        BuffAP = value;
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
            BuffAP = 0f;
            CurrentAP = _originalAP;
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
            BuffAP = 0f;
            CurrentAP = _originalAP;
            HitEndurance = 0;
        }
    }
}

