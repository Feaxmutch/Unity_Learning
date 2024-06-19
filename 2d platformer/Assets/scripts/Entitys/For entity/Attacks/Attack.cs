public class Attack
{
    public Attack(Entity owner, float damage)
    {
        Damage = damage;
        Owner = owner;
    }

    public float Damage { get; }

    protected Entity Owner { get; }

    public virtual void DoAttack(Entity target)
    {
        target.Health.TakeDamage(Damage);
    }
}
