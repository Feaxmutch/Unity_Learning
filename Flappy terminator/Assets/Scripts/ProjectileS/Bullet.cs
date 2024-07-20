using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Mover))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private ProjectileSO _projectile;

    private GameMode _gameMode;
    private Mover _mover;
    private SpriteRenderer _spriteRenderer;
    private Ship _owner;
    private Type _targetType;

    public event Action<Bullet> Deactivated;

    public Type TargetType { get => _targetType; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ship ship) && ship != _owner)
        {
            if (ship.GetType() == _targetType)
            {
                ship.TakeHit(this);
            }
        }
        else if (collision.TryGetComponent(out DeactivateZone _) == false)
        {
            return;
        }

        Deactivate();
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _projectile.Sprite;
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
        _mover.StopMoving();
    }

    public void Initialize(GameMode gameMode, Vector2 moveDirection) 
    {
        _mover.Initialize(moveDirection, _projectile.Speed);
        _gameMode = gameMode;
        Unsubscribe();
        Subscribe();
    }

    public void Shoot(Ship owner, Type targetType, Vector2 startPosition)
    {
        _owner = owner;
        _targetType = targetType;
        transform.position = startPosition;
        _spriteRenderer.flipX = _mover.MoveDirection.x < 0;
        _mover.StartMoving();
    }

    private void Deactivate()
    {
        if (gameObject.active)
        {
            gameObject.SetActive(false);
            Deactivated?.Invoke(this);
        }
    }

    private void Subscribe()
    {
        if (_gameMode != null)
        {
            _gameMode.Ended += Deactivate;
        }
    }

    private void Unsubscribe()
    {
        if (_gameMode != null)
        {
            _gameMode.Ended -= Deactivate;
        }
    }
}
