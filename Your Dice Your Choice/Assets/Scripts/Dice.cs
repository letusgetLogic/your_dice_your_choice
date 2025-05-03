using UnityEngine;

public class Dice : MonoBehaviour
{
    public Sprite[] DiceSide;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        var defaultSprite = gameObject.GetComponent<Sprite>();
        defaultSprite = DiceSide[6];
    }

    /// <summary>   
    /// Initializes dice side.
    /// </summary>
    /// <param name="sideIndex"></param>
    public void InitializeSide(int sideIndex)
    {
        var currentSprite = gameObject.GetComponent<Sprite>();
        currentSprite = DiceSide[sideIndex];
    }
}

