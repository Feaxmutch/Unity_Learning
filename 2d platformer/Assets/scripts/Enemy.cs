using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private EntityDetector _entityDetactor;
    [SerializeField] private float _folowingTime;

    private float _sucsesDistance = 0.2f;
    private Coroutine _delayedFolowing;
    private Coroutine _patrol;
    private Transform _target;

    private void OnEnable()
    {
        Health.HealthIsOver += Death;
        _entityDetactor.EntityIsDetected += TryAttack;
    }

    private void OnDisable()
    {
        Health.HealthIsOver -= Death;
        _entityDetactor.EntityIsDetected -= TryAttack;
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
        Debug.Log("folowing stoped");
    }

    private IEnumerator FolowTheTarget()
    {
        while (true)
        {
            if (transform.position.x > _target.position.x)
            {
                _mover.Move(Vector2.left);
            }
            else
            {
                _mover.Move(Vector2.right);
            }

            if (transform.position.y + 1f < _target.position.y)
            {
                _mover.Jump();
            }

            yield return null;
        }
    }

    private void FindPlayer()
    {
        RaycastHit2D[] hits2D = new RaycastHit2D[10];
        Physics2D.Raycast(transform.position + new Vector3(0, 0.5f), _mover.LookDirection, new ContactFilter2D(), hits2D, 30);

        if (hits2D[1].collider?.gameObject.TryGetComponent(out Player player) ?? false)
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
            Debug.Log("folowing started");
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void TryAttack(Entity entity)
    {
        if (entity is Player)
        {
            Player player = entity as Player;
            player.Health.TakeDamage(20);
        }
    }
}
