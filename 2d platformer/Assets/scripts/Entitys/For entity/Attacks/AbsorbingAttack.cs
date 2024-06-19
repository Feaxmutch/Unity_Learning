public class AbsorbingAttack : Attack
{
    private float _healValue;
    private Entity _currentTarget;

    public AbsorbingAttack(Entity owner, float damage) : base(owner, damage)
    {

    }

    public override void DoAttack(Entity target)
    {
        if (target != _currentTarget)
        {
            if (_currentTarget != null)
            {
                _currentTarget.Health.TakedDamage -= HealOwner;
            }

            _currentTarget = target;
            _currentTarget.Health.TakedDamage += HealOwner;
        }

        if (target.Health.Value > Damage)
        {
            _healValue = Damage;
        }
        else
        {
            _healValue = target.Health.Value;
        }

        target.Health.TakeDamage(Damage);
    }

    private void HealOwner()
    {
        Owner.Health.TakeHeal(_healValue);
        _healValue = 0;
        _currentTarget.Health.TakedDamage -= HealOwner;
        _currentTarget = null;
    }
}
