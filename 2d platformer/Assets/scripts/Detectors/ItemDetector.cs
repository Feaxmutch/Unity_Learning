public class ItemDetector : Detector
{
    public event ItemAction ItemDetected;

    private Item _detectedItem;

    private void Awake()
    {
        DetectSolution = () => ReturnedCollider.TryGetComponent(out _detectedItem);
    }

    private void OnEnable()
    {
        ColliderDetected += ThrowItem;
    }

    private void ThrowItem()
    {
        ItemDetected?.Invoke(_detectedItem);
    }
}

public delegate void ItemAction(Item item);
