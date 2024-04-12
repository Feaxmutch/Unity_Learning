using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private List<Spawner> _spawners;

    private void Start()
    {
        StartCoroutine(DelayedRandomSpawn());
    }

    private IEnumerator DelayedRandomSpawn()
    {
        WaitForSeconds wait = new(_spawnDelay);

        while (true)
        {
            _spawners[Random.Range(0, _spawners.Count)].Spawn();
            yield return wait;
        }
    }
}
