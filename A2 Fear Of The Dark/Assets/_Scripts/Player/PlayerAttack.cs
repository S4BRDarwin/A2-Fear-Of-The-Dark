using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private EnemyInRangeTracker enemyInRangeTracker;
    [SerializeField] private List<GameObject> enemiesToDamage = new List<GameObject>();

    private bool attacking = false;

    void Update()
    {
        if (!attacking)
        {
            if (Input.GetMouseButtonDown(0))
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
            // weaponHitbox.GetComponent<Collider>().enabled = true;
            Attack();
            // weaponHitbox.GetComponent<Collider>().enabled = false;
            yield return new WaitForSeconds(1f / weaponManager.fireRate);
        }
        attacking = false;
    }

    private void Attack()
    {
        enemiesToDamage = enemyInRangeTracker.attackableEnemies;
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