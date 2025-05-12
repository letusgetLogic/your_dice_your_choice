using System;
using System.IO;
using UnityEngine;
using Assets.Scripts.Characters;
using TMPro;

public class CharacterPanel : MonoBehaviour
{
    //public GameObject Avatar;
    public TextMeshProUGUI CharacterName;
    public GameObject[] DicePanel;

    public GameObject CharacterObject { get; private set; }
    public Character Character { get; private set; }

    
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
    /// References the action and the character name in UI. ( CharacterName couldn't be set in SetCharacter(), because Character.Data isn't loaded at those time. )
    /// </summary>
    public void SetAction()
    {
        for (int i = 0; i < DicePanel.Length; i++)
        {
            string action = Character.Data.ActionTypes[i].ToString();
            
            DicePanel[i].GetComponent<DicePanel>().SetName(action);
        }

        CharacterName.text = Character.Data.Type.ToString();
    }
}

