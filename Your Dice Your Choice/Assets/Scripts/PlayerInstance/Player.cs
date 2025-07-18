using System;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.CharacterPrefab;
using UnityEngine;

public class Player
{
    public string Name { get; private set; }
    public PlayerType PlayerType { get; private set; }
    public List<GameObject> Characters { get; private set; }
    public RollPanel RollPanel { get; private set; }
    //public GameObject RerollPanelObject { get; private set; }

    public Player(string name, PlayerType playerType)
    {
        Name = name;
        PlayerType = playerType; 
        Characters = CharacterGenerator.Instance.CreateCharactersFor(this, playerType);
        RollPanel = PanelManager.Instance.GetRollPanelFor(playerType);
        //RerollPanelObject = PanelManager.Instance.GetRerollPanelFor(player);

        SettingsForCharacters();
    }

    /// <summary>
    /// Sets others to character.
    /// </summary>
    private void SettingsForCharacters()
    {
        foreach (var characterObject in Characters)
        {
            SetBattlePosition(characterObject);

            SetDescriptonPanelForAction(characterObject);
        }
    }

    /// <summary>
    /// Sets the position and look direction for each character in the right side.
    /// </summary>
    private void SetBattlePosition(GameObject characterObject)
    {
        if (PlayerType == PlayerType.PlayerRight)
        {
            var characterRotatation = characterObject.GetComponent<CharacterRotation>();
            characterRotatation.RotateBody(180);

            var characterMovement = characterObject.GetComponent<CharacterMovement>();
            characterMovement.SetBodyPivotPosition();
        }
    }

    /// <summary>
    /// Sets the description panel for each action for the character.
    /// </summary>
    /// <param name="characterObject"></param>
    private void SetDescriptonPanelForAction(GameObject characterObject)
    {
        var characterPanel = characterObject.GetComponent<Character>().Panel.GetComponent<CharacterPanel>();

        characterPanel.SetDescriptonPanelForAction();
    }

    /// <summary>
    /// Removes the character on the list.
    /// </summary>
    /// <param name="characterList"></param>
    /// <param name="characterObject"></param>
    public void RemoveCharacter(GameObject characterObject)
    {
        Characters.Remove(characterObject);

        // If there are no characters left, end the match.
        if (Characters.Count == 0)
        {
            BattleManager.Instance.EndMatch(PlayerType);
        }
    }
}
