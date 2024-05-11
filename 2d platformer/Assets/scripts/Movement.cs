using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody2D.AddForce(new Vector2(0, _jumpForce));
    }

    public void MoveLeft()
    {
        transform.Translate(Vector3.left * _walkSpeed * Time.deltaTime);
    }

    public void MoveRight()
    {
        transform.Translate(Vector3.right * _walkSpeed * Time.deltaTime);
    }
}
