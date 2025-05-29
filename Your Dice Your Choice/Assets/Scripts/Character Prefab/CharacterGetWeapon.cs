using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.Characters
{
    public class CharacterGetWeapon : CharacterComponents
    {
        /// <summary>
        /// Sets the weapon as child of left hand.
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="transform"></param>
        public void SetWeaponToLeftHand(Character character)
        {
            var weaponObject = Instantiate(WeaponManager.Instance.GetWeapon(character.Data.WeaponLeft));
            weaponObject.transform.SetParent(LeftHandTransform, false);
        }

        /// <summary>
        /// Sets the weapon as child of right hand.
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="transform"></param>
        public void SetWeaponToRightHand(Character character)
        {
            var weaponObject = Instantiate(WeaponManager.Instance.GetWeapon(character.Data.WeaponRight));
            weaponObject.transform.SetParent(RightHandTransform, false);
        }
    }
}
