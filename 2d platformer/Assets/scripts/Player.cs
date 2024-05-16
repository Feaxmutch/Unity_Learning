using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Mover), typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D), typeof(BoxCollider2D))]
public class Player : MonoBehaviour , IDamageble
{
    private const string ParametrIsFalling = "IsFalling";

    [SerializeField] private int _health;
    [SerializeField] private GroundDetector _groundDetector;

    private Mover _movement;
    private Animator _animator;
    private Dictionary<KeyCode, string> _animatorKeyParametrs = new();

    private KeyCode _jumpButton = KeyCode.Space;
    private KeyCode _moveLeftButton = KeyCode.A;
    private KeyCode _moveRightButton = KeyCode.D;
    private KeyCode _noneButton = KeyCode.None;

    public event UnityAction ScoreChanged;

    public int Score { get; private set; } = 0;

    private bool OnGround 
    {
        get 
        {
            _animator.SetBool(ParametrIsFalling, !_groundDetector.OnGround);
            return _groundDetector.OnGround; 
        } 
    }

    private void Start()
    {
        _movement = GetComponent<Mover>();
        _animator = GetComponent<Animator>();
        _animatorKeyParametrs[_jumpButton] = "OnJump";
        _animatorKeyParametrs[_moveLeftButton] = "IsMooving";
        _animatorKeyParametrs[_moveRightButton] = "IsMooving";
        _animatorKeyParametrs[_noneButton] = "IsMooving";
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
        BoxCollider2D[] boxCollider2Ds = GetComponents<BoxCollider2D>();
        boxCollider2Ds[0].isTrigger = false;
        boxCollider2Ds[1].isTrigger = true;
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
            _animator.SetTrigger(_animatorKeyParametrs[_jumpButton]);
        }

        if (Input.GetKey(_moveRightButton))
        {
            _movement.Move(Vector2.right);
            _animator.SetBool(_animatorKeyParametrs[_moveRightButton], true);
        }
        else if (Input.GetKey(_moveLeftButton))
        {
            _movement.Move(Vector2.left);
            _animator.SetBool(_animatorKeyParametrs[_moveLeftButton], true);
        }
        else
        {
            _animator.SetBool(_animatorKeyParametrs[_noneButton], false);
        }
    }
}
