using UnityEngine;

public abstract class ObjectSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [field : SerializeField] protected T Prefab { get; private set; }

    protected ClippingFinder ClippingFinder { get; private set; }

    protected virtual void Awake()
    {
        ClippingFinder = new();
    }

    public abstract bool TrySpawn(Vector3 spawnPosition, out T spawnedObject);
}
