using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Resource : MonoBehaviour
{
    public event Action<Resource> Deactivated;

    public BoxCollider Collider { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<BoxCollider>();
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
