using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.Characters
{
    public class CharacterControl : MonoBehaviour
    {
        public Transform CharacterTransform {  get; private set; }
        public Transform PivotTransform { get; private set; }
        public Transform BodyTransform {  get; private set; }
        public Transform LeftHandTransform { get; private set; }
        public Transform RightHandTransform { get; private set; }


        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            CharacterTransform = transform;
            PivotTransform = transform.Find("Pivot").gameObject.GetComponent<Transform>();
            BodyTransform = transform.Find("Pivot").Find("Character Body").gameObject.GetComponent<Transform>();
            LeftHandTransform = transform.Find("Pivot").Find("Character Left Hand").gameObject.GetComponent<Transform>();
            RightHandTransform = transform.Find("Pivot").Find("Character Right Hand").gameObject.GetComponent<Transform>();
        }

        /// <summary>
        /// Rotate the pivot point.
        /// </summary>
        public void RotatePivot(int number)
        {
            var rotation = PivotTransform.rotation;
            rotation.z += number;
            PivotTransform.rotation = rotation;
        }

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
