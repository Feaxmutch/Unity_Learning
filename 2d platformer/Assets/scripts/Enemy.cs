using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;

    private float _sucsesDistance = 0.2f;
    private Movement _movement;

    private void Start()
    {
        _movement = GetComponent<Movement>();
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
                        _movement.MoveLeft();
                    }
                    else
                    {
                        _movement.MoveRight();
                    }

                    yield return null;
                }
            }
        }
    }

    //private IEnumerator FolowAPlayer()
    //{
    //    while (true)
    //    {
    //        if (transform.position.x > _player.position.x)
    //        {
    //            _movement.MoveLeft();
    //        }
    //        else
    //        {
    //            _movement.MoveRight();
    //        }

    //        yield return null;
    //    }
    //}
}
