using System.Collections;
using Assets.Scripts.DicePrefab;
using UnityEngine;

namespace Assets.Scripts
{
    public class RollDice : MonoBehaviour
    {
        public static RollDice Instance { get; private set; }

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
        /// Rolls dice.
        /// </summary>
        public void Roll(GameObject[] diceList, int rollFrequency, 
                        float animTimer, System.Action action)
        {
            StartCoroutine(AnimateDiceRoll(diceList, rollFrequency, animTimer, action));
        }

        /// <summary>
        /// Shows all dice per roll.
        /// </summary>
        /// <returns></returns>
        public IEnumerator AnimateDiceRoll(GameObject[] diceList, int rollFrequency, 
                                            float animTimer, System.Action action)
        {
            for (int i = 0; i < rollFrequency; i++)
            {
                foreach (var dice in diceList)
                {
                    var diceScript = dice.GetComponent<Dice>();
                    int sideIndex = UnityEngine.Random.Range(1, diceScript.DiceSide.Length);
                    diceScript.InitializeSide(sideIndex);
                }

                yield return new WaitForSeconds(animTimer);
            }

            action?.Invoke();
        }
    }
}
