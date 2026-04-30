using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPlayerDetection : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent navAgent;
    // [SerializeField] private EnemyAttack enemyAttack;

    [Header("Settings")]
    [SerializeField] private float sightRange = 10f;
    [SerializeField] private LayerMask playerLayer;

    private bool canSeePlayer;

    void Update()
    {
        // Stop chasing while attacking
        if (this.GetComponentInChildren<EnemyAttack>().IsAttacking)
        {
            navAgent.isStopped = true;
            return;
        }

        canSeePlayer = IsPlayerInSight();

        if (canSeePlayer)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        navAgent.SetDestination(player.transform.position);
        navAgent.isStopped = false;
    }

    private bool IsPlayerInSight()
    {
        bool isPlayerInRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        if (!isPlayerInRange) return false;

        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        Debug.DrawRay(transform.position, directionToPlayer * sightRange, Color.red);
        
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hitInfo, sightRange, playerLayer))
        {
            if (hitInfo.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }
}
