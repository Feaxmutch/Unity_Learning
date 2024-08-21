using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(FixedJoint))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _grapPoint;

    private Base _base;
    private List<WaitingArea> _waitingAreas;
    private NavMeshAgent _agent;
    private FixedJoint _joint;
    private Resource _resource;

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
        UpdateDestination();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (IsHoldingResource)
        {
            if (collider.gameObject.TryGetComponent(out Base @base))
            {
                if (@base == _base)
                {
                    GiveResource();
                }
            }
        }
        else
        {
            if (collider.gameObject.TryGetComponent(out Resource resource))
            {
                if (resource.transform == CurrentTarget)
                {
                    GrapResource(resource);
                }
            }
        }
    }

    public void Initialize(Base @base, List<WaitingArea> waitingAreas)
    {
        _base = @base;
        _waitingAreas = waitingAreas;
    }

    public void MoveTo(Transform target)
    {
        CurrentTarget = target;
    }

    private void GrapResource(Resource resource)
    {
        resource.transform.position = _grapPoint.position;
        resource.transform.rotation = _grapPoint.rotation;
        _joint.connectedBody = resource.Rigidbody;
        _resource = resource;
        _resource.Collider.enabled = false;
        MoveTo(_base.transform);
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
        }
    }

    private void UpdateDestination()
    {
        if (CurrentTarget != null)
        {
            _agent.destination = CurrentTarget.position;
        }
        else
        {
            foreach (var area in _waitingAreas)
            {
                if (area.IsInArea(this))
                {
                    return;
                }
            }

            foreach (var area in _waitingAreas)
            {
                if (area.IsFree)
                {
                    _agent.destination = area.transform.position;
                    return;
                }
            }

            _agent.destination = transform.position;
        }
    }
}