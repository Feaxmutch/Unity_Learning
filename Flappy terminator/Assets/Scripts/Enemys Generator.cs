using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemysGenerator : MonoBehaviour
{
    [SerializeField] private GameMode _gameMode;
    [SerializeField] private Enemy _prefab;
    [SerializeField] private float _spawnRate;
    [SerializeField] private Vector2 _spawnPoint;
    [SerializeField] private Vector2 _pointOffset;

    private WaitForSeconds _wait;
    private ObjectPool<Ship> _enemyPool;
    private Coroutine _generate;

    public event Action EnemyDeactivated;

    private void Awake()
    {
        _enemyPool = new(_prefab);
        _wait = new(_spawnRate);
    }

    private void OnEnable()
    {
        _gameMode.Started += OnGameStarted;
        _gameMode.Ended += OnGameEnded;
        _enemyPool.Geted += PlaceEnemy;
        _enemyPool.Created += OnEnemyCreated;
        _enemyPool.Released += OnEnemyReleased;
        
    }

    private void OnDisable()
    {
        _gameMode.Started -= OnGameStarted;
        _gameMode.Ended -= OnGameEnded;
        _enemyPool.Geted -= PlaceEnemy;
        _enemyPool.Created -= OnEnemyCreated;
        _enemyPool.Released -= OnEnemyReleased;
    }

    private IEnumerator Generate()
    {
        while (enabled)
        {
            _enemyPool.Get();
            yield return _wait;
        }
    }

    private void PlaceEnemy(Ship enemy)
    {
        enemy.transform.position = _spawnPoint + Random.insideUnitCircle * _pointOffset;
        enemy.Deactivated += OnEnemyDeactivated;
    }

    private void OnEnemyCreated(Ship enemy)
    {
        enemy.Initialize(_gameMode, Vector2.left);
    }

    private void OnEnemyReleased(Ship enemy)
    {
        EnemyDeactivated.Invoke();
    }

    private void OnEnemyDeactivated(Ship enemy)
    {
        _enemyPool.Release(enemy);
        enemy.Deactivated -= OnEnemyDeactivated;
    }

    protected virtual void OnGameEnded()
    {
        if (_generate != null)
        {
            StopCoroutine(_generate);
        }
    }

    protected virtual void OnGameStarted()
    {
        _generate = StartCoroutine(Generate());
    }
}
