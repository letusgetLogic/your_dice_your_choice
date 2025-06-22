using UnityEngine;
using TMPro;
using Assets.Scripts.CharacterPrefab;
using Assets.Scripts.ActionPopupPrefab;
using Assets.Scripts.ActionDatas;
using Assets.Scripts;

public class CharacterPanel : MonoBehaviour
{
    //public GameObject Avatar;
    public TextMeshProUGUI CharacterName;
    public GameObject[] ActionPanelPrefabs;

    public GameObject CharacterObject { get; private set; }
    public Character Character { get; private set; }
    public PlayerType Player { get; private set; }

    /// <summary>
    /// References Object and Script Character.
    /// </summary>
    /// <param name="characterObject"></param>
    public void SetCharacter(GameObject characterObject, PlayerType player)
    {
        CharacterObject = characterObject;
        Player = player;
        Character = CharacterObject.GetComponent<Character>();
        CharacterName.text = Character.Data.Type.ToString();
    }

    /// <summary>
    /// References the action in UI.
    /// </summary>
    public void SetAction()
    {
        for (int i = 0; i < ActionPanelPrefabs.Length; i++)
        {
            if (i >= Character.Data.ActionData.Length)
            {
                ActionPanelPrefabs[i].SetActive(false);
                continue;
            }

            var actionData = Character.Data.ActionData[i];

            ActionPanelPrefabs[i].GetComponent<ActionPanel>().SetData(actionData, CharacterObject, this, i);
        }
    }

    /// <summary>
    /// Sets the description panel for each action. 
    /// </summary>
    public void SetDescriptonPanelForAction() // Can't be in SetAction(), because the character panel's position has not yet been determined.
    {
        for (int i = 0; i < Character.Data.ActionData.Length; i++)
        {
            var actionData = Character.Data.ActionData[i];

            ActionPanelPrefabs[i].GetComponent<ActionPanel>().ActionPopup.SetPosition();
            ActionPanelPrefabs[i].GetComponent<ActionPanel>().ActionPopup.SetText(actionData.Description);
        }
    }
}

