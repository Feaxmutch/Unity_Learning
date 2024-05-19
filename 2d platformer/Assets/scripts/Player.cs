using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Mover), typeof(Animator), typeof(PlayerAnimatorHandler))]
[RequireComponent(typeof(BoxCollider2D), typeof(BoxCollider2D))]
public class Player : MonoBehaviour , IDamageble
{
    [SerializeField] private int _health;
    [SerializeField] private GroundDetector _groundDetector;

    private Mover _movement;

    private KeyCode _jumpButton = KeyCode.Space;
    private KeyCode _moveLeftButton = KeyCode.A;
    private KeyCode _moveRightButton = KeyCode.D;

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

    private void Start()
    {
        _movement = GetComponent<Mover>();
    }

    private void Update()
    {
        ApplyInput();

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            Score += coin.ScoreValue;
            Destroy(coin.gameObject);
            ScoreChanged?.Invoke();
        }
    }

    private void Reset() 
    {
        _health = 100;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

    private void ApplyInput() 
    {
        if (OnGround && Input.GetKeyDown(_jumpButton))
        {
            _movement.Jump();
            OnJump?.Invoke();
        }

        if (Input.GetKey(_moveRightButton))
        {
            _movement.Move(Vector2.right);
            OnMove?.Invoke();
        }
        else if (Input.GetKey(_moveLeftButton))
        {
            _movement.Move(Vector2.left);
            OnMove?.Invoke();
        }
        else
        {
            OnStop?.Invoke();
        }
    }
}
