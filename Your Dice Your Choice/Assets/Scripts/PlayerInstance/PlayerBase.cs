using System.Collections.Generic;
using Assets.Scripts.CharacterPrefab;
using TMPro;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public static PlayerBase Instance { get; private set; }
    public Player PlayerLeft { get; private set; }
    public Player PlayerRight { get; private set; }

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

    /// <summary>
    /// Create an instance for the player.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="name"></param>
    /// <param name="player"></param>
    public void Create(string name, PlayerType playerType)
    {
        switch (playerType)
        {
            case PlayerType.None:
                throw new System.Exception("PlayerBAse.Create() -> player = None");

            case PlayerType.PlayerLeft:
                PlayerLeft = new Player(name, PlayerType.PlayerLeft);
                break;

            case PlayerType.PlayerRight:
                PlayerRight = new Player(name, PlayerType.PlayerRight);
                break;
        }
    }

    /// <summary>
    /// Determines the winning player based on the specified losing player.
    /// </summary>
    /// <param name="loser">The player type that represents the losing player. Must be either <see cref="PlayerType.PlayerLeft"/> or <see
    /// cref="PlayerType.PlayerRight"/>.</param>
    /// <returns>The <see cref="Player"/> instance representing the winning player.</returns>
    /// <exception cref="System.Exception">Thrown if <paramref name="loser"/> is not a valid <see cref="PlayerType"/>.</exception>
    public Player GetWinner(PlayerType loser)
    {
        return loser switch
        {
            PlayerType.PlayerLeft => PlayerRight,
            PlayerType.PlayerRight => PlayerLeft,
            _ => throw new System.Exception("PlayerBase.GetWinner() -> playerType is not valid"),
        };
    }
}

