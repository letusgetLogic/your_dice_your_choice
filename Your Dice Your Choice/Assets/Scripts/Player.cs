using System.Collections.Generic;
using Assets.Scripts.CharacterPrefab;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name{ get; private set; }
    public TurnState PlayerTurn { get; private set; }
    public List<GameObject> Characters { get; private set; }

    /// <summary>
    /// Initializes the data.
    /// </summary>
    public void Initialize(string name, TurnState playerTurn)
    {
        Name = name;
        PlayerTurn = playerTurn;
        Characters = LevelGenerator.Instance.CreateCharactersFor(playerTurn);

        SettingsForCharacters();
    }

    /// <summary>
    /// Sets the look direction for each character and the description panel for each action.
    /// </summary>
    private void SettingsForCharacters()
    {
        foreach (var characterObject in Characters)
        {
            if (PlayerTurn == TurnState.PlayerRight)
            {
                var characterRotatation = characterObject.GetComponent<CharacterRotation>();
                characterRotatation.RotatePivot(180);
            }

            SetDescriptonPanel(characterObject);
        }
    }

    /// <summary>
    /// Sets the description panel for each action for the character.
    /// </summary>
    /// <param name="characterObject"></param>
    private void SetDescriptonPanel(GameObject characterObject)
    {
        var characterPanel = characterObject.GetComponent<Character>().Panel.GetComponent<CharacterPanel>();

        characterPanel.SetDescriptonPanel();
    }
}
