using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
       Component[] components = collision.gameObject.GetComponents(typeof(IDamageble));

        foreach (var component in components)
        {
            IDamageble damageble = component as IDamageble;
            damageble.TakeDamage(_damage);
        }
    }
}
