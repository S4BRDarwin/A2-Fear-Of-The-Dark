using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WeaponSO pipeData;
    [SerializeField] private WeaponSO axeData;
    [SerializeField] private WeaponChangeSo weaponChangeSo;
    
    [Header("Current Weapon Stats")]
    [SerializeField] public int damage { get; private set; }
    [SerializeField] public float fireRate { get; private set; }
    [SerializeField] public float range { get; private set; }

    void OnEnable()
    {
        weaponChangeSo.ChangeWeaponToPipe += () => UpdateWeaponStats(pipeData);
        weaponChangeSo.ChangeWeaponToAxe += () => UpdateWeaponStats(axeData);
    }

    private void UpdateWeaponStats(WeaponSO newWeaponData)
    {
        damage = newWeaponData.damage;
        fireRate = newWeaponData.fireRate;
        range = newWeaponData.range;
    }
}
