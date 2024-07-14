using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Mover))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : Initializable
{
    [SerializeField] private ProjectileSO _projectile;

    private GameMode _gameMode;
    private Mover _mover;
    private SpriteRenderer _spriteRenderer;
    private Ship _owner;

    public event Action<Bullet> Deactivated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ship ship) && ship != _owner)
        {
            ship.TakeHit(this);
        }
        else if (collision.TryGetComponent(out DeactivateZone _) == false)
        {
            return;
        }

        Deactivate();
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

    public void Initialize(GameMode gameMode) 
    {
        Initialize();
        _gameMode = gameMode;
        Subscribe();
    }

    protected override void Initialize()
    {
        _mover = GetComponent<Mover>();
        _mover.Speed = _projectile.Speed;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _projectile.Sprite;
    }

    public void Shoot(Ship owner, Vector2 startPosition, Vector2 direction)
    {
        _owner = owner;
        transform.position = startPosition;
        _mover.MoveDirection = direction;
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
