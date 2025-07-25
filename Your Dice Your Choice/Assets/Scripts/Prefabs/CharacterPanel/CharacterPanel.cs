using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _characterName;
    [SerializeField] private GameObject[] _actionPanelPrefabs;
    [SerializeField] private Image _panelImage;
    [SerializeField] private GameObject _inactive;

    [SerializeField] private Color _clickingCharacter;
    public Color ClickingCharacter
    {
        get { return _clickingCharacter; }
        private set { _clickingCharacter = value; }
    }
    [SerializeField] private float _resetColorTime = 0.5f;

    public GameObject CharacterObject { get; private set; }
    public Character Character { get; private set; }
    public PlayerType PlayerType { get; private set; }
    public ActionPanel[] ActiveActionPanels { get; private set; }

    /// <summary>
    /// References Object and Script Character.
    /// </summary>
    /// <param name="characterObject"></param>
    public void SetCharacter(GameObject characterObject, PlayerType player)
    {
        CharacterObject = characterObject;
        PlayerType = player;
        Character = CharacterObject.GetComponent<Character>();
        _characterName.text = Character.Name;
    }

    /// <summary>
    /// References the action in UI.
    /// </summary>
    public void SetAction()
    {
        ActiveActionPanels = new ActionPanel[Character.Data.ActionData.Length];

        for (int i = 0; i < _actionPanelPrefabs.Length; i++)
        {
            // The amount of action of a character can vary.
            if (i >= Character.Data.ActionData.Length)
            {
                _actionPanelPrefabs[i].SetActive(false);
                break;
            }

            ActiveActionPanels[i] = _actionPanelPrefabs[i].GetComponent<ActionPanel>();

            var actionData = Character.Data.ActionData[i];
            ActiveActionPanels[i].SetData(actionData, CharacterObject, this, i);
        }
    }

    /// <summary>
    /// Sets the action inactive.
    /// </summary>
    public void SetActionInactive()
    {
        foreach (var actionPanelObject in ActiveActionPanels)
        {
            var actionPanel = actionPanelObject.GetComponent<ActionPanel>();
            var diceSlotAction = actionPanel.DiceSlotAction;
            var actionPanelMouseEvent = actionPanel.GetComponent<ActionPanelMouseEvent>();

            actionPanel.SetEnabled(diceSlotAction, false);
            actionPanel.SetEnabled(actionPanelMouseEvent, false);

           _inactive.SetActive(true);
        }
    }

    /// <summary>
    /// Changes the color of the panel when clicking on a character.
    /// </summary>
    public void ChangeColorOnClickingCharacter()
    {
        Color origin = _panelImage.color;
        SetPanelColor(ClickingCharacter);
        StartCoroutine(ResetPanelColor(origin));

    }

    /// <summary>
    /// Resets the color of the panel after a delay.
    /// </summary>
    /// <param name="origin"></param>
    /// <returns></returns>
    private IEnumerator ResetPanelColor(Color origin)
    {
        yield return new WaitForSeconds(_resetColorTime);

        SetPanelColor(origin);
    }

    /// <summary>
    /// Sets the color of the panel.
    /// </summary>
    /// <param name="color"></param>
    private void SetPanelColor(Color color)
    {
        _panelImage.color = color;
    }


}

