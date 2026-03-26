using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyTrackerSO enemyTrackerSO;
    [SerializeField] private GameObject gate;

    [SerializeField] private List<GameObject> enemiesInArea = new List<GameObject>();

    void OnEnable()
    {
        enemyTrackerSO.EnemyKilled += RemoveEnemy;
        enemyTrackerSO.EnemySpawned += AddEnemy;
    }

    void OnDisable()
    {
        enemyTrackerSO.EnemyKilled -= RemoveEnemy;
        enemyTrackerSO.EnemySpawned -= AddEnemy;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (enemiesInArea.Contains(enemy))
        {
            enemiesInArea.Remove(enemy);
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        if (!enemiesInArea.Contains(enemy))
        {
            enemiesInArea.Add(enemy);
        }
    }

    void Update()
    {
        if (enemiesInArea.Count == 0)
        {
            gate.SetActive(false);
        }
    }
}
