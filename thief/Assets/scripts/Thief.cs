using UnityEngine;

[RequireComponent(typeof(PathFolower))]
public class Thief : MonoBehaviour
{
    private PathFolower _pathFolower;

    private void Start()
    {
        _pathFolower = GetComponent<PathFolower>();
    }

    
    private void Update()
    {
        
    }
}
