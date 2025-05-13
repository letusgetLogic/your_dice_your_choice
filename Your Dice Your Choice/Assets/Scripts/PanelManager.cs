using Assets.Scripts.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance {  get; private set; }

    public TextMeshProUGUI NameTextLeft;
    public TextMeshProUGUI NameTextRight;
    public GameObject[] CharacterPanelsLeft;
    public GameObject[] CharacterPanelsRight;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    /// <summary>
    /// Hides all character panels.
    /// </summary>
    public void HideAllPanel()
    {
        foreach (GameObject panel in CharacterPanelsLeft)
        {
            panel.gameObject.SetActive(false);
        }

        foreach (GameObject panel in CharacterPanelsRight)
        {
            panel.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// References Character and CharacterPanel to each other.
    /// </summary>
    /// <param name="characterPrefab"></param>
    public void SetPanel()
    {
        for (int i = 0; i < PlayerManager.Instance.PlayerLeft.Characters.Count; i++)
        {
            CharacterPanelsLeft[i].SetActive(true);
            
            var character = PlayerManager.Instance.PlayerLeft.Characters[i];

            character.GetComponent<Character>().SetPanel(CharacterPanelsLeft[i]);

            CharacterPanelsLeft[i].GetComponent<CharacterPanel>().SetCharacter(character);
        }

        for (int i = 0; i < PlayerManager.Instance.PlayerRight.Characters.Count; i++)
        {
            CharacterPanelsRight[i].SetActive(true);

            var character = PlayerManager.Instance.PlayerRight.Characters[i];

            character.GetComponent<Character>().SetPanel(CharacterPanelsRight[i]);

            CharacterPanelsRight[i].GetComponent<CharacterPanel>().SetCharacter(character);
        }
    }

    /// <summary>
    /// References the action for each DicePanel.
    /// </summary>
    public void SetAction()
    {
        for (int i = 0; i < PlayerManager.Instance.PlayerLeft.Characters.Count; i++)
        {
            var character = PlayerManager.Instance.PlayerLeft.Characters[i].GetComponent<Character>();
            var panel = character.Panel.GetComponent<CharacterPanel>();
            panel.SetAction();
        }
        
        for (int i = 0; i < PlayerManager.Instance.PlayerRight.Characters.Count; i++)
        {
            var character = PlayerManager.Instance.PlayerRight.Characters[i].GetComponent<Character>();
            var panel = character.Panel.GetComponent<CharacterPanel>();
            panel.SetAction();
        }
    }

}
