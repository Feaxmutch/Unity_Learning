using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SolidDetector : Detector
{
    public bool SolidDetected { get => _colliders2D.Count > 0; }

    private void Awake()
    {
        _predicate = () => ReturnedCollider.isTrigger == false;
    }
}
