using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private RainbowCube _cubePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Vector3 _maxSpawnOffset;
    [SerializeField] private float _spawnRate;

    private ObjectPool<RainbowCube> _cubesPool;
    private Coroutine _getingCorutine;
    
    private void Awake()
    {
        _cubesPool = new(_cubePrefab);
    }

    private void OnEnable()
    {
        _cubesPool.Created += SubscribePool;
        _cubesPool.Geted += Spawn;
        _getingCorutine = StartCoroutine(GetingFromPool(_spawnRate));
    }

    private void OnDisable()
    {
        _cubesPool.Created -= SubscribePool;
        _cubesPool.Geted -= Spawn;
        StopCoroutine(_getingCorutine);
    }

    private IEnumerator GetingFromPool(float getRate)
    {
        WaitForSeconds delay = new(getRate);

        while (enabled)
        {
            _cubesPool.Get();
            yield return delay;
        }
    }

    private void SubscribePool(RainbowCube newCube)
    {
        newCube.Deactivated += _cubesPool.Release;
    }

    private void Spawn(RainbowCube cube)
    {
        cube.Reset();
        Vector3 maxSpawnPosision = _spawnPoint.position + _maxSpawnOffset;
        Vector3 minSpawnPosition = _spawnPoint.position + (_maxSpawnOffset * -1);
        cube.transform.position = GetRandomVector(minSpawnPosition, maxSpawnPosision);
    }

    private Vector3 GetRandomVector(Vector3 minVector, Vector3 maxVector)
    {
        float x = Random.Range(minVector.x, maxVector.x);
        float y = Random.Range(minVector.y, maxVector.y);
        float z = Random.Range(minVector.z, maxVector.z);

        return new Vector3(x, y, z);
    }
}
