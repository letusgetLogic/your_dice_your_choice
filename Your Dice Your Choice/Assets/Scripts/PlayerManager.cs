using System.Collections.Generic;
using Assets.Scripts.CharacterPrefab;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [HideInInspector] public Player PlayerLeft;
    [HideInInspector] public Player PlayerRight;


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
    /// Create the instance of player and instantiate the characters.
    /// </summary>
    public void CreateInstanceFor(Player player, string name, TurnState playerTurn)
    {
        player = gameObject.AddComponent<Player>();

        player.Initialize(name, playerTurn);
    }
}

