using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Base _mainBase;
    [SerializeField] private ResourceHolder _resourceHolder;

    private NavMeshAgent _agent;
    private Coroutine _destinationUpdating;
    private bool _isOnBase;

    private event Action ArrivedBase;

    public bool IsHoldingResource => _resourceHolder.IsHolding;
    public Resource HoldedResource => _resourceHolder.CurrentResource;
    public bool IsHaveTarget => CurrentTarget != null;
    public Base MainBase => _mainBase;

    public Transform CurrentTarget { get; private set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _isOnBase = true;
    }

    private void OnEnable()
    {
        ArrivedBase += DoActionsOnBase;
        _resourceHolder.DetectedResource += TryGrapTarget;
        _resourceHolder.DroppedResource += StopMoving;
        _destinationUpdating = StartCoroutine(UpdateDestinationInLoop(1));
    }

    private void OnDisable()
    {
        ArrivedBase -= DoActionsOnBase;
        _resourceHolder.DetectedResource -= TryGrapTarget;
        _resourceHolder.DroppedResource -= StopMoving;
        StopCoroutine(_destinationUpdating);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Base @base))
        {
            if (@base == MainBase)
            {
                _isOnBase = true;
                ArrivedBase?.Invoke();
            }
        }

        if (collider.transform == CurrentTarget)
        {
            if (collider.gameObject.TryGetComponent(out Builder<Base> baseBuilder))
            {
                StopMoving();
                SetBase(baseBuilder.Build());
                _isOnBase = true;
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Base @base))
        {
            if (@base == MainBase)
            {
                _isOnBase = false;
            }
        }
    }

    public void Initialize(Base @base)
    {
        SetBase(@base);
    }

    public void MoveTo(Transform target)
    {
        CurrentTarget = target;
        _agent.isStopped = false;
        UpdateDestination();
        TryGrapTarget();

        if (target == MainBase.transform && _isOnBase)
        {
            ArrivedBase?.Invoke();
        }
    }

    public void StopMoving()
    {
        CurrentTarget = null;
        _agent.isStopped = true;
    }

    public void SetBase(Base @base)
    {
        _mainBase = @base;
    }

    private void GiveResource()
    {
        MainBase.TakeResource(_resourceHolder.CurrentResource);
    }

    private void TryGrapTarget()
    {
        if (IsHaveTarget)
        {
            if (CurrentTarget.TryGetComponent(out Resource resource))
            {
                if (_resourceHolder.TryGrap(resource))
                {
                    if (_isOnBase)
                    {
                        GiveResource();
                    }
                    else
                    {
                        MoveTo(MainBase.transform);
                    }
                }
            }
        }
    }

    private void DoActionsOnBase()
    {
        if (IsHoldingResource)
        {
            GiveResource();
        }

        if (CurrentTarget == _mainBase.transform)
        {
            StopMoving();
        }
    }

    private IEnumerator UpdateDestinationInLoop(float updateDelay)
    {
        WaitForSeconds wait = new(updateDelay);

        while (enabled)
        {
            UpdateDestination();
            yield return wait;
        }
    }

    private void UpdateDestination()
    {
        if (IsHaveTarget)
        {
            _agent.destination = CurrentTarget.position;
        }
    }
}