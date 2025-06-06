using System.Collections.Generic;
using System;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public bool IsDiceOnSlot { get; private set; }
    public bool DiceOnEndDrag { get; private set; }
    public bool DiceSlotOnDrop { get; private set; }

    private DiceDragEvent _dragEvent => GetComponent<DiceDragEvent>();


    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        Debug.Log("DiceManager.Start()");
        //IsDiceOnSlot = true;
    }

    /// <summary>
    /// Sets the component DiceDragEvent true/false.
    /// </summary>
    public void SetDragEventEnable(bool value)
    {
        _dragEvent.enabled = value;
    }

    /// <summary>
    /// Sets IsDIceOnSlot true/false
    /// </summary>
    /// <param name="value"></param>
    public void SetIsDiceOnSlot(bool value)
    {
        IsDiceOnSlot = value;
    }

    /// <summary>
    /// Sets DiceOnEndDrag true/false
    /// </summary>
    /// <param name="value"></param>
    public void SetDiceOnEndDrag(bool value)
    {
        DiceOnEndDrag = value;
    }

    /// <summary>
    /// Sets DiceSlotOnDrop true/false
    /// </summary>
    /// <param name="value"></param>
    public void SetDiceSlotOnDrop(bool value)
    {
        DiceSlotOnDrop = value;
    }

}
