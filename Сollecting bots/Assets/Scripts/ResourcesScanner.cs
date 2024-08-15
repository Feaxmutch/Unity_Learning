using UnityEngine;
using System;

public class ResourcesScanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;

    public event Action<Transform> Founded;

    public void Scan()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _scanRadius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Resource resource))
            {
                Founded.Invoke(resource.transform);
            }
        }
    }
}

