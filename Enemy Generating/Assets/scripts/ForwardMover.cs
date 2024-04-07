using UnityEngine;

public class ForwardMover : MonoBehaviour
{
    [Range(0, Degree.Max)] [SerializeField] private float _moveDirection;
    [Min(0)] [SerializeField] public float _speed;

    private Animator _animator;

    public float MoveDirection { get => _moveDirection; set => _moveDirection = Mathf.Clamp(value, 0, Degree.Max); }

    private void Update()
    {
        Move();
        _animator.SetFloat("Speed", _speed);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Move()
    {
        transform.rotation = Quaternion.Euler(Vector3.up * MoveDirection);
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
