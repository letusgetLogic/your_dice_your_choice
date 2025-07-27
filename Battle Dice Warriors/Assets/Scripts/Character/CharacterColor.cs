using TMPro;
using UnityEngine;

public class CharacterColor : MonoBehaviour
{
    public Color PlayerColor { get; private set; }

    [SerializeField] private SpriteRenderer[] _colorSpriteRenderers;
    [SerializeField] private TextMeshProUGUI _nameTextShader;
    [SerializeField] private TextMeshProUGUI _nameText;

    /// <summary>
    /// Sets the color and the name to the character of the target player.
    /// </summary>
    /// <param name="color"></param>
    public void SetColorAndName(Color color, string characterName)
    {
        foreach (var item in _colorSpriteRenderers)
        {
            item.color = color;
        }

        _nameText.text = characterName;
        _nameTextShader.text = characterName;

        PlayerColor = color;
    }
}

