using System;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public Sprite[] DiceSide;

    [NonSerialized] public int CurrentNumber = 0; // Does it need to be a property?

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        var defaultImage = gameObject.GetComponent<Image>();
        defaultImage.sprite = DiceSide[6];
    }

    /// <summary>   
    /// Initializes dice side.
    /// </summary>
    /// <param name="sideIndex"></param>
    public void InitializeSide(int sideIndex)
    {
        var currentImage = gameObject.GetComponent<Image>();
        currentImage.sprite = DiceSide[sideIndex];
        CurrentNumber = sideIndex;
    }
}

