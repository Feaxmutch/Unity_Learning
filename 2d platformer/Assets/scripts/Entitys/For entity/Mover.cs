using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Eye _solidFinder;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private bool _isInvertFlip;

    private Rigidbody2D _rigidbody2D;
    private Coroutine _cooldownedJump = null;
    private float _fallMultiplyer = 0.15f; 
    private bool _isMoving = false;
    private bool _isJump = false;

    public event UnityAction Moved;
    public event UnityAction Stoped;
    public event UnityAction Jumped;

    public Vector2 LookDirection { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _solidFinder.SetMask(new string[] {ObjectLayers.Ground });
        _solidFinder.LookDirection = Vector2.down;
    }

    private void Update()
    {
        if (_isMoving == false)
        {
            Stoped?.Invoke();
        }

        _isMoving = false;
    }

    private void FixedUpdate()
    {
        if (_isJump)
        {
            if (IsGround() && _cooldownedJump == null)
            {
                _cooldownedJump = StartCoroutine(CooldownedJump());
                Jumped?.Invoke();
            }

            _isJump = false;
        }
    }

    public void Jump()
    {
        _isJump = true;
    }

    public void Move(Vector2 direction)
    {
        direction = direction.normalized;
        LookDirection = direction;
        transform.Translate(direction * _walkSpeed * Time.deltaTime);

        if (_spriteRenderer != null)
        {
            _spriteRenderer.flipX = _isInvertFlip ? direction.x < 0 == false : direction.x < 0;
        }

        Moved?.Invoke();
        _isMoving = true;
    }

    public bool IsGround()
    {
        return _solidFinder.TryFindComponent(out TilemapCollider2D collider2D);
    }

    private IEnumerator CooldownedJump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce * ((_rigidbody2D.velocity.y * -1 * _fallMultiplyer) + 1));
        yield return new WaitForSeconds(_jumpCooldown);
        _cooldownedJump = null;
    }
}
