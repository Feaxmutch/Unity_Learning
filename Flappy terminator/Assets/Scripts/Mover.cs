using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Coroutine _move;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;

    public Vector2 MoveDirection { get => _moveDirection; private set => _moveDirection = value.normalized; }

    public float Speed { get => _speed; private set => _speed = Mathf.Max(0, value); }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 moveDirection, float speed)
    {
        MoveDirection = moveDirection;
        Speed = speed;
    }

    public void StartMoving()
    {
        if (_move == null && _rigidbody != null)
        {
            _move = StartCoroutine(Move());
        }
    }

    public void StopMoving()
    {
        if (_move != null)
        {
            StopCoroutine(_move);
            _move = null;
        }
    }

    private IEnumerator Move()
    {
        while (enabled)
        {
            _rigidbody.velocity = (MoveDirection * Speed);
            yield return null;
        }
    }
}
