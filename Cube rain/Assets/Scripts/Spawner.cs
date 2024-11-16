using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : PoollableObject
{
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;

    public event Action<T> ObjectSpawned;
    public event Action SpawnedObjectsChanged;
    public event Action Initialized;

    public IPoolCounter PoolCounter { get => _pool; }

    public int SpawnedObjects { get; private set; }

    private void Awake()
    {
        _pool = new(_prefab);
        Initialized?.Invoke();
    }

    protected T Spawn(Vector3 spawnPosition)
    {
        T newObject = _pool.Get();
        newObject.Reset();
        newObject.transform.position = spawnPosition;
        SpawnedObjects++;
        SpawnedObjectsChanged?.Invoke();
        ObjectSpawned?.Invoke(newObject);
        return newObject;
    }
}
