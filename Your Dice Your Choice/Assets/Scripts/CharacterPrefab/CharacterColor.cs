using UnityEngine;

namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterColor : MonoBehaviour
    {
        private SpriteRenderer _bodySpriteRenderer;
        private SpriteRenderer _leftHandSpriteRenderer;
        private SpriteRenderer _rightHandSpriteRenderer;
        public Color PlayerColor {  get; private set; }

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            _bodySpriteRenderer = transform.Find("Pivot").Find("Character Body").Find("Color").gameObject.GetComponent<SpriteRenderer>();
            _leftHandSpriteRenderer = transform.Find("Pivot").Find("Character Left Hand").Find("Color").gameObject.GetComponent<SpriteRenderer>();
            _rightHandSpriteRenderer = transform.Find("Pivot").Find("Character Right Hand").Find("Color").gameObject.GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Sets the color to the character of the target player.
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            _bodySpriteRenderer.color = color;
            _leftHandSpriteRenderer.color = color;
            _rightHandSpriteRenderer.color = color;
            PlayerColor = color;
        }
    }
}
