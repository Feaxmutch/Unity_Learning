using UnityEngine;

public class PoollingSpawner<T> : ObjectSpawner<T> where T : PoollableObject
{
    private ObjectPool<T> _objectPool;

    protected override void Awake()
    {
        base.Awake();
        _objectPool = new(Prefab);
    }

    public override bool TrySpawn(Vector3 spawnPosition, out T spawnedObject)
    {
        T spawningObject = _objectPool.Get();
        bool isFree = ClippingFinder.PositionIsFree(spawnPosition, spawningObject);

        if (isFree)
        {
            spawningObject.transform.position = spawnPosition;
            spawnedObject = spawningObject;
        }
        else
        {
            spawningObject.Deactivate();
            spawnedObject = null;
        }

        
        return isFree;
    }
}
