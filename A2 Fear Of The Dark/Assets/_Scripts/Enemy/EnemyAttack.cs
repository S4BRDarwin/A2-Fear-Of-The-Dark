using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    
    [SerializeField] private bool canAttackPlayer = false;
    public bool IsAttacking { get; private set; }
    [SerializeField] GameObject player;

    void OnEnable()
    {
        // player = GameObject.FindGameObjectWithTag("Player");
    }

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
        if (canAttackPlayer && !IsAttacking)
        {
            IsAttacking = true;
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(1);
        yield return new WaitForSeconds(1f);
        IsAttacking = false;
    }
}