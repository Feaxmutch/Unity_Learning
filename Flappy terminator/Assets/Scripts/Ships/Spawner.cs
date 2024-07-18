using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _prefab;
    [SerializeField] private float _delay;
    [SerializeField] private float _countObjectsInSpawn;
    [SerializeField] private float _maxLifeTimeSeconds;
    [SerializeField] private float _minLifeTimeSeconds;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private int _countSpawnObjects;
    [SerializeField] private bool _globalSpawn;

    private ObjectPool<MonoBehaviour> _objectPool;
    private Coroutine _spawnCoroutine;

    public float LifeTimeSeconds { get; private set; }

    public event UnityAction<int> Spawn;
    public event UnityAction<int> Release;

    private void Awake()
    {
        _objectPool = new ObjectPool<MonoBehaviour>
        (
            createFunc: () => Create(GetRandomPosition()),
            actionOnGet: (spawnObject) => ActionOnGet(spawnObject),
            actionOnRelease: (spawnObject) => spawnObject.gameObject.SetActive(false),
            actionOnDestroy: (spawnObject) => Delete(spawnObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void OnEnable()
    {
        LifeTimeSeconds = Random.Range(_minLifeTimeSeconds, _maxLifeTimeSeconds);
    }

    private void Update()
    {
        if (_globalSpawn)
            if (_spawnCoroutine == null)
                _spawnCoroutine = StartCoroutine(SpawnWithDelay());
    }


    public virtual void ActionOnGet(MonoBehaviour spawnObject)
    {
        spawnObject.gameObject.SetActive(true);
    }

    public virtual MonoBehaviour Create(Vector3 vector3)
    {
        MonoBehaviour spawnObject = Instantiate(_prefab, vector3, transform.rotation);

        _countSpawnObjects++;
        Spawn?.Invoke(_countSpawnObjects);

        return spawnObject;
    }

    protected Vector3 GetRandomPosition()
    {
        Bounds spawnArea = GetComponent<Collider>().bounds;

        float positionX = Random.Range(spawnArea.min.x, spawnArea.max.x);
        float positionY = Random.Range(spawnArea.min.y, spawnArea.max.y);
        float positionZ = Random.Range(spawnArea.min.z, spawnArea.max.z);

        return new Vector3(positionX, positionY, positionZ);
    }

    public virtual void Delete(MonoBehaviour spawnObject)
    {
        Destroy(spawnObject.gameObject, LifeTimeSeconds);
    }

    public virtual void ReleaseT(MonoBehaviour spawnObject)
    {
        _objectPool.Release(spawnObject);
        Release?.Invoke(_objectPool.CountActive);
    }

    private IEnumerator SpawnWithDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        for (int i = 0; i < _countObjectsInSpawn; i++)
            _objectPool.Get();

        yield return wait;

        _spawnCoroutine = null;
    }
}