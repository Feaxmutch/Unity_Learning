using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    private PoollableObject _prefab;

    public ObjectPool<PoollableObject> Pool { get; private set; }

    public int SpawnedObjectsCount { get; private set; }

    protected virtual void Awake()
    {
        Pool = new(_prefab);
    }

    protected virtual void OnEnable()
    {
        Pool.Released += UnsubscribePool;
    }

    protected virtual void OnDisable()
    {
        Pool.Released -= UnsubscribePool;
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
        SpawnedObjectsCount++;
        return newObject;
    }

    private void UnsubscribePool(PoollableObject obj)
    {
        obj.Deactivated -= Pool.Release;
    }
}
