using System;
using UnityEngine;

public abstract class Spawner : MonoBehaviour, IInitializeble
{
    private PoollableObject _prefab;

    public event Action<PoollableObject> ObjectSpawned;
    public event Action SpawnedObjectsChanged;
    public event Action Initialized;

    public ObjectPool<PoollableObject> Pool { get; private set; }

    public int SpawnedObjects { get; private set; }

    protected virtual void Awake()
    {
        Initialize();
    }

    protected virtual void OnEnable()
    {
        Pool.Released += UnsubscribePool;
    }

    protected virtual void OnDisable()
    {
        Pool.Released -= UnsubscribePool;
    }

    protected virtual void Initialize()
    {
        Pool = new(_prefab);
        Initialized?.Invoke();
    }

    protected void SetPrefab(PoollableObject newPrefab)
    {
        _prefab = newPrefab;
    }

    protected PoollableObject Spawn(Vector3 spawnPosition)
    {
        PoollableObject newObject = Pool.Get();
        newObject.Reset();
        newObject.transform.position = spawnPosition;
        newObject.Deactivated += Pool.Release;
        SpawnedObjects++;
        ObjectSpawned?.Invoke(newObject);
        return newObject;
    }

    private void UnsubscribePool(PoollableObject obj)
    {
        obj.Deactivated -= Pool.Release;
    }
}
