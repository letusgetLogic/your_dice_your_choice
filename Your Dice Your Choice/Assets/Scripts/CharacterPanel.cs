using System;
using System.IO;
using UnityEngine;
using Assets.Scripts.Characters;

public class CharacterPanel : MonoBehaviour
{
    //public GameObject Avatar;
    public GameObject[] DicePanel;

    public GameObject CharacterObject { get; private set; }
    public Character Character { get; private set; }

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        //InitializeSlots();

    }

    /// <summary>
    /// References Object and Script Character.
    /// </summary>
    /// <param name="character"></param>
    public void SetCharacter(GameObject character)
    {
        CharacterObject = character;
        Character = CharacterObject.GetComponent<Character>();
    }

    /// <summary>
    /// References the action in UI.
    /// </summary>
    public void SetAction(GameObject character)
    {
        for (int i = 0; i < DicePanel.Length; i++)
        {
            string action = character.GetComponent<Character>().Data.ActionTypes[i].ToString();
            DicePanel[i].GetComponent<DicePanel>().ActionName.text = action;
        }
    }

    private void InitializeSlots()
    {
        throw new NotImplementedException();
    }
}

