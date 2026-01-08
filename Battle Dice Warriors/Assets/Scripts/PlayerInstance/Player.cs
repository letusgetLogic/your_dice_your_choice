using System.Collections.Generic;
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
        }
    }

    /// <summary>
    /// Sets the position and look direction for each character in the right side.
    /// </summary>
    private void SetBattlePosition(GameObject characterObject)
    {
        if (PlayerType == PlayerType.PlayerRight)
        {
            var characterMovement = characterObject.GetComponent<CharacterMovement>();
            characterMovement.SetBodyPivotPosition();
            characterMovement.RotateBody(180);
        }
    }

    /// <summary>
    /// Counts down the round endurance of each action of each character at the end of the round.
    /// </summary>
    public void CountDownRoundEndurance(PlayerType lastTurn)
    {
        foreach (var characterObject in Characters)
        {
            ActionPanel[] actionPanels = characterObject.GetComponent<Character>().Panel.ActiveActionPanels;

            if (actionPanels == null || actionPanels.Length == 0)
            {
                continue;
            }

            foreach (var actionPanel in actionPanels)
            {
                if (actionPanel.Action == null)
                {
                    continue;
                }
                actionPanel.Action.CountDownRoundEndurance(lastTurn);
            }
        }
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
            BattleController.Instance.EndMatch(PlayerType);
        }
    }
}
