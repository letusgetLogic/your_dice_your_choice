using TMPro;
using UnityEngine;

public class DevelopTool : MonoBehaviour
{
    public static DevelopTool Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _openArrow;
    [SerializeField] private GameObject _window;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    public void OnWindow()
    {
        _openArrow.text = _window.activeSelf ? ">" : "<";
        _window.SetActive(!_window.activeSelf);
    }

    public void OnMatchOver()
    {
        OnWindow();
        
        //LevelManager.Instance.SetPhase(Phase.Initialization);
        //LevelManager.Instance.SetPhase(Phase.Battle);
        LevelManager.Instance.SubmitWinnerFrom(PlayerType.PlayerLeft);
        LevelManager.Instance.SetPhase(Phase.MatchOver);
    }
}

