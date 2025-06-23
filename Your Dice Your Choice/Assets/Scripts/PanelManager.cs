using System;
using Assets.Scripts;
using Assets.Scripts.ActionPopupPrefab;
using Assets.Scripts.CharacterPrefab;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance {  get; private set; }

    public GameObject Canvas;
    public TextMeshProUGUI NameTextLeft;
    public TextMeshProUGUI NameTextRight;
    public GameObject[] CharacterPanelsLeft;
    public GameObject[] CharacterPanelsRight;
    public GameObject RollPanelLeft;
    //public GameObject RerollPanelLeft; 
    public GameObject RollPanelRight;
    //public GameObject RerollPanelRight;
    public GameObject CharacterInfoPanel;

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

        HideAllPanel();
        HideDiceOnPanel(RollPanelLeft);
        HideDiceOnPanel(RollPanelRight);
    }

    /// <summary>
    /// Gets the roll panel.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public RollPanel GetRollPanelFor(PlayerType player)
    {
        switch (player)
        {
            case PlayerType.PlayerLeft:
                return RollPanelLeft.GetComponent<RollPanel>();
            case PlayerType.PlayerRight:
                return RollPanelRight.GetComponent<RollPanel>();
        }

        throw new Exception("PanelManager.GetRollPanelFor() -> player case invalid");
    }
    
    ///// <summary>
    ///// Gets the reroll panel.
    ///// </summary>
    ///// <param name="player"></param>
    ///// <returns></returns>
    ///// <exception cref="Exception"></exception>
    //public GameObject GetRerollPanelFor(PlayerType player)
    //{
    //    switch (player)
    //    {
    //        case PlayerType.PlayerLeft:
    //            return RerollPanelLeft;
    //        case PlayerType.PlayerRight:
    //            return RerollPanelRight;
    //    }

    //    throw new Exception("PanelManager.GetRerollPanelFor() -> player case invalid");
    //}

    /// <summary>
    /// Hides all character panels.
    /// </summary>
    private void HideAllPanel()
    {
        foreach (GameObject panel in CharacterPanelsLeft)
        {
            panel.gameObject.SetActive(false);
        }

        foreach (GameObject panel in CharacterPanelsRight)
        {
            panel.gameObject.SetActive(false);
        }

        RollPanelLeft.SetActive(false);
        RollPanelRight.SetActive(false);
        //RerollPanelLeft.SetActive(false);
        //RerollPanelRight.SetActive(false);

        // Set the inactive panel in the scene active to create the singleton instance.
        CharacterInfoPanel.SetActive(true); 
        CharacterInfoPanel.SetActive(false);
    }

    /// <summary>
    /// Shows the roll and reroll panels.
    /// </summary>
    public void ShowRollPanels()
    {
        RollPanelLeft.SetActive(true);
        RollPanelRight.SetActive(true);

        //RerollPanelLeft.SetActive(true);
        //RerollPanelRight.SetActive(true);
    }

    /// <summary>
    /// Hides Dice on Panel.
    /// </summary>
    /// <param name="panel"></param>
    private void HideDiceOnPanel(GameObject panel)
    {
        var rollPanel = panel.GetComponent<RollPanel>();
        rollPanel.HideAllDice();
    }

    /// <summary>
    /// Sets active, transfers data, sets action for the character panel.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="index"></param>
    /// <param name="characterObject"></param>
    /// <returns></returns>
    public GameObject GetPanel(PlayerType player, int index, GameObject characterObject)
    {
        var characterPanelObject = CharacterPanels(player)[index];
        characterPanelObject.SetActive(true);

        var characterPanel = characterPanelObject.GetComponent<CharacterPanel>();
        characterPanel.SetCharacter(characterObject, player);
        characterPanel.SetAction();

        return characterPanelObject;
    }

    /// <summary>
    /// Return the serialized panels in PanelManager for the corresponding player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private GameObject[] CharacterPanels(PlayerType player)
    {
        if (player == PlayerType.PlayerLeft)
        {
            return CharacterPanelsLeft;
        }
        else if (player == PlayerType.PlayerRight)
        {
            return CharacterPanelsRight;
        }

        return null;
    }
}
