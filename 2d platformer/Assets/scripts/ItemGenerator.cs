using System.Collections;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Item _prefab;
    [SerializeField] private Transform[] _spawnPositions;

    private void Start()
    {
        StartCoroutine(Spawning(_spawnDelay));
    }

    private IEnumerator Spawning(float spawnDelay)
    {
        WaitForSeconds wait = new(spawnDelay);

        while (enabled)
        {
            Vector3 spawnPosition = _spawnPositions[Random.Range(0, _spawnPositions.Length)].position;

            Instantiate(_prefab, spawnPosition, Quaternion.Euler(Vector3.zero));
            yield return wait;
        }
    }
}
