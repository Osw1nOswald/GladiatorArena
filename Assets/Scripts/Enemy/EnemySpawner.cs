using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    public GameObject enemyToSpawn; // 👈 ПЕРЕИМЕНОВАЛ
    public float spawnTime = 3f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    void SpawnEnemy()
    {
        if (enemyToSpawn != null) // 👈 И ЗДЕСЬ ИЗМЕНИ
        {
            Instantiate(enemyToSpawn, transform.position, transform.rotation);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}