using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private SolidDetector _groundDetector;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;

    private Rigidbody2D _rigidbody2D;
    private Coroutine _cooldownedJump = null;
    private Vector3 _defaultScale;
    private float _fallMultiplyer = 0.15f; 
    private bool _isMoving = false;
    private bool _isJump = false;

    public event UnityAction OnMove;
    public event UnityAction OnStop;
    public event UnityAction OnJump;

    public bool OnGround { get => _groundDetector.SolidDetected; }

    public Vector2 LookDirection { get => new Vector2(transform.localScale.x, 0).normalized; }

    private void Awake()
    {
        _defaultScale = transform.localScale;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isMoving == false)
        {
            OnStop?.Invoke();
        }

        _isMoving = false;
    }

    private void FixedUpdate()
    {
        if (_isJump)
        {
            if (_groundDetector.SolidDetected && _cooldownedJump == null)
            {
                _cooldownedJump = StartCoroutine(CooldownedJump());
                OnJump?.Invoke();
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
        transform.Translate(direction * _walkSpeed * Time.deltaTime);
        transform.localScale = new Vector3(_defaultScale.x * direction.x, _defaultScale.y, _defaultScale.z);
        OnMove?.Invoke();
        _isMoving = true;
    }

    private IEnumerator CooldownedJump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce * ((_rigidbody2D.velocity.y * -1 * _fallMultiplyer) + 1));
        yield return new WaitForSeconds(_jumpCooldown);
        _cooldownedJump = null;
    }
}
