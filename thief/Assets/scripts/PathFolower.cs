using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFolower : MonoBehaviour
{
    [SerializeField] private List<Path> _paths;

    private float _movementSpeed;
    private float _rotationSpeed;
    private bool _isMoving;

    private void OnEnable()
    {
        for (int i = 0; i < _paths.Count; i++)
        {
            _paths[i].PathEnded += StopMoving;
        }
    }

    public void StartMoving(bool isLooping, int pathIndex)
    {
        _paths[pathIndex].ResetPoint();
        StartCoroutine(Moving(isLooping, pathIndex));
    }

    public void StopMoving()
    {
        _isMoving = false;
    }

    public void SetMovementSpeed(float speed)
    {
        _movementSpeed = speed;
    }

    public void SetRotationSpeed(float speed)
    {
        _rotationSpeed = speed;
    }

    private IEnumerator Moving(bool isLooping, int pathIndex)
    {
        _paths[pathIndex].IsLooping = isLooping;
        _isMoving = true;

        while (_isMoving)
        {
            Transform point = _paths[pathIndex].CurrentPoint;

            float targetX = point.position.x;
            float targetZ = point.position.z;

            while (point.position.x != transform.position.x || point.position.z != transform.position.z)
            {
                Quaternion finalRotation = Quaternion.LookRotation(point.position - transform.position);
                finalRotation = Quaternion.Euler(Vector3.up * finalRotation.eulerAngles.y);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, Time.deltaTime * _rotationSpeed);
                float targetY = transform.position.y;
                Vector3 targetPosition = new(targetX, targetY, targetZ);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * _movementSpeed);
                yield return null;
            }

            _paths[pathIndex].NextPoint();
        }
    }
}
