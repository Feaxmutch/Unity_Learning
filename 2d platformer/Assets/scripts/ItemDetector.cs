using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemDetector : Detector
{
    public event ItemAction ItemIsDetected;

    private void Awake()
    {
        _predicate = () => ReturnedCollider.TryGetComponent(out Item item);
    }

    private void OnEnable()
    {
        ColliderIsDetected += GetItem;
    }

    private void GetItem(Collider2D collider)
    {
        ItemIsDetected?.Invoke(collider.GetComponent<Item>());
    }
}

public delegate void ItemAction(Item item);
