using System.Collections.Generic;
using Assets.Scripts.Characters;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public string Name;
    [HideInInspector] public TurnState PlayerTurn;
    [HideInInspector] public List<GameObject> Characters;

    /// <summary>
    /// Initializes the data.
    /// </summary>
    public void Initialize(string name, TurnState playerTurn)
    {
        Name = name;
        PlayerTurn = playerTurn;
        Characters = LevelGenerator.Instance.CreateCharactersFor(playerTurn);
    }

    /// <summary>
    /// Sets the look direction for the characters.
    /// </summary>
    public void RotateLookDirection(int number)
    {
        foreach (var characterObject in Characters)
        {
            var characterMovement = characterObject.GetComponent<CharacterMovement>();
            characterMovement.RotatePivot(number);
        }
    }
}
