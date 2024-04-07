using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [Range(0,359)] [SerializeField] private float _moveDirection;

    public void Spawn()
    {
        Vector3 SpawnPosition = GetSpawnPosition();
        GameObject gameObject = Instantiate(_prefab, SpawnPosition, transform.rotation);
        ForwardMover forwardMover = gameObject.GetComponent<ForwardMover>();
        forwardMover.MoveDirection = _moveDirection;
    }

    private Vector3 GetSpawnPosition()
    {
        Ray ray = new(transform.position, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        return hit.point;
    }
}
