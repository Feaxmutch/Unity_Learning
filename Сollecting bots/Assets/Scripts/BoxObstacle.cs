using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))] 
[RequireComponent(typeof(NavMeshObstacle))] 
public class BoxObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 _center;
    [SerializeField] private Vector3 _size;

    private BoxCollider _boxCollider;
    private NavMeshObstacle _obstacle;

    private void OnValidate()
    {
        if (_boxCollider == null || _obstacle == null)
        {
            Initialize(_center, _size);
        }

        UpdateComponents();
    }

    public void Initialize(Vector3 center, Vector3 size)
    {
        _boxCollider = GetComponent<BoxCollider>();
        _obstacle = GetComponent<NavMeshObstacle>();
        _obstacle.shape = NavMeshObstacleShape.Box;
        _obstacle.carving = true;
        _obstacle.carveOnlyStationary = false;
        _center = center;
        _size = size;
        UpdateComponents();
    }

    private void UpdateComponents()
    {
        _boxCollider.center = _center;
        _boxCollider.size = _size;
        _obstacle.center = _center;
        _obstacle.size = _size;
    }
}
