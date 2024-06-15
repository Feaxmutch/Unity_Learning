using UnityEngine;

[RequireComponent(typeof(Health), typeof(Mover))]
public class Entity : MonoBehaviour
{
    protected Mover Mover { get; private set; }

    public Health Health { get; private set; }

    protected virtual void Awake()
    {
        Health = GetComponent<Health>();
        Mover = GetComponent<Mover>();
    }
}
