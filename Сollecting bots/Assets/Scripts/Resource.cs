using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    public event Action<Resource> Deactivated;

    public BoxCollider Collider { get; private set; }

    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<BoxCollider>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Reset()
    {
        Collider.enabled = true;
    }

    private void OnDisable()
    {
        Deactivated?.Invoke(this);
    }
}
