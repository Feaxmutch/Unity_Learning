using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;

    private float _sucsesDistance = 0.2f;
    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void Start()
    {
        
        StartCoroutine(Patrol());
    }

    private IEnumerator Patrol()
    {
        while (enabled)
        {
            foreach (var point in _patrolPoints)
            {

                while (Mathf.Abs(transform.position.x - point.position.x) > _sucsesDistance)
                {
                    if (transform.position.x > point.position.x)
                    {
                        _mover.Move(Vector2.left);
                    }
                    else
                    {
                        _mover.Move(Vector2.right);
                    }

                    yield return null;
                }
            }
        }
    }
}
