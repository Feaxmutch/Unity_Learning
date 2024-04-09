using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetFolower))]
public class PathFolower : MonoBehaviour
{
    [SerializeField] private List<PathPoint> _pathPoints = new();

    private PathPoint _currentPoint;
    private TargetFolower _targetFolower;
    private int _pointIndex;

    private void Start()
    {
        _targetFolower = GetComponent<TargetFolower>();
        _pointIndex = 0;
        SwichPoint(_pointIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PathPoint pathPoint) && pathPoint == _currentPoint)
        {
            _pointIndex++;
            _pointIndex = _pointIndex < _pathPoints.Count ? _pointIndex : 0;
            SwichPoint(_pointIndex);
        }
    }

    private void SwichPoint(int pointIndex)
    {
        _currentPoint = _pathPoints[Mathf.Clamp(pointIndex, 0, _pathPoints.Count - 1)];
        _targetFolower.SetTarget(_currentPoint.gameObject);
    }
}
