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

    protected override void Awake()
    {
        base.Awake();
        Initialize(_gameMode);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Subscribe();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Unsubscribe();
    }

    public override void Reset()
    {
        base.Reset();
        transform.position = _defaultPosition;
    }

    protected override void Initialize()
    {
        base.Initialize();
        _jumper = new(Rigidbody, _jumpForce);
        Mover.MoveDirection = Vector2.right;
        Rigidbody.gravityScale = _gravityScale;
        _input = GetComponent<PlayerInput>();
    }

    protected override void Subscribe()
    {
        base.Subscribe();

        if (GameMode != null)
        {
            GameMode.PreparedToStart += () => Rigidbody.gravityScale = 0;
            GameMode.PreparedToStart += () => gameObject.SetActive(true);
            GameMode.Started += () => Rigidbody.gravityScale = _gravityScale;
            GameMode.Started += () => _input.enabled = true;
            GameMode.Ended += () => _input.enabled = false;
        }

        _input.SendedJump += _jumper.Jump;
        _input.SendedShoot += Shoot;
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();

        if (GameMode != null)
        {
            GameMode.PreparedToStart -= () => Rigidbody.gravityScale = 0;
            GameMode.PreparedToStart -= () => gameObject.SetActive(true);
            GameMode.Started -= () => Rigidbody.gravityScale = _gravityScale;
            GameMode.Started -= () => _input.enabled = true;
            GameMode.Ended -= () => _input.enabled = false;
        }

        _input.SendedJump -= _jumper.Jump;
        _input.SendedShoot -= Shoot;
    }
}
