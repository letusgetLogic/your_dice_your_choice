using System.Collections.Generic;
using Assets.Scripts.Characters;
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

        if (PlayerTurn == TurnState.PlayerRight) RotateLookDirection(180);
    }

    /// <summary>
    /// Sets the look direction for the characters.
    /// </summary>
    private void RotateLookDirection(int number)
    {
        foreach (var characterObject in Characters)
        {
            var characterRotatation = characterObject.GetComponent<CharacterRotation>();
            characterRotatation.RotatePivot(number);
        }
    }
}
