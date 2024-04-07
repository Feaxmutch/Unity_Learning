using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;

    private List<Spawner> _spawners = new();

    private void Start()
    {
        FindSpawners();
        StartCoroutine(DelayedRandomSpawn());
    }

    private void FindSpawners()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        foreach (var spawnPoint in spawnPoints)
        {
            _spawners.Add(spawnPoint.GetComponent<Spawner>());
        }
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
