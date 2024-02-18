using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private const float MinViewportPosition = -0.1f;
    private const float MaxViewportPosition = 1.1f;

    [SerializeField] private Character[] _enemyPrefabs;
    [SerializeField] private int _enemyCount = 10;
    [SerializeField] private float _spawnDelay = 1f;

    private EnemySpawnPoint[] _spawnPoints;
    private Camera _mainCamera;
    private List<CharacterHealth> _enemiesHealth = new List<CharacterHealth>();

    private int _spawnedEnemyCount;
    private float _spawnTimer;

    public Action<Character> OnSpawnEnemy;
    public Action OnAllEnemiesDie;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _spawnPoints = FindObjectsOfType<EnemySpawnPoint>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if(_spawnedEnemyCount >= _enemyCount)
        {
            return;
        }

        _spawnTimer -= Time.deltaTime;
        if(_spawnTimer <= 0)
        {
            SpawnEnemy();
            ResetSpawnTimer();
        }
    }

    private void SpawnEnemy()
    {
        EnemySpawnPoint spawnPoint = GetRandomSpawnPoint();
        Character newEnemy = Instantiate(GetRandomEnemyPrefab(), spawnPoint.transform.position, Quaternion.identity);
        _spawnedEnemyCount++;

        CharacterHealth newHealth = newEnemy.GetComponent<CharacterHealth>();
        newHealth.OnDieWithObject += RemoveEnemy;
        _enemiesHealth.Add(newHealth);

        OnSpawnEnemy?.Invoke(newEnemy);
    }

    private EnemySpawnPoint GetRandomSpawnPoint()
    {
        List<EnemySpawnPoint> possiblePoints = GetSpawnPointsOutOfCamera();
        if(possiblePoints.Count > 0)
        {
            return possiblePoints[Random.Range(0, possiblePoints.Count)];
        }
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
    }

    private List<EnemySpawnPoint> GetSpawnPointsOutOfCamera()
    {
        List<EnemySpawnPoint> possiblePoints = new List<EnemySpawnPoint>();
        Vector3 pointViewportPosition;
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            pointViewportPosition = _mainCamera.WorldToViewportPoint(_spawnPoints[i].transform.position);
            if (pointViewportPosition.x >= MinViewportPosition && pointViewportPosition.x <= MaxViewportPosition
                && pointViewportPosition.y >= MinViewportPosition && pointViewportPosition.y <= MaxViewportPosition)
            {
                continue;
            }
            possiblePoints.Add(_spawnPoints[i]);
        }
        return possiblePoints;
    }

    private Character GetRandomEnemyPrefab()
    {
        return _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
    }

    private void ResetSpawnTimer()
    {
        _spawnTimer = _spawnDelay;
    }

    private void RemoveEnemy(CharacterHealth health)
    {
        _enemiesHealth.Remove(health);
        health.OnDieWithObject -= RemoveEnemy;

        if (_enemiesHealth.Count <= 0)
        {
            OnAllEnemiesDie?.Invoke();
        }
    }
}
