using UnityEngine;

public class CameraSizeController : MonoBehaviour
{
    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        var cam = GetComponent<Camera>();

        if (cam != null && cam.orthographic)
        {
            cam.orthographicSize = LevelManager.Instance.Data.CamOrthographicSize;
        }
        else
        {
            Debug.LogWarning("Camera is not in orthographic mode or is not assigned.");
        }
    }
}