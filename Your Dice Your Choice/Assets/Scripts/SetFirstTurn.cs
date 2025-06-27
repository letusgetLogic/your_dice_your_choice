using System.Collections;
using Assets.Scripts.DicePrefab;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class SetFirstTurn : MonoBehaviour
    {
        public static SetFirstTurn Instance { get; private set; }
        public GameObject[] TurnDice { get; private set; }

        public GameObject PlayerPanelLeft;
        public GameObject PlayerPanelRight;

        [SerializeField] private GameObject _turnDiceLeft;
        [SerializeField] private GameObject _turnDiceRight;


        [SerializeField] private int _rollFrequency = 10;
        [SerializeField] private float _animTimer = 0.25f;

        private Vector3 _startScale;

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

            _startScale = _turnDiceLeft.GetComponent<RectTransform>().localScale;
        }

        /// <summary>
        /// Start method.
        /// </summary>
        private void Start()
        {
            TurnDice = new GameObject[]
            {
                _turnDiceLeft,
                _turnDiceRight,
            };
        }

        /// <summary>
        /// Sets the turn dice and the player panel at the start state.
        /// </summary>
        public void SetDiceAndPanel()
        {
            _turnDiceLeft.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            _turnDiceRight.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            _turnDiceLeft.SetActive(true);
            _turnDiceRight.SetActive(true);

            PlayerPanelLeft.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            PlayerPanelRight.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        }

        /// <summary>
        /// Rolls turn dice.
        /// </summary>
        public void RollTurnDice()
        {
            RollDice.Instance.Roll(TurnDice, _rollFrequency, _animTimer, CheckDiceNumber);
        }

        /// <summary>
        /// Checks dice numbers.
        /// </summary>
        public void CheckDiceNumber()
        {
            int numberLeft = _turnDiceLeft.GetComponent<Dice>().CurrentNumber;
            int numberRight = _turnDiceRight.GetComponent<Dice>().CurrentNumber;

            if (numberLeft == numberRight)
                StartCoroutine(Reroll());
            else
            {
                var turnState = numberLeft > numberRight ? 
                                PlayerType.PlayerLeft : 
                                PlayerType.PlayerRight;
                StartCoroutine(SetTurn(turnState));
            }
        }

        /// <summary>
        /// Checks the turn dice.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Reroll()
        {
            yield return new WaitForSeconds(1);

            RollTurnDice();
        }

        /// <summary>
        /// Sets the first turn.
        /// </summary>
        /// <param name="turnState"></param>
        /// <returns></returns>
        private IEnumerator SetTurn(PlayerType turnState)
        {
            yield return new WaitForSeconds(1);

            MatchIntroManager.Instance.LeftIntroShaderText.gameObject.SetActive(false);
            MatchIntroManager.Instance.RightIntroShaderText.gameObject.SetActive(false);

            TurnManager.Instance.SwitchTurn(turnState);

            StartCoroutine(EndMatchIntro());
        }

        /// <summary>
        /// Ends the match intro.
        /// </summary>
        /// <returns></returns>
        private IEnumerator EndMatchIntro()
        {
            yield return new WaitForSeconds(1);

            _turnDiceLeft.SetActive(false);
            _turnDiceRight.SetActive(false);

            MatchIntroManager.Instance.EndPhase();
        }

        /// <summary>
        /// Scales up the alpha values. 
        /// </summary>
        public void ScaleUp(float ratio)
        {
            Vector3 diceScale = _startScale * ratio;
            Vector3 panelScale = new Vector3(ratio, ratio, ratio);

            if (ratio >= 1)
            {
                _turnDiceLeft.GetComponent<RectTransform>().localScale = _startScale;
                return;
            }

            _turnDiceLeft.GetComponent<RectTransform>().localScale = diceScale;
            _turnDiceRight.GetComponent<RectTransform>().localScale = diceScale;

            PlayerPanelLeft.GetComponent<RectTransform>().localScale = panelScale;
            PlayerPanelRight.GetComponent<RectTransform>().localScale = panelScale;
        }

    }
}
