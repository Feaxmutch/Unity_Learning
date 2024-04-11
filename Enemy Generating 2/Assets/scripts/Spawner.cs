using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private TargetFolower _prefab;
    [SerializeField] private GameObject _target;

    public void Spawn()
    {
        Vector3 spawnPosition = GetSpawnPosition();
        TargetFolower targetFolower = Instantiate(_prefab);
        targetFolower.gameObject.transform.position = spawnPosition;
        targetFolower.SetTarget(_target);
        
    }

    private Vector3 GetSpawnPosition()
    {
        Ray ray = new(transform.position, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        return hit.point;
    }
}
