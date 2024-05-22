using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Mover), typeof(PlayerAnimatorHandler))]
[RequireComponent(typeof(BoxCollider2D), typeof(Health), typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private ItemDetector _itemDetector;

    private Mover _mover;
    private PlayerInput _playerInput;
    private Health _health;
    private bool _isJump;

    public event UnityAction ScoreChanged;
    public event UnityAction OnMove;
    public event UnityAction OnStop;
    public event UnityAction OnJump;

    public int Score { get; private set; } = 0;

    public bool OnGround 
    {
        get 
        {
            return _groundDetector.OnGround; 
        } 
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _playerInput = GetComponent<PlayerInput>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.HealthIsOver += Death;
        _itemDetector.ItemIsDetected += TryPickupItem;
        _playerInput.SendingJump += Jump;
        _playerInput.SendingLeft += MoveLeft;
        _playerInput.SendingRight += MoveRight;
        _playerInput.NotSending += Stop;
    }

    private void OnDisable()
    {
        _health.HealthIsOver -= Death;
        _itemDetector.ItemIsDetected -= TryPickupItem;
        _playerInput.SendingJump -= Jump;
        _playerInput.SendingLeft -= MoveLeft;
        _playerInput.SendingRight -= MoveRight;
        _playerInput.NotSending -= Stop;
    }

    private void FixedUpdate()
    {
        if (_isJump)
        {
            _mover.Jump();
            OnJump?.Invoke();
            _isJump = false;
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
    
    private void TryPickupItem(Item item)
    {
        bool pickupIsSuccessful = true;

        if (item is Coin)
        {
            Coin coin = item as Coin;
            Score += coin.ScoreValue;
            ScoreChanged?.Invoke();
        }
        else
        {
            pickupIsSuccessful = false;
        }

        if (pickupIsSuccessful)
        {
            Destroy(item.gameObject);
        }
    }

    private void Jump()
    {
        if (OnGround)
        {
            _isJump = true;
        }
    }

    private void MoveLeft()
    {
        _mover.Move(Vector2.left);
        OnMove?.Invoke();
    }

    private void MoveRight()
    {
        _mover.Move(Vector2.right);
        OnMove?.Invoke();
    }

    private void Stop()
    {
        OnStop?.Invoke();
    }
}
