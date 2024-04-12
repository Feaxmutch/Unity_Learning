using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ForwardMover : MonoBehaviour
{
    private const string Speed = nameof(Speed);

    [Range(0, Degree.Max)] [SerializeField] private float _moveDirection;
    [Min(0)] [SerializeField] private float _speed;

    private Animator _animator;

    private float MoveDirection 
    { 
        get => _moveDirection; 
        set => _moveDirection = Mathf.Clamp(value, 0, Degree.Max); 
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        _animator.SetFloat(Speed, _speed);
    }

    private void OnValidate()
    {
        RotateByDirection();
    }

    public void SetDirection(Vector3 target)
    {
        transform.LookAt(target);
        MoveDirection = transform.rotation.eulerAngles.y;
        RotateByDirection();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void RotateByDirection()
    {
        transform.rotation = Quaternion.Euler(Vector3.up * MoveDirection);
    }
}
