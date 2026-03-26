using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTrackerSO", menuName = "EnemyTrackerSO")]
public class EnemyTrackerSO : ScriptableObject
{
    
    public Action<GameObject> EnemyKilled;
    public Action<GameObject> EnemySpawned;

    public void RaiseEventEnemyKilled(GameObject enemy)
    {
        EnemyKilled?.Invoke(enemy);
    }

    public void RaiseEventEnemySpawned(GameObject enemy)
    {
        EnemySpawned?.Invoke(enemy);
    }

}
