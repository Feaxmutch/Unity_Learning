using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(BoxCollider2D))]
[RequireComponent(typeof(Mover))]
public abstract class Ship : Initializable
{
    [SerializeField] private ShipSO _ship;
    [SerializeField] private Bullet _bullet;

    private SpriteRenderer _spriteRenderer;
    private ObjectPooll<Bullet> _bulletPool;

    public event Action<Ship> Deactivated;

    protected GameMode GameMode { get; private set; }

    protected bool IsSpriteFlip { get => _spriteRenderer.flipX; set => _spriteRenderer.flipX = value; }

    protected Mover Mover { get; private set; }

    protected Rigidbody2D Rigidbody { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DeactivateZone _))
        {
            Deactivate();
        }
    }

    protected virtual void OnEnable()
    {
        Subscribe();
    }

    protected virtual void OnDisable()
    {
        Mover.StopMoving();
        Unsubscribe();
    }

    public virtual void Reset()
    {

    }

    public virtual void Initialize(GameMode gameMode, Vector2 moveDirection)
    {
        Initialize();
        Mover.Initialize(moveDirection, _ship.Speed);
        GameMode = gameMode;
        Unsubscribe();
        Subscribe();
    }


    protected override void Initialize()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _ship.Sprite;
        Mover = GetComponent<Mover>();
        _bulletPool = new(_bullet);
        _bulletPool.Created += OnBulletCreated;
        _bulletPool.Geted += OnShoot;
    }

    public void TakeHit(Bullet bullet)
    {
        if (bullet.TargetType == GetType())
        {
            Deactivate();
        }
    }

    protected abstract void OnShoot(Bullet bullet);

    protected void Shoot()
    {
        _bulletPool.Get();
    }

    private void Deactivate()
    {
        if (gameObject.active)
        {
            gameObject.SetActive(false);
            Deactivated?.Invoke(this);
        }
    }

    private void OnBulletCreated(Bullet bullet)
    {
        bullet.Initialize(GameMode, Mover.MoveDirection);
        bullet.Deactivated += _bulletPool.Release;
    }

    protected virtual void OnGameEnded()
    {
        Deactivate();
    }

    protected virtual void OnGamePreparedToStart()
    {
        Reset();
    }

    protected virtual void OnGameStarted()
    {

    }

    protected virtual void Subscribe()
    {
        if (GameMode != null)
        {
            GameMode.Started += OnGameStarted;
            GameMode.PreparedToStart += OnGamePreparedToStart;
            GameMode.Ended += OnGameEnded;
        }
    }

    protected virtual void Unsubscribe()
    {
        if (GameMode != null)
        {
            GameMode.PreparedToStart -= OnGamePreparedToStart;
            GameMode.Ended -= OnGameEnded;
        }
    }
}
