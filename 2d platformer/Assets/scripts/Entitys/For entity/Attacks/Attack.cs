using UnityEngine;

public class Attack
{
    public Attack(float damage, Entity owner)
    {
        Damage = Mathf.Max(0, damage);
        Owner = owner;
    }

    public float Damage { get; }

    protected Entity Owner { get; }

    public virtual void DoAttack(Entity target)
    {
        target.Health.TakeDamage(Damage);
    }
}
