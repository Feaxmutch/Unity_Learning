using System;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    protected List<Collider2D> _colliders2D = new();

    protected Collider2D ReturnedCollider { get; private set; } = null;

    protected Func<bool> _predicate;

    protected event ColliderAction ColliderIsDetected;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ReturnedCollider = collider;

        if (_predicate.Invoke())
        {
            _colliders2D.Add(collider);
            ColliderIsDetected?.Invoke(collider);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        ReturnedCollider = collider;

        if (_predicate.Invoke())
        {
            _colliders2D.Remove(collider);
        }
    }

    private void Reset()
    {
        _colliders2D.Clear();
    }
}

public delegate void ColliderAction(Collider2D collider);
