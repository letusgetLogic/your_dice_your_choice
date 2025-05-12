using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public string Name { get; set; }
    public TurnState Player {  get; set; }
    public List<GameObject> Characters { get; set; }

    public PlayerStats(string name, TurnState player, List<GameObject> characters)
    {
        Name = name;
        Player = player;
        Characters = characters;
    }
}

