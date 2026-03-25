using UnityEngine;
using System.Collections.Generic;

public class EnemyInRangeTracker : MonoBehaviour
{
    [SerializeField] public List<GameObject> attackableEnemies = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            attackableEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            attackableEnemies.Remove(other.gameObject);
        }
    }

}
