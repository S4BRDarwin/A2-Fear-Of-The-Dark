using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WeaponSO pipeData;
    [SerializeField] private WeaponSO axeData;
    [SerializeField] private WeaponChangeSo weaponChangeSo;
    [SerializeField] private PlayerAttack playerAttack;
    
    [Header("Current Weapon Stats")]
    [SerializeField] public int damage = 0;
    [SerializeField] public float fireRate = 0;
    [SerializeField] public float range = 0;

    void OnEnable()
    {
        weaponChangeSo.ChangeWeaponToPipe += () => UpdateWeaponStats(pipeData);
        weaponChangeSo.ChangeWeaponToAxe += () => UpdateWeaponStats(axeData);
    }

    private void UpdateWeaponStats(WeaponSO newWeaponData)
    {
        if (playerAttack.enabled != true)
        {
            playerAttack.enabled = true;
        }

        damage = newWeaponData.damage;
        fireRate = newWeaponData.fireRate;
        range = newWeaponData.range;
    }
}
