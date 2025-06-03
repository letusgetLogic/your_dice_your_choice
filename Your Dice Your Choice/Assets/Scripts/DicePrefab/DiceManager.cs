using System.Collections.Generic;
using System;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    protected bool IsDiceOnSlot { get; set; }

    private DiceDragEvent _dragEvent => GetComponent<DiceDragEvent>();


    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        IsDiceOnSlot = false;
    }

    /// <summary>
    /// Sets the component DiceDragEvent true/false.
    /// </summary>
    protected void SetDragEventEnable(bool value)
    {
        _dragEvent.enabled = value;
    }

}
