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
    public void SetPanel(GameObject characterPrefab, int index, TurnState player)
    {
        if (player == TurnState.PlayerLeft)
        {
            CharacterPanelsLeft[index].SetActive(true);
            
            characterPrefab.GetComponent<Character>().SetPanel(CharacterPanelsLeft[index]);
            CharacterPanelsLeft[index].GetComponent<CharacterPanel>().SetCharacter(characterPrefab);
        }
        else if (player == TurnState.PlayerRight)
        {
            CharacterPanelsRight[index].SetActive(true);
            
            characterPrefab.GetComponent<Character>().SetPanel(CharacterPanelsRight[index]);
            CharacterPanelsRight[index].GetComponent<CharacterPanel>().SetCharacter(characterPrefab);
        }
    }
}
