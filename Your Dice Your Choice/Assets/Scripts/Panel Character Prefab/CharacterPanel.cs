using UnityEngine;
using TMPro;
using Assets.Scripts.Characters;
using Assets.Scripts.ActionPanel;

public class CharacterPanel : MonoBehaviour
{
    //public GameObject Avatar;
    public TextMeshProUGUI CharacterName;
    public GameObject[] ActionPanel;

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
        CharacterName.text = Character.Data.Type.ToString();
    }

    /// <summary>
    /// References the action in UI.
    /// </summary>
    public void SetAction()
    {
        for (int i = 0; i < ActionPanel.Length; i++)
        {
            var actionData = Character.Data.ActionData[i];

            ActionPanel[i].GetComponent<ActionPanel>().SetData(actionData, CharacterObject);
        }
    }
}

