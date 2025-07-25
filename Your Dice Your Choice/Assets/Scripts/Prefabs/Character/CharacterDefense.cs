using System;
using UnityEngine;

public class CharacterDefense : MonoBehaviour
{
    public float CurrentDP { get; private set; }
    public float BuffDP { get; private set; }
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
    public void SetBuffDP(float value)
    {
        BuffDP = value;
    }


    /// <summary>
    /// Sets the value default.
    /// </summary>
    public void SetDefault()
    {
        CurrentDP = _originalDP;
        BuffDP = 0f;
    }

}

