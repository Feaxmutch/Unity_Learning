using UnityEngine;

[RequireComponent(typeof(ForwardMover))]
public class TargetFolower : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private ForwardMover _mover;

    private void Start()
    {
        _mover = GetComponent<ForwardMover>();
    }

    private void Update()
    {
        _mover.SetDirection(_target.transform.position);
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }
}