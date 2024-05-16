using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;

    private float _sucsesDistance = 0.2f;
    private Mover _movement;

    private void Start()
    {
        _movement = GetComponent<Mover>();
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
                        _movement.Move(Vector2.left);
                    }
                    else
                    {
                        _movement.Move(Vector2.right);
                    }

                    yield return null;
                }
            }
        }
    }
}
