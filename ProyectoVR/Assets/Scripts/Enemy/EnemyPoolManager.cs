using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> enemyPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);   // ← el manager vive entre escenas

        for (int i = 0; i < poolSize; i++)
        {
            SpawnAndEnqueue();
        }
    }

    private GameObject SpawnAndEnqueue()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform); // opcional: parent = manager
        DontDestroyOnLoad(enemy);                               // ← evita destrucción
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
        return enemy;
    }


    public GameObject GetEnemyFromPool()
    {
        if (enemyPool.Count > 0)
        {
            GameObject enemy = enemyPool.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }

        GameObject extra = Instantiate(enemyPrefab);
        return extra;
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}