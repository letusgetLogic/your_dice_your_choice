using System.Collections.Generic;
using Assets.Scripts.Characters;
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
    /// Create the player instances.
    /// </summary>
    public void CreateInstancesForPlayers()
    {
        PlayerLeft = gameObject.AddComponent<Player>();
        PlayerLeft.Name = PanelManager.Instance.NameTextLeft.text;
        PlayerLeft.PlayerTurn = TurnState.PlayerLeft;
        PlayerLeft.Characters = LevelGenerator.Instance.CreateCharactersFor(TurnState.PlayerLeft);
        PlayerLeft.CharacterPanels = PanelManager.Instance.CharacterPanelsLeft;
        for (int i = 0; i < PlayerManager.Instance.PlayerLeft.Characters.Count; i++)
        {
            Debug.Log(PlayerManager.Instance.PlayerLeft.Characters[i] + " " + PlayerManager.Instance.PlayerLeft.Characters[i].GetComponent<Character>().Panel);
            PlayerManager.Instance.PlayerLeft.Characters[i].GetComponent<Character>().Panel.GetComponent<CharacterPanel>().SetAction();
        }
        PlayerRight = gameObject.AddComponent<Player>();
        PlayerRight.Name = PanelManager.Instance.NameTextRight.text;
        PlayerRight.PlayerTurn = TurnState.PlayerRight;
        PlayerRight.Characters = LevelGenerator.Instance.CreateCharactersFor(TurnState.PlayerRight);
        PlayerRight.CharacterPanels = PanelManager.Instance.CharacterPanelsRight;
        for (int i = 0; i < PlayerManager.Instance.PlayerLeft.Characters.Count; i++)
        {
            Debug.Log(PlayerManager.Instance.PlayerLeft.Characters[i] + " " + PlayerManager.Instance.PlayerLeft.Characters[i].GetComponent<Character>().Panel);
            PlayerManager.Instance.PlayerLeft.Characters[i].GetComponent<Character>().Panel.GetComponent<CharacterPanel>().SetAction();
        }
    }
}

