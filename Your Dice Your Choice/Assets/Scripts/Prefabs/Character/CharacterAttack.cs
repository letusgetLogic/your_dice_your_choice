using System;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public float CurrentAP { get; private set; }
    public float BuffAP { get; private set; }
    public string BuffAPText { get; private set; }
    public Color BuffAPColor { get; private set; }

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
    /// Sets the value of attack points buff.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="hit"></param>
    /// <param name="round"></param>
    public void SetBuffAP(float value)
    {
        BuffAP = value;
    }

    /// <summary>
    /// Sets the value default.
    /// </summary>
    public void SetDefault()
    {
        CurrentAP = _originalAP;
        BuffAP = 0f;
    }

}

