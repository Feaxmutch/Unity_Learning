using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemDetector : MonoBehaviour
{
    public event ItemAction ItemIsDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item))
        {
            ItemIsDetected?.Invoke(item);
        }
    }
}

public delegate void ItemAction(Item item);
