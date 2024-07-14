using UnityEngine;

public class Jumper
{
    private Rigidbody2D _rigidbody;
    private float _jumpVelocity;

    public Jumper(Rigidbody2D rigidbody, float jumpVelocity)
    {
        _rigidbody = rigidbody;
        _jumpVelocity = jumpVelocity;
    }

    public void Jump()
    {
        Vector2 currentVelocity = _rigidbody.velocity;
        Vector2 newVelocity = new(currentVelocity.x, _jumpVelocity);
        _rigidbody.velocity = newVelocity;
    }
}
