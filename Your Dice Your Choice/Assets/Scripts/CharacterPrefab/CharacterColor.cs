using UnityEngine;

namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterColor : MonoBehaviour
    {
        public Color PlayerColor {  get; private set; }

        [SerializeField] private SpriteRenderer[] _colorSpriteRenderers;

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
