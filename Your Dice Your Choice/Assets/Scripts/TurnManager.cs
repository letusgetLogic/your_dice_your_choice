using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public TurnState Turn { get; private set; }

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

        Turn = TurnState.None;
    }

    /// <summary>
    /// Sets the first turn.
    /// </summary>
    public void PhaseSetFirstTurn()
    {

    }

    /// <summary>
    /// Sets turn.
    /// </summary>
    /// <param name="state"></param>
    public void Set(TurnState state)
    {
        Turn = state;
    }
}
