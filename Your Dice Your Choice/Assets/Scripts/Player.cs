using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string Name;
    public TurnState PlayerTurn;
    public List<GameObject> Characters;
    public GameObject[] CharacterPanel;

    public Player(string name, TurnState player, List<GameObject> characters)
    {
        Name = name;
        PlayerTurn = player;
        Characters = characters;
    }
}

