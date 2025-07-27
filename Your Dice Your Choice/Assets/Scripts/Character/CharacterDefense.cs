using System;
using UnityEngine;

public class CharacterDefense : MonoBehaviour
{
    public float CurrentDP { get; set; }
    public float CurrentBuffDP { get; set; }
    public int CurrentDamageReduction { get; set; }
    public string CurrentBuffDPText { get; set; }
    public enum BuffType
    {
        None,
        DP,
        DamageReduction
    }
    public BuffType CurrentBuffType { get; set; } = BuffType.None;

    /// <summary>
    /// Sets the value default.
    /// </summary>
    public void SetDefault()
    {
        CurrentDP = GetComponent<Character>().Data.DP;
        CurrentBuffDP = 0f;
        CurrentBuffType = BuffType.None;
        CurrentBuffDPText = "";
    }

}

