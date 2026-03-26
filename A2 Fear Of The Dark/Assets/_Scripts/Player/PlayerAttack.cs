using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private GameObject weaponHitbox;
    [SerializeField] private List<GameObject> enemiesToDamage = new List<GameObject>();

    private bool attacking = false;

    void Update()
    {
        if (!attacking)
        {
            if (Input.GetMouseButtonDown(0) && weaponManager.damage > 0)
            {
                Debug.Log("Starting Attack Coroutine");
                attacking = true;
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (Input.GetMouseButton(0))
        {
            Debug.Log("Attack Coroutine Started");

            Attack();
            
            yield return new WaitForSeconds(0.1f);

            weaponHitbox.GetComponent<MeshRenderer>().enabled = false;
            
            yield return new WaitForSeconds((1f / weaponManager.fireRate) - 0.1f);
        }
        Debug.Log("Ending Attack Coroutine");
        attacking = false;
    }

    private void Attack()
    {
        enemiesToDamage = weaponHitbox.GetComponent<EnemyInRangeTracker>().attackableEnemies;
        weaponHitbox.GetComponent<MeshRenderer>().enabled = true;
        foreach (GameObject enemy in enemiesToDamage)
        {
            if (enemy != null)
            {
                Debug.Log($"Attacking {enemy}");
                enemy.GetComponent<EnemyHealth>().TakeDamage(weaponManager.damage);
            }
            else
                Debug.Log($"Enemy is {enemy}");
        }
    }
}