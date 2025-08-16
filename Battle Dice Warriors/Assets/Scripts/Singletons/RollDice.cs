using System.Collections;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    public static RollDice Instance { get; private set; }

    [SerializeField] private int _rollFrequency = 10;
    [SerializeField] private float _animTimer = 0.1f;

    public int RollFrequency => _rollFrequency;
    public float AnimTimer => _animTimer;

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
    /// Animates dice roll.
    /// </summary>
    /// <returns></returns>
    public IEnumerator AnimateDiceRoll(GameObject[] diceList, int rollFrequency,
                                        float animTimer, System.Action action)
    {
        for (int i = 0; i < rollFrequency; i++)
        {
            foreach (var diceObject in diceList)
            {
                var dice = diceObject.GetComponent<Dice>();
                var diceDisplay = diceObject.GetComponent<DiceDisplay>();
                int sideIndex = UnityEngine.Random.Range(1, diceDisplay.DiceSide.Length);
                dice.InitializeSide(sideIndex);
            }

            yield return new WaitForSeconds(animTimer);
        }

        action?.Invoke();
    }
}
