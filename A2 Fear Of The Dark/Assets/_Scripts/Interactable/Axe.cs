using UnityEngine;

public class Axe : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private WeaponChangeSo weaponChangeSo;

    public string promptMessage => "Press E to pick up Axe";

    public void Interact()
    {
        weaponChangeSo.RaiseChangeWeaponEvent(WeaponSO.Weapons.Axe);
    }
}
