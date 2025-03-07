using UnityEngine;

public class InstantiationSpawner<T> : ObjectSpawner<T> where T : MonoBehaviour
{
    public override bool TrySpawn(Vector3 spawnPosition, out T spawnedObject)
    {
        bool isFree = ClippingFinder.PositionIsFree(spawnPosition, Prefab);

        if (isFree)
        {
            spawnedObject = Instantiate(Prefab);
            spawnedObject.transform.position = spawnPosition;
        }
        else
        {
            spawnedObject = null;
        }

        return isFree;
    }
}
