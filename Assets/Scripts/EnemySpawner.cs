using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Character _enemyPrefab;
    [SerializeField] private int _enemyCount = 10;

    private EnemySpawnPoint[] _spawnPoints;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _spawnPoints = FindObjectsOfType<EnemySpawnPoint>();
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            EnemySpawnPoint spawnPoint = GetRandomSpawnPoint();
            Instantiate(_enemyPrefab, spawnPoint.transform.position, Quaternion.identity);

        }
    }

    private EnemySpawnPoint GetRandomSpawnPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
    }
}
