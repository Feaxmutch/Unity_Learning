using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<RainbowCube>
{
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _spawnRate;

    private Coroutine _cubeSpawning;

    private void OnEnable()
    {
        _cubeSpawning = StartCoroutine(CubesSpawning(_spawnRate));
    }

    private void OnDisable()
    {
        StopCoroutine(_cubeSpawning);
    }

    private IEnumerator CubesSpawning(float spawnDelay)
    {
        WaitForSeconds delay = new(spawnDelay);

        while (enabled)
        {
            Vector2 spawnPoint2D = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPosition = new(spawnPoint2D.x, transform.position.y, spawnPoint2D.y);
            RainbowCube newCube = Spawn(spawnPosition);
            yield return delay;
        }
    }
}
