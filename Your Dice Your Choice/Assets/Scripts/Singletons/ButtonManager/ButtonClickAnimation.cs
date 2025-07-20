using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ButtonClickAnimation : MonoBehaviour
    {
        public static ButtonClickAnimation Instance { get; private set; }

        [SerializeField] private float _scaleSize = 1.2f;
        [SerializeField] private float _delayReset = 0.2f;

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
        /// Scales the specified button to a predefined size.
        /// </summary>
        /// <remarks>This method adjusts the button's scale to a predefined value and initiates a
        /// coroutine to reset the scale after a certain period.</remarks>
        /// <param name="button">The button to be scaled. Must not be <see langword="null"/>.</param>
        public void ScaleSize(Button button)
        {
            button.transform.localScale = Vector3.one * _scaleSize;
            StartCoroutine(ResetButtonScale(button));
        }

        /// <summary>
        /// Resets the scale of the specified button to its default value after a delay.
        /// </summary>
        /// <remarks>The button's scale is reset to <see cref="Vector3.one"/> after a delay defined by the
        /// internal configuration. This method is intended to be used as part of a coroutine.</remarks>
        /// <param name="button">The button whose scale will be reset.</param>
        /// <returns>An enumerator that performs the scale reset operation after the specified delay.</returns>
        private IEnumerator ResetButtonScale(Button button)
        {
            yield return new WaitForSeconds(_delayReset);

            button.transform.localScale = Vector3.one;
        }
    }
}
