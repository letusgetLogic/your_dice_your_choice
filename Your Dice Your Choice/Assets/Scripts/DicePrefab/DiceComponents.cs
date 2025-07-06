using UnityEngine;

public class DiceComponents : MonoBehaviour
{
    public DiceDragEvent DragEvent => GetComponent<DiceDragEvent>();

    /// <summary>
    /// Sets the component enabled true/false.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="value"></param>
    public void SetEnabled(Component component, bool value)
    {
        if (component is Behaviour behaviour)
        {
            behaviour.enabled = value;
        }
    }
}
