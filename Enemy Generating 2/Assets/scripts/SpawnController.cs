using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;

    private Spawner[] _spawners;

    private void Start()
    {
        FindSpawners();
        StartCoroutine(DelayedRandomSpawn());
    }

    private void FindSpawners()
    {
        _spawners = FindObjectsOfType<Spawner>();
    }

    private IEnumerator DelayedRandomSpawn()
    {
        WaitForSeconds wait = new(_spawnDelay);

        while (true)
        {
            _spawners[Random.Range(0, _spawners.Length)].Spawn();
            yield return wait;
        }
    }
}
