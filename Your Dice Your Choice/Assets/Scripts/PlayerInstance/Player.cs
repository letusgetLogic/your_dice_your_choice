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

    public Player(string name, PlayerType player)
    {
        Name = name;
        PlayerType = player; 
        Characters = CharacterGenerator.Instance.CreateCharactersFor(player);
        RollPanel = PanelManager.Instance.GetRollPanelFor(player);
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
            characterRotatation.RotateBodyTransform(180);

            var characterMovement = characterObject.GetComponent<CharacterMovement>();
            var localPosition = characterObject.GetComponent<CharacterComponents>().BodyPivotTransform.localPosition;
            var newPosition = new Vector3(localPosition.x * (-1), localPosition.y, localPosition.z);
            characterMovement.SetBodyPivot(newPosition);
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
}
