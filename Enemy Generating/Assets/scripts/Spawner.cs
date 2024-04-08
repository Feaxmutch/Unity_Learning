using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ForwardMover _prefab;
    [Range(0, Degree.Max)] [SerializeField] private float _moveDirection;

    public void Spawn()
    {
        Vector3 spawnPosition = GetSpawnPosition();
        ForwardMover newMover = Instantiate(_prefab);
        newMover.gameObject.transform.position = spawnPosition;
        newMover.SetDirection(_moveDirection);
    }

    private Vector3 GetSpawnPosition()
    {
        Ray ray = new(transform.position, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        return hit.point;
    }
}
