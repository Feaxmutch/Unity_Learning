using UnityEngine;

public class EntityDetector : Detector
{
    public event EntityAction EntityIsDetected;

    private void Awake()
    {
        _predicate = () => ReturnedCollider.TryGetComponent(out Entity entity);
    }

    private void OnEnable()
    {
        ColliderIsDetected += GetEntity;
    }

    private void GetEntity(Collider2D collider)
    {
        EntityIsDetected?.Invoke(collider.GetComponent<Entity>());
    }

    public delegate void EntityAction(Entity entity);
}
