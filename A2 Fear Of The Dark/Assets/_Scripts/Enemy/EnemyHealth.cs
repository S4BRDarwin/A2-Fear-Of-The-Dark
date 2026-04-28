using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyTrackerSO enemyTrackerSO;

    [Header("Settings")]
    [SerializeField] private float maxHealth = 5;
    [SerializeField] private float currentHealth;
    [SerializeField] private bool takingDamage = false;

    private void Start()
    {
        currentHealth = maxHealth;
        enemyTrackerSO.RaiseEventEnemySpawned(gameObject);
    }

    public void StartTakeDamage(float damage, float delay)
    {
        if (takingDamage) return;
        StartCoroutine(TakeDamage(damage, delay));
    }

    private IEnumerator TakeDamage(float damage, float delay)
    {
        takingDamage = true;
        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage, current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
        yield return new WaitForSeconds(delay);
        takingDamage = false;
    }

    private void Die()
    {
        enemyTrackerSO.RaiseEventEnemyKilled(gameObject);
        Destroy(gameObject);
    }
}