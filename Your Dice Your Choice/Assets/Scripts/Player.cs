using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string Name;
    public TurnState PlayerTurn;
    public List<GameObject> Characters;
    public GameObject[] CharacterPanels;

    public Player(string name, TurnState player, List<GameObject> characters, GameObject[] characterPanels)
    {
        Name = name;
        PlayerTurn = player;
        Characters = characters;
        CharacterPanels = characterPanels;
    }

    public Player PlayerLeft;
    public Player PlayerRight;

    /// <summary>
    /// Create the player instances.
    /// </summary>
    private void CreateInstances()
    {
        PlayerLeft = new Player(
            PanelManager.Instance.NameTextLeft.text,
            TurnState.PlayerLeft,
            )
    }
}

