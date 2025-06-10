using System.Collections;
using System.Collections.Generic;
using Unity.FPS.AI;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("프리팹")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("스폰 옵션")]
    [SerializeField] private float spawnRange = 5f;
    [SerializeField] private float delayBetweenSpawns = 3f;
    [SerializeField] private int maxSpawnCount = 10;
    [SerializeField] private int maxSpawnTry = 10;

    private List<GameObject> _spawnedEnemies = new List<GameObject>();
    private int currentSpawnCount = 0;
    private Coroutine spawnCoroutine;

    public int CurrentAliveCount => _spawnedEnemies.Count;

    private void Update()
    {
        if (spawnCoroutine == null && currentSpawnCount < maxSpawnCount)
        {
            spawnCoroutine = StartCoroutine(Spawn_Coroutine());
        }
    }

    private IEnumerator Spawn_Coroutine()
    {
        Vector3 position = GetRandomPositionOnNavMesh(transform.position, spawnRange);
        if (position != Vector3.zero)
        {
            GameObject spawned = Instantiate(enemyPrefab, position, Quaternion.identity);
            _spawnedEnemies.Add(spawned);
            currentSpawnCount++;

            EnemyController controller = spawned.GetComponent<EnemyController>();
            if (controller != null)
            {
                controller.OnEnemyDied += OnEnemyDied;
            }
        }

        yield return new WaitForSeconds(delayBetweenSpawns);
        spawnCoroutine = null;
    }

    private void OnEnemyDied(GameObject enemy)
    {
        _spawnedEnemies.Remove(enemy);
    }

    private Vector3 GetRandomPositionOnNavMesh(Vector3 center, float range)
    {
        for (int i = 0; i < maxSpawnTry; i++)
        {
            Vector3 randomPos = center + new Vector3(
                Random.Range(-range, range),
                0f,
                Random.Range(-range, range)
            );

            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return Vector3.zero;
    }
}
