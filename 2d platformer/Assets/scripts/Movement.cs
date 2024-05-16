using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
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

    public void MoveLeft()
    {
        transform.Translate(Vector3.left * _walkSpeed * Time.deltaTime);
        transform.localScale = new Vector3(_defaultScale.x * -1, _defaultScale.y, _defaultScale.z);
    }

    public void MoveRight()
    {
        transform.Translate(Vector3.right * _walkSpeed * Time.deltaTime);
        transform.localScale = _defaultScale;
    }
}
