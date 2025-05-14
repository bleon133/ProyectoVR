using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProb : MonoBehaviour
{
    public static EnemyProb Instance { get; private set; }

    [SerializeField] private float spawnRadius;
    [SerializeField] private Transform playerTransform;

    private float spawnProbability = 0f;
    [SerializeField] private float normalTeleportIncrease = 5f;
    [SerializeField] private float peligrosoTeleportIncrease = 10f;
    private float maxProbability = 100f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterTeleport(TeleportType teleportType)
    {
        float previousProbability = spawnProbability;

        if (teleportType == TeleportType.Seguro)
        {
            Debug.Log("[EnemyProb] Teleport seguro. No se incrementa probabilidad ni se genera enemigo.");
            return;
        }

        spawnProbability += (teleportType == TeleportType.Normal) ? normalTeleportIncrease : peligrosoTeleportIncrease;

        spawnProbability = Mathf.Min(spawnProbability, maxProbability);

        Debug.Log($"[EnemyProb] Teleport activado: {teleportType}");
        Debug.Log($"[EnemyProb] Probabilidad Anterior: {previousProbability}% - Nueva Probabilidad: {spawnProbability}");

        TrySpawnEnemy();
    }

    private void TrySpawnEnemy()
    {
        float randomValue = Random.Range(0f, 100f);
        bool spawn = randomValue <= spawnProbability;

        Debug.Log($"[EnemyProb] Número Aleatorio: {randomValue}. Probabilidad Actual: {spawnProbability}%");

        if (spawn)
        {
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0f;

            Vector3 spawnPosition = playerTransform.position + randomOffset;
            spawnPosition.y = playerTransform.position.y - 1.5f;

            GameObject enemy = EnemyPoolManager.Instance.GetEnemyFromPool();
            enemy.SetActive(false);
            enemy.transform.position = spawnPosition;
            enemy.transform.rotation = Quaternion.identity;

            EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();
           
            if (behavior != null)
            {
                behavior.rol = EnemyRole.Hostil;
                Debug.Log("Se cambio a modo hostil");
            }

            Debug.Log("El enemigo ha aparecido");
            enemy.transform.LookAt(playerTransform);

            enemy.SetActive(true);
            spawnProbability = 0f;
        }
        else
        {
            Debug.Log("No apareció ningún enemigo esta vez");
        }
    }
}
