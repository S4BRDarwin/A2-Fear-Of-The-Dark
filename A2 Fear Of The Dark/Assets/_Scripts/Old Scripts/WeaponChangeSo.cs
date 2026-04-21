using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponChangeSo", menuName = "ScriptableObjects/WeaponChangeSo")]
public class WeaponChangeSo : ScriptableObject
{
    [SerializeField] private WeaponSO weaponSO;
    public Action ChangeWeaponToPipe;
    public Action ChangeWeaponToAxe;

    public void RaiseChangeWeaponEvent(WeaponSO.Weapons weapon)
    {
        if (weapon == WeaponSO.Weapons.Pipe)
        {
            ChangeWeaponToPipe?.Invoke();
        }
        else if (weapon == WeaponSO.Weapons.Axe)
        {
            ChangeWeaponToAxe?.Invoke();
        }
    }
}
