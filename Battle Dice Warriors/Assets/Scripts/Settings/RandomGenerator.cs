using System.Collections;
using TMPro;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
    {
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private int _minValue = 1;
    [SerializeField] private int _maxValue = 4;
    [SerializeField] private int _iterations = 10;
    [SerializeField] private float _delay = 0.1f;

    /// <summary>
    /// Triggers the random number generation process.
    /// </summary>
    public void Trigger()
    {
        StartCoroutine(GenerateRandomNumbers());
    }

    /// <summary>
    /// Generates random numbers within the specified range and updates the text.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateRandomNumbers()
    {
        for (int i = 0; i < _iterations; i++)
        {
            int randomNumber = UnityEngine.Random.Range(_minValue, _maxValue + 1);
            _text.text = randomNumber.ToString();
            yield return new WaitForSeconds(_delay);
        }
    }
}

