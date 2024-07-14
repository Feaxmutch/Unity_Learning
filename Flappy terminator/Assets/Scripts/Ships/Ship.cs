using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(BoxCollider2D))]
[RequireComponent(typeof(Mover))]
public abstract class Ship : Initializable
{
    [SerializeField] private ShipSO _ship;
    [SerializeField] private Bullet _bullet;

    private SpriteRenderer _spriteRenderer;
    private ObjectPool<Bullet> _bulletPool;

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

    public virtual void Initialize(GameMode gameMode)
    {
        GameMode = gameMode;
        Subscribe();
    }


    protected override void Initialize()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _ship.Sprite;
        Mover = GetComponent<Mover>();
        Mover.Speed = _ship.Speed;
        _bulletPool = new(_bullet);
        _bulletPool.Created += OnBulletCreated;
        _bulletPool.Geted += (bullet) => bullet.Shoot(this, transform.position, Mover.MoveDirection);
    }

    public void TakeHit(Bullet _)
    {
        Deactivate();
    }

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
        bullet.Initialize(GameMode);
        bullet.Deactivated += _bulletPool.Release;
    }

    protected virtual void Subscribe()
    {
        if (GameMode != null)
        {
            GameMode.PreparedToStart += Reset;
            GameMode.Ended += Deactivate;
        }
    }

    protected virtual void Unsubscribe()
    {
        if (GameMode != null)
        {
            GameMode.PreparedToStart -= Reset;
            GameMode.Ended -= Deactivate;
        }
    }
}
