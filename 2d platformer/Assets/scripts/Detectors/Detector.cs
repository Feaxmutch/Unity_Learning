using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Detector : MonoBehaviour
{
    protected List<Collider2D> Colliders2D { get; private set; } = new();

    protected Collider2D ReturnedCollider { get; private set; } = null;

    protected Func<bool> DetectSolution { get; set; }

    protected event Action ColliderDetected;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ReturnedCollider = collider;

        if (DetectSolution.Invoke())
        {
            Colliders2D.Add(collider);
            ColliderDetected?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        ReturnedCollider = collider;

        if (DetectSolution.Invoke())
        {
            Colliders2D.Remove(collider);
        }
    }

    private void Reset()
    {
        Colliders2D.Clear();
    }
}
