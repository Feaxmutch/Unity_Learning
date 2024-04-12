using UnityEngine;

[RequireComponent(typeof(ForwardMover))]
public class TargetFolower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private ForwardMover _mover;

    private void Start()
    {
        _mover = GetComponent<ForwardMover>();
    }

    private void Update()
    {
        _mover.SetDirection(_target.position);
    }

    public void Init(Transform target)
    {
        _target = target;
    }
}
