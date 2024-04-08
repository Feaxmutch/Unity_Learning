using UnityEngine;

[RequireComponent(typeof(ForwardMover))]
public class ZombieController : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private ForwardMover _mover;

    private void Start()
    {
        _mover = GetComponent<ForwardMover>();
    }

    private void Update()
    {
        _mover.SetDirection(Vector3.zero);
    }
}
