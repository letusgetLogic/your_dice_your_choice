using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableData/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public int MapLength;
    public int MapHeight;
    public float CamOrthographicSize;
    public int CharacterAmount;
}
