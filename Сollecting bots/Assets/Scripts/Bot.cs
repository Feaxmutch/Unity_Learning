using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(FixedJoint))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _grapPoint;
    [SerializeField] private Base _base;

    private NavMeshAgent _agent;
    private FixedJoint _joint;
    private Resource _resource;

    public event Action<Bot> GrappedResource;
    public event Action<Bot> GivedResource;

    public bool IsHoldingResource { get => _resource != null; }

    public bool IsMooving { get => _agent.velocity.magnitude > 0.1f; }

    public Transform CurrentTarget { get; private set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _joint = GetComponent<FixedJoint>();
    }

    private void FixedUpdate()
    {
        if (CurrentTarget != null)
        {
            _agent.destination = CurrentTarget.position;
        }
        else
        {
            _agent.destination = transform.position;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Resource resource) && IsHoldingResource == false)
        {
            if (resource.transform == CurrentTarget)
            {
                GrapResource(resource);
            }
        }
        else if (collider.gameObject.TryGetComponent(out Base @base) && IsHoldingResource == true)
        {
            if (@base == _base)
            {
                GiveResource();
            }
        }
    }

    public void Initialize(Base @base)
    {
        _base = @base;
    }

    public void MoveTo(Transform target)
    {
        CurrentTarget = target;
    }

    private void GrapResource(Resource resource)
    {
        resource.transform.position = _grapPoint.position;
        resource.transform.rotation = _grapPoint.rotation;
        _joint.connectedBody = resource.GetComponent<Rigidbody>();
        _resource = resource;
        _resource.Collider.enabled = false;
        GrappedResource?.Invoke(this);
    }

    private void GiveResource()
    {
        if (CurrentTarget == _resource.transform)
        {
            CurrentTarget = null;
        }

        if (IsHoldingResource)
        {
            _base.TakeResource(_resource);
            _joint.connectedBody = null;
            _resource = null;
            CurrentTarget = null;
            GivedResource?.Invoke(this);
        }
    }
}