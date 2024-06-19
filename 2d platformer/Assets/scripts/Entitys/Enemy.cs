using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private Eye _playerFinder;
    [SerializeField] private Eye _closePlayerFinder;
    [SerializeField] private float _folowingTime;
    [SerializeField] private int _attackDamage;

    private float _sucsesDistance = 0.2f;
    private Attack _attack;
    private Coroutine _delayedFolowing;
    private Coroutine _patrol;
    private Transform _target;

    protected override void Awake()
    {
        base.Awake();
        _playerFinder.SetMask(new string[] { ObjectLayers.Player, ObjectLayers.Ground });
        _closePlayerFinder.SetMask(new string[] { ObjectLayers.Player, ObjectLayers.Ground });
        _attack = new Attack(this, _attackDamage);
    }

    private void OnEnable()
    {
        Health.HealthIsOver += Death;
    }

    private void OnDisable()
    {
        Health.HealthIsOver -= Death;
    }

    private void Start()
    {
        _patrol = StartCoroutine(Patrol());
        StartCoroutine(FolowTheTarget());
    }

    private void Update()
    {
        FindPlayer();
    }

    private void Reset()
    {
        _target = transform;
    }

    private IEnumerator Patrol()
    {
        while (enabled)
        {
            foreach (var point in _patrolPoints)
            {
                _target = point;
                yield return new WaitWhile(() => Mathf.Abs(transform.position.x - point.position.x) > _sucsesDistance);
            }
        }
    }

    private IEnumerator DelayedFolowing(Player player)
    {
        _target = player.transform;
        yield return new WaitForSeconds(_folowingTime);
        _delayedFolowing = null;
        _patrol = StartCoroutine(Patrol());
    }

    private IEnumerator FolowTheTarget()
    {
        while (enabled)
        {
            if (transform.position.x > _target.position.x)
            {
                Mover.Move(Vector2.left);
            }
            else
            {
                Mover.Move(Vector2.right);
            }

            _playerFinder.LookDirection = Mover.LookDirection;
            _closePlayerFinder.LookDirection = Mover.LookDirection;

            if (transform.position.y + 1f < _target.position.y)
            {
                Mover.Jump();
            }

            yield return null;
        }
    }

    private void FindPlayer()
    {
        if (_playerFinder.TryFindComponent(out Player player))
        {
            if (_delayedFolowing != null)
            {
                StopCoroutine(_delayedFolowing);
            }

            if (_patrol != null)
            {
                StopCoroutine(_patrol);
                _patrol = null;
            }

            _delayedFolowing = StartCoroutine(DelayedFolowing(player));

            if (_closePlayerFinder.TryFindComponent(out player))
            {
                _attack.DoAttack(player);
            }
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
