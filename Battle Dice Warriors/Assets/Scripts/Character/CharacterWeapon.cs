using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    [SerializeField] private Transform _leftHandTransform;
    [SerializeField] private Transform _rightHandTransform;
    [SerializeField] private Transform _leftArmRotate;
    [SerializeField] private Transform _rightArmRotate;
    [SerializeField] private float _shieldingPosition;
    [SerializeField] private float _shieldingLerpTime = 1.0f;
    [SerializeField] private float _shieldingRotateSpeed = 25f;
    public GameObject WeaponObjectLeft { get; private set; } // sword
    public GameObject WeaponObjectRight { get; private set; } // shield

    private Transform _leftArmDefault;
    private Transform _rightArmDefault;

    private float _shieldingCurLerpingTime;
    public bool IsProtecting { get; set; } = false;
    public bool IsRelaxing { get; set; } = false;

    private void Start()
    {
        _leftArmDefault = _leftArmRotate;
        _rightArmDefault = _rightArmRotate;
    }


    /// <summary>
    /// FixedUpdate method.
    /// </summary>
    private void FixedUpdate()
    {
        if (IsProtecting)
            IsProtecting = SmoothRotate(_leftArmRotate, Quaternion.Euler(0, 0, _shieldingPosition));

        if (IsRelaxing)
            IsRelaxing = SmoothRotate(_leftArmRotate, Quaternion.identity);
    }

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


    public void SetDefaultShield()
    {
        _leftArmRotate.rotation = new Quaternion(0f, 0f, 0f ,1f);
    }

    public bool SmoothRotate(Transform rotate, Quaternion target)
    {
        _shieldingCurLerpingTime = Mathf.Clamp01(
               _shieldingCurLerpingTime + (_shieldingRotateSpeed * Time.fixedDeltaTime));

        float t = _shieldingCurLerpingTime / _shieldingLerpTime;

        if (t >= 1)
        {
            _shieldingCurLerpingTime = 0f;
            return false;
        }

        rotate.localRotation = Quaternion.Lerp(
            rotate.localRotation,
            target,
            t);

        return true;
    }

    private bool MoveShieldTo(Vector3 target)
    {
        _shieldingCurLerpingTime = Mathf.Clamp01(
                _shieldingCurLerpingTime + (_shieldingRotateSpeed * Time.fixedDeltaTime));

        float t = _shieldingCurLerpingTime / _shieldingLerpTime;

        if (t >= 1)
        {
            _shieldingCurLerpingTime = 0f;
            return false;
        }

        _leftArmRotate.localRotation = Quaternion.Lerp(
            _leftArmRotate.localRotation,
            Quaternion.LookRotation(target),
            t);

        return true;

        //Vector3 direction = _shieldingPosition.position - _rightArmRotate.position;
        //Quaternion targetRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.RotateTowards(_rightArmRotate.rotation, targetRotation, _rotateShieldSpeed * Time.deltaTime);
    }

}

