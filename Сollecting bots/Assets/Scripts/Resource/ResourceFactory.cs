using System;
using System.Collections;
using UnityEngine;

public class ResourceFactory : MonoBehaviour
{
    [SerializeField] private RandomResourceSpawner _spawner;
    [SerializeField] private GameBalance _gameBalance;

    private float _spawnDelay;
    private Coroutine _spawnCorutine;

    private void OnEnable()
    {
        _spawnDelay = _gameBalance.ResourcesSpawnDelay;
        _spawnCorutine = StartCoroutine(SpawningInLoop());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnCorutine);
    }

    private IEnumerator SpawningInLoop()
    {
        bool isSpawned;

        while (enabled)
        {
            isSpawned = false;

            while (isSpawned == false)
            {
                isSpawned = _spawner.TrySpawn();
                yield return null;
            }

            _spawnDelay = Math.Max(_spawnDelay / _gameBalance.ResourcesSpawnMultiplyer, _gameBalance.MinResourcesSpawnDelay);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
