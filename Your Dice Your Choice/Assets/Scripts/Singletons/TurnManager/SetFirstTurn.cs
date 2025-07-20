using System.Collections;
using Assets.Scripts.DicePrefab;
using Assets.Scripts.MatchIntro;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class SetFirstTurn : MonoBehaviour
    {
        public static SetFirstTurn Instance { get; private set; }

        [SerializeField] private GameObject _turnDiceLeft;
        [SerializeField] private GameObject _turnDiceRight;

        private GameObject[] _panels;
        private Vector3 _originScale;

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

            _originScale = _turnDiceLeft.GetComponent<RectTransform>().localScale;
        }

        public void InitializePanels()
        {
            _panels = new GameObject[]
 {
                PanelManager.Instance.PlayerPanelLeft,
                PanelManager.Instance.PlayerPanelRight,
                PanelManager.Instance.RollPanelLeft,
                PanelManager.Instance.RollPanelRight
 };
        }

        /// <summary>
        /// Sets the turn dice and the player panel at the start state.
        /// </summary>
        public void SetTurnDiceAndPanel()
        {
            _turnDiceLeft.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            _turnDiceRight.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            _turnDiceLeft.SetActive(true);
            _turnDiceRight.SetActive(true);

            foreach (var panel in _panels)
            {
                PanelManager.Instance.SetScale(panel, new Vector3(0, 0, 0));
            }

            foreach (var panel in _panels)
            {
                PanelManager.Instance.SetActive(panel, true);
            }
        }

        /// <summary>
        /// Rolls turn dice.
        /// </summary>
        public void RollTurnDice()
        {
            var turnDice = new GameObject[]
            {
                _turnDiceLeft,
                _turnDiceRight,
            };

            RollDice.Instance.Roll(
                turnDice,
                RollDice.Instance.RollFrequency,
                RollDice.Instance.AnimTimer,
                CheckDiceNumber);
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
                var firstTurn = numberLeft > numberRight ?
                                PlayerType.PlayerLeft :
                                PlayerType.PlayerRight;

                StartCoroutine(SetTurn(firstTurn));
            }
        }

        /// <summary>
        /// Checks the turn dice.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Reroll()
        {
            yield return new WaitForSeconds(1f);

            RollTurnDice();
        }

        /// <summary>
        /// Sets the first turn.
        /// </summary>
        /// <param name="firstTurn"></param>
        /// <returns></returns>
        private IEnumerator SetTurn(PlayerType firstTurn)
        {
            yield return new WaitForSeconds(1f);

            MatchIntroController.Instance.SetIntroInactive();

            TurnManager.Instance.SetFirstTurn(firstTurn);

            StartCoroutine(EndMatchIntro());
        }

        /// <summary>
        /// Ends the match intro.
        /// </summary>
        /// <returns></returns>
        private IEnumerator EndMatchIntro()
        {
            yield return new WaitForSeconds(1f);

            _turnDiceLeft.SetActive(false);
            _turnDiceRight.SetActive(false);

            MatchIntroController.Instance.EndPhase();
        }

        /// <summary>
        /// Scales up the alpha values. 
        /// </summary>
        public void ScaleUp(float ratio)
        {
            Vector3 diceScale = _originScale * ratio;
            Vector3 panelScale = new Vector3(ratio, ratio, ratio);

            if (ratio >= 1)
            {
                _turnDiceLeft.GetComponent<RectTransform>().localScale = _originScale;
                _turnDiceRight.GetComponent<RectTransform>().localScale = _originScale;

                foreach (var panel in _panels)
                {
                    PanelManager.Instance.SetScale(panel, Vector3.one);
                }

                return;
            }

            _turnDiceLeft.GetComponent<RectTransform>().localScale = diceScale;
            _turnDiceRight.GetComponent<RectTransform>().localScale = diceScale;

            foreach (var panel in _panels)
            {
                PanelManager.Instance.SetScale(panel, panelScale);
            }
        }

    }
}
