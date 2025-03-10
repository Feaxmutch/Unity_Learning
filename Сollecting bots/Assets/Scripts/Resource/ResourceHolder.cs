using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(FixedJoint))]
public class ResourceHolder : MonoBehaviour
{
    private List<Resource> _resourcesInGrapZone;
    private Resource _currentResource;
    private FixedJoint _joint;

    public event Action DetectedResource;
    public event Action DroppedResource;

    public bool IsHolding => _currentResource != null;
    public Resource CurrentResource => _currentResource;

    private void Awake()
    {
        _resourcesInGrapZone = new();
        _joint = GetComponent<FixedJoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            _resourcesInGrapZone.Add(resource);
            DetectedResource?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            _resourcesInGrapZone.Remove(resource);

            if (resource == _currentResource)
            {
                Drop();
            }
        }
    }

    public bool TryGrap(Resource resource)
    {
        if (IsHolding == false)
        {
            if (_resourcesInGrapZone.Contains(resource))
            {
                Grap(resource);
                return true;
            }
        }

        return false;
    }

    private void Drop()
    {
        if (IsHolding)
        {
            _joint.connectedBody = null;
            _resourcesInGrapZone.Remove(_currentResource);
            _currentResource.Deactivated -= Drop;
            _currentResource = null;
            DroppedResource?.Invoke();
        }
    }

    private void Grap(Resource resource)
    {
        resource.transform.position = transform.position;
        resource.transform.rotation = transform.rotation;
        _joint.connectedBody = resource.Rigidbody;
        _currentResource = resource;
        _currentResource.Deactivated += Drop;
    }
}
