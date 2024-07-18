using System.Collections;
using UnityEngine;

public class Enemy : Ship
{
    [SerializeField] private float _fireRate;

    private Coroutine _shooting;

    protected override void OnEnable()
    {
        base.OnEnable();
        Mover.StartMoving();
        _shooting = StartCoroutine(Shooting());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopCoroutine(_shooting);
    }

    protected override void OnShoot(Bullet bullet)
    {
        bullet.Shoot(this, typeof(Player), transform.position);
    }

    protected override void Initialize()
    {
        base.Initialize();
        IsSpriteFlip = true;
    }

    private IEnumerator Shooting()
    {
        WaitForSeconds wait = new(_fireRate);

        while (enabled)
        {
            yield return wait;
            Shoot();
        }
    }
}
