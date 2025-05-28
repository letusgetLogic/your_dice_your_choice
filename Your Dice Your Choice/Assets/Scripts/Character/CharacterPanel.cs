using UnityEngine;
using TMPro;
using Assets.Scripts.Characters;
using Assets.Scripts.DicePanel;

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
        CharacterName.text = Character.Data.Type.ToString();
    }

    /// <summary>
    /// References the action in UI.
    /// </summary>
    public void SetAction()
    {
        for (int i = 0; i < DicePanel.Length; i++)
        {
            var actionData = Character.Data.ActionData[i];

            DicePanel[i].GetComponent<DicePanel>().SetData(actionData, CharacterObject);
        }
    }
}

