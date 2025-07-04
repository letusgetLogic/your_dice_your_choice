using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterGetWeapon : MonoBehaviour
    {
        private Transform _leftHandTransform => GetComponent<CharacterComponents>().LeftHandTransform;
        private Transform _rightHandTransform => GetComponent<CharacterComponents>().RightHandTransform;

        /// <summary>
        /// Sets the weapon as child of left hand.
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="transform"></param>
        public void SetWeaponToLeftHand(Character character)
        {
            if (character.Data.WeaponLeft == null)
                return;

            var weaponObject = Instantiate(character.Data.WeaponLeft);
            weaponObject.transform.SetParent(_leftHandTransform, false);
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

            var weaponObject = Instantiate(character.Data.WeaponRight);
            weaponObject.transform.SetParent(_rightHandTransform, false);
        }
    }
}
