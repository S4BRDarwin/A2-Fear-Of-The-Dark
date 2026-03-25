using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    [Header("Weapon Stats")]
    public int damage;
    public float fireRate;
    public float range;
    public float durability;

    public Weapons weaponType;

    public enum Weapons
    {
        Pipe,
        Axe
    }
}
