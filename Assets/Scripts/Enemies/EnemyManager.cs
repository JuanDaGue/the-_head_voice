using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject bombEnemyPrefab;
    public GameObject attackEnemyPrefab;

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;
    public int bombEnemyCount = 2;
    public int attackEnemyCount = 2;
    public float spawnInterval = 10f;

    void Start()
    {
        SpawnEnemies(bombEnemyPrefab, bombEnemyCount);
        SpawnEnemies(attackEnemyPrefab,  attackEnemyCount);

        StartCoroutine(SpawnEnemiesRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Validate prefabs before instantiating
            if (bombEnemyPrefab != null)
            {
                SpawnEnemies(bombEnemyPrefab, bombEnemyCount);
            }
            else
            {
                Debug.LogError("Bomb Enemy Prefab is missing!");
            }

            if (attackEnemyPrefab != null)
            {
                SpawnEnemies(attackEnemyPrefab, attackEnemyCount);
            }
            else
            {
                Debug.LogError("Attack Enemy Prefab is missing!");
            }
        }
    }

    void SpawnEnemies(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (spawnPoints.Length == 0) return; // Avoid errors if no spawn points exist

            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            if (prefab != null)
            {
                Instantiate(prefab, sp.position, sp.rotation);
            }
        }
    }
}