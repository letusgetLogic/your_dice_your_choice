using UnityEngine;
using UnityEngine.UI;

public class RollPanel : MonoBehaviour
{
    [SerializeField]
    private Button _rollButton;
    public Button RollButton => _rollButton;

    [SerializeField] private GameObject[] _allDice;

    public GameObject[] PlayDice { get; private set; }

    /// <summary>
    /// Initializes the PlayDice array with the active dice from _allDice
    /// and sets the start state to each.
    /// </summary>
    public void InitializePlayDice()
    {
        PlayDice = new GameObject[LevelManager.Instance.Data.DiceAmount];

        for (int i = 0; i < PlayDice.Length; i++)
        {
            var diceObject = _allDice[i];
            var dice = diceObject.GetComponent<Dice>();
            dice.InitializeIndexOf(gameObject, i);

            diceObject.GetComponent<RectTransform>().localScale = Vector3.zero;
            var diceDragEvent = diceObject.GetComponent<DiceDragEvent>();
            dice.SetComponentEnabled(diceDragEvent, false);

            PlayDice[i] = diceObject;
        }
    }

    /// <summary>
    /// Sets the dice inactive except for the PlayDice.
    /// </summary>
    /// <param name="amount"></param>
    public void SetNonPlayDiceInactive()
    {
        for (int i = PlayDice.Length; i < _allDice.Length; i++)
        {
            var dice = _allDice[i];
            dice.SetActive(false);
        }
    }

    /// <summary>
    /// Sets the dice to their default state.
    /// </summary>
    /// <param name="amount"></param>
    public void SetDiceDefault()
    {
        foreach (var diceObject in PlayDice)
        {
            var dice = diceObject.GetComponent<Dice>();
            dice.SetDefault();

            var diceDisplay = diceObject.GetComponent<DiceDisplay>();
            diceDisplay.SetDefault();
            diceDisplay.SetIdleRolling();
        }
    }

    /// <summary>
    /// Roll Button triggers.
    /// </summary>
    public void Roll()
    {
        ButtonManager.Instance.SetButtonInteractible(RollButton, false);
        ButtonManager.Instance.SetGameObjectActive(ButtonManager.Instance.EndTurnButtonObject, true);

        foreach (var diceObject in PlayDice)
        {
            var diceDisplay = diceObject.GetComponent<DiceDisplay>();
            diceDisplay.IsDiceIdleRolling = false;
        }

        RollDice.Instance.Roll(
            PlayDice,
            RollDice.Instance.RollFrequency,
            RollDice.Instance.AnimTimer,
            SetInteraction);
    }

    /// <summary>
    /// Sets interaction for the dice.
    /// </summary>
    private void SetInteraction()
    {
        BattleController.Instance.State = BattleController.BattleState.PhaseAction;
        SetDragEnabled(PlayDice, true);
    }

    /// <summary>
    /// Sets the component DiceDragEvent enabled true/false.
    /// </summary>
    /// <param name="diceObjects"></param>
    /// <param name="value"></param>
    public void SetDragEnabled(GameObject[] diceObjects, bool value)
    {
        foreach (GameObject diceObject in diceObjects)
        {
            var dice = diceObject.GetComponent<Dice>();
            var diceDragEvent = diceObject.GetComponent<DiceDragEvent>();
            dice.SetComponentEnabled(diceDragEvent, value);
        }
    }

    /// <summary>
    /// Sets the alpha of the dice down.
    /// </summary>
    /// <param name="diceObjects"></param>
    public void SetAlphaDown(GameObject[] diceObjects)
    {
        foreach (GameObject diceObject in diceObjects)
        {
            var diceDisplay = diceObject.GetComponent<DiceDisplay>();
            diceDisplay.SetAlphaDown();
        }
    }

    /// <summary>
    /// Sends the dice back to base.
    /// </summary>
    /// <param name="diceObjects"></param>
    /// <param name="value"></param>
    public void SendBackToBase(GameObject[] diceObjects)
    {
        foreach (GameObject diceObject in diceObjects)
        {
            diceObject.GetComponent<DiceMovement>().SendBackToBase();
        }
    }

    /// <summary>
    /// Sets the PlayDice inactive.
    /// </summary>
    public void SetPlayDiceInactive()
            {
        foreach (GameObject diceObject in PlayDice)
        {
            diceObject.SetActive(false);
        }
    }
}
