using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner
{
    [SerializeField] private RainbowCube _cubePrefab;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _spawnRate;

    private Coroutine _cubeSpawning;

    public event Action<RainbowCube> CubeSpawned;

    protected override void Awake()
    {
        SetPrefab(_cubePrefab);
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _cubeSpawning = StartCoroutine(CubesSpawning(_spawnRate));
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopCoroutine(_cubeSpawning);
    }

    private IEnumerator CubesSpawning(float spawnDelay)
    {
        WaitForSeconds delay = new(spawnDelay);

        while (enabled)
        {
            Vector2 spawnPoint2D = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPosition = new(spawnPoint2D.x, transform.position.y, spawnPoint2D.y);
            RainbowCube newCube = Spawn(spawnPosition) as RainbowCube;
            CubeSpawned?.Invoke(newCube);
            yield return delay;
        }
    }
}
