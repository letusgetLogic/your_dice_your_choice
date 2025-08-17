using UnityEngine;

public class OpenURL : MonoBehaviour
{
    [SerializeField] string _url;

    /// <summary>
    /// Opens the website with url.
    /// </summary>
    /// <param name="url"></param>
    public void OpenWebsite()
    {
        Application.OpenURL(_url);
    }
}
