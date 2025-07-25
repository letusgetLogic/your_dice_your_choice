﻿using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableData/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public MatchType MatchType;
    public int MapLength;
    public int MapHeight;
    public float CamOrthographicSize;
    public int CharacterAmount;
    public int DiceAmount;
}
