using UnityEngine;

public class Eye : MonoBehaviour
{
    [SerializeField] private Vector2 _positionOffset;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask = new();

    private Vector2 _lookDirection;

    public Vector2 LookDirection { get => _lookDirection; set => _lookDirection = value.normalized; }

    public void SetMask(string[] layerNames)
    {
        _layerMask = LayerMask.GetMask(layerNames);
    }

    public void SetMask(int index)
    {
        _layerMask = index;
    }

    public bool TryFindComponent<T>(out T result) where T : Component
    {
        RaycastHit2D[] hits2D = new RaycastHit2D[1];
        ContactFilter2D filter2D = new();
        filter2D.useLayerMask = true;
        filter2D.layerMask = _layerMask;
        Physics2D.Raycast(transform.position, LookDirection, filter2D, hits2D, _maxDistance);

        if (hits2D[0].collider != null)
        {
            return hits2D[0].collider.TryGetComponent(out result);
        }
        else
        {
            result = null;
            return false;
        }
    }
}
