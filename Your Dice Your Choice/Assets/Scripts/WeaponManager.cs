using UnityEngine;
using Assets.Scripts.Weapon;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _shield;
    [SerializeField] private GameObject _bow;
    [SerializeField] private GameObject _knifeRight;
    [SerializeField] private GameObject _knifeLeft;
    [SerializeField] private GameObject _magicStaff;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    /// <summary>
    /// Return the weapon prefab.
    /// </summary>
    /// <param name="weapon"></param>
    /// <returns></returns>
    public GameObject GetWeapon(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.None:
                return new GameObject();

            case WeaponType.Sword:
                return _sword;

            case WeaponType.Shield:
                return _shield;

            case WeaponType.Bow:
                return _bow;

            case WeaponType.KnifeRight:
                return _knifeRight;

            case WeaponType.KnifeLeft:
                return _knifeLeft;

            case WeaponType.MagicStaff:
                return _magicStaff;
        }

        return null;
    }
}
