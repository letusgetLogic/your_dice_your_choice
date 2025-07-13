using Assets.Scripts;
using Assets.Scripts.ActionPanelPrefab;
using Assets.Scripts.CharacterPrefab;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance { get; private set; }

    public GameObject Canvas;

    public TextMeshProUGUI NameTextLeft;
    public TextMeshProUGUI NameTextRight;

    public GameObject PlayerPanelLeft;
    public GameObject PlayerPanelRight;

    public GameObject[] CharacterPanelsLeft;
    public GameObject[] CharacterPanelsRight;

    public GameObject RollPanelLeft;
    public GameObject RollPanelRight;

    public GameObject CharacterInfoPanel;

    //public GameObject RerollPanelLeft; 
    //public GameObject RerollPanelRight;

    private List<ActionPanel> _actionPanelsSetDefaultDescription = new();

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

        SetFirstTurn.Instance.InitializePanels();
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

    /// <summary>
    /// Sets the panels active false.
    /// </summary>
    private void HideAllPanel()
    {
        foreach (GameObject panel in CharacterPanelsLeft)
        {
            SetActive(panel, false);
        }

        foreach (GameObject panel in CharacterPanelsRight)
        {
            SetActive(panel, false);
        }
        
        SetActive(RollPanelLeft, false);
        SetActive(RollPanelRight, false);

        // Set the inactive panel in the scene active to create the singleton instance.
        SetActive(CharacterInfoPanel, true);
        SetActive(CharacterInfoPanel, false);
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
        SetActive(characterPanelObject, true);

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

        throw new Exception("player case invalid in PanelManager.CharacterPanels(PlayerType player)");
    }

    /// <summary>
    /// Adds the action panel, it's description text will be set default at the end of a turn.
    /// </summary>
    /// <param name="actionPanel"></param>
    public void AddSetDefaultDescription(ActionPanel actionPanel)
    {
        _actionPanelsSetDefaultDescription.Add(actionPanel);
    }

    /// <summary>
    /// Removes the action panel, it's description text will be set default at the end of a turn.
    /// </summary>
    /// <param name="actionPanel"></param>
    public void RemoveSetDefaultDescription(ActionPanel actionPanel)
    {
        _actionPanelsSetDefaultDescription.Remove(actionPanel);
    }

    /// <summary>
    /// Sets the default description to the action of each panel on the list and clear the list.
    /// </summary>
    public void SetDefaultDescription()
    {
        foreach (var panel in _actionPanelsSetDefaultDescription)
        {
            var action = panel.Action;
            action.SetDescriptionOf(panel, 0);
        }

        _actionPanelsSetDefaultDescription.Clear();
        _actionPanelsSetDefaultDescription.TrimExcess();
    }

    /// <summary>
    /// Sets the game object active true/false.
    /// </summary>
    /// <param name="button"></param>
    public void SetActive(GameObject gameObject, bool value)
    {
        gameObject.SetActive(value);
    }

    /// <summary>
    /// Sets the scale of the RectTransform.
    /// </summary>
    /// <param name="button"></param>
    public void SetScale(GameObject gameObject, Vector3 scaleSize)
    {
        gameObject.GetComponent<RectTransform>().localScale = scaleSize;
    }
}
