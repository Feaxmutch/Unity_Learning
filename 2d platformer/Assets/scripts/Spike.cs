using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageble>(out var damageble))
        {
            damageble.TakeDamage(_damage);
        }
    }
}
