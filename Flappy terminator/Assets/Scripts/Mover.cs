using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : Initializable
{
    [SerializeField] private float _speed;

    private Coroutine _move;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;

    public Vector2 MoveDirection { get => _moveDirection; set => _moveDirection = value.normalized; }

    public float Speed { get => _speed; set => _speed = Mathf.Max(0, value); }

    protected override void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void StartMoving()
    {
        if (_move == null)
        {
            _move = StartCoroutine(Move());
        }
    }

    public void StopMoving()
    {
        _move = null;
    }

    private IEnumerator Move()
    {
        yield return null;
         
        while (_move != null)
        {
            _rigidbody.velocity = (MoveDirection * Speed);
            yield return null;
        }
    }
}
