using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(FixedJoint))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _grapPoint;

    private NavMeshAgent _agent;
    private FixedJoint _joint;
    private Resource _resource;

    public event Action<Bot> GrappedResource;
    public event Action<Bot> DroppedResource;

    public bool IsHoldingResource { get => _resource != null; }

    public bool IsMooving { get => _agent.velocity.magnitude > 0.1f; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _joint = GetComponent<FixedJoint>();
    }

    private void FixedUpdate()
    {
        if (IsHoldingResource && _resource.gameObject.activeSelf == false)
        {
            DropResource();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Resource resource) && IsHoldingResource == false)
        {
            GrapResource(resource);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Resource resource) && resource == _resource)
        {
            DropResource();
        }
    }

    public void MoveTo(Vector3 position)
    {
        _agent.destination = position;
    }

    private void GrapResource(Resource resource)
    {
        resource.transform.position = _grapPoint.position;
        resource.transform.rotation = _grapPoint.rotation;
        _joint.connectedBody = resource.GetComponent<Rigidbody>();
        _resource = resource;
        GrappedResource?.Invoke(this);
    }

    private void DropResource()
    {
        _joint.connectedBody = null;
        _resource = null;
        DroppedResource?.Invoke(this);
    }
}