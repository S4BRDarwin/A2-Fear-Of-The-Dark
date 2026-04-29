using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    
    [SerializeField] private bool canAttackPlayer = false;
    [SerializeField] private bool attacking = false;
    [SerializeField] GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canAttackPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canAttackPlayer = false;
        }
    }

    private void Update()
    {
        if (canAttackPlayer && !attacking)
        {
            attacking = true;
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(1);
        yield return new WaitForSeconds(1f);
        attacking = false;
    }
}