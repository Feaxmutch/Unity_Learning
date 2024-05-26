using UnityEngine;

[RequireComponent(typeof(Health), typeof(Mover))]
public class Entity : MonoBehaviour
{
    protected Mover _mover;

    public Health Health { get; private set; }

    protected virtual void Awake()
    {
        Health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
    }
}
