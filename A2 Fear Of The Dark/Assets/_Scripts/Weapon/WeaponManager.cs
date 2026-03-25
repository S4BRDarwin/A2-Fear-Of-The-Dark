using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WeaponSO pipeData;
    [SerializeField] private WeaponSO axeData;
    [SerializeField] private WeaponChangeSo weaponChangeSo;
    
    [Header("Current Weapon Stats")]
    [SerializeField] public int damage;
    [SerializeField] public float fireRate;
    [SerializeField] public float range;

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
