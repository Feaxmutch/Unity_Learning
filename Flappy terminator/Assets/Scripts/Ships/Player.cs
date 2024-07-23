using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : Ship
{
    [SerializeField] GameMode _gameMode;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityScale;
    [SerializeField] private Vector3 _defaultPosition;

    private PlayerInput _input;
    private Jumper _jumper;

    public event Action Loosed;

    protected override void Awake()
    {
        base.Awake();
        _jumper = new(Rigidbody, _jumpForce);
        Rigidbody.gravityScale = _gravityScale;
        _input = GetComponent<PlayerInput>();
        Initialize(_gameMode, Vector2.right);
    }

    public override void Reset()
    {
        base.Reset();
        transform.position = _defaultPosition;
    }

    protected override void OnShoot(Bullet bullet)
    {
        bullet.Shoot(this, typeof(Enemy), transform.position);
    }

    protected override void OnGameEnded()
    {
        base.OnGameEnded();
        _input.enabled = false;
    }

    protected override void OnGamePreparedToStart()
    {
        base.OnGamePreparedToStart();
        Rigidbody.gravityScale = 0;
        gameObject.SetActive(true);
    }

    protected override void OnGameStarted()
    {
        base.OnGameStarted();
        Rigidbody.gravityScale = _gravityScale;
        _input.enabled = true;
    }

    private void OnDeactivated(Ship self)
    {
        Loosed.Invoke();
    }

    protected override void Subscribe()
    {
        base.Subscribe();
        _input.SubscribeToKey(_input.JumpKey, _jumper.Jump);
        _input.SubscribeToKey(_input.ShootKey, Shoot);
        Deactivated += OnDeactivated;
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();
        _input.UnsubscibeFromKey(_input.JumpKey, _jumper.Jump);
        _input.UnsubscibeFromKey(_input.ShootKey, Shoot);
        Deactivated -= OnDeactivated;
    }
}
