using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerRespawnSO playerRespawnSO;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 10;

    [Header("Testing")]
    [SerializeField] private int currentHealth;
    [SerializeField] private bool takingDamage = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        playerRespawnSO.RaiseEventPlayerDeath();
    }
}