using UnityEngine;

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
    public GameObject GetWeapon(Weapon weapon)
    {
        switch (weapon)
        {
            case Weapon.None:
                return new GameObject();

            case Weapon.Sword:
                return _sword;

            case Weapon.Shield:
                return _shield;

            case Weapon.Bow:
                return _bow;

            case Weapon.KnifeRight:
                return _knifeRight;

            case Weapon.KnifeLeft:
                return _knifeLeft;

            case Weapon.MagicStaff:
                return _magicStaff;
        }

        return null;
    }
}
