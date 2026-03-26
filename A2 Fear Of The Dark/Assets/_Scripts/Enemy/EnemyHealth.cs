using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyTrackerSO enemyTrackerSO;

    [Header("Settings")]
    [SerializeField] private int maxHealth = 5;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        enemyTrackerSO.RaiseEventEnemySpawned(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage, current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        enemyTrackerSO.RaiseEventEnemyKilled(gameObject);
        Destroy(gameObject);
    }
}