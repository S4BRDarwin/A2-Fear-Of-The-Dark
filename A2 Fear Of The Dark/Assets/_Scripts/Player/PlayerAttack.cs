using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private GameObject weaponHitbox;

    private bool attacking = false;

    void Update()
    {
        if (!attacking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                attacking = true;
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (Input.GetMouseButton(0))
        {
            weaponHitbox.GetComponent<Collider>().enabled = true;
            Attack();
            weaponHitbox.GetComponent<Collider>().enabled = false;
            yield return new WaitForSeconds(weaponManager.fireRate);
        }
        attacking = false;
    }

    private void Attack()
    {
        foreach (GameObject enemy in weaponHitbox.GetComponent<EnemyInRangeTracker>().attackableEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(weaponManager.damage);
            }
        }
    }
}
