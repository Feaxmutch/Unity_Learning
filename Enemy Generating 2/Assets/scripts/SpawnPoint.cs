using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private TargetFolower _prefab;
    [SerializeField] private Transform _target;

    public void Spawn()
    {
        Vector3 spawnPosition = GetSpawnPosition();
        TargetFolower targetFolower = Instantiate(_prefab, spawnPosition, Quaternion.Euler(Vector3.zero));
        targetFolower.Init(_target);
    }

    private Vector3 GetSpawnPosition()
    {
        Ray ray = new(transform.position, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        return hit.point;
    }
}
