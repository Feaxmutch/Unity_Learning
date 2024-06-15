using Unity.VisualScripting;
using UnityEngine;

public class BounceAttack : Attack
{
    private float _bounceForce;
    private Rigidbody2D _rigidbody2D;

    public BounceAttack(float damage, Entity owner, float bounceForce) : base(damage, owner)
    {
        _bounceForce = bounceForce;

        if (Owner.TryGetComponent(out Rigidbody2D rigidbody))
        {
            _rigidbody2D = rigidbody;
        }
        else
        {
            _rigidbody2D = Owner.AddComponent<Rigidbody2D>();
        }
    }

    public override void DoAttack(Entity target)
    {
        base.DoAttack(target);
        _rigidbody2D.AddForce(Vector2.up * _bounceForce);
    }
}
