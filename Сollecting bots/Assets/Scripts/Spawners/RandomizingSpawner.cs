using UnityEngine;

public class RandomizingSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private ObjectSpawner<T> _spawner;
    [SerializeField] private PositionsRandomizer _positionsRandomizer;

    public bool TrySpawn(out T spawnedObject)
    {
        return _spawner.TrySpawn(_positionsRandomizer.GetPosition(), out spawnedObject);
    }

    public bool TrySpawn()
    {
        return TrySpawn(out T _);
    }
}
