using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance {  get; private set; }

    [SerializeField] private TextMeshProUGUI _nameTextLeft;
    [SerializeField] private TextMeshProUGUI _nameTextRight;
    [SerializeField] private GameObject[] _characterPanelsLeft;
    [SerializeField] private GameObject[] _characterPanelsRight;

    /// <summary>
    /// Sets the name of the corresponding player.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="name"></param>
    /// <exception cref="System.Exception"></exception>
    public void SetNameOf(TurnState player, string name)
    {
        switch (player)
        {
            case TurnState.None:
                throw new System.Exception($"TurnState has been set to {player}");
            case TurnState.PlayerLeft:
                _nameTextLeft.text = name;
                return;
            case TurnState.PlayerRight:
                _nameTextRight.text = name;
                return;
        }
    }

    /// <summary>
    /// Gets the serialized panels for the corresponding player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public GameObject[] CharacterPanels(TurnState player)
    {
        switch (player)
        {
            case TurnState.None:
                throw new System.Exception($"TurnState has been set to {player}");
            case TurnState.PlayerLeft:
                return _characterPanelsLeft;
            case TurnState.PlayerRight:
                return _characterPanelsRight;
        }
        
        return default;
    }
}
