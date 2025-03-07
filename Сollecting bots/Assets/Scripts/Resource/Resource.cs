using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : PoollableObject
{
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
