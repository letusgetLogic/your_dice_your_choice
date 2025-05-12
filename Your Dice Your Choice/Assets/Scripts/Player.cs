using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public string Name;
    [HideInInspector] public TurnState PlayerTurn;
    [HideInInspector] public List<GameObject> Characters;
    [HideInInspector] public GameObject[] CharacterPanels;
}
