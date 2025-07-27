using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterGetWeapon : MonoBehaviour
{
    [SerializeField] private Transform _leftHandTransform;
    [SerializeField] private Transform _rightHandTransform;

    public GameObject WeaponObjectLeft { get; private set; }
    public GameObject WeaponObjectRight { get; private set; }

    /// <summary>
    /// Sets the weapon as child of left hand.
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="transform"></param>
    public void SetWeaponToLeftHand(Character character)
    {
        if (character.Data.WeaponLeft == null)
            return;

        WeaponObjectLeft = Instantiate(character.Data.WeaponLeft);
        WeaponObjectLeft.transform.SetParent(_leftHandTransform, false);
    }

    /// <summary>
    /// Sets the weapon as child of right hand.
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="transform"></param>
    public void SetWeaponToRightHand(Character character)
    {
        if (character.Data.WeaponRight == null)
            return;

        WeaponObjectRight = Instantiate(character.Data.WeaponRight);
        WeaponObjectRight.transform.SetParent(_rightHandTransform, false);
    }
}

