using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ResourcesGenerator : MonoBehaviour
{
    [SerializeField] private Resource _resource;
    [SerializeField] private float _spawnDelay;

    private ObjectPool<Resource> _resourcesPool;
    private BoxCollider _spawnArea;

    private void Awake()
    {
        _resourcesPool = new(_resource);
        _spawnArea = GetComponent<BoxCollider>();
        
    }
    
    private void OnEnable()
    {
        _resourcesPool.Geted += TrySpawn;
        _resourcesPool.Released += UnsubscribePool;
    }

    private void Start()
    {
        StartCoroutine(Generating());
    }

    private void TrySpawn(Resource resource)
    {
        SubscribePool(resource);
        Vector3 spawnPosition = transform.position + Vector3.Scale(Random.insideUnitSphere, _spawnArea.size / 2);
        Collider[] colliders = Physics.OverlapBox(spawnPosition, Vector3.one);

        if (colliders.Where(collider => collider.isTrigger == false).Count() == 0)
        {
            resource.transform.position = spawnPosition;
        }
        else
        {
            resource.gameObject.SetActive(false);
        }
    }

    private void UnsubscribePool(Resource resource)
    {
        resource.Deactivated -= _resourcesPool.Release;
    }

    private void SubscribePool(Resource resource)
    {
        resource.Deactivated += _resourcesPool.Release;
    }

    private IEnumerator Generating()
    {
        WaitForSeconds delay = new(_spawnDelay);

        while (enabled)
        {
            _resourcesPool.Get();
            yield return delay;
        }
    }
}
