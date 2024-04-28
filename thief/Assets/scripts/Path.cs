using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Path : MonoBehaviour
{
    [SerializeField] private List<Transform> _pathPoints;
    
    public event UnityAction PathEnded;
    
    private int _pointIndex;

    public Transform CurrentPoint { get; private set; }

    public bool IsLooping { get; set; }

    private void Start()
    {
        ResetPoint();
    }

    public void ResetPoint()
    {
        _pointIndex = 0;
        SetPoint();
    }

    public void NextPoint()
    {
        if (_pointIndex == _pathPoints.Count - 1 && IsLooping == false)
        {
            PathEnded?.Invoke();
            return;
        }

        _pointIndex++;

        if (IsLooping)
        {
            _pointIndex = _pointIndex % _pathPoints.Count;
        }
        else
        {
            _pointIndex = Mathf.Min(_pointIndex, _pathPoints.Count - 1);
        }

        SetPoint();
    }

    private void SetPoint()
    {
        CurrentPoint = _pathPoints[_pointIndex];
    }
}
