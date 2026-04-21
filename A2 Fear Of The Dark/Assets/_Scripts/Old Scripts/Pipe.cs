using Unity.VisualScripting;
using UnityEngine;

public class Pipe : MonoBehaviour, IInteractable
{

    [Header("References")]
    [SerializeField] private WeaponChangeSo weaponChangeSo;

    public string promptMessage => "Press E to pick up Pipe";

    public void Interact()
    {
        weaponChangeSo.RaiseChangeWeaponEvent(WeaponSO.Weapons.Pipe);
    }
}
