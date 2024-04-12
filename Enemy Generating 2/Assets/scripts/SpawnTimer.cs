using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private void Start()
    {
        StartCoroutine(DelayedRandomSpawn());
    }

    private IEnumerator DelayedRandomSpawn()
    {
        WaitForSeconds wait = new(_spawnDelay);

        while (true)
        {
            _spawnPoints[Random.Range(0, _spawnPoints.Count)].Spawn();
            yield return wait;
        }
    }
}
