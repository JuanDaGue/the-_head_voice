using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public int maxEnemies = 10;

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        SpawnEnemies(bombEnemyPrefab, bombEnemyCount);
        SpawnEnemies(attackEnemyPrefab, attackEnemyCount);

        StartCoroutine(SpawnEnemiesRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            CleanUpNullEnemies(); // Remove destroyed ones

            if (activeEnemies.Count >= maxEnemies) continue;

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
        int spawnableCount = Mathf.Min(count, maxEnemies - activeEnemies.Count);

        for (int i = 0; i < spawnableCount; i++)
        {
            if (spawnPoints.Length == 0) return;

            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(prefab, sp.position, sp.rotation);
            activeEnemies.Add(enemy);
        }
    }

    void CleanUpNullEnemies()
    {
        activeEnemies.RemoveAll(e => e == null);
    }
}
