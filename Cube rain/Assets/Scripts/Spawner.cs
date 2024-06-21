using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private RainbowCube _cubePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Vector3 _maxSpawnOffset;
    [SerializeField] private float _spawnRate;

    private MyObjectPool<RainbowCube> _cubesPool;
    

    private void Awake()
    {
        _cubesPool = new(_cubePrefab);
    }

    private void OnEnable()
    {
        _cubesPool.Created += PoolOnCreate;
        _cubesPool.Geted += PoolOnGet;
    }

    private void OnDisable()
    {
        _cubesPool.Created -= PoolOnCreate;
        _cubesPool.Geted -= PoolOnGet;
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetFromPool), 0f, _spawnRate);
    }

    private void GetFromPool()
    {
        _cubesPool.Get();
    }

    private void PoolOnCreate(RainbowCube newCube)
    {
        newCube.Deactivated += _cubesPool.Release;
    }

    private void PoolOnGet(RainbowCube cube)
    {
        cube.Reset();
        Vector3 maxSpawnPosision = _spawnPoint.position + _maxSpawnOffset;
        Vector3 minSpawnPosition = _spawnPoint.position + (_maxSpawnOffset * -1);
        float spawnPositionX = Random.Range(minSpawnPosition.x, maxSpawnPosision.x);
        float spawnPositionY = Random.Range(minSpawnPosition.y, maxSpawnPosision.y);
        float spawnPositionZ = Random.Range(minSpawnPosition.z, maxSpawnPosision.z);
        cube.transform.position = new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ);
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
