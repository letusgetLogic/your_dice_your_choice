using UnityEngine;

namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterColor : MonoBehaviour
    {
        private SpriteRenderer _bodySpriteRenderer;
        private SpriteRenderer _leftHandSpriteRenderer;
        private SpriteRenderer _rightHandSpriteRenderer;

        public Color PlayerColor {  get; private set; }
        private SpriteRenderer[] _colorSpriteRenderers => GetComponent<CharacterComponents>().ColorSpriteRenderers;

        /// <summary>
        /// Sets the color to the character of the target player.
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            foreach(var item in _colorSpriteRenderers)
            {
                item.color = color;
            }

            PlayerColor = color;
        }
    }
}
