using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody2D;
    private Vector3 _defaultScale;

    private void Awake()
    {
        _defaultScale = transform.localScale;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce);
    }

    public void Move(Vector2 direction)
    {
        transform.Translate(direction * _walkSpeed * Time.deltaTime);
        transform.localScale = new Vector3(_defaultScale.x * direction.x, _defaultScale.y, _defaultScale.z);
    }
}
