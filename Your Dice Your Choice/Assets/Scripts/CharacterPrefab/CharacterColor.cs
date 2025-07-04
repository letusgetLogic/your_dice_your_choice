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
            //var characterBody = transform.Find("Pivot").Find("Body Pivot").Find("Character Body");
            //_bodySpriteRenderer = characterBody.Find("Color").gameObject.GetComponent<SpriteRenderer>();
            //_leftHandSpriteRenderer = characterBody.Find("Character Left Hand").Find("Color").gameObject.GetComponent<SpriteRenderer>();
            //_rightHandSpriteRenderer = characterBody.Find("Character Right Hand").Find("Color").gameObject.GetComponent<SpriteRenderer>();

            //_bodySpriteRenderer.color = color;
            //_leftHandSpriteRenderer.color = color;
            //_rightHandSpriteRenderer.color = color;
            
            foreach(var item in _colorSpriteRenderers)
            {
                item.color = color;
            }

            PlayerColor = color;
        }
    }
}
