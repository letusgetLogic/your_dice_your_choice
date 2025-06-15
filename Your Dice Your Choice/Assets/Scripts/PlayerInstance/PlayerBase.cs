using System.Collections.Generic;
using Assets.Scripts.CharacterPrefab;
using TMPro;
using UnityEngine;

public static class PlayerBase
{
    public static Player PlayerLeft { get; private set; }
    public static Player PlayerRight { get; private set; }

    /// <summary>
    /// Create an instance for the player.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="name"></param>
    /// <param name="playerTurn"></param>
    public static void Create(Player player, string name, TurnState playerTurn)
    {
        player = new Player(name, playerTurn);
    }
}

