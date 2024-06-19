using UnityEngine;

public class VampirismAbility : Ability
{
    private AbsorbingAttack _attack;
    private float _radius;
    public VampirismAbility(Player owner, float cooldown, float duration, float damage, float radius) : base(owner, cooldown, duration, false)
    {
        _attack = new AbsorbingAttack(owner, damage);
        _radius = radius;
    }

    protected override void OnActive()
    {
        Collider2D enemyCollider = Physics2D.OverlapCircle(Owner.transform.position, _radius, LayerMask.GetMask(new string[] {ObjectLayers.Enemy}));

        if (enemyCollider?.TryGetComponent(out Enemy enemy) ?? false)
        {
            _attack.DoAttack(enemy);
        }
    }
}
